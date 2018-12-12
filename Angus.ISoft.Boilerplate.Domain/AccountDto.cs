using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angus.ISoft.Boilerplate.Domain
{
    /// <summary>
    /// 账户信息
    /// </summary>
    public class AccountDto
    {
        /// <summary>
        /// 编号
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 账号名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}
