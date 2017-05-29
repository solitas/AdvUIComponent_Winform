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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuButton1 = new Rootech.UI.Component.MenuButton();
            this.SuspendLayout();
            // 
            // menuButton1
            // 
            this.menuButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(41)))), ((int)(((byte)(46)))));
            this.menuButton1.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(148)))), ((int)(((byte)(151)))));
            this.menuButton1.HoverBorderColor = System.Drawing.Color.Empty;
            this.menuButton1.HoverButtonColor = System.Drawing.Color.Empty;
            this.menuButton1.HoverForeColor = System.Drawing.Color.Empty;
            this.menuButton1.Location = new System.Drawing.Point(28, 1);
            this.menuButton1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.menuButton1.Name = "menuButton1";
            this.menuButton1.Size = new System.Drawing.Size(42, 23);
            this.menuButton1.TabIndex = 0;
            this.menuButton1.Text = "File";
            this.menuButton1.UseVisualStyleBackColor = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(642, 570);
            this.Controls.Add(this.menuButton1);
            this.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.IconImage = ((System.Drawing.Image)(resources.GetObject("$this.IconImage")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Rootech.UI.Component.MenuButton menuButton1;
    }
}

