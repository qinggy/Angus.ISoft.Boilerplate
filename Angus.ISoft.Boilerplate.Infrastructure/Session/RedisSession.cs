using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angus.ISoft.Boilerplate.Infrastructure.Session
{
    /// <summary>
    /// Redis存储处理会话
    /// </summary>
    public static class RedisSession
    {

        /// <summary>  
        /// Session有效时长,默认120分钟  
        /// </summary>  
        static int TimeOutMinutes = 120;

        /// <summary>  
        /// Redis缓存访问对象  
        /// </summary>  
        static ConnectionMultiplexer mRedisSession;

        /// <summary>  
        /// Session管理  
        /// </summary>  
        static RedisSession()
        {
            try
            {
                TimeOutMinutes = int.Parse(AppSettings.GetValue("SessionTimeOut"));
                string[] redisMaster = AppSettings.GetValue("SessionRedisMaster").Split(':');
                ConfigurationOptions fRedisConfig = new ConfigurationOptions()
                {
                    Password = redisMaster[0],
                    EndPoints = { { redisMaster[1], int.Parse(redisMaster[2]) } },
                    KeepAlive = 180,
                    DefaultDatabase = int.Parse(ConfigurationManager.AppSettings["SessionRedisDBIndex"])
                };
                mRedisSession = ConnectionMultiplexer.Connect(fRedisConfig);
            }
            catch (Exception ex)
            {
                // Lib.LocalLog.Append("Session Error(SessionService):" + ex.Message, "", "SessionError", "SessionError");
                throw ex;
            }
        }

        /// <summary>  
        /// 添加Session  
        /// </summary>  
        /// <typeparam name="T">Session对象类型</typeparam>  
        /// <param name="aSession">要存为Session的对象</param>  
        /// <param name="aPrefix">前缀</param>  
        /// <returns>令牌</returns>  
        public static string Add<T>(T aSession, string aPrefix = "") where T : BaseSession
        {
            try
            {
                IDatabase client = mRedisSession.GetDatabase();
                aSession.SetExpiresTime(DateTime.Now.AddMinutes(TimeOutMinutes));
                string fExistToken = client.StringGet(aSession.ServerID);//获取服务端唯一码当前对应的客户端令牌  
                if (fExistToken != null)
                {//存在客户端令牌  
                    //更新客户端令牌  
                    if (client.StringSet(fExistToken,
                        JsonConvert.SerializeObject(aSession),
                        aSession.ExpiresTime.Subtract(DateTime.Now)))
                    {
                        return fExistToken;
                    }
                }
                else
                {
                    string token = aPrefix + Guid.NewGuid().ToString("N");
                    if (client.StringSet(token,
                        JsonConvert.SerializeObject(aSession),
                        aSession.ExpiresTime.Subtract(DateTime.Now)) &&//添加Session  
                       client.StringSet(aSession.ServerID,
                        token,
                        aSession.ExpiresTime.AddSeconds(-5).Subtract(DateTime.Now)))//绑定服务端唯一码与客户端令牌（将比Session早5秒失效）  
                    {
                        return token;
                    }
                }
            }
            catch (Exception ex)
            {
                //Lib.LocalLog.Append("Session Error(Add<T>):" + ex.Message, "", "SessionError", "SessionError");
            }
            return null;
        }

        /// <summary>  
        /// 获取Session  
        /// </summary>  
        /// <typeparam name="T">Session对象类型</typeparam>  
        /// <param name="aToken">令牌</param>  
        /// <returns>令牌对应的Session对象</returns>  
        public static T Get<T>(string aToken) where T : BaseSession
        {
            try
            {
                IDatabase client = mRedisSession.GetDatabase();
                T fEntity = null;
                string fSessionValue = client.StringGet(aToken);
                if (!string.IsNullOrEmpty(fSessionValue))
                {
                    fEntity = JsonConvert.DeserializeObject<T>(fSessionValue);
                    CheckExpireTime<T>(aToken, fEntity, client);
                }
                return fEntity;
            }
            catch (Exception ex)
            {
                // Lib.LocalLog.Append("Session Error(Get<T>):" + ex.Message, "", "SessionError", "SessionError");
            }
            return default(T);
        }

        /// <summary>  
        /// 获取Session 字符串值  
        /// </summary>  
        /// <param name="aToken">令牌</param>  
        /// <returns>令牌对应的Session 字符串值</returns>  
        public static string GetValue<T>(string aToken) where T : BaseSession
        {
            try
            {
                IDatabase client = mRedisSession.GetDatabase();
                string fSessionValue = client.StringGet(aToken);
                if (fSessionValue != null)
                {
                    CheckExpireTime<T>(aToken, JsonConvert.DeserializeObject<T>(fSessionValue), client);
                }
                return fSessionValue;
            }
            catch (Exception ex)
            {
                // Lib.LocalLog.Append("Session Error(GetValue<T>):" + ex.Message, "", "SessionError", "SessionError");
            }
            return null;
        }

        /// <summary>  
        /// 删除Session  
        /// </summary>  
        /// <param name="aToken">令牌</param>  
        public static void Remove<T>(string aToken) where T : BaseSession
        {
            try
            {
                IDatabase client = mRedisSession.GetDatabase();
                T fEntity = null;
                string fSessionValue = client.StringGet(aToken);
                if (!string.IsNullOrEmpty(fSessionValue))
                {
                    fEntity = JsonConvert.DeserializeObject<T>(fSessionValue);
                    client.KeyDelete(fEntity.ServerID);
                    client.KeyDelete(aToken);
                }
            }
            catch (Exception ex)
            {
                // Lib.LocalLog.Append("Session Error(Remove):" + ex.Message, "", "SessionError", "SessionError");
            }
        }

        /// <summary>  
        /// 令牌有效时间检查  
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <param name="aToken">令牌</param>  
        /// <param name="aSession"></param>  
        static void CheckExpireTime<T>(string aToken, T aSession, IDatabase client) where T : BaseSession
        {
            if (aSession.ExpiresTime.Subtract(DateTime.Now).TotalMinutes < 10)
            {//离Session过期时间小于10分钟，延长Session有效期  
                try
                {
                    aSession.SetExpiresTime(DateTime.Now.AddMinutes(TimeOutMinutes));
                    client.StringSet(aToken, JsonConvert.SerializeObject(aSession), aSession.ExpiresTime.Subtract(DateTime.Now));
                    client.KeyExpire(aSession.ServerID, aSession.ExpiresTime.AddSeconds(-5).Subtract(DateTime.Now));
                }
                catch (Exception ex)
                {
                    //  Lib.LocalLog.Append("Session Error(6):" + ex.Message, "", "SessionError", "SessionError");
                }
            }
        }

    }
}
