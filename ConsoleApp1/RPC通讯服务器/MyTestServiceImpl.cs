using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPC通讯服务器
{
    internal class MyTestServiceImpl : IMyTestService
    {
        public int Call1(int x, int y)
        {
            return x + y;
        }

        public bool Call2(string name, string pwd)
        {
            if(name=="text"&&pwd=="123")
                return true;
            else
                return false;

        }
    }
}
