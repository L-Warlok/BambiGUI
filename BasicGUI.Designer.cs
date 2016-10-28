namespace BluetoothGUISample
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.serial = new System.IO.Ports.SerialPort(this.components);
            this.getIOtimer = new System.Windows.Forms.Timer(this.components);
            this.leftDuty = new System.Windows.Forms.NumericUpDown();
            this.rightDuty = new System.Windows.Forms.NumericUpDown();
            this.Title = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dutyCycle = new System.Windows.Forms.Label();
            this.mainLoop = new System.Windows.Forms.Timer(this.components);
            this.leftMotor = new System.Windows.Forms.Label();
            this.rightMotor = new System.Windows.Forms.Label();
            this.leftByte = new System.Windows.Forms.NumericUpDown();
            this.rightByte = new System.Windows.Forms.NumericUpDown();
            this.modeSelect = new System.Windows.Forms.ComboBox();
            this.operatingMode = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.connectionMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.runningLabel = new System.Windows.Forms.Label();
            this.sensorControl = new System.Windows.Forms.Label();
            this.forceLeftSensor = new System.Windows.Forms.CheckBox();
            this.forceRightSensor = new System.Windows.Forms.CheckBox();
            this.KRoller = new System.Windows.Forms.NumericUpDown();
            this.K = new System.Windows.Forms.Label();
            this.adjRateLabel = new System.Windows.Forms.Label();
            this.errorLabel = new System.Windows.Forms.Label();
            this.totalErrorLabel = new System.Windows.Forms.Label();
            this.I = new System.Windows.Forms.Label();
            this.IRoller = new System.Windows.Forms.NumericUpDown();
            this.DRoller = new System.Windows.Forms.NumericUpDown();
            this.D = new System.Windows.Forms.Label();
            this.parameters = new System.Windows.Forms.Label();
            this.leftSensorOutput = new System.Windows.Forms.TextBox();
            this.rightSensorOutput = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.leftDuty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightDuty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftByte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightByte)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KRoller)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IRoller)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DRoller)).BeginInit();
            this.SuspendLayout();
            // 
            // serial
            // 
            this.serial.PortName = "COM10";
            // 
            // getIOtimer
            // 
            this.getIOtimer.Enabled = true;
            this.getIOtimer.Interval = 10;
            this.getIOtimer.Tick += new System.EventHandler(this.getIOtimer_Tick);
            // 
            // leftDuty
            // 
            this.leftDuty.DecimalPlaces = 1;
            this.leftDuty.Enabled = false;
            this.leftDuty.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.leftDuty.Location = new System.Drawing.Point(254, 125);
            this.leftDuty.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.leftDuty.Name = "leftDuty";
            this.leftDuty.Size = new System.Drawing.Size(63, 20);
            this.leftDuty.TabIndex = 3;
            this.leftDuty.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.leftDuty.ValueChanged += new System.EventHandler(this.OutputSpeedLeft_ValueChanged);
            // 
            // rightDuty
            // 
            this.rightDuty.DecimalPlaces = 1;
            this.rightDuty.Enabled = false;
            this.rightDuty.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.rightDuty.Location = new System.Drawing.Point(331, 125);
            this.rightDuty.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.rightDuty.Name = "rightDuty";
            this.rightDuty.Size = new System.Drawing.Size(65, 20);
            this.rightDuty.TabIndex = 3;
            this.rightDuty.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.rightDuty.ValueChanged += new System.EventHandler(this.OutputSpeedRight_ValueChanged);
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.Title.Location = new System.Drawing.Point(115, 12);
            this.Title.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(127, 20);
            this.Title.TabIndex = 6;
            this.Title.Text = "Bambi GUI 1.0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(402, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Raw byte";
            // 
            // dutyCycle
            // 
            this.dutyCycle.AutoSize = true;
            this.dutyCycle.Location = new System.Drawing.Point(402, 127);
            this.dutyCycle.Name = "dutyCycle";
            this.dutyCycle.Size = new System.Drawing.Size(75, 13);
            this.dutyCycle.TabIndex = 7;
            this.dutyCycle.Text = "Duty Cycle (%)";
            // 
            // mainLoop
            // 
            this.mainLoop.Enabled = true;
            this.mainLoop.Tick += new System.EventHandler(this.sendLoopTimer_Tick);
            // 
            // leftMotor
            // 
            this.leftMotor.AutoSize = true;
            this.leftMotor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.leftMotor.Location = new System.Drawing.Point(252, 109);
            this.leftMotor.Name = "leftMotor";
            this.leftMotor.Size = new System.Drawing.Size(65, 13);
            this.leftMotor.TabIndex = 8;
            this.leftMotor.Text = "Left Motor";
            // 
            // rightMotor
            // 
            this.rightMotor.AutoSize = true;
            this.rightMotor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rightMotor.Location = new System.Drawing.Point(328, 109);
            this.rightMotor.Name = "rightMotor";
            this.rightMotor.Size = new System.Drawing.Size(73, 13);
            this.rightMotor.TabIndex = 8;
            this.rightMotor.Text = "Right Motor";
            // 
            // leftByte
            // 
            this.leftByte.Enabled = false;
            this.leftByte.Location = new System.Drawing.Point(255, 154);
            this.leftByte.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.leftByte.Name = "leftByte";
            this.leftByte.Size = new System.Drawing.Size(62, 20);
            this.leftByte.TabIndex = 10;
            this.leftByte.Value = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.leftByte.ValueChanged += new System.EventHandler(this.outByteRight_ValueChanged);
            // 
            // rightByte
            // 
            this.rightByte.Enabled = false;
            this.rightByte.Location = new System.Drawing.Point(331, 154);
            this.rightByte.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.rightByte.Name = "rightByte";
            this.rightByte.Size = new System.Drawing.Size(65, 20);
            this.rightByte.TabIndex = 11;
            this.rightByte.Value = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.rightByte.ValueChanged += new System.EventHandler(this.outByteLeft_ValueChanged);
            // 
            // modeSelect
            // 
            this.modeSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.modeSelect.FormattingEnabled = true;
            this.modeSelect.Location = new System.Drawing.Point(172, 56);
            this.modeSelect.Name = "modeSelect";
            this.modeSelect.Size = new System.Drawing.Size(121, 21);
            this.modeSelect.TabIndex = 12;
            this.modeSelect.SelectedIndexChanged += new System.EventHandler(this.modeSelect_Box_SelectedIndexChanged);
            // 
            // operatingMode
            // 
            this.operatingMode.AutoSize = true;
            this.operatingMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.operatingMode.Location = new System.Drawing.Point(43, 57);
            this.operatingMode.Name = "operatingMode";
            this.operatingMode.Size = new System.Drawing.Size(123, 16);
            this.operatingMode.TabIndex = 13;
            this.operatingMode.Text = "Operating Mode:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectionMessage});
            this.statusStrip1.Location = new System.Drawing.Point(0, 325);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(559, 22);
            this.statusStrip1.TabIndex = 16;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // connectionMessage
            // 
            this.connectionMessage.Name = "connectionMessage";
            this.connectionMessage.Size = new System.Drawing.Size(0, 17);
            // 
            // runningLabel
            // 
            this.runningLabel.AutoSize = true;
            this.runningLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runningLabel.ForeColor = System.Drawing.Color.Red;
            this.runningLabel.Location = new System.Drawing.Point(12, 107);
            this.runningLabel.Name = "runningLabel";
            this.runningLabel.Size = new System.Drawing.Size(67, 16);
            this.runningLabel.TabIndex = 17;
            this.runningLabel.Text = "Stopped";
            // 
            // sensorControl
            // 
            this.sensorControl.AutoSize = true;
            this.sensorControl.Location = new System.Drawing.Point(12, 134);
            this.sensorControl.Name = "sensorControl";
            this.sensorControl.Size = new System.Drawing.Size(103, 13);
            this.sensorControl.TabIndex = 18;
            this.sensorControl.Text = "Force sensor control";
            // 
            // forceLeftSensor
            // 
            this.forceLeftSensor.AutoSize = true;
            this.forceLeftSensor.Location = new System.Drawing.Point(132, 133);
            this.forceLeftSensor.Name = "forceLeftSensor";
            this.forceLeftSensor.Size = new System.Drawing.Size(32, 17);
            this.forceLeftSensor.TabIndex = 19;
            this.forceLeftSensor.Text = "L";
            this.forceLeftSensor.UseVisualStyleBackColor = true;
            // 
            // forceRightSensor
            // 
            this.forceRightSensor.AutoSize = true;
            this.forceRightSensor.Location = new System.Drawing.Point(170, 133);
            this.forceRightSensor.Name = "forceRightSensor";
            this.forceRightSensor.Size = new System.Drawing.Size(34, 17);
            this.forceRightSensor.TabIndex = 20;
            this.forceRightSensor.Text = "R";
            this.forceRightSensor.UseVisualStyleBackColor = true;
            // 
            // KRoller
            // 
            this.KRoller.DecimalPlaces = 2;
            this.KRoller.Enabled = false;
            this.KRoller.Location = new System.Drawing.Point(132, 188);
            this.KRoller.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.KRoller.Name = "KRoller";
            this.KRoller.Size = new System.Drawing.Size(60, 20);
            this.KRoller.TabIndex = 23;
            this.KRoller.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // K
            // 
            this.K.AutoSize = true;
            this.K.Location = new System.Drawing.Point(198, 190);
            this.K.Name = "K";
            this.K.Size = new System.Drawing.Size(14, 13);
            this.K.TabIndex = 24;
            this.K.Text = "K";
            // 
            // adjRateLabel
            // 
            this.adjRateLabel.AutoSize = true;
            this.adjRateLabel.Location = new System.Drawing.Point(396, 218);
            this.adjRateLabel.Name = "adjRateLabel";
            this.adjRateLabel.Size = new System.Drawing.Size(88, 13);
            this.adjRateLabel.TabIndex = 25;
            this.adjRateLabel.Text = "Adjustment Rate:";
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.Location = new System.Drawing.Point(252, 231);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(65, 13);
            this.errorLabel.TabIndex = 26;
            this.errorLabel.Text = "Current error";
            // 
            // totalErrorLabel
            // 
            this.totalErrorLabel.AutoSize = true;
            this.totalErrorLabel.Location = new System.Drawing.Point(252, 218);
            this.totalErrorLabel.Name = "totalErrorLabel";
            this.totalErrorLabel.Size = new System.Drawing.Size(55, 13);
            this.totalErrorLabel.TabIndex = 27;
            this.totalErrorLabel.Text = "Total error";
            // 
            // I
            // 
            this.I.AutoSize = true;
            this.I.Location = new System.Drawing.Point(201, 220);
            this.I.Name = "I";
            this.I.Size = new System.Drawing.Size(10, 13);
            this.I.TabIndex = 24;
            this.I.Text = "I";
            // 
            // IRoller
            // 
            this.IRoller.DecimalPlaces = 1;
            this.IRoller.Enabled = false;
            this.IRoller.Location = new System.Drawing.Point(132, 218);
            this.IRoller.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.IRoller.Name = "IRoller";
            this.IRoller.Size = new System.Drawing.Size(60, 20);
            this.IRoller.TabIndex = 32;
            this.IRoller.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // DRoller
            // 
            this.DRoller.DecimalPlaces = 2;
            this.DRoller.Enabled = false;
            this.DRoller.Location = new System.Drawing.Point(132, 245);
            this.DRoller.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.DRoller.Name = "DRoller";
            this.DRoller.Size = new System.Drawing.Size(60, 20);
            this.DRoller.TabIndex = 33;
            this.DRoller.ValueChanged += new System.EventHandler(this.numericUpDown3_ValueChanged);
            // 
            // D
            // 
            this.D.AutoSize = true;
            this.D.Location = new System.Drawing.Point(200, 247);
            this.D.Name = "D";
            this.D.Size = new System.Drawing.Size(15, 13);
            this.D.TabIndex = 34;
            this.D.Text = "D";
            // 
            // parameters
            // 
            this.parameters.AutoSize = true;
            this.parameters.Location = new System.Drawing.Point(12, 188);
            this.parameters.Name = "parameters";
            this.parameters.Size = new System.Drawing.Size(90, 13);
            this.parameters.TabIndex = 35;
            this.parameters.Text = "Parameter control";
            this.parameters.Click += new System.EventHandler(this.label1_Click);
            // 
            // leftSensorOutput
            // 
            this.leftSensorOutput.Location = new System.Drawing.Point(132, 154);
            this.leftSensorOutput.Name = "leftSensorOutput";
            this.leftSensorOutput.Size = new System.Drawing.Size(18, 20);
            this.leftSensorOutput.TabIndex = 36;
            // 
            // rightSensorOutput
            // 
            this.rightSensorOutput.Location = new System.Drawing.Point(170, 154);
            this.rightSensorOutput.Name = "rightSensorOutput";
            this.rightSensorOutput.Size = new System.Drawing.Size(18, 20);
            this.rightSensorOutput.TabIndex = 37;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(559, 347);
            this.Controls.Add(this.rightSensorOutput);
            this.Controls.Add(this.leftSensorOutput);
            this.Controls.Add(this.parameters);
            this.Controls.Add(this.D);
            this.Controls.Add(this.DRoller);
            this.Controls.Add(this.IRoller);
            this.Controls.Add(this.totalErrorLabel);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.adjRateLabel);
            this.Controls.Add(this.I);
            this.Controls.Add(this.K);
            this.Controls.Add(this.KRoller);
            this.Controls.Add(this.forceRightSensor);
            this.Controls.Add(this.forceLeftSensor);
            this.Controls.Add(this.sensorControl);
            this.Controls.Add(this.runningLabel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.operatingMode);
            this.Controls.Add(this.modeSelect);
            this.Controls.Add(this.rightByte);
            this.Controls.Add(this.leftByte);
            this.Controls.Add(this.rightMotor);
            this.Controls.Add(this.leftMotor);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dutyCycle);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.rightDuty);
            this.Controls.Add(this.leftDuty);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "Form1";
            this.Text = "Bambi GUI 1.0";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.leftDuty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightDuty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftByte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightByte)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KRoller)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IRoller)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DRoller)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer getIOtimer;
        private System.Windows.Forms.NumericUpDown leftDuty;
        private System.IO.Ports.SerialPort serial;
        private System.Windows.Forms.NumericUpDown rightDuty;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label dutyCycle;
        private System.Windows.Forms.Timer mainLoop;
        private System.Windows.Forms.Label leftMotor;
        private System.Windows.Forms.Label rightMotor;
        private System.Windows.Forms.NumericUpDown leftByte;
        private System.Windows.Forms.NumericUpDown rightByte;
        private System.Windows.Forms.ComboBox modeSelect;
        private System.Windows.Forms.Label operatingMode;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel connectionMessage;
        private System.Windows.Forms.Label runningLabel;
        private System.Windows.Forms.Label sensorControl;
        private System.Windows.Forms.CheckBox forceLeftSensor;
        private System.Windows.Forms.CheckBox forceRightSensor;
        private System.Windows.Forms.NumericUpDown KRoller;
        private System.Windows.Forms.Label K;
        private System.Windows.Forms.Label adjRateLabel;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.Label totalErrorLabel;
        private System.Windows.Forms.Label I;
        private System.Windows.Forms.NumericUpDown IRoller;
        private System.Windows.Forms.NumericUpDown DRoller;
        private System.Windows.Forms.Label D;
        private System.Windows.Forms.Label parameters;
        private System.Windows.Forms.TextBox leftSensorOutput;
        private System.Windows.Forms.TextBox rightSensorOutput;
    }
}

