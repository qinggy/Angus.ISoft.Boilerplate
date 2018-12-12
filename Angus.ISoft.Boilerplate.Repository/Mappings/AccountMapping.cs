using Angus.ISoft.Boilerplate.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angus.ISoft.Boilerplate.Repository.Mappings
{
    public class AccountMapping : EntityMapping<Account>
    {
        public AccountMapping()
        {
            ToTable("t_base_account");
        }
    }
}
