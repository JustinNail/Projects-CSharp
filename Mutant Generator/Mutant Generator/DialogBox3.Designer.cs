namespace Dark_Heresy_Generator
{
	partial class DialogBox3
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
			this.Choice1_Button = new System.Windows.Forms.Button();
			this.Choice2_Button = new System.Windows.Forms.Button();
			this.Choice3_Button = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// Choice1_Button
			// 
			this.Choice1_Button.DialogResult = System.Windows.Forms.DialogResult.Yes;
			this.Choice1_Button.Location = new System.Drawing.Point(12, 12);
			this.Choice1_Button.Name = "Choice1_Button";
			this.Choice1_Button.Size = new System.Drawing.Size(234, 23);
			this.Choice1_Button.TabIndex = 1;
			this.Choice1_Button.UseVisualStyleBackColor = true;
			// 
			// Choice2_Button
			// 
			this.Choice2_Button.DialogResult = System.Windows.Forms.DialogResult.No;
			this.Choice2_Button.Location = new System.Drawing.Point(12, 41);
			this.Choice2_Button.Name = "Choice2_Button";
			this.Choice2_Button.Size = new System.Drawing.Size(234, 23);
			this.Choice2_Button.TabIndex = 2;
			this.Choice2_Button.UseVisualStyleBackColor = true;
			// 
			// Choice3_Button
			// 
			this.Choice3_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Choice3_Button.Location = new System.Drawing.Point(12, 70);
			this.Choice3_Button.Name = "Choice3_Button";
			this.Choice3_Button.Size = new System.Drawing.Size(234, 23);
			this.Choice3_Button.TabIndex = 3;
			this.Choice3_Button.UseVisualStyleBackColor = true;
			// 
			// DialogBox3
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(258, 108);
			this.ControlBox = false;
			this.Controls.Add(this.Choice3_Button);
			this.Controls.Add(this.Choice2_Button);
			this.Controls.Add(this.Choice1_Button);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DialogBox3";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Choice";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button Choice1_Button;
		private System.Windows.Forms.Button Choice2_Button;
		private System.Windows.Forms.Button Choice3_Button;
	}
}