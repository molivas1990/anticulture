using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using AntiCulture.Audio.Synth;

namespace AntiCulture.Audio.Synth.Devices
{
    public class OpenAlOutput : Device
    {
        #region OpenAl subclass
        public static class OpenAl
        {
            #region Alut subclass
            public static class Alut
            {
                #region Functions
                [DllImport("alut.dll")]
                public static extern void alutInit(ref int argc, ref string argv);

                [DllImport("alut.dll")]
                public static extern void alutExit();
                #endregion

                #region Helpers
                public static void Init()
                {
                    int argc = 0;
                    string argv = null;
                    alutInit(ref argc, ref argv);
                }
                #endregion
            }
            #endregion

            #region Constants
            public const uint AL_FORMAT_MONO8 = 0x1100;
            public const uint AL_FORMAT_MONO16 = 0x1101;
            public const uint AL_FORMAT_STEREO8 = 0x1102;
            public const uint AL_FORMAT_STEREO16 = 0x1103;

            public const uint AL_BUFFERS_PROCESSED = 0x1016;
            #endregion

            #region Functions
            #region Buffer-related
            [DllImport("OpenAL32.dll")]
            public static extern void alGenBuffers(int count, out uint identifier);

            [DllImport("OpenAL32.dll")]
            public static extern void alDeleteBuffers(int count, ref uint identifier);

            [DllImport("OpenAL32.dll")]
            public static extern void alBufferi(uint identifier, uint target, int value);

            [DllImport("OpenAL32.dll")]
            public static extern void alBufferf(uint identifier, uint target, float value);

            [DllImport("OpenAL32.dll")]
            public static extern void alBuffer3f(uint identifier, uint target, float value1, float value2, float value3);

            [DllImport("OpenAL32.dll")]
            public static extern void alBufferData(uint identifier, uint format, byte[] data, uint size, uint frequency);
            #endregion

            #region Source-related
            [DllImport("OpenAL32.dll")]
            public static extern void alGenSources(int count, out uint identifier);

            [DllImport("OpenAL32.dll")]
            public static extern void alDeleteSources(int count, ref uint identifier);

            [DllImport("OpenAL32.dll")]
            public static extern void alSourcei(uint identifier, uint target, int value);

            [DllImport("OpenAL32.dll")]
            public static extern void alSourcef(uint identifier, uint target, float value);

            [DllImport("OpenAL32.dll")]
            public static extern void alSource3f(uint identifier, uint target, float value1, float value2, float value3);

            [DllImport("OpenAL32.dll")]
            public static extern void alGetSourcei(uint identifier, uint target, out int value);

            [DllImport("OpenAL32.dll")]
            public static extern void alSourcePlay(uint identifier);

            [DllImport("OpenAL32.dll")]
            public static extern void alSourcePause(uint identifier);

            [DllImport("OpenAL32.dll")]
            public static extern void alSourceStop(uint identifier);

            [DllImport("OpenAL32.dll")]
            public static extern void alSourceQueueBuffers(uint identifier, uint count, ref uint bufferIdentifiers);

            [DllImport("OpenAL32.dll")]
            public static extern void alSourceUnqueueBuffers(uint identifier, uint count, ref uint bufferIdentifiers);
            #endregion
            #endregion

            #region Helpers
            public static uint GenBuffer()
            {
                uint identifier = 0;
                alGenBuffers(1, out identifier);
                return identifier;
            }

            public static void DeleteBuffer(uint identifier)
            {
                alDeleteBuffers(1, ref identifier);
            }

            public static uint GenSource()
            {
                uint identifier = 0;
                alGenSources(1, out identifier);
                return identifier;
            }

            public static void DeleteSource(uint identifier)
            {
                alDeleteSources(1, ref identifier);
            }
            #endregion
        }
        #endregion

        #region Constants
        public const uint BufferCount = 3;
        public const uint BufferSize = 4096;
        #endregion

        #region Fields
        private InputSlot mLeftChannelInputSlot, mRightChannelInputSlot;
        private uint[] mBufferIdentifiers = new uint[BufferCount];
        private uint mSourceIdentifier = 0;
        private uint mCurrentBuffer = 0;
        #endregion

        #region Constructor
        public OpenAlOutput()
        {
            // Initialize slots
            mLeftChannelInputSlot = InputSlot.Create<SoundBuffer>(this, "LeftChannel");
            mLeftChannelInputSlot.DataReceived += new EventHandler<InputSlot.DataEventArgs>(DataReceived);
            mRightChannelInputSlot = InputSlot.Create<SoundBuffer>(this, "RightChannel");
            mRightChannelInputSlot.DataReceived += new EventHandler<InputSlot.DataEventArgs>(DataReceived);
            base.AddInputSlot(mLeftChannelInputSlot);
            base.AddInputSlot(mRightChannelInputSlot);

            // OpenAl stuff
            OpenAl.Alut.Init();

            for (int i = 0; i < mBufferIdentifiers.Length; ++i)
                mBufferIdentifiers[i] = OpenAl.GenBuffer();

            mSourceIdentifier = OpenAl.GenSource();
        }

        ~OpenAlOutput()
        {
            // FIXME : Destructors are unreliable in C#
            OpenAl.DeleteSource(mSourceIdentifier);

            foreach (uint bufferIdentifier in mBufferIdentifiers)
                OpenAl.DeleteBuffer(bufferIdentifier);

            OpenAl.Alut.alutExit();
        }
        #endregion

        #region Properties
        public InputSlot LeftChannelInputSlot
        {
            get { return mLeftChannelInputSlot; }
        }

        public InputSlot RightChannelInputSlot
        {
            get { return mRightChannelInputSlot; }
        }
        #endregion

        #region Private methods
        private void DataReceived(object sender, InputSlot.DataEventArgs e)
        {
            SoundBuffer buffer = (SoundBuffer)e.Data;

            uint format;
            if (buffer.ChannelCount == 1) format = OpenAl.AL_FORMAT_MONO16;
            else if (buffer.ChannelCount == 2) format = OpenAl.AL_FORMAT_STEREO16;
            else throw new ArgumentException("Only mono and stereo supported", "e");

            // Data must be converted from floats to signed shorts
            uint dataSize = buffer.DataSize;
            byte[] data = new byte[dataSize * 2];
            for (int i = 0; i < dataSize; ++i)
            {
                short sample = (short)(buffer.Data[(uint)i] * (float)short.MaxValue);
                data[i*2] = (byte)(sample >> 8);
                data[i*2 + 1] = (byte)(sample & 0xFF);
            }

            uint bufferIdentifier = mBufferIdentifiers[mCurrentBuffer];

            OpenAl.alBufferData(bufferIdentifier, format, data, (uint)data.Length, buffer.Frequency);
            OpenAl.alSourceQueueBuffers(mSourceIdentifier, 1, ref bufferIdentifier);
            OpenAl.alSourcePlay(mSourceIdentifier);

            mCurrentBuffer = (mCurrentBuffer + 1) % (uint)mBufferIdentifiers.Length;
        }
        #endregion

        #region Device members
        public override string Name
        {
            get { return "OpenAl Output"; }
        }
        #endregion
    }
}
