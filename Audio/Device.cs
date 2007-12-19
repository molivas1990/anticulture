using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AntiCulture.Audio
{
    public abstract class Device
    {
        #region Data members
        private string mName;
        private string mLabel;
        #endregion

        #region Constructor
        public Device(string name)
        {
            mName = name;
        }
        #endregion

        #region Properties
        public string Name
        {
            get { return mName; }
        }

        public string Label
        {
            get { return mLabel; }
            set { mLabel = value; }
        }

        public abstract IEnumerable<Slot> Slots { get; }
        #endregion

        #region Methods
        public virtual Slot FindSlot(string name)
        {
            foreach (Slot i in Slots)
                if (i.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                    return i;
            return null;
        }
        #endregion
    }
}
