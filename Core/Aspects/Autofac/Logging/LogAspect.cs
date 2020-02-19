using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.Utilities.Interceptors;
using Core.Utilities.Messagess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Logging
{
    public class LogAspect : MethodInterception
    {
       private LoggerServiceBase _loggerServiceBase;

        public LogAspect(Type loggerService)
        {
            if(loggerService.BaseType!=typeof(LoggerServiceBase))
            {
                throw new Exception(AspectMessages.WronLoggerType);
            }
            _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggerService);
        }

        protected override void OnBefore(IInvocation invocation)
        {
            _loggerServiceBase.Info(GetLogDetail(invocation));
        }

        private LogDetail GetLogDetail(IInvocation invocation)
        {
            var logParameters = invocation.Arguments.Select(p => new LogParameter
            {
              
               Value=p,
               Type=p.GetType().Name
            }).ToList();
            var logDetail = new LogDetail
            {
                MethodName = invocation.Method.Name,
                LogParameters = logParameters
            };
            return logDetail;
        }
    }
}
