namespace UIComponentTest
{
    partial class TestForm
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
            this.cTabControl1 = new Rootech.UI.Component.CTabControl();
            this.SuspendLayout();
            // 
            // cTabControl1
            // 
            this.cTabControl1.ActiveColor = System.Drawing.SystemColors.Control;
            this.cTabControl1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.cTabControl1.Depth = 0;
            this.cTabControl1.ImageIndex = -1;
            this.cTabControl1.InactiveColor = System.Drawing.SystemColors.Window;
            this.cTabControl1.InActiveForeColor = System.Drawing.SystemColors.ControlText;
            this.cTabControl1.Location = new System.Drawing.Point(0, 0);
            this.cTabControl1.MouseButtonState = Rootech.UI.Component.MouseButtonState.HOVER;
            this.cTabControl1.Name = "cTabControl1";
            this.cTabControl1.OverIndex = -1;
            this.cTabControl1.ScrollButtonStyle = Rootech.UI.Component.CTabScrollButtonStyle.Always;
            this.cTabControl1.SelectedTab = null;
            this.cTabControl1.Size = new System.Drawing.Size(300, 300);
            this.cTabControl1.TabDock = System.Windows.Forms.DockStyle.Top;
            this.cTabControl1.TabFont = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cTabControl1.TabIndex = 0;
            this.cTabControl1.TabMargin = 3;
            this.cTabControl1.TabRenderer = null;
            this.cTabControl1.Visualization = null;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 546);
            this.Controls.Add(this.cTabControl1);
            this.Name = "TestForm";
            this.Text = "TestForm";
            this.ResumeLayout(false);

        }

        #endregion

        private Rootech.UI.Component.CTabControl cTabControl1;
    }
}