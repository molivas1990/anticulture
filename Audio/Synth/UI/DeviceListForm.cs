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
        #region Fields
        private Devices.OpenAlOutput mOutputDevice = new Devices.OpenAlOutput();
        private Devices.SimpleWaveGenerator mWaveGeneratorDevice = new Devices.SimpleWaveGenerator();
        private List<DeviceInfoForm> mDeviceInfoForms = new List<DeviceInfoForm>();
        #endregion

        public DeviceListForm()
        {
            InitializeComponent();

            mWaveGeneratorDevice.OutputSlot.Connect(mOutputDevice.LeftChannelInputSlot);

            AddItem(mOutputDevice);
            AddItem(mWaveGeneratorDevice);
        }

        private void AddItem(Device device)
        {
            ListViewItem item = new ListViewItem();
            item.Text = device.Label;
            item.SubItems.Add(device.Name);
            item.Tag = device;
            mDevicesListView.Items.Add(item);
        }

        private void ItemActivated(object sender, EventArgs e)
        {
            ListViewItem item = mDevicesListView.FocusedItem;
            if (item != null)
            {
                Device device = (Device)item.Tag;

                // Look up our list to see if the form is already open
                foreach (DeviceInfoForm i in mDeviceInfoForms)
                {
                    if (i.Device == device)
                    {
                        // Simply activate it
                        i.Activate();
                        return;
                    }
                }

                // Add a new form
                DeviceInfoForm infoForm = new DeviceInfoForm(device);
                infoForm.Show();
                infoForm.FormClosed += new FormClosedEventHandler(DeviceInfoFormClosed);
                mDeviceInfoForms.Add(infoForm);
            }
        }

        private void DeviceInfoFormClosed(object sender, FormClosedEventArgs e)
        {
            // Remove the form from the list of active forms
            mDeviceInfoForms.Remove((DeviceInfoForm)sender);

            // Update labels
            foreach (ListViewItem i in mDevicesListView.Items)
                i.Text = ((Device)i.Tag).Label;
        }

        private void AddDevice(object sender, EventArgs e)
        {
            MessageBox.Show("Adding device...");
        }
    }
}