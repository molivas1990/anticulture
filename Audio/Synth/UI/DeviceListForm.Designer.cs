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
            this.mDevicesLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mDevicesListView
            // 
            this.mDevicesListView.Location = new System.Drawing.Point(11, 31);
            this.mDevicesListView.Name = "mDevicesListView";
            this.mDevicesListView.Size = new System.Drawing.Size(429, 238);
            this.mDevicesListView.TabIndex = 0;
            this.mDevicesListView.UseCompatibleStateImageBehavior = false;
            this.mDevicesListView.View = System.Windows.Forms.View.SmallIcon;
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
            // DeviceListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 280);
            this.Controls.Add(this.mDevicesLabel);
            this.Controls.Add(this.mDevicesListView);
            this.Name = "DeviceListForm";
            this.Text = "Device list";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView mDevicesListView;
        private System.Windows.Forms.Label mDevicesLabel;
    }
}