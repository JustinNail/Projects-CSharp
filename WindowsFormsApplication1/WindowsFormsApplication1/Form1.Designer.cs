namespace WindowsFormsApplication1
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
			this.MyLabel = new System.Windows.Forms.Label();
			this.MyButton = new System.Windows.Forms.Button();
			this.MyTextBox = new System.Windows.Forms.TextBox();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			this.SuspendLayout();
			// 
			// MyLabel
			// 
			this.MyLabel.AutoSize = true;
			this.MyLabel.Font = new System.Drawing.Font("Microstyle Bold Extended ATT", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.MyLabel.Location = new System.Drawing.Point(133, 9);
			this.MyLabel.Name = "MyLabel";
			this.MyLabel.Size = new System.Drawing.Size(0, 30);
			this.MyLabel.TabIndex = 0;
			this.MyLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// MyButton
			// 
			this.MyButton.Location = new System.Drawing.Point(395, 33);
			this.MyButton.Name = "MyButton";
			this.MyButton.Size = new System.Drawing.Size(60, 37);
			this.MyButton.TabIndex = 1;
			this.MyButton.Text = "Enter";
			this.MyButton.UseVisualStyleBackColor = true;
			this.MyButton.Click += new System.EventHandler(this.MyButton_Click);
			// 
			// MyTextBox
			// 
			this.MyTextBox.Location = new System.Drawing.Point(12, 42);
			this.MyTextBox.Name = "MyTextBox";
			this.MyTextBox.Size = new System.Drawing.Size(260, 20);
			this.MyTextBox.TabIndex = 2;
			// 
			// radioButton1
			// 
			this.radioButton1.AutoSize = true;
			this.radioButton1.Location = new System.Drawing.Point(278, 45);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(46, 17);
			this.radioButton1.TabIndex = 3;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "Blue";
			this.radioButton1.UseVisualStyleBackColor = true;
			this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
			// 
			// radioButton2
			// 
			this.radioButton2.AutoSize = true;
			this.radioButton2.Location = new System.Drawing.Point(330, 45);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new System.Drawing.Size(45, 17);
			this.radioButton2.TabIndex = 4;
			this.radioButton2.TabStop = true;
			this.radioButton2.Text = "Red";
			this.radioButton2.UseVisualStyleBackColor = true;
			this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(467, 305);
			this.Controls.Add(this.radioButton2);
			this.Controls.Add(this.radioButton1);
			this.Controls.Add(this.MyTextBox);
			this.Controls.Add(this.MyButton);
			this.Controls.Add(this.MyLabel);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label MyLabel;
		private System.Windows.Forms.Button MyButton;
		private System.Windows.Forms.TextBox MyTextBox;
		private System.Windows.Forms.RadioButton radioButton1;
		private System.Windows.Forms.RadioButton radioButton2;
	}
}

