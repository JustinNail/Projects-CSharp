using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChatServer
{
	class Program
	{
		public static void Main(String[] args)
		{
			//Server server = new Server();
			//server.Start();
			AsynchronousSocketListener.StartListening();
		}
	}
}
