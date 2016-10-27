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

}
