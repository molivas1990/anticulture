using System;
using System.Collections.Generic;
using System.Text;
using AntiCulture.Audio;

namespace AntiCulture.Audio.Devices
{
    public class SimpleWaveGenerator : Device
    {
        #region WaveType enumeration
        public enum WaveType
        {
            Sine,
            Square,
            Saw,
            Triangle
        }
        #endregion

        #region Data members
        private OutputSlot mOutputSlot;
        #endregion

        #region Constructor
        public SimpleWaveGenerator()
            : base("Simple Wave Generator")
        {
            mOutputSlot = OutputSlot.Create<SampleBuffer>(this, "oOutput");
        }
        #endregion

        #region Properties
        public OutputSlot OutputSlot
        {
            get { return mOutputSlot; }
        }
        #endregion

        #region Methods
        public void Generate(WaveType type, uint sampleCount, uint frequency)
        {
            if (!mOutputSlot.IsConnected) return;

            SampleBuffer buffer = new SampleBuffer(sampleCount, 16, frequency);

            switch (type)
            {
                case WaveType.Sine:
                    for (int i = 0; i < buffer.Data.Length; i += 2)
                    {
                        short value = (short)(Math.Sin((double)i / 10.0) * 30000.0);
                        buffer.Data[i + 1] = (byte)(value & 0xFF);
                        buffer.Data[i] = (byte)(value >> 8);
                    }
                    break;

                case WaveType.Square:
                    for (int i = 0; i < buffer.Data.Length; i += 2)
                    {
                        if ((i % 100) < 50)
                        {
                            buffer.Data[i + 1] = 0x7F;
                            buffer.Data[i] = 0xFF;
                        }
                        else
                        {
                            buffer.Data[i + 1] = 0xFF;
                            buffer.Data[i] = 0xFF;
                        }
                    }
                    break;

                case WaveType.Saw:
                    for (int i = 0; i < buffer.Data.Length; i += 2)
                    {
                        short value = (short)((((double)i / 100.0) % 1.0) * 60000.0 - 30000.0);
                        buffer.Data[i + 1] = (byte)(value & 0xFF);
                        buffer.Data[i] = (byte)(value >> 8);
                    }
                    break;

                case WaveType.Triangle:
                    for (int i = 0; i < buffer.Data.Length; i += 2)
                    {
                        short value = (short)(Math.Abs((((double)i / 100.0) % 2.0) - 1.0) * 60000.0 - 30000.0);
                        buffer.Data[i + 1] = (byte)(value & 0xFF);
                        buffer.Data[i] = (byte)(value >> 8);
                    }
                    break;
            }

            mOutputSlot.Send(buffer);
        }
        #endregion

        #region Overriden properties
        public override IEnumerable<Slot> Slots
        {
            get
            {
                List<Slot> slots = new List<Slot>();
                slots.Add(mOutputSlot);
                return slots;
            }
        }
        #endregion
    }
}
