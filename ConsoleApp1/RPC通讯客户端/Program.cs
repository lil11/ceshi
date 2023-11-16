using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RPC通讯客户端
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("RCP客户端启动");
            MyServiceProxy service = new MyServiceProxy();
            DateTime startTime = DateTime.Now;
            int result = service.Call1(2, 2);
            int min_seconds = (int)(DateTime.Now - startTime).TotalMilliseconds;
            Console.WriteLine("Fun1=resule:"+result + " 耗时 " + min_seconds+"ms");
            bool result1 = service.Call2("text","123");
            Console.WriteLine("Fun2=resule:" + result1);
            Console.WriteLine("OVER!");
            Console.ReadLine();
        }
    }
    public class MyServiceProxy : IMyTestService
    {
        public int Call1(int x, int y)
        {
            List<ArgInfo> argList = new List<ArgInfo>();
            argList.Add(new ArgInfo(TypeEnu.Int, x));
            argList.Add(new ArgInfo(TypeEnu.Int, y));
            byte[] send_data = create_send_package("IMyTestService", "Call1", 2, TypeEnu.Int, argList);
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(new IPEndPoint(IPAddress.Parse("192.168.0.123"), 10000));
            client.Send(send_data);
            byte[] buffer = new byte[4];
            int count = client.Receive(buffer);
            if (count > 0)
            {
                return bytes2int(buffer);
            }
            throw new Exception("系统异常");
        }
        public bool Call2(string name, string pwd)
        {
            List<ArgInfo> argList = new List<ArgInfo>();
            argList.Add(new ArgInfo(TypeEnu.String, name));
            argList.Add(new ArgInfo(TypeEnu.String, pwd));
            byte[] send_data = create_send_package("IMyTestService", "Call2", 2, TypeEnu.Bool, argList);
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(new IPEndPoint(IPAddress.Parse("192.168.0.123"), 10000));
            client.Send(send_data);
            byte[] buffer = new byte[1];
            int count = client.Receive(buffer);
            if (count > 0)
            {
                return buffer[0] == 1;
            }
            throw new Exception("系统异常");
        }
        private byte[] create_send_package(string inter_name, string method_name, int arg_length, TypeEnu return_type, List<ArgInfo> argList)
        {
            List<byte> list = new List<byte>();
            list.Add((byte)inter_name.Length);
            list.AddRange(Encoding.UTF8.GetBytes(inter_name));
            list.Add((byte)method_name.Length);
            list.AddRange(Encoding.UTF8.GetBytes(method_name));
            list.Add((byte)arg_length);
            list.Add((byte)return_type);
            foreach (var arg in argList)
            {
                list.Add((byte)arg.type);
                if (arg.type == TypeEnu.Int)
                {
                    list.AddRange(int2bytes(Convert.ToInt32(arg.value)));
                }
                else if (arg.type == TypeEnu.Bool)
                {
                    bool value = Convert.ToBoolean(arg.value);
                    list.Add(value ? (byte)1 : (byte)0);
                }
                else if (arg.type == TypeEnu.String)
                {
                    string value = arg.value.ToString();
                    list.AddRange(int2bytes(value.Length));
                    list.AddRange(Encoding.UTF8.GetBytes(value));
                }
            }
            return list.ToArray();
        }
        public byte[] int2bytes(int len)
        {
            byte[] data_len = new byte[4];
            data_len[0] = (byte)((len >> 8 * 3) & 0xFF);
            data_len[1] = (byte)((len >> 8 * 2) & 0xFF);
            data_len[2] = (byte)((len >> 8 * 1) & 0xFF);
            data_len[3] = (byte)(len & 0xFF);
            return data_len;
        }
        public int bytes2int(byte[] buffer)
        {
            int value = 0;
            value += (int)(buffer[0] << (8 * 3));
            value += (int)(buffer[1] << (8 * 2));
            value += (int)(buffer[2] << (8 * 1));
            value += (int)(buffer[3]);
            return value;
        }

    }
    public class ArgInfo
    {
        public TypeEnu type { get; set; }
        public object value { get; set; }
        public ArgInfo(TypeEnu type, object value)
        {
            this.type = type;
            this.value = value;
        }
    }
    public enum TypeEnu
    {
        Void = 0,
        Int = 1,
        Bool = 2,
        String = 3
    }
    internal interface IMyTestService
    {
        int Call1(int x, int y);
        bool Call2(string name, string pwd);
    }
}
