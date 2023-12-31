﻿using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RPC通讯服务器
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(new IPEndPoint(IPAddress.Parse("192.168.0.123"), 10000));
            server.Listen(1000);
            Thread t = new Thread(Execute);
            t.IsBackground = true;
            t.Start(server);
            Console.WriteLine("rpc服务器已启动");
            Console.ReadLine();

        }

        private static void Execute(object obj)
        {
            Socket server = obj as Socket;
            while (true)
            {
                Socket client = server.Accept();
                Thread t = new Thread(SingleExecute);
                t.IsBackground = true;
                t.Start(client);
            }

        }

        private static void SingleExecute(object obj)
        {
            // 读取 
            Socket client = obj as Socket;
            byte[] buffer = new byte[8192];
            int count = client.Receive(buffer);
            if (count > 0)
            {
                var data = ServiceHelpercs.Handle(buffer);
                client.Send(data);
            }
            client.Shutdown(SocketShutdown.Both);

        }
    }
}
