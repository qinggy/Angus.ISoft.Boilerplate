using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Angus.ISoft.Boilerplate.Infrastructure.Session
{
    /// <summary>
    /// 扩展会话对象，便于管理会话
    /// </summary>
    public static class SessionExtensions
    {
        private static bool IsDistributedSession = bool.Parse(AppSettings.GetValue("IsDistributedSession"));
        /// <summary>
        /// 添加会话
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="session"></param>
        /// <param name="key">会话key</param>
        /// <param name="value">会话值</param>
        public static void AddItem<T>(this HttpSessionStateBase session, string key, T value)
        {
            if (IsDistributedSession)
            {
                RedisSession.Add(new DistributedSession() { Value = value, ServerID = key });
            }
            else
            {
                session[key] = value;
            }

        }
        /// <summary>
        /// 获取会话
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="session"></param>
        /// <param name="key">key</param>
        /// <returns></returns>
        public static T GetItem<T>(this HttpSessionStateBase session, string key)
        {

            if (IsDistributedSession)
            {
                return (T)RedisSession.Get<DistributedSession>(key).Value;
            }
            else
            {
                if (session[key] == null)
                    return default(T);
                return (T)session[key];
            }
        }

        public static string GetKey(Type itemType)
        {
            return itemType.FullName;
        }

        /// <summary>
        /// 根据key移除session
        /// </summary>
        /// <param name="session"></param>
        /// <param name="key">key</param>
        public static void RemoveItem(this HttpSessionStateBase session, string key)
        {
            if (IsDistributedSession)
            {
                RedisSession.Remove<DistributedSession>(key);
            }
            else
            {
                HttpContext.Current.Session[key] = null;
            }

        }
    }
}
