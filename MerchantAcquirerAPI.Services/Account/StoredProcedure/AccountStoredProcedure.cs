using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MerchantAcquirerAPI.Data;
using MerchantAcquirerAPI.Data.Models;
using MerchantAcquirerAPI.Data.Models.Domains;

namespace MerchantAcquirerAPI.Services.Account.StoredProcedure
{
    public static class AccountStoredProcedure
    {

        public static MccInfo sp_GetUserById(string id, MerchantAcquirerAPIAppContext context)
        {

            var _params = new SqlParameter("id", id);

            return null;
        }
    }
}
