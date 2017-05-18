using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angus.ISoft.Boilerplate.Infrastructure.Session
{
    /// <summary>  
    /// Newtonsoft.Json序列化扩展特性  
    /// <para>DateTime序列化（输出为时间戳）</para>  
    /// </summary>  
    public class TimestampConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }
        /// <summary>
        /// 读取json格式的时间
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return ConvertIntDateTime(int.Parse(reader.Value.ToString()));
        }
        /// <summary>
        /// 写json格式的时间
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(ConvertDateTimeInt((DateTime)value));
        }

        public static DateTime ConvertIntDateTime(int aSeconds)
        {
            return new DateTime(1970, 1, 1).AddSeconds(aSeconds);
        }

        public static int ConvertDateTimeInt(DateTime aDT)
        {
            return (int)(aDT - new DateTime(1970, 1, 1)).TotalSeconds;
        }
    }
}
