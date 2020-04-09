using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace CastleDEMO
{
    /// <summary>
    /// 拦截器 需要实现 IInterceptor接口 Intercept方法
    /// </summary>
    public class DA_LogInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            try
            {
                DateTime begin = DateTime.Now;

                Console.WriteLine("开始DAL {0}调用!", invocation.Method.Name);

                //在被拦截的方法执行完毕后 继续执行
                invocation.Proceed();

                DateTime end = DateTime.Now;
                Console.WriteLine("结束DAL {0}调用!耗时:{1}ms", invocation.Method.Name, (end - begin).TotalMilliseconds);
            }
            catch (Exception ex)
            {
                string methodName = "DA_" + invocation.TargetType.ToString() + "." + invocation.Method.Name;
                Console.WriteLine("{0}方法错误：{1}", methodName, ex.Message);
                //如果没有定义异常处理返回值,就直接抛异常
                if (!invocation.Method.IsDefined(typeof(ExceptionReturnAttribute), false))
                    throw;
                var ls = invocation.Method.GetCustomAttributes(typeof(ExceptionReturnAttribute), false);
                if (null == ls || ls.Length <= 0)
                    throw;

                ExceptionReturnAttribute v = (ExceptionReturnAttribute)ls[0];
                if (null == v.Value && null == v.Type)
                {
                    invocation.ReturnValue = null;
                    return;
                }
                if (null != v.Value)
                {
                    invocation.ReturnValue = v.Value;
                    return;
                }
                if (null != v.Type)
                {
                    invocation.ReturnValue = Activator.CreateInstance(v.Type);
                    return;
                }
            }
        }

        /// <summary>
        /// <para>DAO层异常时，不throw，返回设定的值.</para>
        /// <para>1. 返回复杂类型，使用Type，复杂类型需要有无参的构造函数</para>
        /// <para>2. 返回简单类型，使用value</para>
        /// </summary>
        [AttributeUsage(AttributeTargets.Method)]
        public class ExceptionReturnAttribute : System.Attribute
        {
            /// <summary>
            /// 返回复杂类型，使用Type，复杂类型需要有无参的构造函数
            /// </summary>
            public Type Type { get; set; }

            /// <summary>
            /// 返回简单类型，使用value
            /// </summary>
            public object Value { get; set; }
        }

    }

    public class Interceptor : StandardInterceptor
    {
        /// <summary>
        /// 调用前的拦截器
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PreProceed(IInvocation invocation)
        {
            Console.WriteLine("调用前的拦截器，方法名是：{0}。", invocation.Method.Name);// 方法名   获取当前成员的名称。 
        }
        /// <summary>
        /// 拦截的方法返回时调用的拦截器
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PerformProceed(IInvocation invocation)
        {
            Console.WriteLine("拦截的方法返回时调用的拦截器，方法名是：{0}。", invocation.Method.Name);
            base.PerformProceed(invocation);
        }

        /// <summary>
        /// 调用后的拦截器
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PostProceed(IInvocation invocation)
        {
            Console.WriteLine("调用后的拦截器，方法名是：{0}。", invocation.Method.Name);
        }
    }
}