namespace LoadXmlNav
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
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.button_LoadTree = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// treeView1
			// 
			this.treeView1.Location = new System.Drawing.Point(12, 10);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(706, 265);
			this.treeView1.TabIndex = 0;
			// 
			// button_LoadTree
			// 
			this.button_LoadTree.Location = new System.Drawing.Point(348, 303);
			this.button_LoadTree.Name = "button_LoadTree";
			this.button_LoadTree.Size = new System.Drawing.Size(75, 23);
			this.button_LoadTree.TabIndex = 1;
			this.button_LoadTree.Text = "Load Tree";
			this.button_LoadTree.UseVisualStyleBackColor = true;
			this.button_LoadTree.Click += new System.EventHandler(this.button_LoadTree_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(731, 342);
			this.Controls.Add(this.button_LoadTree);
			this.Controls.Add(this.treeView1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.Button button_LoadTree;
	}
}

