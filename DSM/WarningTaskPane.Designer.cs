﻿
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WarningUserControl));
            this.lblWarningMessage = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblWarningMessage
            // 
            resources.ApplyResources(this.lblWarningMessage, "lblWarningMessage");
            this.lblWarningMessage.ForeColor = System.Drawing.Color.Black;
            this.lblWarningMessage.Name = "lblWarningMessage";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.lblWarningMessage, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // WarningUserControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "WarningUserControl";
            this.Load += new System.EventHandler(this.WarningUserControl_Load);
            this.Resize += new System.EventHandler(this.WarningTaskPane_SizeChanged);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblWarningMessage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
