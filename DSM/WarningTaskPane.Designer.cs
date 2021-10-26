
namespace DSM
{
    partial class WarningUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblWarningMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblWarningMessage
            // 
            this.lblWarningMessage.AutoSize = true;
            this.lblWarningMessage.Location = new System.Drawing.Point(27, 17);
            this.lblWarningMessage.Name = "lblWarningMessage";
            this.lblWarningMessage.Size = new System.Drawing.Size(1105, 21);
            this.lblWarningMessage.TabIndex = 1;
            this.lblWarningMessage.Text = "Delay Send Mode is enabled. This email will be sent at {dateTime}. Outlook must b" +
    "e open and connected to the VPN at the time of sending.";
            // 
            // WarningUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.Controls.Add(this.lblWarningMessage);
            this.MaximumSize = new System.Drawing.Size(10000, 49);
            this.Name = "WarningUserControl";
            this.Size = new System.Drawing.Size(929, 49);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblWarningMessage;
    }
}
