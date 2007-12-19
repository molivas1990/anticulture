using System;
using System.Collections.Generic;
using System.Text;

namespace AntiCulture.Audio
{
    public class OutputSlot : Slot
    {
        #region Constructor
        private OutputSlot(Device device, string name, Type dataType)
            : base(device, name, dataType)
        { }

        public static OutputSlot Create<T>(Device device, string name)
        {
            return new OutputSlot(device, name, typeof(T));
        }

        public static OutputSlot Create<T>(string name)
        {
            return new OutputSlot(null, name, typeof(T));
        }
        #endregion

        #region Methods
        public void Send(object data)
        {
            if (!IsConnected) throw new InvalidOperationException("Cannot send if not connected");
            (mEndPoint as InputSlot).Receive(data);
        }
        #endregion

        #region Overriden methods
        public override void Connect(Slot endPoint)
        {
            if (endPoint == null) throw new ArgumentNullException("endPoint");
            if (endPoint.GetType() != typeof(InputSlot)) throw new ArgumentException("Must be an input slot", "endPoint");
            base.Connect(endPoint);
        }
        #endregion
    }
}
