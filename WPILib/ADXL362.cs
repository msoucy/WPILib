﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkTables.Tables;
using WPILib.Interfaces;
using WPILib.LiveWindows;

namespace WPILib
{
    /// <summary>
    /// ADXL362 SPI Accelerometer
    /// </summary>
    /// <remarks>
    /// This class allows access to an Analog Devices ADXL362 3-axis accelerometer.
    /// </remarks>
    public class ADXL362 : SensorBase, IAccelerometer, ILiveWindowSendable
    {
        public struct AllAxes
        {
            public double XAxis { get; private set; }
            public double YAxis { get; private set; }
            public double ZAxis { get; private set; }

            public AllAxes(double x, double y, double z)
            {
                XAxis = x;
                YAxis = y;
                ZAxis = z;
            }
        }
        

        private const byte RegWrite = 0x0A;
        private const byte RegRead = 0x0B;

        private const byte PartIdRegister = 0x02;
        private const byte DataRegister = 0x0E;
        private const byte FilterCtlRegister = 0x2C;
        private const byte PowerCtlRegister = 0x2D;

        private const byte FilterCtl_Range2G = 0x00;
        private const byte FilterCtl_Range4G = 0x40;
        private const byte FilterCtl_Range8G = (byte)0x80;
        private const byte FilterCtl_ODR_100Hz = 0x03;

        private const byte PowerCtl_UltraLowNoise = 0x20;
        private const byte PowerCtl_AutoSleep = 0x04;
        private const byte PowerCtl_Measure = 0x02;

        public enum Axes
        {
            X = 0x00,
            Y = 0x02,
            Z = 0x04
        }

        private SPI m_spi;
        private double m_gsPerLSB;

        public ADXL362(AccelerometerRange range) : this(SPI.Port.OnboardCS1, range)
        {
        }

        public ADXL362(SPI.Port port, AccelerometerRange range)
        {
            m_spi = new SPI(port);
            m_spi.SetClockRate(3000000);
            m_spi.SetMSBFirst();
            m_spi.SetSampleDataOnRising();
            m_spi.SetClockActiveHigh();
            m_spi.SetChipSelectActiveLow();

            byte[] commands = new byte[]
            {
                RegRead,
                PartIdRegister,
                0,
            };

            m_spi.Transaction(commands, commands, 3);

            if (commands[2] != 0xF2)
            {
                DriverStation.ReportError("Could not find ADXL362", false);
                m_gsPerLSB = 0.0;
                return;
            }

            AccelerometerRange = range;

            commands[0] = RegWrite;
            commands[1] = PowerCtlRegister;
            commands[2] = PowerCtlRegister | PowerCtl_UltraLowNoise;
            m_spi.Write(commands, 3);

            LiveWindow.AddSensor("ADXL362", port.ToString(), this);
        }

        private AccelerometerRange m_range;
        /// <inheritdoc/>
        public AccelerometerRange AccelerometerRange
        {
            get { return m_range; }
            set
            {
                if (m_gsPerLSB == 0.0) return;
                byte[] commands = new byte[3];

                switch (value)
                {
                    case AccelerometerRange.k2G:
                        m_gsPerLSB = 0.001;
                        break;
                    case AccelerometerRange.k4G:
                        m_gsPerLSB = 0.002;
                        break;
                    case AccelerometerRange.k8G:
                    case AccelerometerRange.k16G: //16G not supported, treat as 8G
                        m_gsPerLSB = 0.004;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(value), value, null);
                }

                commands[0] = RegWrite;
                commands[1] = FilterCtlRegister;
                commands[2] = (byte)(FilterCtl_ODR_100Hz | (((int)value & 0x03) << 6));

                m_spi.Write(commands, 3);
            }
        }

        /// <inheritdoc/>
        public double GetX() => GetAcceleration(Axes.X);

        /// <inheritdoc/>
        public double GetY() => GetAcceleration(Axes.Y);

        /// <inheritdoc/>
        public double GetZ() => GetAcceleration(Axes.Z);

        /// <summary>
        /// Get the acceleration of one axis in Gs.
        /// </summary>
        /// <param name="axis">The axis to read from</param>
        /// <returns>Acceleration of the ADXL362 in Gs.</returns>
        public double GetAcceleration(Axes axis)
        {
            if (m_gsPerLSB == 0.0) return 0.0;

            byte[] buffer = new byte[4];
            byte[] command = new byte[] {0, 0, 0, 0};
            command[0] = RegRead;
            command[1] = (byte)(DataRegister + (byte) axis);
            m_spi.Transaction(command, buffer, 4);

            int rawAccel = buffer[3] << 8 | buffer[2];
            return rawAccel * m_gsPerLSB;
        }

        public AllAxes GetAccelerations()
        {
            if (m_gsPerLSB == 0.0)
            {
                return new AllAxes(0,0,0);
            }

            byte[] dataBuffer = new byte[8];

            int[] rawData = new int[3];

            dataBuffer[0] = RegRead;
            dataBuffer[1] = DataRegister;
            m_spi.Transaction(dataBuffer, dataBuffer, 8);

            for (int i = 0; i < 3; i++)
            {
                rawData[i] = dataBuffer[i * 2 + 3] << 8 | dataBuffer[i * 2 + 2];
            }

            return new AllAxes(rawData[0] * m_gsPerLSB, rawData[1] * m_gsPerLSB, rawData[2] * m_gsPerLSB);
        }

        /// <inheritdoc/>
        public void InitTable(ITable subtable)
        {
            Table = subtable;
            UpdateTable();
        }

        /// <inheritdoc/>
        public ITable Table { get; private set; }

        /// <inheritdoc/>
        public string SmartDashboardType => "3AxisAcceleromter";

        /// <inheritdoc/>
        public void UpdateTable()
        {
            if (Table != null)
            {
                AllAxes axes = GetAccelerations();
                Table.PutNumber("X", axes.XAxis);
                Table.PutNumber("Y", axes.YAxis);
                Table.PutNumber("Z", axes.ZAxis);
            }
        }

        /// <inheritdoc/>
        public void StartLiveWindowMode()
        {
        }

        /// <inheritdoc/>
        public void StopLiveWindowMode()
        {
        }
    }
}