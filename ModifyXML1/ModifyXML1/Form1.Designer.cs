namespace ModifyXML1
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.textBox_FirstName = new System.Windows.Forms.TextBox();
			this.combo_EmployeeID = new System.Windows.Forms.ComboBox();
			this.textBox_LastName = new System.Windows.Forms.TextBox();
			this.textBox_Phone = new System.Windows.Forms.TextBox();
			this.textBox_Notes = new System.Windows.Forms.TextBox();
			this.button_Add = new System.Windows.Forms.Button();
			this.button_Update = new System.Windows.Forms.Button();
			this.button_Delete = new System.Windows.Forms.Button();
			this.button_First = new System.Windows.Forms.Button();
			this.button_Prev = new System.Windows.Forms.Button();
			this.button_Last = new System.Windows.Forms.Button();
			this.button_Next = new System.Windows.Forms.Button();
			this.label_Count = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(43, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(67, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Employee ID";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(43, 58);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(57, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "First Name";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(43, 91);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(58, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Last Name";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(43, 124);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(69, 13);
			this.label4.TabIndex = 3;
			this.label4.Text = "Home Phone";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(43, 157);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(35, 13);
			this.label5.TabIndex = 4;
			this.label5.Text = "Notes";
			// 
			// textBox_FirstName
			// 
			this.textBox_FirstName.Location = new System.Drawing.Point(153, 58);
			this.textBox_FirstName.Name = "textBox_FirstName";
			this.textBox_FirstName.Size = new System.Drawing.Size(185, 20);
			this.textBox_FirstName.TabIndex = 5;
			// 
			// combo_EmployeeID
			// 
			this.combo_EmployeeID.FormattingEnabled = true;
			this.combo_EmployeeID.Location = new System.Drawing.Point(153, 24);
			this.combo_EmployeeID.Name = "combo_EmployeeID";
			this.combo_EmployeeID.Size = new System.Drawing.Size(121, 21);
			this.combo_EmployeeID.TabIndex = 6;
			this.combo_EmployeeID.SelectedIndexChanged += new System.EventHandler(this.combo_EmployeeID_SelectedIndexChanged);
			// 
			// textBox_LastName
			// 
			this.textBox_LastName.Location = new System.Drawing.Point(153, 91);
			this.textBox_LastName.Name = "textBox_LastName";
			this.textBox_LastName.Size = new System.Drawing.Size(185, 20);
			this.textBox_LastName.TabIndex = 7;
			// 
			// textBox_Phone
			// 
			this.textBox_Phone.Location = new System.Drawing.Point(153, 124);
			this.textBox_Phone.Name = "textBox_Phone";
			this.textBox_Phone.Size = new System.Drawing.Size(185, 20);
			this.textBox_Phone.TabIndex = 8;
			// 
			// textBox_Notes
			// 
			this.textBox_Notes.Location = new System.Drawing.Point(153, 157);
			this.textBox_Notes.Multiline = true;
			this.textBox_Notes.Name = "textBox_Notes";
			this.textBox_Notes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox_Notes.Size = new System.Drawing.Size(185, 134);
			this.textBox_Notes.TabIndex = 9;
			// 
			// button_Add
			// 
			this.button_Add.Location = new System.Drawing.Point(26, 324);
			this.button_Add.Name = "button_Add";
			this.button_Add.Size = new System.Drawing.Size(75, 23);
			this.button_Add.TabIndex = 10;
			this.button_Add.Text = "Add";
			this.button_Add.UseVisualStyleBackColor = true;
			this.button_Add.Click += new System.EventHandler(this.button_Add_Click);
			// 
			// button_Update
			// 
			this.button_Update.Location = new System.Drawing.Point(144, 324);
			this.button_Update.Name = "button_Update";
			this.button_Update.Size = new System.Drawing.Size(75, 23);
			this.button_Update.TabIndex = 11;
			this.button_Update.Text = "Update";
			this.button_Update.UseVisualStyleBackColor = true;
			this.button_Update.Click += new System.EventHandler(this.button_Update_Click);
			// 
			// button_Delete
			// 
			this.button_Delete.Location = new System.Drawing.Point(262, 324);
			this.button_Delete.Name = "button_Delete";
			this.button_Delete.Size = new System.Drawing.Size(75, 23);
			this.button_Delete.TabIndex = 12;
			this.button_Delete.Text = "Delete";
			this.button_Delete.UseVisualStyleBackColor = true;
			this.button_Delete.Click += new System.EventHandler(this.button_Delete_Click);
			// 
			// button_First
			// 
			this.button_First.Location = new System.Drawing.Point(12, 394);
			this.button_First.Name = "button_First";
			this.button_First.Size = new System.Drawing.Size(41, 23);
			this.button_First.TabIndex = 13;
			this.button_First.Text = "<<";
			this.button_First.UseVisualStyleBackColor = true;
			this.button_First.Click += new System.EventHandler(this.button_First_Click);
			// 
			// button_Prev
			// 
			this.button_Prev.Location = new System.Drawing.Point(60, 394);
			this.button_Prev.Name = "button_Prev";
			this.button_Prev.Size = new System.Drawing.Size(41, 23);
			this.button_Prev.TabIndex = 14;
			this.button_Prev.Text = "<";
			this.button_Prev.UseVisualStyleBackColor = true;
			this.button_Prev.Click += new System.EventHandler(this.button_Prev_Click);
			// 
			// button_Last
			// 
			this.button_Last.Location = new System.Drawing.Point(339, 394);
			this.button_Last.Name = "button_Last";
			this.button_Last.Size = new System.Drawing.Size(41, 23);
			this.button_Last.TabIndex = 15;
			this.button_Last.Text = ">>";
			this.button_Last.UseVisualStyleBackColor = true;
			this.button_Last.Click += new System.EventHandler(this.button_Last_Click);
			// 
			// button_Next
			// 
			this.button_Next.Location = new System.Drawing.Point(292, 394);
			this.button_Next.Name = "button_Next";
			this.button_Next.Size = new System.Drawing.Size(41, 23);
			this.button_Next.TabIndex = 16;
			this.button_Next.Text = ">";
			this.button_Next.UseVisualStyleBackColor = true;
			this.button_Next.Click += new System.EventHandler(this.button_Next_Click);
			// 
			// label_Count
			// 
			this.label_Count.AutoSize = true;
			this.label_Count.Location = new System.Drawing.Point(175, 399);
			this.label_Count.Name = "label_Count";
			this.label_Count.Size = new System.Drawing.Size(0, 13);
			this.label_Count.TabIndex = 17;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(392, 429);
			this.Controls.Add(this.label_Count);
			this.Controls.Add(this.button_Next);
			this.Controls.Add(this.button_Last);
			this.Controls.Add(this.button_Prev);
			this.Controls.Add(this.button_First);
			this.Controls.Add(this.button_Delete);
			this.Controls.Add(this.button_Update);
			this.Controls.Add(this.button_Add);
			this.Controls.Add(this.textBox_Notes);
			this.Controls.Add(this.textBox_Phone);
			this.Controls.Add(this.textBox_LastName);
			this.Controls.Add(this.combo_EmployeeID);
			this.Controls.Add(this.textBox_FirstName);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "Form1";
			this.Text = "Employee Records";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBox_FirstName;
		private System.Windows.Forms.ComboBox combo_EmployeeID;
		private System.Windows.Forms.TextBox textBox_LastName;
		private System.Windows.Forms.TextBox textBox_Phone;
		private System.Windows.Forms.TextBox textBox_Notes;
		private System.Windows.Forms.Button button_Add;
		private System.Windows.Forms.Button button_Update;
		private System.Windows.Forms.Button button_Delete;
		private System.Windows.Forms.Button button_First;
		private System.Windows.Forms.Button button_Prev;
		private System.Windows.Forms.Button button_Last;
		private System.Windows.Forms.Button button_Next;
		private System.Windows.Forms.Label label_Count;
	}
}

