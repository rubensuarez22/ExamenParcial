using System;

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
            this.BTN_ESCALAR = new System.Windows.Forms.Button();
            this.PNL_LEFT = new System.Windows.Forms.Panel();
            this.BTN_1 = new System.Windows.Forms.Button();
            this.CHBX_LINES = new System.Windows.Forms.CheckBox();
            this.CHBX_ROTZ = new System.Windows.Forms.CheckBox();
            this.CHBX_ROTY = new System.Windows.Forms.CheckBox();
            this.CHBX_ROTX = new System.Windows.Forms.CheckBox();
            this.PANEL_BOTTOM = new System.Windows.Forms.Panel();
            this.LBL_STATUS = new System.Windows.Forms.Label();
            this.PNL_HEAD = new System.Windows.Forms.Panel();
            this.MAIN_PNL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PCT_CANVAS)).BeginInit();
            this.PNL_RIGHT.SuspendLayout();
            this.PNL_LEFT.SuspendLayout();
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
            this.MAIN_PNL.Margin = new System.Windows.Forms.Padding(2);
            this.MAIN_PNL.Name = "MAIN_PNL";
            this.MAIN_PNL.Size = new System.Drawing.Size(600, 366);
            this.MAIN_PNL.TabIndex = 2;
            // 
            // PCT_CANVAS
            // 
            this.PCT_CANVAS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PCT_CANVAS.Location = new System.Drawing.Point(112, 65);
            this.PCT_CANVAS.Margin = new System.Windows.Forms.Padding(2);
            this.PCT_CANVAS.Name = "PCT_CANVAS";
            this.PCT_CANVAS.Size = new System.Drawing.Size(376, 223);
            this.PCT_CANVAS.TabIndex = 0;
            this.PCT_CANVAS.TabStop = false;
            // 
            // PNL_RIGHT
            // 
            this.PNL_RIGHT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.PNL_RIGHT.Controls.Add(this.BTN_1);
            this.PNL_RIGHT.Controls.Add(this.BTN_ESCALAR);
            this.PNL_RIGHT.Dock = System.Windows.Forms.DockStyle.Right;
            this.PNL_RIGHT.Location = new System.Drawing.Point(488, 65);
            this.PNL_RIGHT.Margin = new System.Windows.Forms.Padding(2);
            this.PNL_RIGHT.Name = "PNL_RIGHT";
            this.PNL_RIGHT.Size = new System.Drawing.Size(112, 223);
            this.PNL_RIGHT.TabIndex = 4;
            // 
            // BTN_ESCALAR
            // 
            this.BTN_ESCALAR.Location = new System.Drawing.Point(5, 6);
            this.BTN_ESCALAR.Name = "BTN_ESCALAR";
            this.BTN_ESCALAR.Size = new System.Drawing.Size(75, 23);
            this.BTN_ESCALAR.TabIndex = 0;
            this.BTN_ESCALAR.Text = "Escalado";
            this.BTN_ESCALAR.UseVisualStyleBackColor = true;
            this.BTN_ESCALAR.Click += new System.EventHandler(this.BTN_ESCALAR_Click);
            // 
            // PNL_LEFT
            // 
            this.PNL_LEFT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.PNL_LEFT.Controls.Add(this.CHBX_LINES);
            this.PNL_LEFT.Controls.Add(this.CHBX_ROTZ);
            this.PNL_LEFT.Controls.Add(this.CHBX_ROTY);
            this.PNL_LEFT.Controls.Add(this.CHBX_ROTX);
            this.PNL_LEFT.Dock = System.Windows.Forms.DockStyle.Left;
            this.PNL_LEFT.Location = new System.Drawing.Point(0, 65);
            this.PNL_LEFT.Margin = new System.Windows.Forms.Padding(2);
            this.PNL_LEFT.Name = "PNL_LEFT";
            this.PNL_LEFT.Size = new System.Drawing.Size(112, 223);
            this.PNL_LEFT.TabIndex = 3;
            // 
            // BTN_1
            // 
            this.BTN_1.Location = new System.Drawing.Point(5, 35);
            this.BTN_1.Name = "BTN_1";
            this.BTN_1.Size = new System.Drawing.Size(75, 23);
            this.BTN_1.TabIndex = 5;
            this.BTN_1.Text = "Traslacion";
            this.BTN_1.UseVisualStyleBackColor = true;
            this.BTN_1.Click += new System.EventHandler(this.BTN_1_Click);
            // 
            // CHBX_LINES
            // 
            this.CHBX_LINES.AutoSize = true;
            this.CHBX_LINES.Location = new System.Drawing.Point(13, 178);
            this.CHBX_LINES.Name = "CHBX_LINES";
            this.CHBX_LINES.Size = new System.Drawing.Size(77, 17);
            this.CHBX_LINES.TabIndex = 3;
            this.CHBX_LINES.Text = "WireFrame";
            this.CHBX_LINES.UseVisualStyleBackColor = true;
            // 
            // CHBX_ROTZ
            // 
            this.CHBX_ROTZ.AutoSize = true;
            this.CHBX_ROTZ.Location = new System.Drawing.Point(13, 54);
            this.CHBX_ROTZ.Name = "CHBX_ROTZ";
            this.CHBX_ROTZ.Size = new System.Drawing.Size(53, 17);
            this.CHBX_ROTZ.TabIndex = 2;
            this.CHBX_ROTZ.Text = "Rot Z";
            this.CHBX_ROTZ.UseVisualStyleBackColor = true;
            // 
            // CHBX_ROTY
            // 
            this.CHBX_ROTY.AutoSize = true;
            this.CHBX_ROTY.Location = new System.Drawing.Point(13, 30);
            this.CHBX_ROTY.Name = "CHBX_ROTY";
            this.CHBX_ROTY.Size = new System.Drawing.Size(53, 17);
            this.CHBX_ROTY.TabIndex = 1;
            this.CHBX_ROTY.Text = "Rot Y";
            this.CHBX_ROTY.UseVisualStyleBackColor = true;
            // 
            // CHBX_ROTX
            // 
            this.CHBX_ROTX.AutoSize = true;
            this.CHBX_ROTX.Location = new System.Drawing.Point(13, 6);
            this.CHBX_ROTX.Name = "CHBX_ROTX";
            this.CHBX_ROTX.Size = new System.Drawing.Size(53, 17);
            this.CHBX_ROTX.TabIndex = 0;
            this.CHBX_ROTX.Text = "Rot X";
            this.CHBX_ROTX.UseVisualStyleBackColor = true;
            // 
            // PANEL_BOTTOM
            // 
            this.PANEL_BOTTOM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.PANEL_BOTTOM.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PANEL_BOTTOM.Location = new System.Drawing.Point(0, 288);
            this.PANEL_BOTTOM.Margin = new System.Windows.Forms.Padding(2);
            this.PANEL_BOTTOM.Name = "PANEL_BOTTOM";
            this.PANEL_BOTTOM.Size = new System.Drawing.Size(600, 65);
            this.PANEL_BOTTOM.TabIndex = 2;
            // 
            // LBL_STATUS
            // 
            this.LBL_STATUS.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.LBL_STATUS.Location = new System.Drawing.Point(0, 353);
            this.LBL_STATUS.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LBL_STATUS.Name = "LBL_STATUS";
            this.LBL_STATUS.Size = new System.Drawing.Size(600, 13);
            this.LBL_STATUS.TabIndex = 1;
            this.LBL_STATUS.Text = "label1";
            // 
            // PNL_HEAD
            // 
            this.PNL_HEAD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.PNL_HEAD.Dock = System.Windows.Forms.DockStyle.Top;
            this.PNL_HEAD.Location = new System.Drawing.Point(0, 0);
            this.PNL_HEAD.Margin = new System.Windows.Forms.Padding(2);
            this.PNL_HEAD.Name = "PNL_HEAD";
            this.PNL_HEAD.Size = new System.Drawing.Size(600, 65);
            this.PNL_HEAD.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.MAIN_PNL);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "PLAYGROUND";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.MAIN_PNL.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PCT_CANVAS)).EndInit();
            this.PNL_RIGHT.ResumeLayout(false);
            this.PNL_LEFT.ResumeLayout(false);
            this.PNL_LEFT.PerformLayout();
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
        private System.Windows.Forms.CheckBox CHBX_LINES;
        private System.Windows.Forms.CheckBox CHBX_ROTZ;
        private System.Windows.Forms.CheckBox CHBX_ROTY;
        private System.Windows.Forms.CheckBox CHBX_ROTX;
        private System.Windows.Forms.Button BTN_1;
        private System.Windows.Forms.Button BTN_ESCALAR;
    }
}

