﻿
using System;
using System.Runtime.InteropServices;
using System.Reflection;

namespace HAL_FRC
{
    public enum Mode
    {
        /// kTwoPulse -> 0
        TwoPulse = 0,

        /// kSemiperiod -> 1
        Semiperiod = 1,

        /// kPulseLength -> 2
        PulseLength = 2,

        /// kExternalDirection -> 3
        ExternalDirection = 3,
    }

    public class HALDigital
    {
        /*
         * 
         * Reflection stuff. I know that this works. It will just require a major project refactoring to implement. But this will be a much better way, and will 
         * work with the simulator much better, and not require the import hacks reuqired currently.
        private delegate IntPtr RetIntPtrPerIntPtrStatus(IntPtr intptr, ref int status);


        private static RetIntPtrPerIntPtrStatus initDigitalPort;


        internal static void SetupHAL(Assembly assembly)
        {
            Type type;

            type = assembly.GetType("HAL_FRC.HALDigital");

            initDigitalPort = (RetIntPtrPerIntPtrStatus)Delegate.CreateDelegate(typeof(RetIntPtrPerIntPtrStatus), type.GetMethod("initializeDigitalPort"));
        }

        public static IntPtr InitializeDigitalPort(IntPtr portPointer, ref int status)
        {
            return initDigitalPort(portPointer, ref status);
        }
         * */
         


        /// Return Type: void*
        ///port_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "initializeDigitalPort")]
        public static extern System.IntPtr initializeDigitalPort(System.IntPtr portPointer, ref int status);


        /// Return Type: boolean
        ///digital_port_pointer: void*
        [DllImport("libHALAthena_shared.so", EntryPoint = "checkPWMChannel")]
        [return: MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool checkPWMChannel(System.IntPtr digitalPortPointer);


        /// Return Type: boolean
        ///digital_port_pointer: void*
        [DllImport("libHALAthena_shared.so", EntryPoint = "checkRelayChannel")]
        [return: MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool checkRelayChannel(System.IntPtr digitalPortPointer);


        /// Return Type: void
        ///digital_port_pointer: void*
        ///value: unsigned short
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setPWM")]
        public static extern void setPWM(System.IntPtr digitalPortPointer, ushort value, ref int status);


        /// Return Type: boolean
        ///digital_port_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "allocatePWMChannel")]
        [return: MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool allocatePWMChannel(System.IntPtr digitalPortPointer, ref int status);


        /// Return Type: void
        ///digital_port_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "freePWMChannel")]
        public static extern void freePWMChannel(System.IntPtr digitalPortPointer, ref int status);


        /// Return Type: unsigned short
        ///digital_port_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getPWM")]
        public static extern ushort getPWM(System.IntPtr digitalPortPointer, ref int status);


        /// Return Type: void
        ///digital_port_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "latchPWMZero")]
        public static extern void latchPWMZero(System.IntPtr digitalPortPointer, ref int status);


        /// Return Type: void
        ///digital_port_pointer: void*
        ///squelchMask: unsigned int
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setPWMPeriodScale")]
        public static extern void setPWMPeriodScale(System.IntPtr digitalPortPointer, uint squelchMask, ref int status);


        /// Return Type: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "allocatePWM")]
        public static extern System.IntPtr allocatePWM(ref int status);


        /// Return Type: void
        ///pwmGenerator: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "freePWM")]
        public static extern void freePWM(System.IntPtr pwmGenerator, ref int status);


        /// Return Type: void
        ///rate: double
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setPWMRate")]
        public static extern void setPWMRate(double rate, ref int status);


        /// Return Type: void
        ///pwmGenerator: void*
        ///dutyCycle: double
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setPWMDutyCycle")]
        public static extern void setPWMDutyCycle(System.IntPtr pwmGenerator, double dutyCycle, ref int status);


        /// Return Type: void
        ///pwmGenerator: void*
        ///pin: unsigned int
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setPWMOutputChannel")]
        public static extern void setPWMOutputChannel(System.IntPtr pwmGenerator, uint pin, ref int status);


        /// Return Type: void
        ///digital_port_pointer: void*
        ///on: boolean
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setRelayForward")]
        public static extern void setRelayForward(System.IntPtr digitalPortPointer, [MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool on, ref int status);


        /// Return Type: void
        ///digital_port_pointer: void*
        ///on: boolean
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setRelayReverse")]
        public static extern void setRelayReverse(System.IntPtr digitalPortPointer, [MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool on, ref int status);


        /// Return Type: boolean
        ///digital_port_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getRelayForward")]
        [return: MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool getRelayForward(System.IntPtr digitalPortPointer, ref int status);


        /// Return Type: boolean
        ///digital_port_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getRelayReverse")]
        [return: MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool getRelayReverse(System.IntPtr digitalPortPointer, ref int status);


        /// Return Type: boolean
        ///digital_port_pointer: void*
        ///input: boolean
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "allocateDIO")]
        [return: MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool allocateDIO(System.IntPtr digitalPortPointer, [MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool input, ref int status);


        /// Return Type: void
        ///digital_port_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "freeDIO")]
        public static extern void freeDIO(System.IntPtr digitalPortPointer, ref int status);


        /// Return Type: void
        ///digital_port_pointer: void*
        ///value: short
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setDIO")]
        public static extern void setDIO(System.IntPtr digitalPortPointer, short value, ref int status);


        /// Return Type: boolean
        ///digital_port_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getDIO")]
        [return: MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool getDIO(System.IntPtr digitalPortPointer, ref int status);


        /// Return Type: boolean
        ///digital_port_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getDIODirection")]
        [return: MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool getDIODirection(System.IntPtr digitalPortPointer, ref int status);


        /// Return Type: void
        ///digital_port_pointer: void*
        ///pulseLength: double
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "pulse")]
        public static extern void pulse(System.IntPtr digitalPortPointer, double pulseLength, ref int status);


        /// Return Type: boolean
        ///digital_port_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "isPulsing")]
        [return: MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool isPulsing(System.IntPtr digitalPortPointer, ref int status);


        /// Return Type: boolean
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "isAnyPulsing")]
        [return: MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool isAnyPulsing(ref int status);


        /// Return Type: void*
        ///mode: Mode
        ///index: unsigned int*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "initializeCounter")]
        public static extern System.IntPtr initializeCounter(Mode mode, ref uint index, ref int status);


        /// Return Type: void
        ///counter_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "freeCounter")]
        public static extern void freeCounter(System.IntPtr counterPointer, ref int status);


        /// Return Type: void
        ///counter_pointer: void*
        ///size: int
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setCounterAverageSize")]
        public static extern void setCounterAverageSize(System.IntPtr counterPointer, int size, ref int status);


        /// Return Type: void
        ///counter_pointer: void*
        ///pin: unsigned int
        ///analogTrigger: boolean
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setCounterUpSource")]
        public static extern void setCounterUpSource(System.IntPtr counterPointer, uint pin, [MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool analogTrigger, ref int status);


        /// Return Type: void
        ///counter_pointer: void*
        ///risingEdge: boolean
        ///fallingEdge: boolean
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setCounterUpSourceEdge")]
        public static extern void setCounterUpSourceEdge(System.IntPtr counterPointer, [MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool risingEdge, [MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool fallingEdge, ref int status);


        /// Return Type: void
        ///counter_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "clearCounterUpSource")]
        public static extern void clearCounterUpSource(System.IntPtr counterPointer, ref int status);


        /// Return Type: void
        ///counter_pointer: void*
        ///pin: unsigned int
        ///analogTrigger: boolean
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setCounterDownSource")]
        public static extern void setCounterDownSource(System.IntPtr counterPointer, uint pin, [MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool analogTrigger, ref int status);


        /// Return Type: void
        ///counter_pointer: void*
        ///risingEdge: boolean
        ///fallingEdge: boolean
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setCounterDownSourceEdge")]
        public static extern void setCounterDownSourceEdge(System.IntPtr counterPointer, [MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool risingEdge, [MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool fallingEdge, ref int status);


        /// Return Type: void
        ///counter_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "clearCounterDownSource")]
        public static extern void clearCounterDownSource(System.IntPtr counterPointer, ref int status);


        /// Return Type: void
        ///counter_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setCounterUpDownMode")]
        public static extern void setCounterUpDownMode(System.IntPtr counterPointer, ref int status);


        /// Return Type: void
        ///counter_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setCounterExternalDirectionMode")]
        public static extern void setCounterExternalDirectionMode(System.IntPtr counterPointer, ref int status);


        /// Return Type: void
        ///counter_pointer: void*
        ///highSemiPeriod: boolean
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setCounterSemiPeriodMode")]
        public static extern void setCounterSemiPeriodMode(System.IntPtr counterPointer, [MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool highSemiPeriod, ref int status);


        /// Return Type: void
        ///counter_pointer: void*
        ///threshold: double
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setCounterPulseLengthMode")]
        public static extern void setCounterPulseLengthMode(System.IntPtr counterPointer, double threshold, ref int status);


        /// Return Type: int
        ///counter_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getCounterSamplesToAverage")]
        public static extern int getCounterSamplesToAverage(System.IntPtr counterPointer, ref int status);


        /// Return Type: void
        ///counter_pointer: void*
        ///samplesToAverage: int
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setCounterSamplesToAverage")]
        public static extern void setCounterSamplesToAverage(System.IntPtr counterPointer, int samplesToAverage, ref int status);


        /// Return Type: void
        ///counter_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "resetCounter")]
        public static extern void resetCounter(System.IntPtr counterPointer, ref int status);


        /// Return Type: int
        ///counter_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getCounter")]
        public static extern int getCounter(System.IntPtr counterPointer, ref int status);


        /// Return Type: double
        ///counter_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getCounterPeriod")]
        public static extern double getCounterPeriod(System.IntPtr counterPointer, ref int status);


        /// Return Type: void
        ///counter_pointer: void*
        ///maxPeriod: double
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setCounterMaxPeriod")]
        public static extern void setCounterMaxPeriod(System.IntPtr counterPointer, double maxPeriod, ref int status);


        /// Return Type: void
        ///counter_pointer: void*
        ///enabled: boolean
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setCounterUpdateWhenEmpty")]
        public static extern void setCounterUpdateWhenEmpty(System.IntPtr counterPointer, [MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool enabled, ref int status);


        /// Return Type: boolean
        ///counter_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getCounterStopped")]
        [return: MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool getCounterStopped(System.IntPtr counterPointer, ref int status);


        /// Return Type: boolean
        ///counter_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getCounterDirection")]
        [return: MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool getCounterDirection(System.IntPtr counterPointer, ref int status);


        /// Return Type: void
        ///counter_pointer: void*
        ///reverseDirection: boolean
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setCounterReverseDirection")]
        public static extern void setCounterReverseDirection(System.IntPtr counterPointer, [MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool reverseDirection, ref int status);


        /// Return Type: void*
        ///port_a_module: byte
        ///port_a_pin: unsigned int
        ///port_a_analog_trigger: boolean
        ///port_b_module: byte
        ///port_b_pin: unsigned int
        ///port_b_analog_trigger: boolean
        ///reverseDirection: boolean
        ///index: int*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "initializeEncoder")]
        public static extern System.IntPtr initializeEncoder(byte portAModule, uint portAPin, [MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool portAAnalogTrigger, byte portBModule, uint portBPin, [MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool portBAnalogTrigger, [MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool reverseDirection, ref int index, ref int status);


        /// Return Type: void
        ///encoder_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "freeEncoder")]
        public static extern void freeEncoder(System.IntPtr encoderPointer, ref int status);


        /// Return Type: void
        ///encoder_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "resetEncoder")]
        public static extern void resetEncoder(System.IntPtr encoderPointer, ref int status);


        /// Return Type: int
        ///encoder_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getEncoder")]
        public static extern int getEncoder(System.IntPtr encoderPointer, ref int status);


        /// Return Type: double
        ///encoder_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getEncoderPeriod")]
        public static extern double getEncoderPeriod(System.IntPtr encoderPointer, ref int status);


        /// Return Type: void
        ///encoder_pointer: void*
        ///maxPeriod: double
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setEncoderMaxPeriod")]
        public static extern void setEncoderMaxPeriod(System.IntPtr encoderPointer, double maxPeriod, ref int status);


        /// Return Type: boolean
        ///encoder_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getEncoderStopped")]
        [return: MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool getEncoderStopped(System.IntPtr encoderPointer, ref int status);


        /// Return Type: boolean
        ///encoder_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getEncoderDirection")]
        [return: MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool getEncoderDirection(System.IntPtr encoderPointer, ref int status);


        /// Return Type: void
        ///encoder_pointer: void*
        ///reverseDirection: boolean
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setEncoderReverseDirection")]
        public static extern void setEncoderReverseDirection(System.IntPtr encoderPointer, [MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool reverseDirection, ref int status);


        /// Return Type: void
        ///encoder_pointer: void*
        ///samplesToAverage: unsigned int
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setEncoderSamplesToAverage")]
        public static extern void setEncoderSamplesToAverage(System.IntPtr encoderPointer, uint samplesToAverage, ref int status);


        /// Return Type: unsigned int
        ///encoder_pointer: void*
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getEncoderSamplesToAverage")]
        public static extern uint getEncoderSamplesToAverage(System.IntPtr encoderPointer, ref int status);


        /// Return Type: void
        ///encoder_pointer: void*
        ///pin: unsigned int
        ///analogTrigger: boolean
        ///activeHigh: boolean
        ///edgeSensitive: boolean
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setEncoderIndexSource")]
        public static extern void setEncoderIndexSource(System.IntPtr encoderPointer, uint pin, [MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool analogTrigger, [MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool activeHigh, [MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool edgeSensitive, ref int status);


        /// Return Type: unsigned short
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "getLoopTiming")]
        public static extern ushort getLoopTiming(ref int status);


        /// Return Type: void
        ///port: byte
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "spiInitialize")]
        public static extern void spiInitialize(byte port, ref int status);


        /// Return Type: int
        ///port: byte
        ///dataToSend: byte*
        ///dataReceived: byte*
        ///size: byte
        [DllImport("libHALAthena_shared.so", EntryPoint = "spiTransaction")]
        public static extern int spiTransaction(byte port, ref byte dataToSend, ref byte dataReceived, byte size);


        /// Return Type: int
        ///port: byte
        ///dataToSend: byte*
        ///sendSize: byte
        [DllImport("libHALAthena_shared.so", EntryPoint = "spiWrite")]
        public static extern int spiWrite(byte port, ref byte dataToSend, byte sendSize);


        /// Return Type: int
        ///port: byte
        ///buffer: byte*
        ///count: byte
        [DllImport("libHALAthena_shared.so", EntryPoint = "spiRead")]
        public static extern int spiRead(byte port, ref byte buffer, byte count);


        /// Return Type: void
        ///port: byte
        [DllImport("libHALAthena_shared.so", EntryPoint = "spiClose")]
        public static extern void spiClose(byte port);


        /// Return Type: void
        ///port: byte
        ///speed: unsigned int
        [DllImport("libHALAthena_shared.so", EntryPoint = "spiSetSpeed")]
        public static extern void spiSetSpeed(byte port, uint speed);


        /// Return Type: void
        ///port: byte
        ///bpw: byte
        [DllImport("libHALAthena_shared.so", EntryPoint = "spiSetBitsPerWord")]
        public static extern void spiSetBitsPerWord(byte port, byte bpw);


        /// Return Type: void
        ///port: byte
        ///msb_first: int
        ///sample_on_trailing: int
        ///clk_idle_high: int
        [DllImport("libHALAthena_shared.so", EntryPoint = "spiSetOpts")]
        public static extern void spiSetOpts(byte port, int msbFirst, int sampleOnTrailing, int clkIdleHigh);


        /// Return Type: void
        ///port: byte
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "spiSetChipSelectActiveHigh")]
        public static extern void spiSetChipSelectActiveHigh(byte port, ref int status);


        /// Return Type: void
        ///port: byte
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "spiSetChipSelectActiveLow")]
        public static extern void spiSetChipSelectActiveLow(byte port, ref int status);


        /// Return Type: int
        ///port: byte
        [DllImport("libHALAthena_shared.so", EntryPoint = "spiGetHandle")]
        public static extern int spiGetHandle(byte port);


        /// Return Type: void
        ///port: byte
        ///handle: int
        [DllImport("libHALAthena_shared.so", EntryPoint = "spiSetHandle")]
        public static extern void spiSetHandle(byte port, int handle);

        //Actually returns MUTEX_ID
        /// Return Type: int
        ///port: byte
        [DllImport("libHALAthena_shared.so", EntryPoint = "spiGetSemaphore")]
        public static extern System.IntPtr spiGetSemaphore(byte port);


        //Actually takes MUTEX_ID
        /// Return Type: void
        ///port: byte
        ///semaphore: int
        [DllImport("libHALAthena_shared.so", EntryPoint = "spiSetSemaphore")]
        public static extern void spiSetSemaphore(byte port, System.IntPtr semaphore);


        /// Return Type: void
        ///port: byte
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "i2CInitialize")]
        public static extern void i2CInitialize(byte port, ref int status);


        /// Return Type: int
        ///port: byte
        ///deviceAddress: byte
        ///dataToSend: byte*
        ///sendSize: byte
        ///dataReceived: byte*
        ///receiveSize: byte
        [DllImport("libHALAthena_shared.so", EntryPoint = "i2CTransaction")]
        public static extern int i2CTransaction(byte port, byte deviceAddress, ref byte dataToSend, byte sendSize, ref byte dataReceived, byte receiveSize);


        /// Return Type: int
        ///port: byte
        ///deviceAddress: byte
        ///dataToSend: byte*
        ///sendSize: byte
        [DllImport("libHALAthena_shared.so", EntryPoint = "i2CWrite")]
        public static extern int i2CWrite(byte port, byte deviceAddress, ref byte dataToSend, byte sendSize);


        /// Return Type: int
        ///port: byte
        ///deviceAddress: byte
        ///buffer: byte*
        ///count: byte
        [DllImport("libHALAthena_shared.so", EntryPoint = "i2CRead")]
        public static extern int i2CRead(byte port, byte deviceAddress, ref byte buffer, byte count);


        /// Return Type: void
        ///port: byte
        [DllImport("libHALAthena_shared.so", EntryPoint = "i2CClose")]
        public static extern void i2CClose(byte port);


        /// Return Type: void
        ///rate: int
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setPWMRateIntHack")]
        public static extern void setPWMRateIntHack(int rate, ref int status);


        /// Return Type: void
        ///pwmGenerator: void*
        ///dutyCycle: int
        ///status: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "setPWMDutyCycleIntHack")]
        public static extern void setPWMDutyCycleIntHack(System.IntPtr pwmGenerator, int dutyCycle, ref int status);
    }
}
