using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections;

namespace ChatClientForm
{
	public partial class Form1 : Form
	{
		private TcpClient tcpClient=null;
		public Form1()
		{
			InitializeComponent();
		}

		private void button_Connect_Click(object sender, EventArgs e)
		{
			IPAddress ip=null;
			if (!IPAddress.TryParse(textBox_IP.Text,out ip))
			{
				MessageBox.Show("Invalid IP!");
				return;
			}
			tcpClient = new TcpClient();
			tcpClient.Connect(ip, 11000);
			run();
		}

		private void button_Disconnect_Click(object sender, EventArgs e)
		{
			//create a StreamWriter based on the current NetworkStream
			StreamWriter writer = new StreamWriter(tcpClient.GetStream());
			//write our message
			writer.WriteLine("disconnected");
			//ensure the buffer is empty
			writer.Flush();

			tcpClient.Close();
		}
		private void run()
		{
			//create our StreamReader Object, based on the current NetworkStream
			StreamReader reader = new StreamReader(tcpClient.GetStream());
			while (true)
			{
				//call DoEvents so other processes can process
				//simultaneously
				Application.DoEvents();
				//append the current value in the
				RichTextBox chat = richTextBox_Chat;
				//current NetworkStream to the chat window
				chat.AppendText(reader.ReadLine() + "\r\n");
			}
		}

		private void button_Send_Click(object sender, EventArgs e)
		{
			if (tcpClient == null)
			{
				MessageBox.Show("Connect First!");
				return;
			}
			//create a StreamWriter based on the current NetworkStream
			StreamWriter writer = new StreamWriter(tcpClient.GetStream());
			//write our message
			writer.WriteLine(textBox_In.Text);
			//ensure the buffer is empty
			writer.Flush();
			//clear the textbox for our next message
			textBox_In.Text = "";
		}

	}
}
