using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleDEMO
{
    public abstract class PersonDAL : BaseDao
    {
        /// <summary>
        /// 必须是虚方法才能被拦截器拦截
        /// </summary>
        public virtual void SayHello()
        {
            Console.WriteLine("我是虚方法{0}方法", "SayHello");
        }
        public virtual void SayName(string name)
        {
            Console.WriteLine("我是虚方法{0}方法,参数值:{1}", "SayName", name);
        }
        public abstract void AbstactSayOther();
        public void SayOther()
        {
            Console.WriteLine("我是普通方法{0}方法", "SayOther");
        }
    }
}