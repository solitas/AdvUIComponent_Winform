namespace UIComponentTest
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.cMenuStrip1 = new Rootech.UI.Component.CMenuStrip();
            this.tESTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.test11ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.test12ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tEST2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.test21ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.test22ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.test3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.test31ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.test311ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.test32ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.programGroupBuildToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cToolStripMenuItem1 = new Rootech.UI.Component.CToolStripMenuItem();
            this.cTabControl1 = new Rootech.UI.Component.CTabControl();
            this.tabPage1 = new Rootech.UI.Component.CTabPage();
            this.flatTabRenderer1 = new Rootech.UI.Component.FlatTabRenderer();
            this.cMenuStrip1.SuspendLayout();
            this.cTabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cMenuStrip1
            // 
            this.cMenuStrip1.Depth = 0;
            this.cMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tESTToolStripMenuItem,
            this.tEST2ToolStripMenuItem,
            this.test3ToolStripMenuItem,
            this.buildToolStripMenuItem,
            this.cToolStripMenuItem1});
            this.cMenuStrip1.Location = new System.Drawing.Point(36, 0);
            this.cMenuStrip1.MouseButtonState = Rootech.UI.Component.MouseButtonState.HOVER;
            this.cMenuStrip1.Name = "cMenuStrip1";
            this.cMenuStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.cMenuStrip1.Size = new System.Drawing.Size(263, 30);
            this.cMenuStrip1.Stretch = false;
            this.cMenuStrip1.TabIndex = 0;
            this.cMenuStrip1.Text = "cMenuStrip1";
            // 
            // tESTToolStripMenuItem
            // 
            this.tESTToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.test11ToolStripMenuItem,
            this.test12ToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAllToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.toolStripSeparator1,
            this.printToolStripMenuItem,
            this.printPreviewToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.tESTToolStripMenuItem.Name = "tESTToolStripMenuItem";
            this.tESTToolStripMenuItem.Padding = new System.Windows.Forms.Padding(0);
            this.tESTToolStripMenuItem.ShortcutKeyDisplayString = "F";
            this.tESTToolStripMenuItem.Size = new System.Drawing.Size(29, 30);
            this.tESTToolStripMenuItem.Text = "File";
            // 
            // test11ToolStripMenuItem
            // 
            this.test11ToolStripMenuItem.Name = "test11ToolStripMenuItem";
            this.test11ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.test11ToolStripMenuItem.Text = "New Program";
            // 
            // test12ToolStripMenuItem
            // 
            this.test12ToolStripMenuItem.Name = "test12ToolStripMenuItem";
            this.test12ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.test12ToolStripMenuItem.Text = "New Group";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.saveToolStripMenuItem.Text = "Save ";
            // 
            // saveAllToolStripMenuItem
            // 
            this.saveAllToolStripMenuItem.Name = "saveAllToolStripMenuItem";
            this.saveAllToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.saveAllToolStripMenuItem.Text = "Save all";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.saveAsToolStripMenuItem.Text = "Save as...";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.closeToolStripMenuItem.Text = "Close";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.printToolStripMenuItem.Text = "Print";
            // 
            // printPreviewToolStripMenuItem
            // 
            this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
            this.printPreviewToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.printPreviewToolStripMenuItem.Text = "Print Preview ";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(145, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // tEST2ToolStripMenuItem
            // 
            this.tEST2ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.test21ToolStripMenuItem,
            this.test22ToolStripMenuItem});
            this.tEST2ToolStripMenuItem.Name = "tEST2ToolStripMenuItem";
            this.tEST2ToolStripMenuItem.Padding = new System.Windows.Forms.Padding(0);
            this.tEST2ToolStripMenuItem.Size = new System.Drawing.Size(31, 30);
            this.tEST2ToolStripMenuItem.Text = "Edit";
            // 
            // test21ToolStripMenuItem
            // 
            this.test21ToolStripMenuItem.Name = "test21ToolStripMenuItem";
            this.test21ToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.test21ToolStripMenuItem.Text = "Test2-1";
            // 
            // test22ToolStripMenuItem
            // 
            this.test22ToolStripMenuItem.Name = "test22ToolStripMenuItem";
            this.test22ToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.test22ToolStripMenuItem.Text = "Test2-2";
            // 
            // test3ToolStripMenuItem
            // 
            this.test3ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.test31ToolStripMenuItem,
            this.test32ToolStripMenuItem});
            this.test3ToolStripMenuItem.Name = "test3ToolStripMenuItem";
            this.test3ToolStripMenuItem.Padding = new System.Windows.Forms.Padding(0);
            this.test3ToolStripMenuItem.Size = new System.Drawing.Size(37, 30);
            this.test3ToolStripMenuItem.Text = "View";
            // 
            // test31ToolStripMenuItem
            // 
            this.test31ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.test311ToolStripMenuItem});
            this.test31ToolStripMenuItem.Name = "test31ToolStripMenuItem";
            this.test31ToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.test31ToolStripMenuItem.Text = "Test3-1";
            // 
            // test311ToolStripMenuItem
            // 
            this.test311ToolStripMenuItem.Name = "test311ToolStripMenuItem";
            this.test311ToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.test311ToolStripMenuItem.Text = "Test3-1-1";
            // 
            // test32ToolStripMenuItem
            // 
            this.test32ToolStripMenuItem.Name = "test32ToolStripMenuItem";
            this.test32ToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.test32ToolStripMenuItem.Text = "Test3-2";
            // 
            // buildToolStripMenuItem
            // 
            this.buildToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.compileToolStripMenuItem,
            this.programGroupBuildToolStripMenuItem});
            this.buildToolStripMenuItem.Name = "buildToolStripMenuItem";
            this.buildToolStripMenuItem.Padding = new System.Windows.Forms.Padding(0);
            this.buildToolStripMenuItem.Size = new System.Drawing.Size(38, 30);
            this.buildToolStripMenuItem.Text = "Build";
            // 
            // compileToolStripMenuItem
            // 
            this.compileToolStripMenuItem.Name = "compileToolStripMenuItem";
            this.compileToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.compileToolStripMenuItem.Text = "Program Build";
            // 
            // programGroupBuildToolStripMenuItem
            // 
            this.programGroupBuildToolStripMenuItem.Name = "programGroupBuildToolStripMenuItem";
            this.programGroupBuildToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.programGroupBuildToolStripMenuItem.Text = "Program Group Build";
            // 
            // cToolStripMenuItem1
            // 
            this.cToolStripMenuItem1.Depth = 0;
            this.cToolStripMenuItem1.MouseButtonState = Rootech.UI.Component.MouseButtonState.HOVER;
            this.cToolStripMenuItem1.Name = "cToolStripMenuItem1";
            this.cToolStripMenuItem1.Padding = new System.Windows.Forms.Padding(0);
            this.cToolStripMenuItem1.Size = new System.Drawing.Size(36, 30);
            this.cToolStripMenuItem1.Text = "Help";
            this.cToolStripMenuItem1.Visualization = null;
            // 
            // cTabControl1
            // 
            this.cTabControl1.ActiveColor = System.Drawing.SystemColors.Control;
            this.cTabControl1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.cTabControl1.Controls.Add(this.tabPage1);
            this.cTabControl1.Depth = 0;
            this.cTabControl1.ImageIndex = -1;
            this.cTabControl1.InactiveColor = System.Drawing.SystemColors.Window;
            this.cTabControl1.InActiveForeColor = System.Drawing.SystemColors.ControlText;
            this.cTabControl1.Location = new System.Drawing.Point(9, 44);
            this.cTabControl1.MouseButtonState = Rootech.UI.Component.MouseButtonState.HOVER;
            this.cTabControl1.Name = "cTabControl1";
            this.cTabControl1.OverIndex = -1;
            this.cTabControl1.ScrollButtonStyle = Rootech.UI.Component.CTabScrollButtonStyle.Always;
            this.cTabControl1.SelectedIndex = 0;
            this.cTabControl1.SelectedTab = this.tabPage1;
            this.cTabControl1.Size = new System.Drawing.Size(300, 300);
            this.cTabControl1.TabDock = System.Windows.Forms.DockStyle.Top;
            this.cTabControl1.TabFont = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cTabControl1.TabIndex = 1;
            this.cTabControl1.TabMargin = 3;
            this.cTabControl1.TabRenderer = this.flatTabRenderer1;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tabPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPage1.ImageIndex = 0;
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(292, 267);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(642, 456);
            this.Controls.Add(this.cTabControl1);
            this.Controls.Add(this.cMenuStrip1);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.cMenuStrip1.ResumeLayout(false);
            this.cMenuStrip1.PerformLayout();
            this.cTabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Rootech.UI.Component.CMenuStrip cMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tESTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem test11ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem test12ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tEST2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem test21ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem test22ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem test3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem test31ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem test311ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem test32ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buildToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem programGroupBuildToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printPreviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private Rootech.UI.Component.CToolStripMenuItem cToolStripMenuItem1;
        private Rootech.UI.Component.CTabControl cTabControl1;
        private Rootech.UI.Component.FlatTabRenderer flatTabRenderer1;
        private Rootech.UI.Component.CTabPage tabPage1;
    }
}

