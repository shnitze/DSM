
namespace DSM
{
    partial class WarningTaskPane
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
            this.btnDismiss = new System.Windows.Forms.Button();
            this.lblWarningMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnDismiss
            // 
            this.btnDismiss.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnDismiss.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDismiss.Location = new System.Drawing.Point(611, 9);
            this.btnDismiss.Name = "btnDismiss";
            this.btnDismiss.Size = new System.Drawing.Size(89, 32);
            this.btnDismiss.TabIndex = 0;
            this.btnDismiss.Text = "Dismiss";
            this.btnDismiss.UseVisualStyleBackColor = true;
            this.btnDismiss.Click += new System.EventHandler(this.btnDismiss_Click);
            // 
            // lblWarningMessage
            // 
            this.lblWarningMessage.AutoSize = true;
            this.lblWarningMessage.Location = new System.Drawing.Point(27, 17);
            this.lblWarningMessage.Name = "lblWarningMessage";
            this.lblWarningMessage.Size = new System.Drawing.Size(433, 17);
            this.lblWarningMessage.TabIndex = 1;
            this.lblWarningMessage.Text = "Warning: Delay Send Mode is enabled. This email will be sent at {0}";
            // 
            // WarningTaskPane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.Controls.Add(this.lblWarningMessage);
            this.Controls.Add(this.btnDismiss);
            this.MaximumSize = new System.Drawing.Size(10000, 49);
            this.Name = "WarningTaskPane";
            this.Size = new System.Drawing.Size(713, 49);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDismiss;
        private System.Windows.Forms.Label lblWarningMessage;
    }
}
