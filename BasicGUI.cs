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

        int sensorReading = 0;

        byte[] Outputs = new byte[4];
        byte[] Inputs = new byte[4];

        const byte START = 255;
        const byte ZERO = 0;

        // used to add items to the dropdown menu
        private String[] operating_modes = { "Stop", "Manual", "Clockwise", "Counterclockwise", "Reverse Circle", "Squiggle"};

        // Motor max speeds
        private double L_MAX = 1; // 100% 
        private double R_MAX = 1; // 100%

        int left_motor = 127;
        int right_motor = 127;


        // rate of last correction
        private double adjustmentRate = 0; // 
        private double previous_adjustment_rate = 0; // 

        // adjustment rate constant 
        private double K_T = 0;
        private double I_T = 0;
        private double D_T = 0;

        // error
        private int error = 0; // MAX error is 2
        private int prev_error = 0;
        private int totalError = 0;


        public Form1()
        {
            // Initialize required for form controls.
            InitializeComponent();

            KRoller.Value = (decimal)K_T;
            IRoller.Value = (decimal)I_T;
            DRoller.Value = (decimal)D_T;

            foreach (String mode in operating_modes) // add the items to the dropdown menu
            {
                modeSelect.Items.Add(mode);
            }
            this.modeSelect.SelectedIndex = 0;


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
                            sensorReading = Inputs[2];

                        }
                    }
                }
            }
        }



        // main loop 
        private void sendLoopTimer_Tick(object sender, EventArgs e)
        {
            // bit mask the Input byte. bit 0 is left sensor. bit 1 is right sensor.

            int left_sensor = ((1 << SENSOR1_POS) & sensorReading);
            int right_sensor = ((1 << SENSOR2_POS) & sensorReading);
            if (forceRightSensor.Checked)
                right_sensor = 1;
            if (forceLeftSensor.Checked)
                left_sensor = 1;

            leftSensorOutput.Text = left_sensor.ToString();
            rightSensorOutput.Text = right_sensor.ToString();

            switch (modeSelect.SelectedIndex)
            {
                case 0: // stopped
                    left_motor = 127;
                    right_motor = 127;
                    break;
                case 1: // manual
                    left_motor = (byte)leftByte.Value;
                    right_motor = (byte)rightByte.Value;
                    break;
                case 2:
                    // Clockwise: Both sensors should stay on the line. If one is off, turn in opposite direction
                    //            if both are off, turn in last detected direction
                    error = left_sensor + right_sensor;
                    adjustmentRate = error * K_T + (error - prev_error) * D_T + totalError * I_T;
                    left_motor = (int)(L_MAX * 255 - adjustmentRate);
                    if (left_motor < 127)
                        left_motor = 127;
                    right_motor = (int)(R_MAX * 255 + adjustmentRate);
                    if (right_motor < 127)
                        right_motor = 127;
                    prev_error = error;
                    totalError += error;
                    break;
                case 3: // Counterclockwise
                    break;
                case 4: // Reverse Clockwise
                    break;
                case 5: // Squiggle
                    break;
                default:
                    Console.WriteLine(String.Format("Error. Operation mode invalid: {0}\n", modeSelect.SelectedIndex));
                    break;
            }


                // GUI Update
                totalErrorLabel.Text = "Total Error: " + totalError.ToString();
                adjRateLabel.Text = "Adj rate: " + adjustmentRate.ToString();
                errorLabel.Text = "Error: " + error.ToString();

                // ensure proper byte range
                if (left_motor > 255)
                    left_motor = 255; 
                if (right_motor > 255)
                    right_motor = 255; 
                if (left_motor < 0)
                    left_motor = 0;
                if (right_motor < 0)
                    right_motor = 0;

                leftByte.Value = left_motor;
                rightByte.Value = right_motor;



        }

        void Form1_KeyDown(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 53)
                modeSelect.SelectedIndex = (int)e.KeyChar - 48;
            else if (e.KeyChar == (char)Keys.Space)
                modeSelect.SelectedIndex = 0;

            if (Control.ModifierKeys == Keys.Up && modeSelect.SelectedIndex < 5)
                modeSelect.SelectedIndex++;
            else if (Control.ModifierKeys == Keys.Down && modeSelect.SelectedIndex > 0)
                modeSelect.SelectedIndex--;

        }

        // Method controlling PWM1 scrollbar value
        private void OutputSpeedLeft_ValueChanged(object sender, EventArgs e)
        {
            double d_in = (double)leftDuty.Value;

            double a = K2 * d_in + K1; // equation obtained after performing a linear regression on bit v data for this DAC

            if (a > 255) a = 255;
            if (a < 0) a = 0;

            if (leftByte.Focused != true) // only update when that specific box isn't focused
                leftByte.Value = (decimal)Math.Floor(a);
        }
        // Method controlling PWM2 scrollbar value
        private void OutputSpeedRight_ValueChanged(object sender, EventArgs e)
        {
            double d_in = (double)rightDuty.Value;

            double a = K2 * d_in + K1; // equation obtained after performing a linear regression on bit v data for this DAC

            if (a > 255)    a = 255;
            if (a < 0)      a = 0;

            if (rightByte.Focused != true)
                rightByte.Value = (decimal)Math.Floor(a);
        }


        // update duty cycle from raw byte value
        // Method controlling byte1 scrollbar value
        private void outByteRight_ValueChanged(object sender, EventArgs e)
        {
            double b_in = (double)rightByte.Value;

            double a = (b_in - K1) / K2; // the reciprocal of the equation obtained via linear regression

            if (a > 100) a = 100; // ensure value is not out of bounds
            if (a < 0) a = 0;       

            if (rightDuty.Focused != true)
                rightDuty.Value = (decimal)(a);

        }
        // Method controlling byte2 scrollbar value
        private void outByteLeft_ValueChanged(object sender, EventArgs e)
        {
            double b_in = (double)leftByte.Value;

            double a = (b_in - K1) / K2; // the reciprocal of the equation obtained via linear regression

            if (a > 100) a = 100; // ensure value is not out of bounds
            if (a < 0) a = 0;

            if (leftDuty.Focused != true)
                leftDuty.Value = (decimal)(a);

        }
        private void switchMode()
        {
            switch (modeSelect.SelectedIndex)
            {
                case 0:
                    runningLabel.Text = "Stopped";
                    runningLabel.ForeColor = Color.Red;
                    leftByte.Enabled = false;
                    rightByte.Enabled = false;
                    leftDuty.Enabled = false;
                    rightDuty.Enabled = false;
                    KRoller.Enabled = true;
                    IRoller.Enabled = true;
                    DRoller.Enabled = true;
                    break;
                case 1:
                    runningLabel.Text = "Manual";
                    runningLabel.ForeColor = Color.Orange;
                    leftByte.Enabled = true;
                    rightByte.Enabled = true;
                    leftDuty.Enabled = true;
                    rightDuty.Enabled = true;
                    KRoller.Enabled = false;
                    IRoller.Enabled = false;
                    DRoller.Enabled = false;
                    break;
                case 2:
                case 3:
                case 4:
                case 5:
                    runningLabel.Text = "Running";
                    runningLabel.ForeColor = Color.Green;
                    leftByte.Enabled = false;
                    rightByte.Enabled = false;
                    leftDuty.Enabled = false;
                    rightDuty.Enabled = false;
                    KRoller.Enabled = true;
                    IRoller.Enabled = true;
                    DRoller.Enabled = true;
                    break;
                default: // 
                    runningLabel.Text = "10 points to Gryffindor";
                    break;
            }

        }

        // handle input - dropdown box
        private void modeSelect_Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            error = 0;
            totalError = 0;
            prev_error = 0;
            switchMode();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            K_T = (double)KRoller.Value;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            I_T = (double)IRoller.Value;
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            D_T = (double)DRoller.Value;
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }


}
