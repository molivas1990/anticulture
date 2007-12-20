using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AntiCulture.Audio.Synth.UI
{
    public partial class DeviceInfoForm : Form
    {
        #region Fields
        private Device mDevice;
        #endregion

        public DeviceInfoForm(Device device)
        {
            InitializeComponent();

            mDevice = device;

            mTypeLabel.Text = "Type : " + mDevice.Name;
            mLabelTextBox.Text = mDevice.Label;

            mSlotsListView.Columns.Add("Name", -2, HorizontalAlignment.Left);
            mSlotsListView.Columns.Add("Data type", -2, HorizontalAlignment.Left);
            mSlotsListView.Columns.Add("End point", -2, HorizontalAlignment.Left);

            ListViewGroup listViewGroup;
            listViewGroup = mSlotsListView.Groups.Add("Input slots", "Input slots");
            foreach (InputSlot i in mDevice.InputSlots)
            {
                ListViewItem item = new ListViewItem(i.Name);
                //if (i.IsConnected) item.Checked = true;
                if (i.HasDataTypeConstrain) item.SubItems.Add(i.DataType.ToString());
                else item.SubItems.Add("-any-");
                if (i.IsConnected)
                {
                    if (i.EndPoint.HasOwner) item.SubItems.Add(i.EndPoint.Owner.ToString() + ":" + i.EndPoint.Name);
                    else item.SubItems.Add(i.EndPoint.Name);
                }
                else item.SubItems.Add("-none-");
                mSlotsListView.Items.Add(item);
                listViewGroup.Items.Add(item);
            }

            listViewGroup = mSlotsListView.Groups.Add("Output slots", "Output slots");
            foreach (OutputSlot i in mDevice.OutputSlots)
            {
                ListViewItem item = new ListViewItem(i.Name);
                //if (i.IsConnected) item.Checked = true;
                if (i.HasDataTypeConstrain) item.SubItems.Add(i.DataType.ToString());
                else item.SubItems.Add("-any-");
                if (i.IsConnected)
                {
                    if (i.EndPoint.HasOwner) item.SubItems.Add(i.EndPoint.Owner.Label + ":" + i.EndPoint.Name);
                    else item.SubItems.Add(i.EndPoint.Name);
                }
                else item.SubItems.Add("-none-");
                mSlotsListView.Items.Add(item);
                listViewGroup.Items.Add(item);
            }
        }

        private void LabelChanged(object sender, EventArgs e)
        {
            mDevice.Label = ((TextBox)sender).Text;
        }
    }
}