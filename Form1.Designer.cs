namespace ExamenParcial
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.TIMER = new System.Windows.Forms.Timer(this.components);
            this.MAIN_PNL = new System.Windows.Forms.Panel();
            this.PCT_CANVAS = new System.Windows.Forms.PictureBox();
            this.PNL_RIGHT = new System.Windows.Forms.Panel();
            this.PNL_LEFT = new System.Windows.Forms.Panel();
            this.PANEL_BOTTOM = new System.Windows.Forms.Panel();
            this.LBL_STATUS = new System.Windows.Forms.Label();
            this.PNL_HEAD = new System.Windows.Forms.Panel();
            this.MAIN_PNL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PCT_CANVAS)).BeginInit();
            this.SuspendLayout();
            // 
            // TIMER
            // 
            this.TIMER.Enabled = true;
            this.TIMER.Interval = 10;
            this.TIMER.Tick += new System.EventHandler(this.TIMER_Tick);
            // 
            // MAIN_PNL
            // 
            this.MAIN_PNL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.MAIN_PNL.Controls.Add(this.PCT_CANVAS);
            this.MAIN_PNL.Controls.Add(this.PNL_RIGHT);
            this.MAIN_PNL.Controls.Add(this.PNL_LEFT);
            this.MAIN_PNL.Controls.Add(this.PANEL_BOTTOM);
            this.MAIN_PNL.Controls.Add(this.LBL_STATUS);
            this.MAIN_PNL.Controls.Add(this.PNL_HEAD);
            this.MAIN_PNL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MAIN_PNL.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.MAIN_PNL.Location = new System.Drawing.Point(0, 0);
            this.MAIN_PNL.Name = "MAIN_PNL";
            this.MAIN_PNL.Size = new System.Drawing.Size(800, 450);
            this.MAIN_PNL.TabIndex = 2;
            // 
            // PCT_CANVAS
            // 
            this.PCT_CANVAS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PCT_CANVAS.Location = new System.Drawing.Point(150, 80);
            this.PCT_CANVAS.Name = "PCT_CANVAS";
            this.PCT_CANVAS.Size = new System.Drawing.Size(500, 274);
            this.PCT_CANVAS.TabIndex = 0;
            this.PCT_CANVAS.TabStop = false;
            // 
            // PNL_RIGHT
            // 
            this.PNL_RIGHT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.PNL_RIGHT.Dock = System.Windows.Forms.DockStyle.Right;
            this.PNL_RIGHT.Location = new System.Drawing.Point(650, 80);
            this.PNL_RIGHT.Name = "PNL_RIGHT";
            this.PNL_RIGHT.Size = new System.Drawing.Size(150, 274);
            this.PNL_RIGHT.TabIndex = 4;
            // 
            // PNL_LEFT
            // 
            this.PNL_LEFT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.PNL_LEFT.Dock = System.Windows.Forms.DockStyle.Left;
            this.PNL_LEFT.Location = new System.Drawing.Point(0, 80);
            this.PNL_LEFT.Name = "PNL_LEFT";
            this.PNL_LEFT.Size = new System.Drawing.Size(150, 274);
            this.PNL_LEFT.TabIndex = 3;
            // 
            // PANEL_BOTTOM
            // 
            this.PANEL_BOTTOM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.PANEL_BOTTOM.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PANEL_BOTTOM.Location = new System.Drawing.Point(0, 354);
            this.PANEL_BOTTOM.Name = "PANEL_BOTTOM";
            this.PANEL_BOTTOM.Size = new System.Drawing.Size(800, 80);
            this.PANEL_BOTTOM.TabIndex = 2;
            // 
            // LBL_STATUS
            // 
            this.LBL_STATUS.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.LBL_STATUS.Location = new System.Drawing.Point(0, 434);
            this.LBL_STATUS.Name = "LBL_STATUS";
            this.LBL_STATUS.Size = new System.Drawing.Size(800, 16);
            this.LBL_STATUS.TabIndex = 1;
            this.LBL_STATUS.Text = "label1";
            // 
            // PNL_HEAD
            // 
            this.PNL_HEAD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.PNL_HEAD.Dock = System.Windows.Forms.DockStyle.Top;
            this.PNL_HEAD.Location = new System.Drawing.Point(0, 0);
            this.PNL_HEAD.Name = "PNL_HEAD";
            this.PNL_HEAD.Size = new System.Drawing.Size(800, 80);
            this.PNL_HEAD.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.MAIN_PNL);
            this.Name = "Form1";
            this.Text = "PLAYGROUND";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.MAIN_PNL.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PCT_CANVAS)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer TIMER;
        private System.Windows.Forms.Panel MAIN_PNL;
        private System.Windows.Forms.Panel PNL_HEAD;
        private System.Windows.Forms.PictureBox PCT_CANVAS;
        private System.Windows.Forms.Panel PNL_RIGHT;
        private System.Windows.Forms.Panel PNL_LEFT;
        private System.Windows.Forms.Panel PANEL_BOTTOM;
        private System.Windows.Forms.Label LBL_STATUS;
    }
}

