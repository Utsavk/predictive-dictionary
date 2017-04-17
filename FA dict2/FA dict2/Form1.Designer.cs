namespace FA_dict2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.searchbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.predictionbox = new System.Windows.Forms.ListBox();
            this.gobutton = new System.Windows.Forms.Button();
            this.Mean_box = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            this.SuspendLayout();
            // 
            // searchbox
            // 
            this.searchbox.BackColor = System.Drawing.Color.White;
            this.searchbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchbox.ForeColor = System.Drawing.Color.Black;
            this.searchbox.Location = new System.Drawing.Point(78, 259);
            this.searchbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.searchbox.Name = "searchbox";
            this.searchbox.Size = new System.Drawing.Size(240, 31);
            this.searchbox.TabIndex = 1;
            this.searchbox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.searchbox_MouseClick);
            this.searchbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchbox_KeyDown_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(106, 298);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 22);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            this.label1.Visible = false;
            // 
            // predictionbox
            // 
            this.predictionbox.BackColor = System.Drawing.Color.SlateBlue;
            this.predictionbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.predictionbox.FormattingEnabled = true;
            this.predictionbox.ItemHeight = 25;
            this.predictionbox.Location = new System.Drawing.Point(78, 324);
            this.predictionbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.predictionbox.Name = "predictionbox";
            this.predictionbox.Size = new System.Drawing.Size(240, 154);
            this.predictionbox.TabIndex = 5;
            this.predictionbox.Visible = false;
            this.predictionbox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.predictionbox_MouseClick);
            this.predictionbox.SelectedIndexChanged += new System.EventHandler(this.predictionbox_SelectedIndexChanged);
            // 
            // gobutton
            // 
            this.gobutton.BackColor = System.Drawing.Color.SlateBlue;
            this.gobutton.ForeColor = System.Drawing.Color.White;
            this.gobutton.Location = new System.Drawing.Point(325, 259);
            this.gobutton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gobutton.Name = "gobutton";
            this.gobutton.Size = new System.Drawing.Size(73, 35);
            this.gobutton.TabIndex = 6;
            this.gobutton.Text = "GO";
            this.gobutton.UseVisualStyleBackColor = false;
            this.gobutton.Click += new System.EventHandler(this.gobutton_Click);
            // 
            // Mean_box
            // 
            this.Mean_box.AutoSize = true;
            this.Mean_box.Font = new System.Drawing.Font("Lucida Handwriting", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Mean_box.ForeColor = System.Drawing.Color.Blue;
            this.Mean_box.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.Mean_box.Location = new System.Drawing.Point(50, 324);
            this.Mean_box.Name = "Mean_box";
            this.Mean_box.Size = new System.Drawing.Size(0, 21);
            this.Mean_box.TabIndex = 7;
            this.Mean_box.Visible = false;
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::FA_dict2.Properties.Resources.speaker;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Location = new System.Drawing.Point(16, 251);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 50);
            this.button1.TabIndex = 8;
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(78, 520);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(246, 46);
            this.axWindowsMediaPlayer1.TabIndex = 9;
            this.axWindowsMediaPlayer1.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(412, 590);
            this.Controls.Add(this.axWindowsMediaPlayer1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Mean_box);
            this.Controls.Add(this.gobutton);
            this.Controls.Add(this.predictionbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.searchbox);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox searchbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox predictionbox;
        private System.Windows.Forms.Button gobutton;
        private System.Windows.Forms.Label Mean_box;
        private System.Windows.Forms.Button button1;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
    }
}

