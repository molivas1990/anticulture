namespace AntiCulture.Audio.Synth.UI
{
    partial class DeviceListForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mDevicesListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.mDevicesLabel = new System.Windows.Forms.Label();
            this.mAddDeviceButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mDevicesListView
            // 
            this.mDevicesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.mDevicesListView.Location = new System.Drawing.Point(12, 31);
            this.mDevicesListView.Name = "mDevicesListView";
            this.mDevicesListView.Size = new System.Drawing.Size(429, 238);
            this.mDevicesListView.TabIndex = 0;
            this.mDevicesListView.UseCompatibleStateImageBehavior = false;
            this.mDevicesListView.View = System.Windows.Forms.View.Details;
            this.mDevicesListView.ItemActivate += new System.EventHandler(this.ItemActivated);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Label";
            this.columnHeader1.Width = 158;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Type";
            this.columnHeader2.Width = 261;
            // 
            // mDevicesLabel
            // 
            this.mDevicesLabel.AutoSize = true;
            this.mDevicesLabel.Location = new System.Drawing.Point(8, 9);
            this.mDevicesLabel.Name = "mDevicesLabel";
            this.mDevicesLabel.Size = new System.Drawing.Size(46, 13);
            this.mDevicesLabel.TabIndex = 1;
            this.mDevicesLabel.Text = "Devices";
            // 
            // mAddDeviceButton
            // 
            this.mAddDeviceButton.Location = new System.Drawing.Point(183, 275);
            this.mAddDeviceButton.Name = "mAddDeviceButton";
            this.mAddDeviceButton.Size = new System.Drawing.Size(91, 23);
            this.mAddDeviceButton.TabIndex = 2;
            this.mAddDeviceButton.Text = "Add device...";
            this.mAddDeviceButton.UseVisualStyleBackColor = true;
            this.mAddDeviceButton.Click += new System.EventHandler(this.AddDevice);
            // 
            // DeviceListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 301);
            this.Controls.Add(this.mAddDeviceButton);
            this.Controls.Add(this.mDevicesLabel);
            this.Controls.Add(this.mDevicesListView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "DeviceListForm";
            this.Text = "Device list";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView mDevicesListView;
        private System.Windows.Forms.Label mDevicesLabel;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button mAddDeviceButton;
    }
}