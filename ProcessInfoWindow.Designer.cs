namespace WinProcesses
{
    partial class ProcessInfoWindow
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
            txtBoxProcInfo = new TextBox();
            SuspendLayout();
            // 
            // txtBoxProcInfo
            // 
            txtBoxProcInfo.Dock = DockStyle.Fill;
            txtBoxProcInfo.Location = new Point(0, 0);
            txtBoxProcInfo.Multiline = true;
            txtBoxProcInfo.Name = "txtBoxProcInfo";
            txtBoxProcInfo.ReadOnly = true;
            txtBoxProcInfo.Size = new Size(357, 129);
            txtBoxProcInfo.TabIndex = 0;
            // 
            // ProcessInfoWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(357, 129);
            Controls.Add(txtBoxProcInfo);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "ProcessInfoWindow";
            StartPosition = FormStartPosition.CenterParent;
            Text = "ProcessInfoForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtBoxProcInfo;
    }
}