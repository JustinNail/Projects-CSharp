using System.IO;
using System.Net;
using System.Net.Sockets;
using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

namespace ChatServer
{
	class Server
	{
		TcpListener chatServer;
		public static Hashtable nickName;
		public static Hashtable nickNameByConnect;

		public Server()
        {
            //create our nickname and nickname by connection variables
            nickName = new Hashtable(100);
            nickNameByConnect = new Hashtable(100);

			IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
			Console.WriteLine("Host Name: " + Dns.GetHostName());
			//Host's IP Address
			IPAddress ipAddress = null;
			foreach (IPAddress ip in ipHostInfo.AddressList)
			{
				if (ip.AddressFamily == AddressFamily.InterNetwork)
				{
					Console.WriteLine("IP Address {0}: {1}", ip.AddressFamily, ip);
					ipAddress = ip;
					break;
				}
			}

            //create our TCPListener object
            chatServer = new TcpListener(ipAddress,11000);
            
        }
		public void Start()
		{
			//check to see if the server is running
			//while (true) do the commands
			while (true)
			{
				//start the chat server
				chatServer.Start();
				//check if there are any pending connection requests
				if (chatServer.Pending())
				{
					//if there are pending requests create a new connection
					TcpClient chatConnection = chatServer.AcceptTcpClient();
					//display a message letting the user know they're connected
					Console.WriteLine("You are now connected");
					//create a new Communicater Object
					Communicater comm = new Communicater(chatConnection);
				}
			}
		}
		public static void SendMsgToAll(string nick, string msg)
		{
			//create a StreamWriter Object
			StreamWriter writer;
			ArrayList ToRemove = new ArrayList(0);
			//create a new TCPClient Array
			TcpClient[] tcpClient = new TcpClient[nickName.Count];
			//copy the users nickname to the CHatServer values
			nickName.Values.CopyTo(tcpClient, 0);
			//loop through and write any messages to the window
			for (int cnt = 0; cnt < tcpClient.Length; cnt++)
			{
				try
				{
					//check if the message is empty, of the particular
					//index of out array is null, if it is then continue
					if (msg.Trim() == "" || tcpClient[cnt] == null)
					{
						continue;
					}
					//Use the GetStream method to get the current memory
					//stream for this index of our TCPClient array
					writer = new StreamWriter(tcpClient[cnt].GetStream());
					//write our message to the window
					writer.WriteLine(nick + ": " + msg);
					//make sure all bytes are written
					writer.Flush();
					//dispose of the writer object until needed again
					writer = null;
				}
				//here we catch an exception that happens
				//when the user leaves the chatroom
				catch (Exception e)
				{
					string str = (string)nickNameByConnect[tcpClient[cnt]];
					//send the message that the user has left
					SendSystemMessage("** " + str + " ** Has Left The Room.");
					//remove the nickname from the list
					nickName.Remove(str);
					//remove that index of the array, thus freeing it up
					//for another user
					nickNameByConnect.Remove(tcpClient[cnt]);
				}
			}
		}
		public static void SendSystemMessage(string msg)
		{
			//create our StreamWriter object
			StreamWriter writer;
			ArrayList ToRemove = new ArrayList(0);
			//create our TcpClient array
			TcpClient[] tcpClient = new TcpClient[nickName.Count];
			//copy the nickname value to the chat servers list
			nickName.Values.CopyTo(tcpClient, 0);
			//loop through and write any messages to the window
			for (int i = 0; i < tcpClient.Length; i++)
			{
				try
				{
					//check if the message is empty, of the particular
					//index of out array is null, if it is then continue
					if (msg.Trim() == "" || tcpClient[i] == null)
					{
						continue;
					}
					//Use the GetStream method to get the current memory
					//stream for this index of our TCPClient array
					writer = new StreamWriter(tcpClient[i].GetStream());
					//send our message
					writer.WriteLine(msg);
					//make sure the buffer is empty
					writer.Flush();
					//dispose of our writer
					writer = null;
				}
				catch (Exception e)
				{
					nickName.Remove(nickNameByConnect[tcpClient[i]]);
					nickNameByConnect.Remove(tcpClient[i]);
				}
			}
		}
	}
}
