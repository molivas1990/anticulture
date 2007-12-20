using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AntiCulture.Audio.Synth.UI
{
    public partial class DeviceListForm : Form
    {
        private Devices.OpenAlOutput mOutputDevice = new Devices.OpenAlOutput();
        private Devices.SimpleWaveGenerator mWaveGeneratorDevice = new Devices.SimpleWaveGenerator();

        public DeviceListForm()
        {
            InitializeComponent();

            mWaveGeneratorDevice.OutputSlot.Connect(mOutputDevice.LeftChannelInputSlot);

            mDevicesListView.Items.Add(mOutputDevice.ToString()).Tag = mOutputDevice;
            mDevicesListView.Items.Add(mWaveGeneratorDevice.ToString()).Tag = mWaveGeneratorDevice;
            mDevicesListView.ItemActivate += new EventHandler(ItemActivated);
        }

        void ItemActivated(object sender, EventArgs e)
        {
            ListViewItem item = mDevicesListView.FocusedItem;
            if (item != null)
            {
                Device device = (Device)item.Tag;
                new DeviceInfoForm(device).ShowDialog();
                foreach (ListViewItem i in mDevicesListView.Items)
                    i.Text = i.Tag.ToString();
            }
        }
    }
}