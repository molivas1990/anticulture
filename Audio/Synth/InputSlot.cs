using System;
using System.Collections.Generic;
using System.Text;

namespace AntiCulture.Audio.Synth
{
    public class InputSlot
    {
        #region ConnectionEventArgs subclass
        public class ConnectionEventArgs : EventArgs
        {
            #region Data members
            private OutputSlot mEndPoint;
            #endregion

            #region Constructor
            public ConnectionEventArgs(OutputSlot endPoint)
            {
                mEndPoint = endPoint;
            }
            #endregion

            #region Properties
            public OutputSlot EndPoint
            {
                get { return mEndPoint; }
            }
            #endregion
        }
        #endregion

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

        #region Data members
        private Device mOwner;
        private string mName;
        private Type mDataType;
        private OutputSlot mEndPoint;
        #endregion

        #region Events
        public event EventHandler<ConnectionEventArgs> Connected;
        public event EventHandler Disconnected;
        public event EventHandler<DataEventArgs> DataReceived;
        #endregion

        #region Constructor
        public InputSlot(Device owner, string name, Type dataType)
        {
            mOwner = owner;
            mName = name;
            mDataType = dataType;
        }

        public InputSlot(string name, Type dataType)
            : this(null, name, dataType)
        { }

        public InputSlot(string name)
            : this(name, null)
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

        public OutputSlot EndPoint
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
        public void Connect(OutputSlot endPoint)
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

        public void Receive(object data)
        {
            if (mDataType != null && data.GetType() != mDataType) throw new ArgumentException("Invalid data type", "data");
            if (DataReceived != null) DataReceived(this, new DataEventArgs(data));
        }

        internal void MakeDisconnected()
        {
            mEndPoint = null;
            if (Disconnected != null) Disconnected(this, EventArgs.Empty);
        }

        internal void MakeConnected(OutputSlot endPoint)
        {
            mEndPoint = endPoint;
            if (Connected != null) Connected(this, new ConnectionEventArgs(endPoint));
        }
        #endregion
    }
}
