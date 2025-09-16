namespace BrewSimGUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label labelWashed;
        private System.Windows.Forms.Label labelFilled;
        private System.Windows.Forms.Label labelTopped;
        private System.Windows.Forms.Label labelBoxed;
        private System.Windows.Forms.Label labelTitle;

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
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelWashed = new System.Windows.Forms.Label();
            this.labelFilled = new System.Windows.Forms.Label();
            this.labelTopped = new System.Windows.Forms.Label();
            this.labelBoxed = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.labelTitle.Location = new System.Drawing.Point(20, 20);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(415, 45);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Brewery Simulation Status";
            // 
            // labelWashed
            // 
            this.labelWashed.AutoSize = true;
            this.labelWashed.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.labelWashed.Location = new System.Drawing.Point(40, 80);
            this.labelWashed.Name = "labelWashed";
            this.labelWashed.Size = new System.Drawing.Size(195, 32);
            this.labelWashed.TabIndex = 1;
            this.labelWashed.Text = "Washed Buffer: 0";
            // 
            // labelFilled
            // 
            this.labelFilled.AutoSize = true;
            this.labelFilled.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.labelFilled.Location = new System.Drawing.Point(40, 120);
            this.labelFilled.Name = "labelFilled";
            this.labelFilled.Size = new System.Drawing.Size(168, 32);
            this.labelFilled.TabIndex = 2;
            this.labelFilled.Text = "Filled Buffer: 0";
            // 
            // labelTopped
            // 
            this.labelTopped.AutoSize = true;
            this.labelTopped.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.labelTopped.Location = new System.Drawing.Point(40, 160);
            this.labelTopped.Name = "labelTopped";
            this.labelTopped.Size = new System.Drawing.Size(191, 32);
            this.labelTopped.TabIndex = 3;
            this.labelTopped.Text = "Topped Buffer: 0";
            // 
            // labelBoxed
            // 
            this.labelBoxed.AutoSize = true;
            this.labelBoxed.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.labelBoxed.Location = new System.Drawing.Point(40, 200);
            this.labelBoxed.Name = "labelBoxed";
            this.labelBoxed.Size = new System.Drawing.Size(177, 32);
            this.labelBoxed.TabIndex = 4;
            this.labelBoxed.Text = "Boxed Buffer: 0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 277);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.labelWashed);
            this.Controls.Add(this.labelFilled);
            this.Controls.Add(this.labelTopped);
            this.Controls.Add(this.labelBoxed);
            this.Name = "Form1";
            this.Text = "Brewery Simulation";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}