using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MerchantAcquirerAPI.Data.Models.Domains;

namespace MerchantAcquirerAPI.Data.Models.Indentity
{
    public class RolePermission : BaseObject
    {
        public Int64 PermissionId { get; set; }
        public string RoleId { get; set; }
    }
}
