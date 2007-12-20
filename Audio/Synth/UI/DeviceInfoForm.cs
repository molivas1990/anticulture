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
        #region Constants
        private const int RadioButtonSpacing = 2;
        #endregion

        #region Fields
        private Device mDevice;
        #endregion

        #region Constructor
        public DeviceInfoForm(Device device)
        {
            InitializeComponent();

            mDevice = device;

            mTypeLabel.Text = "Type : " + mDevice.Name;
            mLabelTextBox.Text = mDevice.Label;

            int y;
            int maxY = 0;

            // Add input slot radio buttons
            y = mInputSlotsLabel.Bottom + RadioButtonSpacing;
            foreach (InputSlot i in mDevice.InputSlots)
                AddSlotRadioButton(i, ref y);

            maxY = y;

            // Add output slot radio buttons
            y = mOutputSlotsLabel.Bottom + RadioButtonSpacing;
            foreach (OutputSlot i in mDevice.OutputSlots)
                AddSlotRadioButton(i, ref y);

            // Adjust form size
            if (y > maxY) maxY = y;
            this.Height = maxY + (this.Height - this.ClientSize.Height);
        }
        #endregion

        #region Properties
        public Device Device
        {
            get { return mDevice; }
        }
        #endregion

        #region Methods
        private void AddSlotRadioButton(Slot slot, ref int y)
        {
            RadioButton radioButton = new RadioButton();

            // Set data info
            radioButton.AutoSize = true; // Auto-resize to fit text
            radioButton.Text = slot.Name;
            radioButton.Tag = slot;
            radioButton.AutoSize = false; // Stop auto-resizing

            // Set position info
            radioButton.Top = y;
            radioButton.Height = 16;
            y = radioButton.Bottom + RadioButtonSpacing;

            if (slot is InputSlot)
            {
                // Input slots go left
                radioButton.Left = mInputSlotsLabel.Left;
            }
            else
            {
                // Output slots go right
                radioButton.Left = (mOutputSlotsLabel.Left + mOutputSlotsLabel.Width) - radioButton.Width;
                radioButton.CheckAlign = ContentAlignment.MiddleRight;
                radioButton.TextAlign = ContentAlignment.MiddleRight;
            }

            // Set background color depending on type
            if (slot.HasDataTypeConstrain)
            {
                int hashCode = slot.DataType.GetHashCode();
                radioButton.BackColor = Color.FromArgb((hashCode & 0xFF) / 2 + 100, ((hashCode >> 8) & 0xFF) / 2 + 100, ((hashCode >> 16) & 0xFF) / 2 + 100);
            }

            // Prevent the user from checking it, but allow drag-and-dropping
            radioButton.AutoCheck = false;
            radioButton.AllowDrop = true;
            radioButton.Cursor = Cursors.Hand;
            radioButton.MouseDown += new MouseEventHandler(SlotClicked);
            radioButton.DragOver += new DragEventHandler(SlotDraggedOver);
            radioButton.DragDrop += new DragEventHandler(SlotDropped);
            radioButton.MouseEnter += new EventHandler(UpdateRadioButtonToolTip);

            // Set it checked if necessary
            radioButton.Checked = slot.IsConnected;
            slot.Disconnected += new EventHandler(delegate(object sender, EventArgs e) { radioButton.Checked = false; });

            // Add it to tooltip list
            mToolTip.SetToolTip(radioButton, "allo");

            // Add it to the control list
            this.Controls.Add(radioButton);
        }

        private bool AreSlotCompatible(Slot a, Slot b)
        {
            // They have to be of different type (Input/Output)
            if (a.GetType() == b.GetType()) return false;

            // They have to respect their types
            if (a.HasDataTypeConstrain && b.HasDataTypeConstrain && a.DataType != b.DataType) return false;
            
            // They must be on different devices
            if (a.HasOwner && b.HasOwner && a.Owner == b.Owner) return false;

            return true;
        }

        private void UpdateRadioButtonToolTip(object sender, EventArgs e)
        {
            // Set tool tip text
            Slot slot = (Slot)((RadioButton)sender).Tag;

            string toolTipText = "Name : " + slot.Name + "\n";

            if (slot is InputSlot) toolTipText += "Type : Input slot\n";
            else toolTipText += "Type : Output slot\n";

            if (slot.HasDataTypeConstrain) toolTipText += "Data type : " + slot.DataType.Name + "\n";
            else toolTipText += "Data type : -\n";

            if (slot.IsConnected)
            {
                if (slot.EndPoint.HasOwner) toolTipText += "Connected to : " + slot.EndPoint.Owner.Label + ":" + slot.EndPoint.Name;
                else toolTipText += "Connected to : " + slot.EndPoint.Name;
            }
            else
            {
                toolTipText += "Unconnected";
            }

            mToolTip.SetToolTip((Control)sender, toolTipText);
        }

        private void SlotDraggedOver(object sender, DragEventArgs e)
        {
            Slot target = (Slot)((RadioButton)sender).Tag;
            Slot source = (Slot)((RadioButton)e.Data.GetData(typeof(RadioButton))).Tag;

            if (AreSlotCompatible(source, target)) e.Effect = DragDropEffects.Link;
        }

        private void SlotDropped(object sender, DragEventArgs e)
        {
            RadioButton target = (RadioButton)sender;
            RadioButton source = (RadioButton)e.Data.GetData(typeof(RadioButton));

            Slot targetSlot = (Slot)target.Tag;
            Slot sourceSlot = (Slot)source.Tag;

            if (AreSlotCompatible(targetSlot, sourceSlot))
            {
                sourceSlot.Connect(targetSlot);
                target.Checked = true;
                source.Checked = true;
            }
        }

        private void SlotClicked(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            RadioButton radioButton = (RadioButton)sender;
            radioButton.DoDragDrop(radioButton, DragDropEffects.Link);
        }

        private void LabelChanged(object sender, EventArgs e)
        {
            mDevice.Label = ((TextBox)sender).Text;
        }
        #endregion
    }
}