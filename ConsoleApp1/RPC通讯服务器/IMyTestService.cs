using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPC通讯服务器
{
    internal interface IMyTestService
    {
        int Call1(int x, int y);
        bool Call2(string name, string pwd);
    }
}
