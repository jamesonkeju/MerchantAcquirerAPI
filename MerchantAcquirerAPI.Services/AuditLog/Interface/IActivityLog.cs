using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MerchantAcquirerAPI.Data.Models.Domains;
using MerchantAcquirerAPI.Services.AuditLog.DTO;
using MerchantAcquirerAPI.Utilities.Common;

namespace MerchantAcquirerAPI.Services.AuditLog.Concrete
{
    public interface IActivityLog
    {
        #region Interface for Activity Log Service CRUD
        Task<MessageOut> CreateActivityLog(ActivityLog payload);
        Task CreateActivityLogAsync(string description, string controllerName, string actionName, string userid, object record, object OldRecord);
        void CreateActivityLog(string description, string controllerName, string actionName, string userid, object record, object OldRecord);
       
        #endregion
    }
}
