using System;
using System.Collections.Generic;
using System.Text;

namespace AntiCulture.Audio
{
    public class SampleBuffer
    {
        private byte[] mData;
        private uint mBitsPerSample;
        private uint mFrequency;

        public SampleBuffer(uint dataSize, uint bitsPerSample, uint frequency)
        {
            mData = new byte[dataSize];
            mBitsPerSample = bitsPerSample;
            mFrequency = frequency;
        }

        public byte[] Data
        {
            get { return mData; }
        }

        public uint DataSize
        {
            get { return (uint)mData.Length; }
        }

        public uint BitsPerSample
        {
            get { return mBitsPerSample; }
        }

        public uint Frequency
        {
            get { return mFrequency; }
        }
    }
}
