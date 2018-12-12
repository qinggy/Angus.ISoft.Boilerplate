using Angus.ISoft.Boilerplate.DbModel;
using Angus.ISoft.Boilerplate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angus.ISoft.Boilerplate.Service
{
    public interface IAccountService : IBaseService<Account>
    {
        Task<AccountDto> GetAccountInfo(Guid id);
    }
}
