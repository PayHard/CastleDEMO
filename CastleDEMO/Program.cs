using System;

namespace CastleDEMO
{
    class Program
    {
        static void Main(string[] args)
        {
            PersonDAL person = DaoContainer.GetDao<PersonDAL>();
            Console.WriteLine("当前类型:{0},父类型:{1}", person.GetType(), person.GetType().BaseType);
            Console.WriteLine();
            person.SayHello();//拦截
            Console.WriteLine();
            person.SayName("Never、C");//拦截
            Console.WriteLine();
            person.SayOther();//普通方法,无法拦截    
            //person.AbstactSayOther();//抽象方法,可以拦截（但是如果方法没实现拦截时会报错）     
            Console.ReadLine();
        }
    }
}
