using System.Linq;
using Castle.DynamicProxy;
using Newtonsoft.Json;

namespace BookLibrary
{
    public class Logger : IInterceptor
    {
        private static NLog.Logger logger;
        public Logger()
        {
            logger = NLog.LogManager.GetCurrentClassLogger();
        }

        public void Intercept(IInvocation invocation)
        {
            var methodName = invocation.Method;
            var parameters = JsonConvert.SerializeObject(invocation.Arguments);
            logger.Trace("Method: " + methodName + " Arguments: " + parameters);
            invocation.Proceed();
        }
    }
}