using System;
using Newtonsoft.Json;
using PostSharp.Aspects;

namespace ServiceForSeparateDocuments
{
    [Serializable]
    public class LoggerPostSharpAspect : OnMethodBoundaryAspect
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public override void OnEntry(MethodExecutionArgs args)
        {
            var methodName = args.Method;
            var parameters = JsonConvert.SerializeObject(args.Arguments);
            logger.Trace("Method: " + methodName + " Arguments: " + parameters);
        }
    }
}