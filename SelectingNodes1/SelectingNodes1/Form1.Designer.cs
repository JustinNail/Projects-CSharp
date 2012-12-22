namespace SelectingNodes1
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
			this.textBox_Expression = new System.Windows.Forms.TextBox();
			this.textBox_Results = new System.Windows.Forms.TextBox();
			this.label_Expression = new System.Windows.Forms.Label();
			this.label_Results = new System.Windows.Forms.Label();
			this.button_Execute = new System.Windows.Forms.Button();
			this.label_Selected = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// textBox_Expression
			// 
			this.textBox_Expression.Location = new System.Drawing.Point(30, 24);
			this.textBox_Expression.Multiline = true;
			this.textBox_Expression.Name = "textBox_Expression";
			this.textBox_Expression.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox_Expression.Size = new System.Drawing.Size(259, 38);
			this.textBox_Expression.TabIndex = 0;
			// 
			// textBox_Results
			// 
			this.textBox_Results.Location = new System.Drawing.Point(30, 131);
			this.textBox_Results.Multiline = true;
			this.textBox_Results.Name = "textBox_Results";
			this.textBox_Results.ReadOnly = true;
			this.textBox_Results.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox_Results.Size = new System.Drawing.Size(259, 133);
			this.textBox_Results.TabIndex = 1;
			// 
			// label_Expression
			// 
			this.label_Expression.AutoSize = true;
			this.label_Expression.Location = new System.Drawing.Point(27, 8);
			this.label_Expression.Name = "label_Expression";
			this.label_Expression.Size = new System.Drawing.Size(90, 13);
			this.label_Expression.TabIndex = 2;
			this.label_Expression.Text = "XPath Expression";
			// 
			// label_Results
			// 
			this.label_Results.AutoSize = true;
			this.label_Results.Location = new System.Drawing.Point(27, 115);
			this.label_Results.Name = "label_Results";
			this.label_Results.Size = new System.Drawing.Size(42, 13);
			this.label_Results.TabIndex = 3;
			this.label_Results.Text = "Results";
			// 
			// button_Execute
			// 
			this.button_Execute.Location = new System.Drawing.Point(86, 78);
			this.button_Execute.Name = "button_Execute";
			this.button_Execute.Size = new System.Drawing.Size(75, 23);
			this.button_Execute.TabIndex = 4;
			this.button_Execute.Text = "Execute";
			this.button_Execute.UseVisualStyleBackColor = true;
			this.button_Execute.Click += new System.EventHandler(this.button_Execute_Click);
			// 
			// label_Selected
			// 
			this.label_Selected.AutoSize = true;
			this.label_Selected.Location = new System.Drawing.Point(30, 281);
			this.label_Selected.Name = "label_Selected";
			this.label_Selected.Size = new System.Drawing.Size(0, 13);
			this.label_Selected.TabIndex = 5;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(333, 310);
			this.Controls.Add(this.label_Selected);
			this.Controls.Add(this.button_Execute);
			this.Controls.Add(this.label_Results);
			this.Controls.Add(this.label_Expression);
			this.Controls.Add(this.textBox_Results);
			this.Controls.Add(this.textBox_Expression);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBox_Expression;
		private System.Windows.Forms.TextBox textBox_Results;
		private System.Windows.Forms.Label label_Expression;
		private System.Windows.Forms.Label label_Results;
		private System.Windows.Forms.Button button_Execute;
		private System.Windows.Forms.Label label_Selected;
	}
}

