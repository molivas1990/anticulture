using System;
using System.Collections.Generic;
using System.Text;
using AntiCulture.Audio.Synth;

namespace AntiCulture.Audio.Synth.Devices
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
            mOutputSlot = OutputSlot.Create<SoundBuffer>(this, "oOutput");
            base.AddOutputSlot(mOutputSlot);
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

            SoundBuffer buffer = new SoundBuffer(1, sampleCount, frequency);

            switch (type)
            {
                case WaveType.Sine:
                    for (int i = 0; i < buffer.Data.Length; ++i)
                        buffer.Data[i] = (float)Math.Sin((double)i / 100.0) * 0.5f;
                    break;

                case WaveType.Square:
                    for (int i = 0; i < buffer.SampleCount; ++i)
                    {
                        if ((i % 100) < 50)
                            buffer.Data[i] = 0.5f;
                        else
                            buffer.Data[i] = -0.5f;
                    }
                    break;

                case WaveType.Saw:
                    for (int i = 0; i < buffer.Data.Length; ++i)
                        buffer.Data[i] = ((float)i / 100.0f) % 1.0f;
                    break;

                case WaveType.Triangle:
                    for (int i = 0; i < buffer.Data.Length; ++i)
                        buffer.Data[i] = (float)Math.Abs((((double)i / 100.0) % 2.0) - 1.0);
                    break;
            }

            mOutputSlot.Send(buffer);
        }
        #endregion
    }
}
