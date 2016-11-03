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

namespace BluetoothGUISample
{

    public partial class Form1 : Form
    {

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // sensor position on the board
        public const byte SENSOR1_POS = 0;  // position of sensor 1 within the INPUT BYTE
        public const byte SENSOR2_POS = 1;  // position of sensor 2 within the INPUT BYTE

        public const int LEFT = -1; // used for direction control 
        public const int RIGHT = 1;

        public const double K1 = 1.7179, K2 = 2.3623; // duty cycle to bit after regression was performed

        public const double DAC_MIN = 0;
        public const double DAC1_MAX = 8.0; // DAC 1 max voltage
        public const double DAC2_MAX = 8.0; // dac 2 max voltage
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // Declare variables to store inputs and outputs.
        bool open_serial_connection = true;


        //  P and I and D
        private double P = 85;
        private double I = 0.5;
        private double D = 1;

        int Input1 = 0;
        int Input2 = 0;

        byte[] Outputs = new byte[4];
        byte[] Inputs = new byte[4];

        const byte START = 255;
        const byte ZERO = 0;

        // used to add items to the dropdown menu

        private const int CLOCKWISE = 0;
        private const int COUNTERCLOCKWISE = 1;
        private const int REVERSE_CIRCLE = 2;
        private const int SQUIGGLE = 3;
        private const int MANUAL = 4;
        private const int STOP = 5;


        private enum modes { CLOCKWISE, COUNTERCLOCKWISE, REVERSE_CIRCLE, SQUIGGLE, MANUAL, STOP } // operation modes


        // used to tell if the vehicle should be moving or stopped
        private bool is_running = false;
        
        // stores operation mode from dropdown menu
        private int operation_mode = 4; // manual mode by default


        // Motor max speeds
        private double L_MAX = 1; // 80% 
        private double R_MAX = 1; // 80%

        // error decay rate
        private double ERROR_DECAY = 0.8; // 80%

        
        public Form1()
        {
            // Initialize required for form controls.
            InitializeComponent();   

            // initialise sensor boxes
            l_sensorBox.BackColor = Color.Black;
            r_sensorBox.BackColor = Color.Black;

            // set the scrollBoxes to contain the parameter values
            numericUpDown1.Value = (decimal)P;
            numericUpDown2.Value = (decimal)I;
            numericUpDown3.Value = (decimal)D;

            // hide direction arrows
            left_arrow.Visible = false;
            right_arrow.Visible = false;

            // initialise Error Decay scrollBox
            numericUpDown4.Value = (decimal)ERROR_DECAY;

            // initialise the robot with a "not running" state
            is_running = false;

            // operating mode combo box initalisation
            modeSelect_Box.DataSource = Enum.GetValues(typeof(modes));
            modeSelect_Box.SelectedIndex = 5; // set to stopped
            
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
                                    Input1 = Inputs[2];
                                    break;
                                case 1: //Save the data to a variable and place in the textbox. 
                                    Input2 = Inputs[2];
                                    break;
                            }
                        }
                    }
                }
            }
        }

        // set motors to 127 when initialising - stopped
        int left_motor = 127;
        int right_motor = 127;

        // rate of last correction
        private double adjustment_rate = 0; // stores the last adjustment 
        private double previous_adjustment_rate = 0; // stores the last adjustment 

        // error
        private int error = 1; // MAX error is 2
        private int prev_error = 0; 
        private int total_error = 0;
        private int bonus = 0;

        // main loop 
        private void sendLoopTimer_Tick(object sender, EventArgs e)
        {
            // bit mask the Input byte. bit 0 is left sensor. bit 1 is right sensor.
            sendIO(1, ZERO);  // The value 1 indicates Input 2, ZERO maintains a consistent value for the message output.

            // bit masking to obtain individual left and right sensor readings
            int left_sensor = ((1 << SENSOR1_POS) & Input2);
            int right_sensor = ((1 << SENSOR2_POS) & Input2) >> SENSOR2_POS;

            // if manual override box is NOT checked update the values with the sensor readings from Serial input
            if (!checkBox1.Checked)
            {
                sensor_readings_label.Text = String.Format("L: {0} R: {1}", left_sensor, right_sensor);
            }


            // this logic determines whether the vehicle must adjust to the left or right after coming outside of the line
            if (left_sensor == 0 && right_sensor != 0)
            {
                bonus = 1;
            }
            if (left_sensor != 0 && right_sensor == 0)
            {
                bonus = -1;
            }

            if (is_running)
            {   
                if (checkBox1.Checked) // if in manual_mode
                {
                    // read checkBoxes and store states
                    bool l = force_left_sensor.Checked;
                    bool r = force_right_sensor.Checked;

                    // if boolean value is true set sensor value to 1, else set to 0.
                    left_sensor = (l) ? 1 : 0;
                    right_sensor = (r) ? 1 : 0;
                    
                }
                if (operation_mode != STOP && operation_mode != MANUAL) // not in stop mode
                {
                    error = (right_sensor - left_sensor); // compute error
                    total_error += error + bonus; // compute toal error
                }
                // update sensor readings 
                sensor_readings_label.Text = String.Format("L: {0} R: {1}", left_sensor, right_sensor);
                

               /**
                *
                * PID algorithm that attempts to keep the line on the inside of the sensors.
                **/

                // stores the minimum values for left and right motor
                int minl, minr; 
                switch (operation_mode) // operation mode logic
                {
                    case CLOCKWISE: // Clockwise:
                    case SQUIGGLE: // Squiggle
                    case COUNTERCLOCKWISE: // CCW: 
                        // left and right motors - MAX is at 100%
                        L_MAX = 1;
                        R_MAX = 1;

                        // start reducing error if line was crossed to prevent the integral component to cause spinning
                        if ( (left_sensor == 0 && right_sensor == 0 ) || (Math.Sign(error) != Math.Sign(prev_error) ) )
                        {
                            total_error = (int)(ERROR_DECAY * total_error);
                            bonus = 0;
                        }
                        // compute adjustment rate - PID equation
                        adjustment_rate = error * P + (error - prev_error) * D + total_error * I;

                        // minimum values of left and right motors is 60 - which is a very slow reverse speed
                        minl = 60;
                        minr = 60;
                                
                        // set left and right motor speed based on the adjustment rate
                        left_motor = (int)(L_MAX * 255 - adjustment_rate);
                        right_motor = (int)(R_MAX * 255 + adjustment_rate);

                        // if motor speed is less than the specified minimum, set it to minl
                        if (left_motor < minl) left_motor = minl;
                        if (right_motor < minr) right_motor = minr; 
                        break;


                 
                    case REVERSE_CIRCLE: //  reverese CIRCLE
                        // 60% speed in reverse mode
                        L_MAX = 0.6;
                        R_MAX = 0.6;
                        // start reducing error if line was crossed to prevent the integral component to cause spinning
                        if ((left_sensor == 1 && right_sensor == 1) || (Math.Sign(error) != Math.Sign(prev_error)))
                        {
                            total_error = (int)(0.6 * total_error);
                        }
                        // compute adjustment rate - PID equation
                        adjustment_rate = error * P + (error - prev_error) * D + total_error * I;
                        minl = 200; // minimum motor value - actually used as maximum but because of reversed logic this works
                        minr = 200; // 

                        // compute the left and right motor values and find the inverse of it (255 - value) to run in reverse
                        left_motor = 255 - (int)(L_MAX * 255 - adjustment_rate);
                        right_motor = 255 - (int)(R_MAX * 255 + adjustment_rate);

                        // limit minimum values of left and right motors
                        if (left_motor > minl) left_motor = minl;
                        if (right_motor > minr) right_motor = minr;
                        break;

                    case MANUAL: // manual control
                        // set motor values to values of the byte boxes
                        left_motor = (byte)outByte1.Value;
                        right_motor = (byte)outByte2.Value;
                        break;
                    case STOP:
                        // stop both motors
                        left_motor = 127;
                        right_motor = 127;

                        break;
                }

                // GUI Update
                adj_dir_label.Text = "Total Error: " + total_error.ToString();
                adj_rate_label.Text = "Adj rate: " + adjustment_rate.ToString();
                prev_adj_label.Text = "Error: " + error.ToString();
                prev_adj_r_label.Text = "Prev error:" + previous_adjustment_rate.ToString();
                label12.Text = "Bonus " + bonus.ToString();

                // update previous error value
                prev_error = error;
                
                
            }
            else
            {
                // stop motors if is_running flag is cleared
                left_motor = 127;
                right_motor = 127;
            }
            
            // limit the range of the left and right motors to 1 byte
            if (left_motor > 255) left_motor = 255;
            if (right_motor > 255) right_motor = 255;
            if (left_motor < 0) left_motor = 0;
            if (right_motor < 0) right_motor = 0; 

            // prepare the byte value for output
            outByte1.Value = left_motor;
            outByte2.Value = right_motor;
            // update GUI display
            setBoxColors(left_sensor, right_sensor);

            // send values 
            sendIO(2, (byte)left_motor);
            sendIO(3, (byte)right_motor);
            
        }


        // method that changes GUI wheel colours
        // moving forward turns wheels green
        // reversing turns them red
        // sensor turns GREEN when a line is detected
        private void setBoxColors(int left_sensor, int right_sensor)
        {
            // rgb values
            int r, g, b;
            b = 0; // blue is 0
            // if left motor is in reverse colour box red
            if (left_motor <= 127)
            {
                r = 2 * (127 - left_motor);
                g = 0;
            }
            else // if forward colour it green
            {
                g = 2 * (left_motor - 127);
                r = 0; 
            }
            
            // limit colour values to 0-255
            if (r > 255)
                r = 255;
            if (r < 0)
                r = 0;
            // same for green
            if (g > 255)
                g = 255;
            if (g < 0)
                g = 0;

            // translate from int r and g to a readable value
            pictureBox2.BackColor = ColorTranslator.FromWin32(r + 256*g);
            
            // if right motor is in reverse colour it red
            if (right_motor <= 127)
            {
                r = 2 * (127 - right_motor);
                g = 0;
            }
            else
            { // colour it green if positive
                g = 2 * (right_motor - 127);
                r = 0; 
            }

            // limit bounds of byte 0 to 255
            if (r > 255)
                r = 255;
            if (r < 0)
                r = 0;
            // same for green
            if (g > 255)
                g = 255;
            if (g < 0)
                g = 0;
            // update colour of box
            pictureBox1.BackColor = ColorTranslator.FromWin32(r + 256*g + b);

            // SENSOR COLOUR CHANGES - set it white if on, 0 if off
            l_sensorBox.BackColor = (left_sensor == 0) ? Color.White : Color.Black;
            r_sensorBox.BackColor = (right_sensor == 0) ? Color.White : Color.Black;

            if (Math.Abs(left_motor - right_motor) < 5) // if moving forward hide arrows
            {
                left_arrow.Visible = false;
                right_arrow.Visible = false;
            }
            // if turning left - turn on left arrow
            else if (left_motor > right_motor)
            {
                left_arrow.Visible = false; // show right arrow as vehicle is turning right
                right_arrow.Visible = true;
            } // if turning right - turn on right arrow
            else if (left_motor < right_motor) // if forwards, hide them 
            {
                left_arrow.Visible = true; // show left arrow as vehicle turns left
                right_arrow.Visible = false;
            }
            

        }
        

        // Method controlling PWM1 scrollbar value
        private void OutputBox1_ValueChanged(object sender, EventArgs e)
        {
            // read value from outputbox
            double d_in = (double)OutputBox1.Value;

            // convert to byte from pwm value
            double a = K2 * d_in + K1; // equation obtained after performing a linear regression on bit v data for this DAC

            // limit bounds
            if (a > 255) a = 255;
            if (a < 0) a = 0;

            // only update when that specific box isn't focused - ensures it doesn't lock the value as it cyclically updates 
            if (outByte1.Focused != true)
                if (operation_mode == STOP) // and when stopped 
                    outByte1.Value = (decimal)Math.Floor(a);
        }
        // Method controlling PWM2 scrollbar value
        private void OutputBox2_ValueChanged(object sender, EventArgs e)
        {
            // read value from outputbox
            double d_in = (double)OutputBox2.Value;

            double a = K2 * d_in + K1; // equation obtained after performing a linear regression on bit v data for this DAC
            
            // ensure value isn't out of bounds
            if (a > 255)    a = 255;
            if (a < 0)      a = 0;
            // only update box values when one of them isn't focused - ensures they don't cyclically update each other and lock the value
            if (outByte2.Focused != true)
                    if (operation_mode == STOP)
                        outByte2.Value = (decimal)Math.Floor(a);
        }


        // update duty cycle from raw byte value
        // Method controlling byte1 scrollbar value
        private void outByte1_ValueChanged(object sender, EventArgs e)
        {
            // read value from byteBox
            double b_in = (double)outByte1.Value;
            // convert to PWM
            double a = (b_in - K1) / K2; // the reciprocal of the equation obtained via linear regression
            // limit range to 0-100
            if (a > 100) a = 100; // ensure value is not out of bounds
            if (a < 0) a = 0;       

            // only update box values when one of them isn't focused - ensures they don't cyclically update each other and lock the value
            if (OutputBox1.Focused != true)
                if (operation_mode == STOP)
                    OutputBox1.Value = (decimal)(a);

        }
        // Method controlling byte2 scrollbar value
        private void outByte2_ValueChanged(object sender, EventArgs e)
        {
            // read value from byteBox
            double b_in = (double)outByte2.Value;
            // convert to PWM
            double a = (b_in - K1) / K2; // the reciprocal of the equation obtained via linear regression
            // limit range to 0-100
            if (a > 100) a = 100;
            if (a < 0) a = 0;

            // only update box values when one of them isn't focused - ensures they don't cyclically update each other and lock the value
            if (OutputBox2.Focused != true)
                if (operation_mode == 5)
                    OutputBox2.Value = (decimal)(a);

        }

        // handle input - dropdown box
        private void modeSelect_Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            // read and store operating mode
            int index = modeSelect_Box.SelectedIndex; // get index of currently selected item
            operation_mode = index;
            // whenever operating mode is changed, stop the cart. - safety
            is_running = false;
            switch (index)
            {
                // If in the standard operating modes, update the GUI accordingly
                case CLOCKWISE:
                case COUNTERCLOCKWISE:
                case REVERSE_CIRCLE:
                case SQUIGGLE:
                    
                    running_label.Text = "Ready";
                    // disable manual speed override boxes
                    outByte1.Enabled = false;
                    outByte2.Enabled = false;
                    OutputBox1.Enabled = false;
                    OutputBox2.Enabled = false;
                    running_label.ForeColor = Color.Green;
                    break;

                // if in manual operating mode - update GUI accordingly
                case MANUAL:
                    running_label.Text = "Manual Ctrl";
                    // if manual override is checked
                    if (checkBox1.Checked)
                    {
                        // enable manual speed override boxes 
                        outByte1.Enabled = true;
                        outByte2.Enabled = true;
                        OutputBox1.Enabled = true;
                        OutputBox2.Enabled = true;
                    }
                    running_label.ForeColor = Color.Orange;
                    
                    break;
                default: // 
                    // when stopped update the GUI accordingly
                    running_label.Text = "Stopped";
                    running_label.ForeColor = Color.Red;
                    left_arrow.Visible = false;
                    right_arrow.Visible = false;
                    break;
            }
        }

        // Method that handles any changes in the Manual Override Checkbox
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // if it's checked - Manual override is on
            if (checkBox1.Checked)
            {
                // enable checkboxes to force sensor values
                force_left_sensor.Enabled = true;
                force_right_sensor.Enabled = true;
                // if in manual mode also enable manual motor control 
                if (operation_mode == MANUAL) 
                {
                    outByte1.Enabled = true;
                    outByte2.Enabled = true;
                    OutputBox1.Enabled = true;
                    OutputBox2.Enabled = true;
                }
                
            }
            else // if manual override is unchecked disable all manual override boxes
            {
                force_left_sensor.Enabled = false;
                force_right_sensor.Enabled = false;
                outByte1.Enabled = false;
                outByte2.Enabled = false;
                OutputBox1.Enabled = false;
                OutputBox2.Enabled = false;
                
            }
        }

        // if the Proportional scrollBox value changes update the Proportional parameter
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            P = (double)numericUpDown1.Value;
        }

        // if the Integral scrollBox value changes update the Integral parameter
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            I = (double)numericUpDown2.Value;
        }

        // if the Derivative scrollBox value changes update the Derivative parameter
        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            D = (double)numericUpDown3.Value;
        }

        // handles the Reset Button click
        private void button1_Click(object sender, EventArgs e)
        {
            // stop motors
            left_motor = 127;
            right_motor = 127;

            // reset error
            error = 0;
            total_error = 0;
            prev_error = 0;

            // reset direction change flag
            bonus = 0;
        }

        // Error decay scrollbox
        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            // set error decay to scrollbox value
            ERROR_DECAY = (double)numericUpDown4.Value;
        }



        /////////////////////////////////
        // stop when space is pressed
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // if space is pressed 
            if (keyData == Keys.Space)
            {
                // if vehicle is running stop it
                if (is_running == true)
                {
                    // update GUI and flags
                    running_label.Text = "Stopped";
                    running_label.ForeColor = Color.Red;
                    is_running = false;
                    
                }
                else // if vehicle was stopped - start it
                {
                    // update GUI
                    running_label.Text = "Started";
                    running_label.ForeColor = Color.Green;
                    // set the running flag
                    is_running = true;
                }
                return true;    // indicate that the keystroke was handled
            }

            // Call the base class
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }

    
 


}
