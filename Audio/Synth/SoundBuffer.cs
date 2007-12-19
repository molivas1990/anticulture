using System;
using System.Collections.Generic;
using System.Text;

namespace AntiCulture.Audio.Synth
{
    /// <summary>
    /// Represents a buffer of an arbitrary number of channels which stores samples
    /// in float format, ranging from -1.0f to 1.0f
    /// </summary>
    public class SoundBuffer
    {
        #region Fields
        // Interleaved data
        private float[] mData;
        private uint mChannelCount;
        private uint mSampleCount;
        private uint mFrequency;
        #endregion

        #region Constructor
        public SoundBuffer(uint channelCount, uint sampleCount, uint frequency)
        {
            mData = new float[channelCount * sampleCount];
            mChannelCount = channelCount;
            mSampleCount = sampleCount;
            mFrequency = frequency;
        }
        #endregion

        #region Properties
        public uint ChannelCount
        {
            get { return mChannelCount; }
        }

        public uint SampleCount
        {
            get { return mSampleCount; }
        }

        public float[] Data
        {
            get { return mData; }
        }

        public uint DataSize
        {
            get { return (uint)mData.Length; }
        }

        public uint Frequency
        {
            get { return mFrequency; }
        }

        public float this[uint channel, uint sample]
        {
            get { return mData[sample * mChannelCount + channel]; }
            set { mData[sample * mChannelCount + channel] = value; }
        }
        #endregion
    }
}
