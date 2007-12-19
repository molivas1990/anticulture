using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace AntiCulture.Audio
{
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
}
