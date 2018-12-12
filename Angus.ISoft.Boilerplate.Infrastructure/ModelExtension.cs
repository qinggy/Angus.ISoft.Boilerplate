/**********************************************************************************
*项目名称：Angus Isoft Boilerplate
*项目描述：深拷贝
*CLR 版本：4.0.30319.18449
*版 本 号：
*作    者：Angus
*修 改 人：
*修改说明：
*创建时间：2017/05/04 15:12:51
*更新时间：
***********************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BaseEntity = Angus.ISoft.Boilerplate.DbModel;

namespace Angus.ISoft.Boilerplate.Infrastructure
{
    public static class ModelExtensions
    {
        public static readonly Dictionary<Type, Delegate> _mappings = new Dictionary<Type, Delegate>();

        private static bool IsNavigationProperty(Type propertyType)
        {
            if (propertyType.IsPrimitive || propertyType.Equals(typeof(string)))
            {
                return false;
            }

            if (propertyType.Equals(typeof(BaseEntity.Entity)) || propertyType.IsSubclassOf(typeof(BaseEntity.Entity)))
            {
                return true;
            }

            if (propertyType.IsGenericType
                && propertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                return IsNavigationProperty(propertyType.GetGenericArguments()[0]);
            }

            if (propertyType.Equals(typeof(IEnumerable)))
            {
                return true;
            }
            else if (propertyType.IsGenericType
                && propertyType.GetGenericTypeDefinition().Equals(typeof(IEnumerable<>)))
            {
                Type elementType = propertyType.GetGenericArguments()[0];
                return IsNavigationProperty(elementType);
            }
            else if (propertyType.IsGenericType
                && propertyType.GetGenericTypeDefinition().Equals(typeof(IDictionary<,>)))
            {
                Type keyType = propertyType.GetGenericArguments()[0];
                Type valueType = propertyType.GetGenericArguments()[1];
                return IsNavigationProperty(keyType) || IsNavigationProperty(valueType);
            }

            foreach (Type interfaceType in propertyType.GetInterfaces())
            {
                if (IsNavigationProperty(interfaceType))
                {
                    return true;
                }
            }

            return false;
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        public static T Clone<T>(this T source)
            where T : BaseEntity.Entity, new()
        {
            T target = new T();
            target.Id = source.Id;
            target.CopyFrom(source);
            return target;
        }

        public static void CopyFrom<T>(this T target, T source)
            where T : BaseEntity.Entity
        {
            Action<T, T> typedMapping;

            Type type = typeof(T);
            Delegate mapping;

            bool exists;
            lock (_mappings)
            {
                exists = _mappings.TryGetValue(type, out mapping);
            }

            if (exists)
            {
                typedMapping = (Action<T, T>)mapping;
            }
            else
            {
                ParameterExpression sourceParameter = Expression.Parameter(type);
                ParameterExpression targetParameter = Expression.Parameter(type);
                List<Expression> memberAssignments = new List<Expression>();

                foreach (PropertyInfo property in type.GetProperties())
                {
                    if (property.Name == "Id")
                    {
                        continue;
                    }

                    if (!property.CanRead || !property.CanWrite)
                    {
                        continue;
                    }

                    if (IsNavigationProperty(property.PropertyType))
                    {
                        continue;
                    }

                    Expression assign = Expression.Assign(
                        Expression.Property(targetParameter, property),
                        Expression.Property(sourceParameter, property)
                        );
                    memberAssignments.Add(assign);
                }
                BlockExpression body = Expression.Block(memberAssignments);
                Expression<Action<T, T>> lambda = Expression.Lambda<Action<T, T>>(body, sourceParameter, targetParameter);
                typedMapping = lambda.Compile();

                lock (_mappings)
                {
                    _mappings[type] = typedMapping;
                }
            }

            typedMapping(source, target);
        }
        
        public static void CopyFromWithChangeList<T>(this T target, T source, List<string> ChangeList)
           where T : BaseEntity.Entity
        {
            Action<T, T> typedMapping;

            Type type = typeof(T);
            Delegate mapping;

            bool exists;
            lock (_mappings)
            {
                exists = _mappings.TryGetValue(type, out mapping);
            }

            //if (exists)
            //{
            //    typedMapping = (Action<T, T>)mapping;
            //}
            //else
            //{
            ParameterExpression sourceParameter = Expression.Parameter(type);
            ParameterExpression targetParameter = Expression.Parameter(type);
            List<Expression> memberAssignments = new List<Expression>();

            foreach (PropertyInfo property in type.GetProperties())
            {
                if (property.Name == "Id" || !ChangeList.Contains(property.Name))
                {
                    continue;
                }

                if (!property.CanRead || !property.CanWrite)
                {
                    continue;
                }

                if (IsNavigationProperty(property.PropertyType))
                {
                    continue;
                }
                Expression assign = Expression.Assign(
                    Expression.Property(targetParameter, property),
                    Expression.Property(sourceParameter, property)
                    );
                memberAssignments.Add(assign);
            }
            BlockExpression body = Expression.Block(memberAssignments);
            Expression<Action<T, T>> lambda = Expression.Lambda<Action<T, T>>(body, sourceParameter, targetParameter);
            typedMapping = lambda.Compile();

            lock (_mappings)
            {
                _mappings[type] = typedMapping;
            }
            // }

            typedMapping(source, target);
        }
    }
}
