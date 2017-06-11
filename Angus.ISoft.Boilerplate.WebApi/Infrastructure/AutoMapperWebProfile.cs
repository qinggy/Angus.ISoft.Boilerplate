using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Angus.ISoft.Boilerplate.WebApi.Infrastructure
{
    /// <summary>
    /// 实体映射
    /// </summary>
    public class AutoMapperWebProfile : Profile
    {
        public static void Run()
        {
            AutoMapper.Mapper.Initialize(a =>
            {
                a.AddProfile<AutoMapperWebProfile>();
            });
        }


    }
}