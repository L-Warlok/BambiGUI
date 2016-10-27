// Curtin University
// Mechatronics Engineering
// Bluetooth I/O Card - Sample GUI Code

using System;
using System.Drawing;
using System.Windows.Forms;


using static BluetoothGUISample.Params;
namespace BluetoothGUISample
{

    public partial class Form1 : Form
    {
        // Declare variables to store inputs and outputs.
        bool open_serial_connection = true;

        int Input1 = 0;
        int Input2 = 0;

        byte[] Outputs = new byte[4];
        byte[] Inputs = new byte[4];

        const byte START = 255;
        const byte ZERO = 0;

        // used to add items to the dropdown menu
        private String[] operating_modes = { "Stop", "Manual", "Clockwise", "Counterclockwise", "Reverse Circle", "Squiggle"};

        // Motor max speeds
        private double L_MAX = 1; // 100% 
        private double R_MAX = 1; // 100%

        int operation_mode = 0;


        public Form1()
        {
            // Initialize required for form controls.
            InitializeComponent();




            numericUpDown1.Value = (decimal)K_T;
            numericUpDown2.Value = (decimal)I_T;
            numericUpDown3.Value = (decimal)D_T;

            foreach (String mode in operating_modes) // add the items to the dropdown menu
            {
                modeSelect_Box.Items.Add(mode);
            }
            this.modeSelect_Box.SelectedIndex = operation_mode;


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
                case 5: // manual
                    left_motor = (byte)outByte1.Value;
                    right_motor = (byte)outByte2.Value;
                    break;
                default:
                    Console.WriteLine(String.Format("Error. Operation mode invalid: {0}\n", operation_mode));
                    break;
            }


                // GUI Update
                adj_dir_label.Text = "Total Error: " + total_error.ToString();
                adj_rate_label.Text = "Adj rate: " + adjustment_rate.ToString();
                prev_adj_label.Text = "Error: " + error.ToString();
                prev_adj_r_label.Text = "Prev error:" + previous_adjustment_rate.ToString();

                // line tracking


                // ensure proper byte range

                prev_error = error;

                if (left_motor > 255) left_motor = 255; 
                if (right_motor > 255) right_motor = 255; 
                if (left_motor < 0) left_motor = 0;
                if (right_motor < 0) right_motor = 0;

                outByte1.Value = left_motor;
                outByte2.Value = right_motor;

            switch (operation_mode)
            {
                case 0:
                    running_label.Text = "Stopped";
                    running_label.ForeColor = Color.Red;
                    outByte1.Enabled = false;
                    outByte2.Enabled = false;
                    OutputBox1.Enabled = false;
                    OutputBox2.Enabled = false;
                    break;
                case 1:
                    running_label.Text = "Manual";
                    running_label.ForeColor = Color.Orange;
                    outByte1.Enabled = true;
                    outByte2.Enabled = true;
                    OutputBox1.Enabled = true;
                    OutputBox2.Enabled = true;
                    break;
                case 2:
                case 3:
                case 4:
                case 5:
                    running_label.Text = "Running";
                    running_label.ForeColor = Color.Green;
                    outByte1.Enabled = false;
                    outByte2.Enabled = false;
                    OutputBox1.Enabled = false;
                    OutputBox2.Enabled = false;
                    break;
                default: // 
                    running_label.Text = "10 points to Gryffindor";
                    break;
            }
            modeSelect_Box.SelectedIndex = operation_mode;

        }
        void Form1_KeyDown(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 53)
            {
                operation_mode = (int)e.KeyChar - 48;
            }
            else if (e.KeyChar == (char)Keys.Space)
                operation_mode = 0;

            if (Control.ModifierKeys == Keys.Up && operation_mode < 5)
                operation_mode++;
            else if (Control.ModifierKeys == Keys.Down && operation_mode > 0)
                operation_mode--;

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
            operation_mode = modeSelect_Box.SelectedIndex; // get index of currently selected item

        }


        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            K_T = (double)numericUpDown1.Value;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            I_T = (double)numericUpDown2.Value;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }


}
