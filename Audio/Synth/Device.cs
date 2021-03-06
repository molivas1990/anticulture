using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AntiCulture.Audio.Synth
{
    public abstract class Device
    {
        #region Fields
        private string mLabel;
        private List<InputSlot> mInputSlots = new List<InputSlot>();
        private List<OutputSlot> mOutputSlots = new List<OutputSlot>();
        #endregion

        #region Properties
        public abstract string Name { get; }

        public string Label
        {
            get
            {
                if (mLabel == null) mLabel = Name + " " + base.GetHashCode().ToString();
                return mLabel;
            }
            set { mLabel = value; }
        }

        public IEnumerable<InputSlot> InputSlots
        {
            get { return mInputSlots; }
        }

        public IEnumerable<OutputSlot> OutputSlots
        {
            get { return mOutputSlots; }
        }
        #endregion

        #region Methods
        public InputSlot FindInputSlot(string name)
        {
            foreach (InputSlot i in mInputSlots)
                if (i.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                    return i;
            return null;
        }

        public OutputSlot FindOutputSlot(string name)
        {
            foreach (OutputSlot i in mOutputSlots)
                if (i.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                    return i;
            return null;
        }

        protected void AddInputSlot(InputSlot slot)
        {
            mInputSlots.Add(slot);
        }

        protected void AddOutputSlot(OutputSlot slot)
        {
            mOutputSlots.Add(slot);
        }
        #endregion
    }
}
