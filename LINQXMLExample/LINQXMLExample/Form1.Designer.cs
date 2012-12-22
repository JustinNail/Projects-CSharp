namespace LINQXMLExample
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
			this.group_Load = new System.Windows.Forms.GroupBox();
			this.button_Load = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.textBox_Load = new System.Windows.Forms.TextBox();
			this.radioButton_Internal = new System.Windows.Forms.RadioButton();
			this.radioButton_String = new System.Windows.Forms.RadioButton();
			this.radioButton_File = new System.Windows.Forms.RadioButton();
			this.group_Search = new System.Windows.Forms.GroupBox();
			this.button_Search = new System.Windows.Forms.Button();
			this.richTextBox_Results = new System.Windows.Forms.RichTextBox();
			this.textBox_Search = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.group_Tree = new System.Windows.Forms.GroupBox();
			this.button_LoadTree = new System.Windows.Forms.Button();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.comboBox_Field = new System.Windows.Forms.ComboBox();
			this.group_Load.SuspendLayout();
			this.group_Search.SuspendLayout();
			this.group_Tree.SuspendLayout();
			this.SuspendLayout();
			// 
			// group_Load
			// 
			this.group_Load.Controls.Add(this.button_Load);
			this.group_Load.Controls.Add(this.label2);
			this.group_Load.Controls.Add(this.label1);
			this.group_Load.Controls.Add(this.textBox_Load);
			this.group_Load.Controls.Add(this.radioButton_Internal);
			this.group_Load.Controls.Add(this.radioButton_String);
			this.group_Load.Controls.Add(this.radioButton_File);
			this.group_Load.Location = new System.Drawing.Point(13, 13);
			this.group_Load.Name = "group_Load";
			this.group_Load.Size = new System.Drawing.Size(315, 225);
			this.group_Load.TabIndex = 0;
			this.group_Load.TabStop = false;
			this.group_Load.Text = "Load";
			// 
			// button_Load
			// 
			this.button_Load.Location = new System.Drawing.Point(112, 193);
			this.button_Load.Name = "button_Load";
			this.button_Load.Size = new System.Drawing.Size(75, 23);
			this.button_Load.TabIndex = 6;
			this.button_Load.Text = "Load";
			this.button_Load.UseVisualStyleBackColor = true;
			this.button_Load.Click += new System.EventHandler(this.button_Load_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(21, 20);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(81, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Open XML from";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(21, 122);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(99, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Enter Path or String";
			// 
			// textBox_Load
			// 
			this.textBox_Load.Location = new System.Drawing.Point(21, 141);
			this.textBox_Load.Multiline = true;
			this.textBox_Load.Name = "textBox_Load";
			this.textBox_Load.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox_Load.Size = new System.Drawing.Size(272, 46);
			this.textBox_Load.TabIndex = 3;
			// 
			// radioButton_Internal
			// 
			this.radioButton_Internal.AutoSize = true;
			this.radioButton_Internal.Location = new System.Drawing.Point(24, 91);
			this.radioButton_Internal.Name = "radioButton_Internal";
			this.radioButton_Internal.Size = new System.Drawing.Size(97, 17);
			this.radioButton_Internal.TabIndex = 2;
			this.radioButton_Internal.TabStop = true;
			this.radioButton_Internal.Text = "Internal Source";
			this.radioButton_Internal.UseVisualStyleBackColor = true;
			// 
			// radioButton_String
			// 
			this.radioButton_String.AutoSize = true;
			this.radioButton_String.Location = new System.Drawing.Point(24, 67);
			this.radioButton_String.Name = "radioButton_String";
			this.radioButton_String.Size = new System.Drawing.Size(52, 17);
			this.radioButton_String.TabIndex = 1;
			this.radioButton_String.TabStop = true;
			this.radioButton_String.Text = "String";
			this.radioButton_String.UseVisualStyleBackColor = true;
			// 
			// radioButton_File
			// 
			this.radioButton_File.AutoSize = true;
			this.radioButton_File.Location = new System.Drawing.Point(24, 43);
			this.radioButton_File.Name = "radioButton_File";
			this.radioButton_File.Size = new System.Drawing.Size(41, 17);
			this.radioButton_File.TabIndex = 0;
			this.radioButton_File.TabStop = true;
			this.radioButton_File.Text = "File";
			this.radioButton_File.UseVisualStyleBackColor = true;
			// 
			// group_Search
			// 
			this.group_Search.Controls.Add(this.comboBox_Field);
			this.group_Search.Controls.Add(this.button_Search);
			this.group_Search.Controls.Add(this.richTextBox_Results);
			this.group_Search.Controls.Add(this.textBox_Search);
			this.group_Search.Controls.Add(this.label4);
			this.group_Search.Controls.Add(this.label3);
			this.group_Search.Location = new System.Drawing.Point(13, 258);
			this.group_Search.Name = "group_Search";
			this.group_Search.Size = new System.Drawing.Size(315, 177);
			this.group_Search.TabIndex = 1;
			this.group_Search.TabStop = false;
			this.group_Search.Text = "Search";
			// 
			// button_Search
			// 
			this.button_Search.Enabled = false;
			this.button_Search.Location = new System.Drawing.Point(218, 49);
			this.button_Search.Name = "button_Search";
			this.button_Search.Size = new System.Drawing.Size(75, 23);
			this.button_Search.TabIndex = 5;
			this.button_Search.Text = "Search";
			this.button_Search.UseVisualStyleBackColor = true;
			this.button_Search.Click += new System.EventHandler(this.button_Search_Click);
			// 
			// richTextBox_Results
			// 
			this.richTextBox_Results.Location = new System.Drawing.Point(21, 102);
			this.richTextBox_Results.Name = "richTextBox_Results";
			this.richTextBox_Results.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.richTextBox_Results.Size = new System.Drawing.Size(272, 60);
			this.richTextBox_Results.TabIndex = 4;
			this.richTextBox_Results.Text = "";
			// 
			// textBox_Search
			// 
			this.textBox_Search.Location = new System.Drawing.Point(24, 76);
			this.textBox_Search.Name = "textBox_Search";
			this.textBox_Search.Size = new System.Drawing.Size(100, 20);
			this.textBox_Search.TabIndex = 3;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(21, 59);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(102, 13);
			this.label4.TabIndex = 2;
			this.label4.Text = "Enter text for search";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(21, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(106, 13);
			this.label3.TabIndex = 1;
			this.label3.Text = "Select field to search";
			// 
			// group_Tree
			// 
			this.group_Tree.Controls.Add(this.button_LoadTree);
			this.group_Tree.Controls.Add(this.treeView1);
			this.group_Tree.Location = new System.Drawing.Point(426, 13);
			this.group_Tree.Name = "group_Tree";
			this.group_Tree.Size = new System.Drawing.Size(314, 225);
			this.group_Tree.TabIndex = 3;
			this.group_Tree.TabStop = false;
			this.group_Tree.Text = "Tree Display";
			// 
			// button_LoadTree
			// 
			this.button_LoadTree.Enabled = false;
			this.button_LoadTree.Location = new System.Drawing.Point(142, 193);
			this.button_LoadTree.Name = "button_LoadTree";
			this.button_LoadTree.Size = new System.Drawing.Size(75, 23);
			this.button_LoadTree.TabIndex = 1;
			this.button_LoadTree.Text = "Load Tree";
			this.button_LoadTree.UseVisualStyleBackColor = true;
			this.button_LoadTree.Click += new System.EventHandler(this.button_LoadTree_Click);
			// 
			// treeView1
			// 
			this.treeView1.Location = new System.Drawing.Point(21, 20);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(278, 161);
			this.treeView1.TabIndex = 0;
			// 
			// comboBox_Field
			// 
			this.comboBox_Field.FormattingEnabled = true;
			this.comboBox_Field.Location = new System.Drawing.Point(24, 33);
			this.comboBox_Field.Name = "comboBox_Field";
			this.comboBox_Field.Size = new System.Drawing.Size(121, 21);
			this.comboBox_Field.TabIndex = 6;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(767, 447);
			this.Controls.Add(this.group_Tree);
			this.Controls.Add(this.group_Search);
			this.Controls.Add(this.group_Load);
			this.Name = "Form1";
			this.Text = "Form1";
			this.group_Load.ResumeLayout(false);
			this.group_Load.PerformLayout();
			this.group_Search.ResumeLayout(false);
			this.group_Search.PerformLayout();
			this.group_Tree.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox group_Load;
		private System.Windows.Forms.Button button_Load;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox_Load;
		private System.Windows.Forms.RadioButton radioButton_Internal;
		private System.Windows.Forms.RadioButton radioButton_String;
		private System.Windows.Forms.RadioButton radioButton_File;
		private System.Windows.Forms.GroupBox group_Search;
		private System.Windows.Forms.Button button_Search;
		private System.Windows.Forms.RichTextBox richTextBox_Results;
		private System.Windows.Forms.TextBox textBox_Search;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox group_Tree;
		private System.Windows.Forms.Button button_LoadTree;
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.ComboBox comboBox_Field;
	}
}

