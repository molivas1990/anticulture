using System;
using System.Collections.Generic;
using System.Text;

namespace AntiCulture.Audio.Synth
{
    public class OutputSlot
    {
        #region ConnectionEventArgs subclass
        public class ConnectionEventArgs : EventArgs
        {
            #region Data members
            private InputSlot mEndPoint;
            #endregion

            #region Constructor
            public ConnectionEventArgs(InputSlot endPoint)
            {
                mEndPoint = endPoint;
            }
            #endregion

            #region Properties
            public InputSlot EndPoint
            {
                get { return mEndPoint; }
            }
            #endregion
        }
        #endregion

        #region Data members
        private Device mOwner;
        private string mName;
        private Type mDataType;
        private InputSlot mEndPoint;
        #endregion

        #region Events
        public event EventHandler<ConnectionEventArgs> Connected;
        public event EventHandler Disconnected;
        #endregion

        #region Constructor
        public OutputSlot(Device owner, string name, Type dataType)
        {
            mOwner = owner;
            mName = name;
            mDataType = dataType;
        }

        public OutputSlot(string name, Type dataType)
            : this(null, name, dataType)
        {}

        public OutputSlot(string name)
            : this(name, null)
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

        #region Properties
        public bool HasOwner
        {
            get { return mOwner != null; }
        }

        public Device Owner
        {
            get { return mOwner; }
        }

        public string Name
        {
            get { return mName; }
        }

        public bool HasDataTypeConstrain
        {
            get { return mDataType != null; }
        }

        public Type DataType
        {
            get { return mDataType; }
        }

        public bool IsConnected
        {
            get { return mEndPoint != null; }
        }

        public InputSlot EndPoint
        {
            get
            {
                if (!IsConnected) throw new InvalidOperationException("Querying slot endpoint when unconnected");
                return mEndPoint;
            }
            set
            {
                if (value == null) Disconnect();
                else Connect(value);
            }
        }
        #endregion

        #region Methods
        public void Connect(InputSlot endPoint)
        {
            if (endPoint == null) throw new ArgumentNullException("endPoint");
            if (endPoint == mEndPoint) return;
            if (IsConnected)
            {
                mEndPoint.MakeDisconnected();
                mEndPoint = null;
                if (Disconnected != null) Disconnected(this, EventArgs.Empty);
            }
            if (endPoint.HasDataTypeConstrain && endPoint.DataType != mDataType)
                throw new InvalidOperationException("Uncompatible slot data types");
            mEndPoint = endPoint;
            mEndPoint.MakeConnected(this);
        }

        public void Disconnect()
        {
            if (IsConnected)
            {
                mEndPoint.MakeDisconnected();
                mEndPoint = null;
                if (Disconnected != null) Disconnected(this, EventArgs.Empty);
            }
        }

        public void Send(object data)
        {
            if (mDataType != null && data.GetType() != mDataType) throw new ArgumentException("Invalid data type", "data");
            if (IsConnected) mEndPoint.Receive(data);
        }

        internal void MakeDisconnected()
        {
            mEndPoint = null;
            if (Disconnected != null) Disconnected(this, EventArgs.Empty);
        }

        internal void MakeConnected(InputSlot endPoint)
        {
            mEndPoint = endPoint;
            if (Connected != null) Connected(this, new ConnectionEventArgs(endPoint));
        }
        #endregion
    }
}
