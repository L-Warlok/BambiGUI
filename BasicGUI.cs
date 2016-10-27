// Curtin University
// Mechatronics Engineering
// Bluetooth I/O Card - Sample GUI Code

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using static BluetoothGUISample.Params;
namespace BluetoothGUISample
{
    
    public partial class Form1 : Form
    {
        // Declare variables to store inputs and outputs.
        bool open_serial_connection = true;
        bool byteRead = false;

        int Input1 = 0;
        int Input2 = 0;

        byte[] Outputs = new byte[4];
        byte[] Inputs = new byte[4];

        const byte START = 255;
        const byte ZERO = 0;

        // used to add items to the dropdown menu
        private String[] operating_modes = { "Clockwise", "Counterclockwise", "Reverse Circle", "Squiggle", "Manual", "Stop" };

        // used to tell if the vehicle should be moving or stopped
        private bool is_running = false;
        private bool is_manual = false;

        // stores operation mode from dropdown menu
        private int operation_mode = 4; // manual mode by default

        // date time
        float start_time;
        

        // Motor max speeds
        private double L_MAX = 0.8; // 80% 
        private double R_MAX = 0.8; // 80%

        // StopWatch 
        private Stopwatch stopwatch;


        private enum OpStates // finite state machine states
        {
            START,
            STRAIGHT,
            SEARCH_N_DESTROY,
            LEFT,
            SHARP_LEFT,
            RIGHT,
            SHARP_RIGHT
        }

        // 
        OpStates state;
        OpStates prev_state;
        public Form1()
        {
            // Initialize required for form controls.
            InitializeComponent();

            // initialise finite state machine
            state = OpStates.START;
            prev_state = state;
            // initialise stopwatch
            stopwatch = new Stopwatch();
            stopwatch.Reset();
            stopwatch.Start();
            start_time = stopwatch.ElapsedMilliseconds;

            numericUpDown1.Value = (decimal)K_T;
            numericUpDown2.Value = (decimal)I_T;
            numericUpDown3.Value = (decimal)D_T;

            is_running = false;

            foreach (String mode in operating_modes) // add the items to the dropdown menu
            {
                modeSelect_Box.Items.Add(mode);
            }
            
            
            // Establish connection on the Virtual Serial Port
            if (open_serial_connection == true)
            {
                if (!serial.IsOpen)                                  // Check if the bluetooth has been connected.
                {
                    try
                    {
                        serial.Open();                               //Try to connect to the bluetooth
                    }
                    catch
                    {
                        connectionMessage.Text = "ERROR: Failed to connect.";
                    }
                }
            }            
        }

        

        // Send a four byte message to the Arduino via serial.
        private void sendIO(byte PORT, byte DATA)
        {
            
            Outputs[0] = START;    //Set the first byte to the start value that indicates the beginning of the message.
            Outputs[1] = PORT;     //Set the second byte to represent the port where, Input 1 = 0, Input 2 = 1, Output 1 = 2 & Output 2 = 3. This could be enumerated to make writing code simpler... (see Arduino driver)
            Outputs[2] = DATA;  //Set the third byte to the value to be assigned to the port. This is only necessary for outputs, however it is best to assign a consistent value such as 0 for input ports.
            Outputs[3] = (byte)(START + PORT + DATA); //Calculate the checksum byte, the same calculation is performed on the Arduino side to confirm the message was received correctly.

            if (serial.IsOpen)
            {
                serial.Write(Outputs, 0, 4);         //Send all four bytes to the IO card.                      
            }
        }

        private void Get1_Click(object sender, EventArgs e) //Press the button to request value from Input 1, Arduino Port F.
        {
            sendIO(0, ZERO);  // The value 0 indicates Input 1, ZERO just maintains a fixed value for the discarded data in order to maintain a consistent package format.
        }

        private void Get2_Click(object sender, EventArgs e) //Press the button to request value from Input 1, Arduino Port K.
        {
            sendIO(1, ZERO);  // The value 1 indicates Input 2, ZERO maintains a consistent value for the message output.
        }

        private void getIOtimer_Tick(object sender, EventArgs e) //It is best to continuously check for incoming data as handling the buffer or waiting for event is not practical in C#.
        {
            if (serial.IsOpen) //Check that a serial connection exists.
            {
                if (serial.BytesToRead >= 4) //Check that the buffer contains a full four byte package.
                {
                    //statusBox.Text = "Incoming"; // A status box can be used for debugging code.
                    Inputs[0] = (byte)serial.ReadByte(); //Read the first byte of the package.

                    if (Inputs[0] == START) //Check that the first byte is in fact the start byte.
                    {
                        //statusBox.Text = "Start Accepted";

                        //Read the rest of the package.
                        Inputs[1] = (byte)serial.ReadByte();
                        Inputs[2] = (byte)serial.ReadByte();
                        Inputs[3] = (byte)serial.ReadByte();

                        //Calculate the checksum.
                        byte checkSum = (byte)(Inputs[0] + Inputs[1] + Inputs[2]);

                        //Check that the calculated check sum matches the checksum sent with the message.
                        if (Inputs[3] == checkSum)
                        {
                            //statusBox.Text = "CheckSum Accepted";

                            //Check which port the incoming data is associated with.
                            switch (Inputs[1])
                            {
                                case 0: //Save the data to a variable and place in the textbox.
                                    //statusBox.Text = "Input1";
                                    Input1 = Inputs[2];
                                    InputBox1.Text = Input1.ToString();
                                    break;
                                case 1: //Save the data to a variable and place in the textbox. 
                                    //statusBox.Text = "Input2";
                                    Input2 = Inputs[2];
                                    InputBox2.Text = Input2.ToString();
                                    break;
                            }
                        }
                    }
                }
            }
        }


        int left_motor = 127;
        int right_motor = 127;


        // time since last adjustment
        private double deltaT_adjustment = 0.1; // ms
        private double last_adjustment_ms = 0;
        // rate of last correction
        private double adjustment_rate = 0; // 
        private double previous_adjustment_rate = 0; // 
        // last adjustment direction
        private int adjustment_direction = LEFT; // adjustment direction
        private int previous_adjustment_direction = LEFT;
        // adjustment rate constant 
        private double K_T = 0.25;
        private double I_T = 1;
        private double D_T = 6;
        // error
        private int error = 1; // MAX error is 2
        private int prev_error = 0;
        private int total_error = 0;
        // main loop 
        private void sendLoopTimer_Tick(object sender, EventArgs e)
        {
            // bit mask the Input byte. bit 0 is left sensor. bit 1 is right sensor.

            int left_sensor = ((1 << SENSOR1_POS) & Input1);
            int right_sensor = ((1 << SENSOR2_POS) & Input1);

            

 

            if (is_running)
            {
                if (is_manual)
                {
                    bool l = force_left_sensor.Checked;
                    bool r = force_right_sensor.Checked;

                    if (l) left_sensor = 1;
                    else left_sensor = 0;
                    if (r) right_sensor = 1;
                    else right_sensor = 0;
                }

                sensor_readings_label.Text = String.Format("L: {0} R: {1}", left_sensor, right_sensor);

                /**
                 * Tracking works like this.
                 * Control algorithm aims to keep the inside sensor on top of the line.
                 * When deviated: Based on the other sensor's reading it can
                 * be determined in what direction the robot is going and how
                 * its direction needs to be adjusted.
                 * */


                int l_1 = left_sensor;
                int r_1 = right_sensor;


                // set error to be current position - half of max position.
                // is either 1, 0, or -1;
                error = (l_1 + r_1) - 1;
                total_error += error;

                // unused currently
                // may be of use later
                // to be determined
                if (left_sensor == 1) left_sensor = LINE;
                if (right_sensor == 1) right_sensor = LINE;
                if (left_sensor == 0) left_sensor = NO_LINE;
                if (right_sensor == 0) right_sensor = NO_LINE;



                switch (operation_mode)
                {
                    case 0: // Clockwise: Right sensor on the inside of track: right sensor stays on line
                        // left motor speed slightly higher than right motor speed to force it to slowly
                        // turn clockwise towards the right
                        L_MAX = 0.9;
                        R_MAX = 0.85;

                        adjustment_rate = error * K_T + (error - prev_error) * D_T + total_error * I_T;
                        left_motor = (int)(L_MAX * 255 - adjustment_rate);
                        if (left_motor < 127) left_motor = 127;
                        right_motor = (int)(R_MAX * 255 + adjustment_rate);
                        if (right_motor < 127) right_motor = 127;
                        break;
                    case 1: // CCW: Left sensor on the inside of track, keep line under that



                        break;
                    case 2: // Reverse CIRCLE
                        // swap left and right
                        // swap PWM
                        // instead of outputting MAX*PWM output (1-MAX)*PWM

                        break;
                    case 3: // Squiggle
                        break;
                    case 4: // manual
                        left_motor = (byte)outByte1.Value;
                        right_motor = (byte)outByte2.Value;
                        break;
                    default:
                        Console.WriteLine(String.Format("Error. Operation mode invalid: {0}\n", operation_mode));
                        break;
                }

                // GUI Update
                deltaT_label.Text = "deltaT:" + deltaT_adjustment.ToString();
                last_adj_ms_label.Text = "Last Adjustment ms: + " + last_adjustment_ms.ToString();
                adj_dir_label.Text = "Total Error: " + total_error.ToString();
                adj_rate_label.Text = "Adj rate: " + adjustment_rate.ToString();
                prev_adj_label.Text = "Error: " + error.ToString();
                prev_adj_r_label.Text = "Prev error:" + previous_adjustment_rate.ToString();

                // line tracking


                // store the time since a line was last found. 0 to 15 if it was previously read 
                // accuracy limited by DateTime to 15 
                // if (left_sensor == -1) deltaT_left = DateTime.Now.Millisecond - 
                // if (right_sensor == -1) deltaT_right = DateTime.Now.Millisecond - deltaT_right;

                // ensure proper byte range

                prev_error = error;

                if (left_motor > 255) left_motor = 255; 
                if (right_motor > 255) right_motor = 255; 
                if (left_motor < 0) left_motor = 0;
                if (right_motor < 0) right_motor = 0;

                outByte1.Value = left_motor;
                outByte2.Value = right_motor;

                

            }
            else
            {
                left_motor = 127;
                right_motor = 127;
            }
            sendIO(2, (byte)left_motor);
            sendIO(3, (byte)right_motor);
            // read sensors
            sendIO(0, ZERO);  // The value 0 indicates Input 1, ZERO just maintains a fixed value for the discarded data in order to maintain a consistent package format

        }
        

        // Method controlling PWM1 scrollbar value
        private void OutputBox1_ValueChanged(object sender, EventArgs e)
        {
            double d_in = (double)OutputBox1.Value;

            double a = K2 * d_in + K1; // equation obtained after performing a linear regression on bit v data for this DAC

            if (a > 255) a = 255;
            if (a < 0) a = 0;

            if (outByte1.Focused != true) // only update when that specific box isn't focused
                outByte1.Value = (decimal)Math.Floor(a);
        }
        // Method controlling PWM2 scrollbar value
        private void OutputBox2_ValueChanged(object sender, EventArgs e)
        {
            double d_in = (double)OutputBox2.Value;

            double a = K2 * d_in + K1; // equation obtained after performing a linear regression on bit v data for this DAC

            if (a > 255)    a = 255;
            if (a < 0)      a = 0;

            if (outByte2.Focused != true)
                outByte2.Value = (decimal)Math.Floor(a);
        }


        // update duty cycle from raw byte value
        // Method controlling byte1 scrollbar value
        private void outByte1_ValueChanged(object sender, EventArgs e)
        {
            double b_in = (double)outByte2.Value;

            double a = (b_in - K1) / K2; // the reciprocal of the equation obtained via linear regression

            if (a > 100) a = 100; // ensure value is not out of bounds
            if (a < 0) a = 0;       

            if (OutputBox1.Focused != true)
                OutputBox1.Value = (decimal)(a);

        }
        // Method controlling byte2 scrollbar value
        private void outByte2_ValueChanged(object sender, EventArgs e)
        {
            double b_in = (double)outByte2.Value;

            double a = (b_in - K1) / K2; // the reciprocal of the equation obtained via linear regression

            if (a > 100) a = 100; // ensure value is not out of bounds
            if (a < 0) a = 0;

            if (OutputBox2.Focused != true)
                OutputBox2.Value = (decimal)(a);

        }
        // handle input - dropdown box
        private void modeSelect_Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = modeSelect_Box.SelectedIndex; // get index of currently selected item
            operation_mode = index;

            switch (index)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    is_running = true;
                    running_label.Text = "Running";
                    running_label.ForeColor = Color.Green;
                    break;
                case 4:
                    is_running = true;
                    running_label.Text = "Manual Ctrl";
                    running_label.ForeColor = Color.Orange;
                    is_manual = true;
                    break;
                default: // 
                    is_running = false;
                    running_label.Text = "Stopped";
                    running_label.ForeColor = Color.Red;
                    break;
            }



            //String selected = modeSelect_Box.SelectedItem.ToString();
            
            //// Clockwise, CCW, Reverse, Squiggle
            //if (selected == operating_modes[0]) // clockwise operating mode
            //{
                
            //} else if (selected == operating_modes[1]) // counter clockwise operating mode
            //{

            //} else if (selected == operating_modes[2]) // reverse operating mode
            //{

            //} else if (selected == operating_modes[3]) // squiggle mode
            //{

            //}
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                force_left_sensor.Enabled = true;
                force_right_sensor.Enabled = true;
                outByte1.Enabled = true;
                outByte2.Enabled = true;
                OutputBox1.Enabled = true;
                OutputBox2.Enabled = true;

                numericUpDown1.Enabled = true;
                numericUpDown2.Enabled = true;
                numericUpDown3.Enabled = true;
                is_manual = true;
            }
            else
            {
                force_left_sensor.Enabled = false;
                force_right_sensor.Enabled = false;
                outByte1.Enabled = false;
                outByte2.Enabled = false;
                OutputBox1.Enabled = false;
                OutputBox2.Enabled = false;
                numericUpDown1.Enabled = false;
                numericUpDown2.Enabled = false;
                numericUpDown3.Enabled = false;
                is_manual = false;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            K_T = (double)numericUpDown1.Value;
        }

        private long millis()
        {
            return stopwatch.ElapsedMilliseconds;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            I_T = (double)numericUpDown2.Value;
        }
    }


}
