using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BluetoothGUISample
{
    public static class Params
    {
        // sensor position on the board
        public const byte SENSOR1_POS = 0;  // position of sensor 1 within the INPUT BYTE
        public const byte SENSOR2_POS = 1;  // position of sensor 2 within the INPUT BYTE

        public const int LEFT = -1;
        public const int RIGHT = 1;

        public const int LINE = -1;
        public const int NO_LINE = 1;

        // duty_cycle_out = (b_in - K1) / K2; - bit to duty cycle
        // byte_out =  K2 * d_in + K1; - duty cycle to bit
        public const double K1 = 1.7179, K2 = 2.3623; //       

        public const double DAC_MIN = 0;
        public const double DAC1_MAX = 8.0; // DAC 1 max voltage
        public const double DAC2_MAX = 8.0; // dac 2 max voltage

        

    }

    // code graveyard
    /* 
        private void sendLoopTimer_Tick(object sender, EventArgs e)
        {
            // bit mask the Input byte. bit 0 is left sensor. bit 1 is right sensor.

            int left_sensor = ((1 << SENSOR1_POS) & Input1);
            int right_sensor = ((1 << SENSOR2_POS) & Input1);

            

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

            
             //* Tracking works like this.
             //* Control algorithm aims to keep the inside sensor on top of the line.
             //* When deviated: Based on the other sensor's reading it can
             //* be determined in what direction the robot is going and how
             //* its direction needs to be adjusted.
             //* 

    // if it's positive, it becomes negative - 
    // if it's 0 it becomes positive
    // matches lecture logic.

    // 
    int l_1 = left_sensor;
    int r_1 = right_sensor;



    error = (l_1 + r_1) - 1; // set error to be current position - half of max position.

            if (left_sensor == 1) left_sensor = LINE;
            if (right_sensor == 1) right_sensor = LINE;
            if (left_sensor == 0) left_sensor = NO_LINE;
            if (right_sensor == 0) right_sensor = NO_LINE;


            if (is_running)
            {
                switch (operation_mode)
                {
                    case 0: // Clockwise: Right sensor on the inside of track: right sensor stays on line
                        // left motor speed slightly higher than right motor speed to force it to slowly
                        // turn clockwise towards the right
                        L_MAX = 0.8;
                        R_MAX = 0.7;

                        if (right_sensor == LINE && left_sensor == NO_LINE) // if inner sensor is on line and outer one isn't
                        {
                            // keep direction, leave left and right motors unchanged
                            // vehicle is on the line
                            left_motor = (int)(255 * L_MAX);
                            right_motor = (int)(255 * R_MAX);

                        }
                        else
                        {
                            // find difference between last adjustment and current time
                            deltaT_adjustment = millis() - last_adjustment_ms;
                            if (left_sensor == LINE) // if the left sensor is detecting a line
                            { 
                                adjustment_direction = LEFT; // adjustment direction is towards the left
                                if (previous_adjustment_direction == adjustment_direction)
                                {
                                    adjustment_rate = I_T* adjustment_rate +  K_T / deltaT_adjustment + P_T*(right_sensor* left_sensor);
                                }
                                else
                                {
                                    adjustment_rate = K_T / deltaT_adjustment;
                                }
                            }
                            else 
                            {
                                adjustment_direction = RIGHT; // adjust towards right
                                if (previous_adjustment_direction == adjustment_direction) // adjust more if it's been adjusting for a while
                                {
                                    adjustment_rate = I_T* previous_adjustment_rate + K_T / deltaT_adjustment + P_T* (right_sensor* left_sensor); 
                                }
                                else
                                { // set adjustment rate just based on time
                                    adjustment_rate = K_T / deltaT_adjustment;
                                }
                            }
                           
                            last_adjustment_ms = millis();
previous_adjustment_direction = adjustment_direction;
                            previous_adjustment_rate = adjustment_rate;

                            left_motor = (int)(L_MAX* (left_motor - adjustment_direction* adjustment_rate)); 
                            right_motor = (int)(R_MAX* (right_motor + adjustment_direction* adjustment_rate));

                            if (left_motor< 120) left_motor = 120;
                            if (right_motor< 120) right_motor = 120;

                        }
                        
                        
                        break;
                    case 1: // CCW: Left sensor on the inside of track, keep line under that
                        L_MAX = 0.7;
                        R_MAX = 0.8;

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
                adj_dir_label.Text = "Adj direction: " + ((adjustment_direction == LEFT) ? "LEFT" : "RIGHT");
                adj_rate_label.Text = "Adj rate: " + adjustment_rate.ToString();
                prev_adj_label.Text = "Prev adj dir: " + ((previous_adjustment_direction == LEFT) ? "LEFT" : "RIGHT");
                prev_adj_r_label.Text = "Prev adj rate:" + previous_adjustment_rate.ToString();

                // line tracking


                // store the time since a line was last found. 0 to 15 if it was previously read 
                // accuracy limited by DateTime to 15 
                // if (left_sensor == -1) deltaT_left = DateTime.Now.Millisecond - 
                // if (right_sensor == -1) deltaT_right = DateTime.Now.Millisecond - deltaT_right;

                // ensure proper byte range
                if (left_motor > 255) left_motor = (int)(L_MAX* 255); 
                if (right_motor > 255) right_motor = (int)(R_MAX* 255); 
                if (left_motor< 0) left_motor = 0;
                if (right_motor< 0) right_motor = 0;

                outByte1.Value = left_motor;
                outByte2.Value = right_motor;

                sendIO(2, (byte)left_motor);
                sendIO(3, (byte)right_motor);

            }
            // read sensors
            sendIO(0, ZERO);  // The value 0 indicates Input 1, ZERO just maintains a fixed value for the discarded data in order to maintain a consistent package format

        } */
}
