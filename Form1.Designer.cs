namespace GSMTemperature
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс  следует удалить; иначе ложно.</param>
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.labelActive = new System.Windows.Forms.Label();
            this.labelCurrentTemp = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelLastActive = new System.Windows.Forms.Label();
            this.labelPower = new System.Windows.Forms.Label();
            this.labelMinTemp = new System.Windows.Forms.Label();
            this.labelMaxTemp = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.menuItem2);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "Отправить";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "Настройки";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // labelActive
            // 
            this.labelActive.Location = new System.Drawing.Point(6, 5);
            this.labelActive.Name = "labelActive";
            this.labelActive.Size = new System.Drawing.Size(207, 28);
            this.labelActive.Text = "Датчик: не активен";
            // 
            // labelCurrentTemp
            // 
            this.labelCurrentTemp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCurrentTemp.Font = new System.Drawing.Font("Segoe Condensed", 36F, System.Drawing.FontStyle.Bold);
            this.labelCurrentTemp.Location = new System.Drawing.Point(5, 186);
            this.labelCurrentTemp.Name = "labelCurrentTemp";
            this.labelCurrentTemp.Size = new System.Drawing.Size(231, 77);
            this.labelCurrentTemp.Text = "--.- oC";
            this.labelCurrentTemp.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(6, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(230, 28);
            this.label3.Text = "Текущая температура";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(207, 28);
            this.label4.Text = "Последняя активность:";
            // 
            // labelLastActive
            // 
            this.labelLastActive.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelLastActive.Location = new System.Drawing.Point(6, 55);
            this.labelLastActive.Name = "labelLastActive";
            this.labelLastActive.Size = new System.Drawing.Size(230, 27);
            this.labelLastActive.Text = "нет данных";
            this.labelLastActive.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelPower
            // 
            this.labelPower.Location = new System.Drawing.Point(6, 85);
            this.labelPower.Name = "labelPower";
            this.labelPower.Size = new System.Drawing.Size(230, 25);
            this.labelPower.Text = "Питание: аккумулятор";
            // 
            // labelMinTemp
            // 
            this.labelMinTemp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMinTemp.Font = new System.Drawing.Font("Segoe Condensed", 12F, System.Drawing.FontStyle.Bold);
            this.labelMinTemp.Location = new System.Drawing.Point(6, 132);
            this.labelMinTemp.Name = "labelMinTemp";
            this.labelMinTemp.Size = new System.Drawing.Size(94, 30);
            this.labelMinTemp.Text = "--.-- oC";
            this.labelMinTemp.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelMaxTemp
            // 
            this.labelMaxTemp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMaxTemp.Font = new System.Drawing.Font("Segoe Condensed", 12F, System.Drawing.FontStyle.Bold);
            this.labelMaxTemp.Location = new System.Drawing.Point(149, 132);
            this.labelMaxTemp.Name = "labelMaxTemp";
            this.labelMaxTemp.Size = new System.Drawing.Size(87, 30);
            this.labelMaxTemp.Text = "--.-- oC";
            this.labelMaxTemp.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(6, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 22);
            this.label2.Text = "Мин.";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.Location = new System.Drawing.Point(142, 110);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 22);
            this.label5.Text = "Макс.";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(131F, 131F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 266);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelMaxTemp);
            this.Controls.Add(this.labelMinTemp);
            this.Controls.Add(this.labelPower);
            this.Controls.Add(this.labelLastActive);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelCurrentTemp);
            this.Controls.Add(this.labelActive);
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "GSM Датчик температуры";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelActive;
        private System.Windows.Forms.Label labelCurrentTemp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelLastActive;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.Label labelPower;
        private System.Windows.Forms.Label labelMinTemp;
        private System.Windows.Forms.Label labelMaxTemp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
    }
}

