using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angus.ISoft.Boilerplate.Infrastructure.Session
{
    /// <summary>
    /// 分布式会话的实体存储类
    /// </summary>
    public class DistributedSession : BaseSession
    {
        /// <summary>
        /// 存储值
        /// </summary>
        public object Value { get; set; }
        /// <summary>
        /// 会话的key
        /// </summary>
        public override string ServerID { get; set; }

    }
}
