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
            this.components = new System.ComponentModel.Container();
            this.mInputSlotsLabel = new System.Windows.Forms.Label();
            this.mTypeLabel = new System.Windows.Forms.Label();
            this.mLabelTextBox = new System.Windows.Forms.TextBox();
            this.mLabelLabel = new System.Windows.Forms.Label();
            this.mOutputSlotsLabel = new System.Windows.Forms.Label();
            this.mToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // mInputSlotsLabel
            // 
            this.mInputSlotsLabel.AutoSize = true;
            this.mInputSlotsLabel.Location = new System.Drawing.Point(12, 48);
            this.mInputSlotsLabel.Name = "mInputSlotsLabel";
            this.mInputSlotsLabel.Size = new System.Drawing.Size(55, 13);
            this.mInputSlotsLabel.TabIndex = 4;
            this.mInputSlotsLabel.Text = "Input slots";
            // 
            // mTypeLabel
            // 
            this.mTypeLabel.AutoSize = true;
            this.mTypeLabel.Location = new System.Drawing.Point(12, 6);
            this.mTypeLabel.Name = "mTypeLabel";
            this.mTypeLabel.Size = new System.Drawing.Size(31, 13);
            this.mTypeLabel.TabIndex = 7;
            this.mTypeLabel.Text = "Type";
            // 
            // mLabelTextBox
            // 
            this.mLabelTextBox.Location = new System.Drawing.Point(51, 24);
            this.mLabelTextBox.Name = "mLabelTextBox";
            this.mLabelTextBox.Size = new System.Drawing.Size(333, 20);
            this.mLabelTextBox.TabIndex = 8;
            this.mLabelTextBox.TextChanged += new System.EventHandler(this.LabelChanged);
            // 
            // mLabelLabel
            // 
            this.mLabelLabel.Location = new System.Drawing.Point(12, 27);
            this.mLabelLabel.Name = "mLabelLabel";
            this.mLabelLabel.Size = new System.Drawing.Size(38, 13);
            this.mLabelLabel.TabIndex = 9;
            this.mLabelLabel.Text = "Label : ";
            // 
            // mOutputSlotsLabel
            // 
            this.mOutputSlotsLabel.AutoSize = true;
            this.mOutputSlotsLabel.Location = new System.Drawing.Point(321, 48);
            this.mOutputSlotsLabel.Name = "mOutputSlotsLabel";
            this.mOutputSlotsLabel.Size = new System.Drawing.Size(63, 13);
            this.mOutputSlotsLabel.TabIndex = 10;
            this.mOutputSlotsLabel.Text = "Output slots";
            this.mOutputSlotsLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // mToolTip
            // 
            this.mToolTip.AutomaticDelay = 0;
            this.mToolTip.ShowAlways = true;
            this.mToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.mToolTip.ToolTipTitle = "Slot description";
            this.mToolTip.UseAnimation = false;
            this.mToolTip.UseFading = false;
            // 
            // DeviceInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 262);
            this.Controls.Add(this.mOutputSlotsLabel);
            this.Controls.Add(this.mLabelLabel);
            this.Controls.Add(this.mLabelTextBox);
            this.Controls.Add(this.mTypeLabel);
            this.Controls.Add(this.mInputSlotsLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "DeviceInfoForm";
            this.Text = "Device information";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label mInputSlotsLabel;
        private System.Windows.Forms.Label mTypeLabel;
        private System.Windows.Forms.TextBox mLabelTextBox;
        private System.Windows.Forms.Label mOutputSlotsLabel;
        private System.Windows.Forms.ToolTip mToolTip;
        private System.Windows.Forms.Label mLabelLabel;
    }
}