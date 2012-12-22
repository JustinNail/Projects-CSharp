namespace ChatClientForm
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
			this.richTextBox_Chat = new System.Windows.Forms.RichTextBox();
			this.textBox_In = new System.Windows.Forms.TextBox();
			this.textBox_IP = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.button_Connect = new System.Windows.Forms.Button();
			this.button_Send = new System.Windows.Forms.Button();
			this.button_Disconnect = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// richTextBox_Chat
			// 
			this.richTextBox_Chat.Location = new System.Drawing.Point(2, 4);
			this.richTextBox_Chat.Name = "richTextBox_Chat";
			this.richTextBox_Chat.Size = new System.Drawing.Size(535, 312);
			this.richTextBox_Chat.TabIndex = 0;
			this.richTextBox_Chat.Text = "";
			// 
			// textBox_In
			// 
			this.textBox_In.Location = new System.Drawing.Point(2, 322);
			this.textBox_In.Name = "textBox_In";
			this.textBox_In.Size = new System.Drawing.Size(620, 20);
			this.textBox_In.TabIndex = 1;
			// 
			// textBox_IP
			// 
			this.textBox_IP.Location = new System.Drawing.Point(547, 33);
			this.textBox_IP.Name = "textBox_IP";
			this.textBox_IP.Size = new System.Drawing.Size(158, 20);
			this.textBox_IP.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(544, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(61, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "IP Address:";
			// 
			// button_Connect
			// 
			this.button_Connect.Location = new System.Drawing.Point(547, 59);
			this.button_Connect.Name = "button_Connect";
			this.button_Connect.Size = new System.Drawing.Size(75, 23);
			this.button_Connect.TabIndex = 4;
			this.button_Connect.Text = "Connect";
			this.button_Connect.UseVisualStyleBackColor = true;
			this.button_Connect.Click += new System.EventHandler(this.button_Connect_Click);
			// 
			// button_Send
			// 
			this.button_Send.Location = new System.Drawing.Point(547, 293);
			this.button_Send.Name = "button_Send";
			this.button_Send.Size = new System.Drawing.Size(75, 23);
			this.button_Send.TabIndex = 5;
			this.button_Send.Text = "Send";
			this.button_Send.UseVisualStyleBackColor = true;
			this.button_Send.Click += new System.EventHandler(this.button_Send_Click);
			// 
			// button_Disconnect
			// 
			this.button_Disconnect.Location = new System.Drawing.Point(628, 59);
			this.button_Disconnect.Name = "button_Disconnect";
			this.button_Disconnect.Size = new System.Drawing.Size(75, 23);
			this.button_Disconnect.TabIndex = 6;
			this.button_Disconnect.Text = "Disconnect";
			this.button_Disconnect.UseVisualStyleBackColor = true;
			this.button_Disconnect.Click += new System.EventHandler(this.button_Disconnect_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(711, 345);
			this.Controls.Add(this.button_Disconnect);
			this.Controls.Add(this.button_Send);
			this.Controls.Add(this.button_Connect);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBox_IP);
			this.Controls.Add(this.textBox_In);
			this.Controls.Add(this.richTextBox_Chat);
			this.Name = "Form1";
			this.Text = "Chat Client";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.RichTextBox richTextBox_Chat;
		private System.Windows.Forms.TextBox textBox_In;
		private System.Windows.Forms.TextBox textBox_IP;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button_Connect;
		private System.Windows.Forms.Button button_Send;
		private System.Windows.Forms.Button button_Disconnect;
	}
}

