using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MerchantAcquirerAPI.Data.Models.ViewModel;
using MerchantAcquirerAPI.Services.Account.DTO;
using MerchantAcquirerAPI.Utilities.Common;

namespace MerchantAcquirerAPI.Services.Account.Interface
{
    public interface IAccount
    {

        Task<ApiResult<Utilities.LDAPModel.CustomerDetail>> AccountValidation(string AccountNo);
        Task<ApiResult<Token>> AccessToken();


        ////Task<MessageOut> activeuser(string userid);
        ////string getHOPEmailAddress(string branchid);
        ////Task<MessageOut> UpdateLastLoin(string userid, string ip);
        ////Task<MessageOut> getbranchid(string branchname);
        ////List<string> getbranchidListByRegion(int region);
        ////string getReba(string branchcode);
        ////bool isAuthorised(int sessid);
        ////// UserInfo UserInfoFromTable(string userid);
        ////bool UpdateMid(int mid);
        ////string GenerateMerchantId(string AccountNo, string address);  //  look for GetNewMID
        ////string GenerateTerminald(string AccountNo, string address);

        ////string getbranchname(string branchid);
        ////string getMerchantName(string mid);
        ////string Getptspinfo(string ptsp);
        ////int getroleid(string rolename);
        ////string getrolename(int roleid);





        //Task<MessageOut> Login(Data.Payload.UserLoginPayload payload);
        //Task<MessageOut> Register(Data.Payload.AdminUserSettingViewModel payload);


    }
}
