﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChatClient
{
	class Program
	{
		static void Main(string[] args)
		{
			//Client client = new Client();
			AsynchronousClient.StartClient();
		}
	}
}