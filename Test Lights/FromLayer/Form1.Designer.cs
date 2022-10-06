namespace FromLayer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnReset = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btnFadeAllLights = new System.Windows.Forms.Button();
            this.btnFadeLight = new System.Windows.Forms.Button();
            this.btnOn = new System.Windows.Forms.Button();
            this.btnOff = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(57, 276);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(150, 29);
            this.btnReset.TabIndex = 0;
            this.btnReset.Text = "Reset All Lights";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Location = new System.Drawing.Point(57, 44);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(238, 184);
            this.listBox1.TabIndex = 1;
            // 
            // btnFadeAllLights
            // 
            this.btnFadeAllLights.Location = new System.Drawing.Point(396, 44);
            this.btnFadeAllLights.Name = "btnFadeAllLights";
            this.btnFadeAllLights.Size = new System.Drawing.Size(150, 29);
            this.btnFadeAllLights.TabIndex = 2;
            this.btnFadeAllLights.Text = "Fade All Lights";
            this.btnFadeAllLights.UseVisualStyleBackColor = true;
            this.btnFadeAllLights.Click += new System.EventHandler(this.btnFadeAllLights_Click);
            // 
            // btnFadeLight
            // 
            this.btnFadeLight.Location = new System.Drawing.Point(396, 98);
            this.btnFadeLight.Name = "btnFadeLight";
            this.btnFadeLight.Size = new System.Drawing.Size(150, 29);
            this.btnFadeLight.TabIndex = 3;
            this.btnFadeLight.Text = "Fade Selected Light";
            this.btnFadeLight.UseVisualStyleBackColor = true;
            this.btnFadeLight.Click += new System.EventHandler(this.btnFadeLight_Click);
            // 
            // btnOn
            // 
            this.btnOn.Location = new System.Drawing.Point(396, 151);
            this.btnOn.Name = "btnOn";
            this.btnOn.Size = new System.Drawing.Size(150, 29);
            this.btnOn.TabIndex = 4;
            this.btnOn.Text = "Turn Light On";
            this.btnOn.UseVisualStyleBackColor = true;
            this.btnOn.Click += new System.EventHandler(this.btnOn_Click);
            // 
            // btnOff
            // 
            this.btnOff.Location = new System.Drawing.Point(396, 199);
            this.btnOff.Name = "btnOff";
            this.btnOff.Size = new System.Drawing.Size(150, 29);
            this.btnOff.TabIndex = 5;
            this.btnOff.Text = "Turn Light Off";
            this.btnOff.UseVisualStyleBackColor = true;
            this.btnOff.Click += new System.EventHandler(this.btnOff_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnOff);
            this.Controls.Add(this.btnOn);
            this.Controls.Add(this.btnFadeLight);
            this.Controls.Add(this.btnFadeAllLights);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btnReset);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Button btnReset;
        private ListBox listBox1;
        private Button btnFadeAllLights;
        private Button btnFadeLight;
        private Button btnOn;
        private Button btnOff;
    }
}