using Angus.ISoft.Boilerplate.Infrastructure.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Angus.ISoft.Boilerplate.WebApi.ExtensionAttribute
{
    public class BaseLogAttribute : FilterAttribute
    {
        protected LogTypeEnum logType { get; set; }

        public BaseLogAttribute(LogTypeEnum _logType)
        {
            logType = _logType;
        }

        protected virtual void WriteLog()
        {
            switch (logType)
            {
                case LogTypeEnum.Debug:
                    break;
                case LogTypeEnum.Info:
                    break;
                case LogTypeEnum.Warn:
                    break;
                case LogTypeEnum.Error:
                    break;
                case LogTypeEnum.Fatal:
                    break;
                default:
                    break;
            }
        }
    }
}