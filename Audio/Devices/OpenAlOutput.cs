using System;
using System.Collections.Generic;
using System.Text;
using AntiCulture.Audio;

namespace AntiCulture.Audio.Devices
{
    public class OpenAlOutput : Device
    {
        #region Constants
        public const uint BufferCount = 3;
        public const uint BufferSize = 4096;
        #endregion

        #region Data members
        private List<Slot> mInputSlots = new List<Slot>();
        private uint[] mBufferIdentifiers = new uint[BufferCount];
        private uint mSourceIdentifier = 0;
        private uint mCurrentBuffer = 0;
        #endregion

        #region Constructor
        public OpenAlOutput()
            : base("OpenAl Output")
        {
            // Initialize slots
            InputSlot slot;

            slot = InputSlot.Create<SampleBuffer>(this, "iLeftChannel");
            slot.DataReceived +=new EventHandler<InputSlot.DataEventArgs>(DataReceived);
            mInputSlots.Add(slot);

            slot = InputSlot.Create<SampleBuffer>(this, "iRightChannel");
            slot.DataReceived += new EventHandler<InputSlot.DataEventArgs>(DataReceived);
            mInputSlots.Add(slot);

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
            get { return (InputSlot)mInputSlots[0]; }
        }

        public InputSlot RightChannelInputSlot
        {
            get { return (InputSlot)mInputSlots[1]; }
        }
        #endregion

        #region Private methods
        private void DataReceived(object sender, InputSlot.DataEventArgs e)
        {
            InputSlot slot = sender as InputSlot;
            
            uint bufferIdentifier = mBufferIdentifiers[mCurrentBuffer];

            SampleBuffer samples = (SampleBuffer)e.Data;

            uint format;
            if (samples.BitsPerSample == 8) format = OpenAl.AL_FORMAT_MONO8;
            else if (samples.BitsPerSample == 16) format = OpenAl.AL_FORMAT_MONO16;
            else throw new ArgumentException("Buffer bits per sample ain't 8 or 16", "e");

            OpenAl.alBufferData(bufferIdentifier, format, samples.Data, samples.DataSize, samples.Frequency);
            OpenAl.alSourceQueueBuffers(mSourceIdentifier, 1, ref bufferIdentifier);
            OpenAl.alSourcePlay(mSourceIdentifier);

            mCurrentBuffer = (mCurrentBuffer + 1) % (uint)mBufferIdentifiers.Length;
        }
        #endregion

        #region Overriden methods
        public override IEnumerable<Slot> Slots
        {
            get { return (IEnumerable<Slot>)mInputSlots; }
        }
        #endregion
    }
}
