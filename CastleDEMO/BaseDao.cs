using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace CastleDEMO
{
    /// <summary>
    /// Dao 类的接口
    /// 继承实现BaseDao的类,其相关接口访问的公共方法必须要声明 virtual 方法才能被拦截器拦截。
    /// </summary>
    public abstract class BaseDao
    {

    }

    /// <summary>
    /// Dao容器,必须依赖于此类来创建Dao对象，使Dao受控，可进行检查等
    /// </summary>
    public class DaoContainer
    {
        //ProxyGenerator上自身有缓存
        //实例化【代理类生成器】 
        public static ProxyGenerator generator = new ProxyGenerator();
        public static T GetDao<T>() where T : BaseDao
        {
            //实例化【拦截器】 
            DA_LogInterceptor interceptor = new DA_LogInterceptor();
            //使用【代理类生成器】创建T对象，而不是使用new关键字来实例化 
            return generator.CreateClassProxy<T>(interceptor);
        }
    }
}