using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Http通讯
{
    internal class Server
    {
        public async void ListenAsync()
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://loaclhost:51111/Myapp/");
            listener.Start();

            HttpListenerContext context = await listener.GetContextAsync();
            string msg = "You asked for:" + context.Request.RawUrl;

            context.Response.ContentLength64=Encoding.UTF8.GetByteCount(msg);
            context.Response.StatusCode = (int)HttpStatusCode.OK;

            using (Stream s = context.Response.OutputStream)
            using (StreamWriter write = new StreamWriter(s))
                await write.WriteAsync(msg);
            listener.Stop();

        }
    }
}
