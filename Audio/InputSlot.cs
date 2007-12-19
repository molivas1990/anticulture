using System;
using System.Collections.Generic;
using System.Text;

namespace AntiCulture.Audio
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
        private InputSlot(Device device, string name, Type dataType)
            : base(device, name, dataType)
        { }

        public static InputSlot Create<T>(Device device, string name)
        {
            return new InputSlot(device, name, typeof(T));
        }

        public static InputSlot Create<T>(string name)
        {
            return new InputSlot(null, name, typeof(T));
        }
        #endregion

        #region Methods
        public void Receive(object data)
        {
            if (data.GetType() != DataType) throw new ArgumentException("Invalid data type", "data");
            if (DataReceived != null) DataReceived(this, new DataEventArgs(data));
        }
        #endregion

        #region Overriden methods
        public override void Connect(Slot endPoint)
        {
            if (endPoint == null) throw new ArgumentNullException("endPoint");
            if (endPoint.GetType() != typeof(OutputSlot)) throw new ArgumentException("Must be an output slot", "endPoint");
            base.Connect(endPoint);
        }
        #endregion
    }
}
