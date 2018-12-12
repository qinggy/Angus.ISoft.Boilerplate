using Angus.ISoft.Boilerplate.DbModel;
using Angus.ISoft.Boilerplate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angus.ISoft.Boilerplate.Service
{
    public class AccountService : BaseService<Account>, IAccountService
    {
        public async Task<AccountDto> GetAccountInfo(Guid id)
        {
            AccountDto accountDto = new AccountDto();
            var account = await base.SingleOrDefaultAsync(a => a.Id == id).ConfigureAwait(false);
            AutoMapper.Mapper.Map(account, accountDto);
            return accountDto;
        }
    }
}
