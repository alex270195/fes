namespace FES
{
    partial class Interface
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Interface));
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxCoeff = new System.Windows.Forms.TextBox();
            this.comboBoxRadiationType = new System.Windows.Forms.ComboBox();
            this.buttonCheckCrossSection = new System.Windows.Forms.Button();
            this.buttonCompute = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label2.Location = new System.Drawing.Point(12, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(210, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Выберите тип излучения";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label3.Location = new System.Drawing.Point(12, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(200, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Введите коэффициент точности";
            // 
            // textBoxCoeff
            // 
            this.textBoxCoeff.Location = new System.Drawing.Point(247, 54);
            this.textBoxCoeff.Name = "textBoxCoeff";
            this.textBoxCoeff.Size = new System.Drawing.Size(80, 20);
            this.textBoxCoeff.TabIndex = 5;
            // 
            // comboBoxRadiationType
            // 
            this.comboBoxRadiationType.FormattingEnabled = true;
            this.comboBoxRadiationType.Items.AddRange(new object[] {
            "Алюминий",
            "Магний"});
            this.comboBoxRadiationType.Location = new System.Drawing.Point(247, 19);
            this.comboBoxRadiationType.MaxDropDownItems = 2;
            this.comboBoxRadiationType.Name = "comboBoxRadiationType";
            this.comboBoxRadiationType.Size = new System.Drawing.Size(80, 21);
            this.comboBoxRadiationType.TabIndex = 6;
            // 
            // buttonCheckCrossSection
            // 
            this.buttonCheckCrossSection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCheckCrossSection.Location = new System.Drawing.Point(12, 91);
            this.buttonCheckCrossSection.Name = "buttonCheckCrossSection";
            this.buttonCheckCrossSection.Size = new System.Drawing.Size(110, 41);
            this.buttonCheckCrossSection.TabIndex = 8;
            this.buttonCheckCrossSection.Text = "Просмотреть сечения";
            this.buttonCheckCrossSection.UseVisualStyleBackColor = true;
            this.buttonCheckCrossSection.Click += new System.EventHandler(this.buttonCheckCrossSection_Click);
            // 
            // buttonCompute
            // 
            this.buttonCompute.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCompute.Location = new System.Drawing.Point(141, 91);
            this.buttonCompute.Name = "buttonCompute";
            this.buttonCompute.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonCompute.Size = new System.Drawing.Size(100, 41);
            this.buttonCompute.TabIndex = 9;
            this.buttonCompute.Text = "Рассчитать";
            this.buttonCompute.UseVisualStyleBackColor = true;
            this.buttonCompute.Click += new System.EventHandler(this.buttonCompute_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonExit.Image = ((System.Drawing.Image)(resources.GetObject("buttonExit.Image")));
            this.buttonExit.Location = new System.Drawing.Point(262, 91);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(65, 41);
            this.buttonExit.TabIndex = 10;
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // Interface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 147);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonCompute);
            this.Controls.Add(this.buttonCheckCrossSection);
            this.Controls.Add(this.comboBoxRadiationType);
            this.Controls.Add(this.textBoxCoeff);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Interface";
            this.Text = "ФЭС";
            this.Load += new System.EventHandler(this.Interface_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxCoeff;
        private System.Windows.Forms.ComboBox comboBoxRadiationType;
        private System.Windows.Forms.Button buttonCheckCrossSection;
        private System.Windows.Forms.Button buttonCompute;
        private System.Windows.Forms.Button buttonExit;
    }
}

