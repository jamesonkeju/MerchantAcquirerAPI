using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MerchantAcquirerAPI.Data.Models;
using MerchantAcquirerAPI.Data.Payload;

namespace MerchantAcquirerAPI.Services.SessionTokenGenerator.Interface
{
    public interface ISessionTokenGenerator
    {
        string GenerateToken(VwUserInfornation session);
        Task<string> GenerateToken(ApplicationUser usr);
    }
}
