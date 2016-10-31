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
        byte[] Outputs = new byte[4];
        byte[] Inputs = new byte[4];

        // Various serial communication parameters
        bool open_serial_connection = true;
        const byte START = 255;
        const byte ZERO = 0;


        // int to store the 2 sensor reading bits
        int sensorReading = 0;

        // Items for the drop down menu
        private String[] operatingModes = { "Stop", "Manual", "Reverse Circle", "Clockwise", "Counterclockwise", "Squiggle"};

        // Motor max speeds
        private double L_MAX = 1; // 100% 
        private double R_MAX = 1; // 100%

        // Byte value of motor speeds
        int leftMotor = 127;
        int rightMotor = 127;

        // Control mode, taken from the index of the selected menu option
        int mode = 0;

        // Rate of correction
        private double adjustmentRate = 0;

        // Adjustment rate constants     stop:manual:reverse circle:Clockwise:Counterclockwise:Squiggle
        private double[] K_T =         { 0, 0, 0, 0, 0, 0 };
        private double[] I_T =         { 0, 0, 0, 0, 0, 0 };
        private double[] D_T =         { 0, 0, 0, 0, 0, 0 };

        // Error
        private int error = 0; // MAX error is 1
        private int prevError = 0;
        private int totalError = 0;


        public Form1()
        {
            // Initialise required for form controls.
            InitializeComponent();

            //Initialise the PID parameter rollers
            KRoller.Value = (decimal)K_T[mode];
            IRoller.Value = (decimal)I_T[mode];
            DRoller.Value = (decimal)D_T[mode];

            foreach (String mode in operatingModes) // Add the items to the dropdown menu
            {
                modeSelect.Items.Add(mode);
            }
            this.modeSelect.SelectedIndex = 0;  // Set default inital mode to Stop


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
            
            Outputs[0] = START;     // Set the first byte to the start value that indicates the beginning of the message.
            Outputs[1] = PORT;      // Set the second byte to represent the port where, Input 1 = 0, Input 2 = 1, Output 1 = 2 & Output 2 = 3.
            Outputs[2] = DATA;      // Set the third byte to the value to be assigned to the port. This is only necessary for outputs, however it is best to assign a consistent value such as 0 for input ports.
            Outputs[3] = (byte)(START + PORT + DATA); //Calculate the checksum byte, the same calculation is performed on the Arduino side to confirm the message was received correctly.

            if (serial.IsOpen)
            {
                serial.Write(Outputs, 0, 4);         // Send all four bytes to the IO card.                      
            }
        }

        private void getIOtimer_Tick(object sender, EventArgs e) // It is best to continuously check for incoming data as handling the buffer or waiting for event is not practical in C#.
        {
            if (serial.IsOpen) // Check that a serial connection exists.
            {
                if (serial.BytesToRead >= 4) // Check that the buffer contains a full four byte package.
                {
                    Inputs[0] = (byte)serial.ReadByte(); // Read the first byte of the package.

                    if (Inputs[0] == START) // Check that the first byte is in fact the start byte.
                    {
                        // Read the rest of the package.
                        Inputs[1] = (byte)serial.ReadByte();
                        Inputs[2] = (byte)serial.ReadByte();
                        Inputs[3] = (byte)serial.ReadByte();

                        // Calculate the checksum.
                        byte checkSum = (byte)(Inputs[0] + Inputs[1] + Inputs[2]);

                        // Check that the calculated check sum matches the checksum sent with the message.
                        if (Inputs[3] == checkSum)
                        {
                            sensorReading = Inputs[2];

                        }
                    }
                }
            }
        }




        // main loop 
        private void sendLoopTimer_Tick(object sender, EventArgs e)
        {
            // Bit mask the Input byte. bit 0 is left sensor. bit 1 is right sensor.
            int leftSensor = ((1 << SENSOR1_POS) & sensorReading);
            int rightSensor = ((1 << SENSOR2_POS) & sensorReading);

            // Override sensor inputs for debugging purposes
            if (forceRightSensor.Checked)
                rightSensor = 1;
            if (forceLeftSensor.Checked)
                leftSensor = 1;

            // Main control loop
            switch (mode)
            {
                case 0: // Stopped
                    leftMotor = 127;
                    rightMotor = 127;
                    break;
                case 1: // Manual
                    leftMotor = (byte)leftByte.Value;
                    rightMotor = (byte)rightByte.Value;
                    break;
                case 2:
                    // Reverse : One sensor should stay on the line. If one is off, continue straight
                    //            if both are off, turn in last detected direction
                    error = leftSensor + rightSensor - 1;
                    if (error == 0)
                        totalError = 0;
                    adjustmentRate = error * K_T[mode] + (error - prevError) * D_T[mode] + totalError * I_T[mode];
                    leftMotor = (int)(L_MAX * 0 - adjustmentRate);
                    if (leftMotor > 127)
                        leftMotor = 127;
                    rightMotor = (int)(R_MAX * 0 + adjustmentRate);
                    if (rightMotor > 127)
                        rightMotor = 127;
                    prevError = error;
                    totalError += error;
                    break;
                case 3:
                case 4:
                case 5:
                    // Forward: One sensor should stay on the line. If one is off, continue straight
                    //            if both are off, turn in last detected direction
                    error = leftSensor + rightSensor-1;
                    if (error == 0)
                        totalError = 0;
                    adjustmentRate = error * K_T[mode] + (error - prevError) * D_T[mode] + totalError * I_T[mode];
                    leftMotor = (int)(L_MAX * 255 - adjustmentRate);
                    if (leftMotor < 127)
                        leftMotor = 127;
                    rightMotor = (int)(R_MAX * 255 + adjustmentRate);
                    if (rightMotor < 127)
                        rightMotor = 127;
                    prevError = error;
                    totalError += error;
                    break;
            }



            // GUI Update
            totalErrorLabel.Text = "Total Error: " + totalError.ToString();
            adjRateLabel.Text = "Adj rate: " + adjustmentRate.ToString();
            errorLabel.Text = "Error: " + error.ToString();
            leftSensorOutput.Text = leftSensor.ToString();
            rightSensorOutput.Text = rightSensor.ToString();

            // Ensure proper byte range
            if (leftMotor > 255)
                leftMotor = 255;
            if (rightMotor > 255)
                rightMotor = 255;
            if (leftMotor < 0)
                leftMotor = 0;
            if (rightMotor < 0)
                rightMotor = 0;

            leftByte.Value = leftMotor;
            rightByte.Value = rightMotor;

        }

        // Spacebar stops the cart
        void Form1_KeyDown(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
                modeSelect.SelectedIndex = 0;
        }

        // Method controlling left motor byte scrollbar value
        private void outSpeedLeft_ValueChanged(object sender, EventArgs e)
        {
            double d_in = (double)leftDuty.Value;

            double a = K2 * d_in + K1; // equation obtained after performing a linear regression on bit v data for this DAC

            if (a > 255) a = 255;
            if (a < 0) a = 0;

            leftByte.Value = (decimal)Math.Floor(a);
        }

        // Method controlling right motor byte scrollbar value
        private void outSpeedRight_ValueChanged(object sender, EventArgs e)
        {
            double d_in = (double)rightDuty.Value;

            double a = K2 * d_in + K1; // equation obtained after performing a linear regression on bit v data for this DAC

            if (a > 255)    a = 255;
            if (a < 0)      a = 0;

            rightByte.Value = (decimal)Math.Floor(a);
        }

        
        // Update duty cycle from raw byte value
        // Method controlling right PWM scrollbar value
        private void outByteRight_ValueChanged(object sender, EventArgs e)
        {
            double b_in = (double)rightByte.Value;

            double a = (b_in - K1) / K2; // the reciprocal of the equation obtained via linear regression

            if (a > 100) a = 100; // ensure value is not out of bounds
            if (a < 0) a = 0;
            rightDuty.Value = (decimal)(a);

        }

        // Method controlling left PWM scrollbar value
        private void outByteLeft_ValueChanged(object sender, EventArgs e)
        {
            double b_in = (double)leftByte.Value;

            double a = (b_in - K1) / K2; // the reciprocal of the equation obtained via linear regression

            if (a > 100) a = 100; // ensure value is not out of bounds
            if (a < 0) a = 0;
            leftDuty.Value = (decimal)(a);


        }

        // Method to change the GUI when the control mode is changed
        private void switchMode()
        {
            switch (modeSelect.SelectedIndex)
            {
                case 0: // Stopped mode
                    runningLabel.Text = "Stopped";
                    runningLabel.ForeColor = Color.Red;
                    leftByte.Enabled = false;
                    rightByte.Enabled = false;
                    leftDuty.Enabled = false;
                    rightDuty.Enabled = false;
                    KRoller.Enabled = false;
                    IRoller.Enabled = false;
                    DRoller.Enabled = false;
                    break;
                case 1: // Manual control mode
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
                case 5: // Any other running more
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
                default: // Something is broken
                    runningLabel.Text = "10 points to Gryffindor";
                    modeSelect.SelectedIndex = 0;                    
                    break;
            }

        }

        // Dropdown box control
        private void modeSelect_Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            error = 0;
            totalError = 0;
            prevError = 0;
            switchMode();
            mode = modeSelect.SelectedIndex;
            KRoller.Value = (decimal)K_T[mode];
            IRoller.Value = (decimal)I_T[mode];
            DRoller.Value = (decimal)D_T[mode];
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            K_T[mode] = (double)KRoller.Value;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            I_T[mode] = (double)IRoller.Value;
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            D_T[mode] = (double)DRoller.Value;
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }


}
