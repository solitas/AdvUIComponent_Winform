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
            this.cButton1 = new Rootech.UI.Component.CButton();
            this.cButton2 = new Rootech.UI.Component.CButton();
            this.SuspendLayout();
            // 
            // cButton1
            // 
            this.cButton1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.cButton1.Depth = 0;
            this.cButton1.InnerBorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.cButton1.Location = new System.Drawing.Point(474, 209);
            this.cButton1.MouseButtonState = Rootech.UI.Component.MouseButtonState.HOVER;
            this.cButton1.Name = "cButton1";
            this.cButton1.OuterBorderColor = System.Drawing.SystemColors.AppWorkspace;
            this.cButton1.Radius = 3;
            this.cButton1.Size = new System.Drawing.Size(75, 47);
            this.cButton1.TabIndex = 0;
            this.cButton1.Text = "&OK";
            this.cButton1.UseVisualStyleBackColor = true;
            this.cButton1.Visualization = null;
            // 
            // cButton2
            // 
            this.cButton2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.cButton2.Depth = 0;
            this.cButton2.InnerBorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.cButton2.Location = new System.Drawing.Point(555, 209);
            this.cButton2.MouseButtonState = Rootech.UI.Component.MouseButtonState.HOVER;
            this.cButton2.Name = "cButton2";
            this.cButton2.OuterBorderColor = System.Drawing.SystemColors.AppWorkspace;
            this.cButton2.Radius = 3;
            this.cButton2.Size = new System.Drawing.Size(75, 47);
            this.cButton2.TabIndex = 0;
            this.cButton2.Text = "&Cancel";
            this.cButton2.UseVisualStyleBackColor = true;
            this.cButton2.Visualization = null;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 268);
            this.Controls.Add(this.cButton2);
            this.Controls.Add(this.cButton1);
            this.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Rootech.UI.Component.CButton cButton1;
        private Rootech.UI.Component.CButton cButton2;
    }
}

