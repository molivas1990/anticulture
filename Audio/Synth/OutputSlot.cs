using System;
using System.Collections.Generic;
using System.Text;

namespace AntiCulture.Audio.Synth
{
    public class OutputSlot : Slot
    {
        #region Constructor
        public OutputSlot(Device owner, string name, Type dataType)
            : base(owner, name, dataType)
        {}

        public OutputSlot(string name, Type dataType)
            : base(name, dataType)
        {}

        public OutputSlot(string name)
            : base(name)
        {}

        public static OutputSlot Create<T>(Device owner, string name)
        {
            return new OutputSlot(owner, name, typeof(T));
        }

        public static OutputSlot Create<T>(string name)
        {
            return new OutputSlot(name, typeof(T));
        }
        #endregion

        #region Methods
        public override void Connect(Slot endPoint)
        {
            if (!(endPoint is InputSlot)) throw new ArgumentException("Only input slots can be connected to output slots", "endPoint");
            base.Connect(endPoint);
        }

        public void Send(object data)
        {
            if (HasDataTypeConstrain && data.GetType() != DataType) throw new ArgumentException("Invalid data type", "data");
            if (IsConnected) ((InputSlot)EndPoint).Receive(data);
        }
        #endregion
    }
}
