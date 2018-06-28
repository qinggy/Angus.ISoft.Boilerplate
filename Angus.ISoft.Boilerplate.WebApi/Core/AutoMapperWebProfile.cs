using Angus.ISoft.Boilerplate.DbModel;
using Angus.ISoft.Boilerplate.Domain;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Angus.ISoft.Boilerplate.WebApi.Core
{
    /// <summary>
    /// 实体映射
    /// </summary>
    public class AutoMapperWebProfile : Profile
    {
        public static void Register()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DemoClass, DemoClassDto>();
                cfg.AddProfile<AutoMapperWebProfile>();
            });
        }
    }
}