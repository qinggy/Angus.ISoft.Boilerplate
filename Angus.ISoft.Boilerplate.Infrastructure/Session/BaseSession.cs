using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angus.ISoft.Boilerplate.Infrastructure.Session
{
    /// <summary>
    /// 分布式会话基类
    /// </summary>
    public abstract class BaseSession
    {
        DateTime mExpiresTime = DateTime.Now;

        [JsonConverter(typeof(TimestampConverter))]
        public DateTime ExpiresTime
        {
            get { return mExpiresTime; }
        }

        /// <summary>  
        /// 设置Session过期时间  
        /// </summary>  
        /// <param name="aExpiresTime"></param>  
        internal void SetExpiresTime(DateTime aExpiresTime)
        {
            mExpiresTime = aExpiresTime;
        }

        [JsonIgnore]
        /// <summary>  
        /// 获取服务端ID （会话key） 
        /// </summary>  
        public abstract string ServerID { get; set; }
    }
}
