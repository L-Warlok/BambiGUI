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
            this.OutputBox1 = new System.Windows.Forms.NumericUpDown();
            this.OutputBox2 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.mainLoop = new System.Windows.Forms.Timer(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.outByte1 = new System.Windows.Forms.NumericUpDown();
            this.outByte2 = new System.Windows.Forms.NumericUpDown();
            this.modeSelect_Box = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.connectionMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.running_label = new System.Windows.Forms.Label();
            this.state_label = new System.Windows.Forms.Label();
            this.force_left_sensor = new System.Windows.Forms.CheckBox();
            this.force_right_sensor = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.sensor_readings_label = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.adj_rate_label = new System.Windows.Forms.Label();
            this.prev_adj_label = new System.Windows.Forms.Label();
            this.adj_dir_label = new System.Windows.Forms.Label();
            this.deltaT_label = new System.Windows.Forms.Label();
            this.prev_adj_r_label = new System.Windows.Forms.Label();
            this.last_adj_ms_label = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.OutputBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OutputBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.outByte1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.outByte2)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // serial
            // 
            this.serial.PortName = "COM9";
            // 
            // getIOtimer
            // 
            this.getIOtimer.Enabled = true;
            this.getIOtimer.Interval = 10;
            this.getIOtimer.Tick += new System.EventHandler(this.getIOtimer_Tick);
            // 
            // OutputBox1
            // 
            this.OutputBox1.DecimalPlaces = 1;
            this.OutputBox1.Enabled = false;
            this.OutputBox1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.OutputBox1.Location = new System.Drawing.Point(254, 125);
            this.OutputBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.OutputBox1.Name = "OutputBox1";
            this.OutputBox1.Size = new System.Drawing.Size(63, 20);
            this.OutputBox1.TabIndex = 3;
            this.OutputBox1.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.OutputBox1.ValueChanged += new System.EventHandler(this.OutputBox1_ValueChanged);
            // 
            // OutputBox2
            // 
            this.OutputBox2.DecimalPlaces = 1;
            this.OutputBox2.Enabled = false;
            this.OutputBox2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.OutputBox2.Location = new System.Drawing.Point(331, 125);
            this.OutputBox2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.OutputBox2.Name = "OutputBox2";
            this.OutputBox2.Size = new System.Drawing.Size(65, 20);
            this.OutputBox2.TabIndex = 3;
            this.OutputBox2.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.OutputBox2.ValueChanged += new System.EventHandler(this.OutputBox2_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(285, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Bambi GUI 2.0";
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
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(402, 127);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Duty Cycle (%)";
            // 
            // mainLoop
            // 
            this.mainLoop.Enabled = true;
            this.mainLoop.Tick += new System.EventHandler(this.sendLoopTimer_Tick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(252, 109);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Left Motor";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(328, 109);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Right Motor";
            // 
            // outByte1
            // 
            this.outByte1.Enabled = false;
            this.outByte1.Location = new System.Drawing.Point(255, 154);
            this.outByte1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.outByte1.Name = "outByte1";
            this.outByte1.Size = new System.Drawing.Size(62, 20);
            this.outByte1.TabIndex = 10;
            this.outByte1.Value = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.outByte1.ValueChanged += new System.EventHandler(this.outByte1_ValueChanged);
            // 
            // outByte2
            // 
            this.outByte2.Enabled = false;
            this.outByte2.Location = new System.Drawing.Point(331, 154);
            this.outByte2.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.outByte2.Name = "outByte2";
            this.outByte2.Size = new System.Drawing.Size(65, 20);
            this.outByte2.TabIndex = 11;
            this.outByte2.Value = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.outByte2.ValueChanged += new System.EventHandler(this.outByte2_ValueChanged);
            // 
            // modeSelect_Box
            // 
            this.modeSelect_Box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.modeSelect_Box.FormattingEnabled = true;
            this.modeSelect_Box.Location = new System.Drawing.Point(172, 56);
            this.modeSelect_Box.Name = "modeSelect_Box";
            this.modeSelect_Box.Size = new System.Drawing.Size(121, 21);
            this.modeSelect_Box.TabIndex = 12;
            this.modeSelect_Box.SelectedIndexChanged += new System.EventHandler(this.modeSelect_Box_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(40, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(123, 16);
            this.label8.TabIndex = 13;
            this.label8.Text = "Operating Mode:";
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(12, 277);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(536, 10);
            this.label2.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(12, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(536, 10);
            this.label3.TabIndex = 15;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectionMessage});
            this.statusStrip1.Location = new System.Drawing.Point(0, 491);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(560, 22);
            this.statusStrip1.TabIndex = 16;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // connectionMessage
            // 
            this.connectionMessage.Name = "connectionMessage";
            this.connectionMessage.Size = new System.Drawing.Size(0, 17);
            // 
            // running_label
            // 
            this.running_label.AutoSize = true;
            this.running_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.running_label.ForeColor = System.Drawing.Color.Red;
            this.running_label.Location = new System.Drawing.Point(12, 107);
            this.running_label.Name = "running_label";
            this.running_label.Size = new System.Drawing.Size(67, 16);
            this.running_label.TabIndex = 17;
            this.running_label.Text = "Stopped";
            // 
            // state_label
            // 
            this.state_label.AutoSize = true;
            this.state_label.Location = new System.Drawing.Point(12, 137);
            this.state_label.Name = "state_label";
            this.state_label.Size = new System.Drawing.Size(38, 13);
            this.state_label.TabIndex = 18;
            this.state_label.Text = "State: ";
            // 
            // force_left_sensor
            // 
            this.force_left_sensor.AutoSize = true;
            this.force_left_sensor.Enabled = false;
            this.force_left_sensor.Location = new System.Drawing.Point(132, 133);
            this.force_left_sensor.Name = "force_left_sensor";
            this.force_left_sensor.Size = new System.Drawing.Size(32, 17);
            this.force_left_sensor.TabIndex = 19;
            this.force_left_sensor.Text = "L";
            this.force_left_sensor.UseVisualStyleBackColor = true;
            // 
            // force_right_sensor
            // 
            this.force_right_sensor.AutoSize = true;
            this.force_right_sensor.Enabled = false;
            this.force_right_sensor.Location = new System.Drawing.Point(170, 133);
            this.force_right_sensor.Name = "force_right_sensor";
            this.force_right_sensor.Size = new System.Drawing.Size(34, 17);
            this.force_right_sensor.TabIndex = 20;
            this.force_right_sensor.Text = "R";
            this.force_right_sensor.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(96, 110);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(119, 17);
            this.checkBox1.TabIndex = 21;
            this.checkBox1.Text = "Manual Override";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // sensor_readings_label
            // 
            this.sensor_readings_label.AutoSize = true;
            this.sensor_readings_label.Location = new System.Drawing.Point(143, 161);
            this.sensor_readings_label.Name = "sensor_readings_label";
            this.sensor_readings_label.Size = new System.Drawing.Size(33, 13);
            this.sensor_readings_label.TabIndex = 22;
            this.sensor_readings_label.Text = "L: R: ";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DecimalPlaces = 2;
            this.numericUpDown1.Location = new System.Drawing.Point(132, 188);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(60, 20);
            this.numericUpDown1.TabIndex = 23;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(99, 190);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(27, 13);
            this.label9.TabIndex = 24;
            this.label9.Text = "K_T";
            // 
            // adj_rate_label
            // 
            this.adj_rate_label.AutoSize = true;
            this.adj_rate_label.Location = new System.Drawing.Point(396, 218);
            this.adj_rate_label.Name = "adj_rate_label";
            this.adj_rate_label.Size = new System.Drawing.Size(88, 13);
            this.adj_rate_label.TabIndex = 25;
            this.adj_rate_label.Text = "Adjustment Rate:";
            // 
            // prev_adj_label
            // 
            this.prev_adj_label.AutoSize = true;
            this.prev_adj_label.Location = new System.Drawing.Point(252, 231);
            this.prev_adj_label.Name = "prev_adj_label";
            this.prev_adj_label.Size = new System.Drawing.Size(95, 13);
            this.prev_adj_label.TabIndex = 26;
            this.prev_adj_label.Text = "Prev Adj Direction:";
            // 
            // adj_dir_label
            // 
            this.adj_dir_label.AutoSize = true;
            this.adj_dir_label.Location = new System.Drawing.Point(252, 218);
            this.adj_dir_label.Name = "adj_dir_label";
            this.adj_dir_label.Size = new System.Drawing.Size(70, 13);
            this.adj_dir_label.TabIndex = 27;
            this.adj_dir_label.Text = "Adj Direction:";
            // 
            // deltaT_label
            // 
            this.deltaT_label.AutoSize = true;
            this.deltaT_label.Location = new System.Drawing.Point(251, 194);
            this.deltaT_label.Name = "deltaT_label";
            this.deltaT_label.Size = new System.Drawing.Size(40, 13);
            this.deltaT_label.TabIndex = 29;
            this.deltaT_label.Text = "deltaT:";
            // 
            // prev_adj_r_label
            // 
            this.prev_adj_r_label.AutoSize = true;
            this.prev_adj_r_label.Location = new System.Drawing.Point(396, 231);
            this.prev_adj_r_label.Name = "prev_adj_r_label";
            this.prev_adj_r_label.Size = new System.Drawing.Size(76, 13);
            this.prev_adj_r_label.TabIndex = 30;
            this.prev_adj_r_label.Text = "Prev Adj Rate:";
            // 
            // last_adj_ms_label
            // 
            this.last_adj_ms_label.AutoSize = true;
            this.last_adj_ms_label.Location = new System.Drawing.Point(328, 194);
            this.last_adj_ms_label.Name = "last_adj_ms_label";
            this.last_adj_ms_label.Size = new System.Drawing.Size(64, 13);
            this.last_adj_ms_label.TabIndex = 31;
            this.last_adj_ms_label.Text = "Last Adj ms:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(99, 218);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(23, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "I_T";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.DecimalPlaces = 2;
            this.numericUpDown2.Location = new System.Drawing.Point(132, 218);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(60, 20);
            this.numericUpDown2.TabIndex = 32;
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.DecimalPlaces = 2;
            this.numericUpDown3.Location = new System.Drawing.Point(132, 245);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(60, 20);
            this.numericUpDown3.TabIndex = 33;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(98, 245);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(28, 13);
            this.label11.TabIndex = 34;
            this.label11.Text = "D_T";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(458, 99);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 35;
            this.button1.Text = "Reset";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Location = new System.Drawing.Point(499, 418);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(34, 50);
            this.pictureBox1.TabIndex = 36;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Black;
            this.pictureBox2.Location = new System.Drawing.Point(406, 418);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(37, 50);
            this.pictureBox2.TabIndex = 37;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Pink;
            this.pictureBox3.Location = new System.Drawing.Point(433, 335);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(76, 110);
            this.pictureBox3.TabIndex = 38;
            this.pictureBox3.TabStop = false;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(96, 8);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(119, 21);
            this.comboBox1.TabIndex = 39;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(11, 13);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(79, 16);
            this.label12.TabIndex = 13;
            this.label12.Text = "Controller:";
            // 
            // label13
            // 
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label13.Location = new System.Drawing.Point(12, 43);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(536, 1);
            this.label13.TabIndex = 15;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 513);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.numericUpDown3);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.last_adj_ms_label);
            this.Controls.Add(this.prev_adj_r_label);
            this.Controls.Add(this.deltaT_label);
            this.Controls.Add(this.adj_dir_label);
            this.Controls.Add(this.prev_adj_label);
            this.Controls.Add(this.adj_rate_label);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.sensor_readings_label);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.force_right_sensor);
            this.Controls.Add(this.force_left_sensor);
            this.Controls.Add(this.state_label);
            this.Controls.Add(this.running_label);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.modeSelect_Box);
            this.Controls.Add(this.outByte2);
            this.Controls.Add(this.outByte1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OutputBox2);
            this.Controls.Add(this.OutputBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "Form1";
            this.Text = "Bambi GUI 1.0";
            ((System.ComponentModel.ISupportInitialize)(this.OutputBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OutputBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.outByte1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.outByte2)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer getIOtimer;
        private System.Windows.Forms.NumericUpDown OutputBox1;
        private System.IO.Ports.SerialPort serial;
        private System.Windows.Forms.NumericUpDown OutputBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Timer mainLoop;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown outByte1;
        private System.Windows.Forms.NumericUpDown outByte2;
        private System.Windows.Forms.ComboBox modeSelect_Box;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel connectionMessage;
        private System.Windows.Forms.Label running_label;
        private System.Windows.Forms.Label state_label;
        private System.Windows.Forms.CheckBox force_left_sensor;
        private System.Windows.Forms.CheckBox force_right_sensor;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label sensor_readings_label;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label adj_rate_label;
        private System.Windows.Forms.Label prev_adj_label;
        private System.Windows.Forms.Label adj_dir_label;
        private System.Windows.Forms.Label deltaT_label;
        private System.Windows.Forms.Label prev_adj_r_label;
        private System.Windows.Forms.Label last_adj_ms_label;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
    }
}

