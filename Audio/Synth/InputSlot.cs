using System;
using System.Collections.Generic;
using System.Text;

namespace AntiCulture.Audio.Synth
{
    public class InputSlot : Slot
    {
        #region DataEventArgs subclass
        public class DataEventArgs : EventArgs
        {
            #region Data members
            private object mData;
            #endregion

            #region Constructor
            public DataEventArgs(object data)
            {
                mData = data;
            }
            #endregion

            #region Properties
            public object Data
            {
                get { return mData; }
            }
            #endregion
        }
        #endregion

        #region Events
        public event EventHandler<DataEventArgs> DataReceived;
        #endregion

        #region Constructor
        public InputSlot(Device owner, string name, Type dataType)
            : base(owner, name, dataType)
        {}

        public InputSlot(string name, Type dataType)
            : base(name, dataType)
        { }

        public InputSlot(string name)
            : base(name)
        { }

        public static InputSlot Create<T>(Device owner, string name)
        {
            return new InputSlot(owner, name, typeof(T));
        }

        public static InputSlot Create<T>(string name)
        {
            return new InputSlot(name, typeof(T));
        }
        #endregion

        #region Methods
        public override void Connect(Slot endPoint)
        {
            if (!(endPoint is OutputSlot)) throw new ArgumentException("Only output slots can be connected to input slots", "endPoint");
            base.Connect(endPoint);
        }

        public void Receive(object data)
        {
            if (HasDataTypeConstrain && data.GetType() != DataType) throw new ArgumentException("Invalid data type", "data");
            if (DataReceived != null) DataReceived(this, new DataEventArgs(data));
        }
        #endregion
    }
}
