namespace AntiCulture.Audio.Synth.UI
{
    partial class DeviceInfoForm
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
            this.mSlotsLabel = new System.Windows.Forms.Label();
            this.mSlotsListView = new System.Windows.Forms.ListView();
            this.mTypeLabel = new System.Windows.Forms.Label();
            this.mLabelTextBox = new System.Windows.Forms.TextBox();
            this.mLabelLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mSlotsLabel
            // 
            this.mSlotsLabel.AutoSize = true;
            this.mSlotsLabel.Location = new System.Drawing.Point(12, 48);
            this.mSlotsLabel.Name = "mSlotsLabel";
            this.mSlotsLabel.Size = new System.Drawing.Size(30, 13);
            this.mSlotsLabel.TabIndex = 4;
            this.mSlotsLabel.Text = "Slots";
            // 
            // mSlotsListView
            // 
            this.mSlotsListView.Location = new System.Drawing.Point(15, 64);
            this.mSlotsListView.Name = "mSlotsListView";
            this.mSlotsListView.Size = new System.Drawing.Size(498, 182);
            this.mSlotsListView.TabIndex = 6;
            this.mSlotsListView.UseCompatibleStateImageBehavior = false;
            this.mSlotsListView.View = System.Windows.Forms.View.Details;
            // 
            // mTypeLabel
            // 
            this.mTypeLabel.AutoSize = true;
            this.mTypeLabel.Location = new System.Drawing.Point(12, 9);
            this.mTypeLabel.Name = "mTypeLabel";
            this.mTypeLabel.Size = new System.Drawing.Size(31, 13);
            this.mTypeLabel.TabIndex = 7;
            this.mTypeLabel.Text = "Type";
            // 
            // mLabelTextBox
            // 
            this.mLabelTextBox.Location = new System.Drawing.Point(53, 25);
            this.mLabelTextBox.Name = "mLabelTextBox";
            this.mLabelTextBox.Size = new System.Drawing.Size(167, 20);
            this.mLabelTextBox.TabIndex = 8;
            this.mLabelTextBox.TextChanged += new System.EventHandler(this.LabelChanged);
            // 
            // mLabelLabel
            // 
            this.mLabelLabel.AutoSize = true;
            this.mLabelLabel.Location = new System.Drawing.Point(12, 28);
            this.mLabelLabel.Name = "mLabelLabel";
            this.mLabelLabel.Size = new System.Drawing.Size(42, 13);
            this.mLabelLabel.TabIndex = 9;
            this.mLabelLabel.Text = "Label : ";
            // 
            // DeviceInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 262);
            this.Controls.Add(this.mLabelLabel);
            this.Controls.Add(this.mLabelTextBox);
            this.Controls.Add(this.mTypeLabel);
            this.Controls.Add(this.mSlotsListView);
            this.Controls.Add(this.mSlotsLabel);
            this.MaximizeBox = false;
            this.Name = "DeviceInfoForm";
            this.Text = "Device information";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label mSlotsLabel;
        private System.Windows.Forms.ListView mSlotsListView;
        private System.Windows.Forms.Label mTypeLabel;
        private System.Windows.Forms.TextBox mLabelTextBox;
        private System.Windows.Forms.Label mLabelLabel;
    }
}