using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json测试
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Student student = new Student();
            student.Id = 1;
            student.Name = "Test";
            student.dateTime = DateTime.Now;
            student.hobbies = new List<hobby>();
            for (int n=0;n<10;n++)
            {
                hobby hobby = new hobby();
                hobby.ID = n;
                hobby.Desc = n.ToString() + "tostring";
                student.hobbies.Add( hobby );
            }
            Console.WriteLine(JsonConvert.SerializeObject(student));
            Console.ReadLine();

            //反序列化
            //Studet student = JsonConvert.DeserializeObject<Studet>(json);


        }

    }
    
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime dateTime { get; set; }
        public List<hobby> hobbies { get; set; }
    }
    public class hobby
    {
        public int ID { get; set; }
        public string Desc { get;set; }
    }
}
