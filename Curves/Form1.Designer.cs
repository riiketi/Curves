namespace Curves
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.Browse_btn = new System.Windows.Forms.Button();
            this.M_tb = new System.Windows.Forms.TextBox();
            this.N_tb = new System.Windows.Forms.TextBox();
            this.M_label = new System.Windows.Forms.Label();
            this.N_label = new System.Windows.Forms.Label();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.SuspendLayout();
            // 
            // Browse_btn
            // 
            this.Browse_btn.Location = new System.Drawing.Point(10, 6);
            this.Browse_btn.Name = "Browse_btn";
            this.Browse_btn.Size = new System.Drawing.Size(150, 20);
            this.Browse_btn.TabIndex = 0;
            this.Browse_btn.Text = "Обзор";
            this.Browse_btn.UseVisualStyleBackColor = true;
            this.Browse_btn.Click += new System.EventHandler(this.Browse_btn_Click);
            // 
            // M_tb
            // 
            this.M_tb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.M_tb.Location = new System.Drawing.Point(310, 6);
            this.M_tb.Name = "M_tb";
            this.M_tb.ReadOnly = true;
            this.M_tb.Size = new System.Drawing.Size(395, 20);
            this.M_tb.TabIndex = 1;
            // 
            // N_tb
            // 
            this.N_tb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.N_tb.Location = new System.Drawing.Point(830, 6);
            this.N_tb.Name = "N_tb";
            this.N_tb.ReadOnly = true;
            this.N_tb.Size = new System.Drawing.Size(160, 20);
            this.N_tb.TabIndex = 2;
            // 
            // M_label
            // 
            this.M_label.AutoSize = true;
            this.M_label.Location = new System.Drawing.Point(200, 9);
            this.M_label.Name = "M_label";
            this.M_label.Size = new System.Drawing.Size(101, 13);
            this.M_label.TabIndex = 3;
            this.M_label.Text = "Количество строк:";
            // 
            // N_label
            // 
            this.N_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.N_label.AutoSize = true;
            this.N_label.Location = new System.Drawing.Point(720, 9);
            this.N_label.Name = "N_label";
            this.N_label.Size = new System.Drawing.Size(109, 13);
            this.N_label.TabIndex = 4;
            this.N_label.Text = "Количество кривых:";
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.zedGraphControl1.AutoScroll = true;
            this.zedGraphControl1.AutoSize = true;
            this.zedGraphControl1.IsAutoScrollRange = true;
            this.zedGraphControl1.IsShowHScrollBar = true;
            this.zedGraphControl1.IsShowVScrollBar = true;
            this.zedGraphControl1.IsSynchronizeXAxes = true;
            this.zedGraphControl1.IsSynchronizeYAxes = true;
            this.zedGraphControl1.IsZoomOnMouseCenter = true;
            this.zedGraphControl1.Location = new System.Drawing.Point(12, 32);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(984, 418);
            this.zedGraphControl1.TabIndex = 5;
            this.zedGraphControl1.UseExtendedPrintDialog = true;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1008, 462);
            this.Controls.Add(this.zedGraphControl1);
            this.Controls.Add(this.N_label);
            this.Controls.Add(this.M_label);
            this.Controls.Add(this.N_tb);
            this.Controls.Add(this.M_tb);
            this.Controls.Add(this.Browse_btn);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Browse_btn;
        private System.Windows.Forms.TextBox M_tb;
        private System.Windows.Forms.TextBox N_tb;
        private System.Windows.Forms.Label M_label;
        private System.Windows.Forms.Label N_label;
        private ZedGraph.ZedGraphControl zedGraphControl1;
    }
}

