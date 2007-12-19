using System;
using System.Collections.Generic;
using System.Text;

namespace AntiCulture.Audio
{
    public abstract class Slot
    {
        #region ConnectionEventArgs subclass
        public class ConnectionEventArgs : EventArgs
        {
            #region Data members
            private Slot mEndPoint;
            #endregion

            #region Constructor
            public ConnectionEventArgs(Slot endPoint)
            {
                mEndPoint = endPoint;
            }
            #endregion

            #region Properties
            public Slot EndPoint
            {
                get { return mEndPoint; }
            }
            #endregion
        }
        #endregion

        #region Data members
        private Device mDevice;
        private string mName;
        private Type mDataType;
        protected Slot mEndPoint;
        #endregion

        #region Events
        public event EventHandler<ConnectionEventArgs> Connected;
        public event EventHandler Disconnected;

        protected void FireConnectedEvent(Slot endPoint) { if (Connected != null) Connected(this, new ConnectionEventArgs(endPoint)); }
        protected void FireDisconnectedEvent() { if (Disconnected != null) Disconnected(this, EventArgs.Empty); }
        #endregion

        #region Constructor
        protected Slot(Device device, string name, Type dataType)
        {
            mDevice = device;
            mName = name;
            mDataType = dataType;
        }
        #endregion

        #region Properties
        public Device Device
        {
            get { return mDevice; }
        }

        public string Name
        {
            get { return mName; }
        }

        public Type DataType
        {
            get { return mDataType; }
        }

        public bool IsConnected
        {
            get { return mEndPoint != null; }
        }

        public Slot EndPoint
        {
            get
            {
                if (!IsConnected) throw new InvalidOperationException("Querying slot endpoint when unconnected");
                return mEndPoint;
            }
            set { Connect(value); }
        }
        #endregion

        #region Methods
        public virtual void Connect(Slot endPoint)
        {
            if (endPoint == null) throw new ArgumentNullException("endPoint");
            if (endPoint == mEndPoint) return;
            if (IsConnected)
            {
                mEndPoint.mEndPoint = null;
                mEndPoint.FireDisconnectedEvent();
            }
            mEndPoint = endPoint;
            mEndPoint.mEndPoint = this;
            mEndPoint.FireConnectedEvent(this);
        }

        public virtual void Disconnect()
        {
            if (!IsConnected) return;
            mEndPoint.mEndPoint = null;
            mEndPoint.FireDisconnectedEvent();
            mEndPoint = null;
        }
        #endregion
    }
}
