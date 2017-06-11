using Angus.ISoft.Boilerplate.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angus.ISoft.Boilerplate.Repository.Mappings
{
    public class DemoMapping : EntityMapping<DemoClass>
    {
        public DemoMapping()
        {
            ToTable("DemoClass");
        }
    }
}
