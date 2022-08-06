using MerchantAcquirerAPI.Data;
using MerchantAcquirerAPI.Data.Models.Domains;
using MerchantAcquirerAPI.Services.AccountType.Interface;
using MerchantAcquirerAPI.Services.AuditLog.Concrete;
using MerchantAcquirerAPI.Services.CommonRoute;
using MerchantAcquirerAPI.Services.CustomerRequest.dto;
using MerchantAcquirerAPI.Services.CustomerRequest.Interface;
using MerchantAcquirerAPI.Services.DataAccess;
using MerchantAcquirerAPI.Services.FileHandler;
using MerchantAcquirerAPI.Services.Terminal.Interface;
using MerchantAcquirerAPI.Utilities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantAcquirerAPI.Services.CustomerRequest.Concrete
{
    public class CustomerRequestServices : ICustomerRequest
    {
        private MerchantAcquirerAPIAppContext _context;
        private IActivityLog _activityLogService;
        private ILogger<CustomerRequestServices> _logger;
        private ICommonRoute _commonServices;
        private IFileHandler _fileHandler;
        private IConfiguration _configuration;
        public CustomerRequestServices(MerchantAcquirerAPIAppContext context, ILogger<CustomerRequestServices> logger,
            IActivityLog activityLogService, ICommonRoute commonServices, IFileHandler fileHandler, IConfiguration configuration )
        {
            _context = context;
            _activityLogService = activityLogService;
            _logger = logger;
            _commonServices = commonServices;
            _fileHandler = fileHandler;
            _configuration = configuration;
        }


        public  ApiResult<string> GetNewMID(string AccountNo, string address)
        {
            MerchantAcquirerAPIAppContext _contextNew = new MerchantAcquirerAPIAppContext();
            var msg = new ApiResult<string>();
            string temp = "";

            try
            {

                if (AccountNo == "")
                {
                    msg.HasError = true;
                    msg.StatusCode = CommonResponseMessage.MobileFailed;
                    msg.Message = "Account Number is a required field. Please supply Merchant Account Number";
                    return msg;
                }

                if (address == "")
                {
                    msg.HasError = true;
                    msg.StatusCode = CommonResponseMessage.MobileFailed;
                    msg.Message = "Account address is a required field. Please supply Merchant address";
                    return msg;
                }


                var getRequest = _contextNew.PosReq.Where(a => a.AcctNo == AccountNo && a.PhyAddress == address).FirstOrDefault();

                if (getRequest != null)
                {

                    temp = getRequest.MerchantID;

                }
                else
                {

                    var rstep = _contextNew.MerchantIDTab.FirstOrDefault();

                    int lastid = Convert.ToInt32(rstep.LastMid);
                    int newid = lastid + 1;


                    // Update last record
                    rstep.LastMid = newid.ToString();
                    rstep.DateLastGen = DateTime.Now;
                    _contextNew.SaveChanges();


                    string bracode = AccountNo.Substring(0, 3);
                    string prefix = _configuration["Midprefix"];
                    string remx = newid.ToString().PadLeft(9, '0');

                    temp = prefix + bracode + remx;

                }

                msg.HasError = false;
                msg.Message = "Record Saved Successful";
                msg.StatusCode = CommonResponseMessage.MobileSuccessful;
                msg.Result = temp;
                return msg;


            }
            catch (Exception ex)
            {

                msg.Message = CommonResponseMessage.InternalError;
                msg.HasError = true;
                msg.Result = null;
                return msg;
            }
        }


        public async Task<string>  generateTerminalNo()
        {
            LDAP lp = new LDAP(_configuration);
            bool checker = true;
            string tempgenerateTerminal = "";
            string generateTerminal = "";
            try
            {
                
                while (checker == true)
                {
                     generateTerminal = lp.GenerateTerminalId();

                     tempgenerateTerminal = generateTerminal.Substring(0, 4);

                    var checkDuplicate = await _context.NewTerminalTemp.
                                                                Where(a => a.TerminalNumber == tempgenerateTerminal).ToListAsync();

                    if (checkDuplicate.Count == 0)
                    {
                        var insertData = new NewTerminalTemp
                        {
                            LastGeneratedDate = DateTime.Now,
                            TerminalNumber = tempgenerateTerminal.ToUpper()
                        };

                        checker = true;
                        break;
                    }
                }

                return tempgenerateTerminal.ToUpper();

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<ApiResult<POSRequestResponse>> CreatePOSRequest(POSRequest payload, string AppFileName, string AcceptanceFileName)
        {
            string TransactionRef = GenericUtil.uniqueid();
            var msg = new ApiResult<POSRequestResponse>();
            LDAP lp = new LDAP(_configuration);

            string temp = _configuration["TermPrefix"];

            string terminalNo = await generateTerminalNo();
            string NewTerminalNumber = temp + terminalNo;
            var data = new POSRequestResponse();
            try
            {

                // what do we need to check to ensure it not a duplicate 
                var newreq = new PosReq();
              
                newreq.AcctNo = payload.AccountNumber;
                newreq.AcctName = payload.AccountName;
                newreq.ContactName = payload.ContactName;
                newreq.MerchantEmail = payload.MerchantEmail;
                newreq.PTSP = "";
                newreq.ContactTitle = payload.ContactTitle;
                newreq.AcctType = payload.AccountType;
                newreq.BankCode = _configuration["BankCode"];
                newreq.BusOcpCode =payload.BusinessOccupation;
                newreq.MccCode = payload.MccType;
                newreq.StateCode = payload.StateCode;
                newreq.TermOwnerCode = payload.TermOwnerCode;
                newreq.TermCode = payload.TermCode;
                newreq.ReqDate = DateTime.Now;
                newreq.ReqBranch = _configuration["Branch"];
                newreq.ReqStatId = Convert.ToInt32(_configuration["RequestStatusCode"]);
                newreq.SlipHeader = payload.MerchantAddress;
                newreq.UserId = _configuration["userId"];
                newreq.PhyAddress = payload.PhysicalAddress;
                newreq.MobilePhone = payload.ContactPhone;
                newreq.MerchantURL = payload.MerchantURL;
                newreq.AcctBranch = payload.AccountBranch;
                newreq.MandateAttach = "Yes";
                newreq.ROId = _configuration["ROId"];
                newreq.Reminder = 0;
                newreq.Sec_contactName = payload.SecondaryContactName;
                newreq.Sec_contact_mobile = payload.SecondaryContactPhone;
                newreq.Network_Operator = payload.SecondaryNetworkOperator;
                newreq.Sec_Network = payload.PreferedTerminalNetwork;
                newreq.Sec_Operator = payload.PreferedNetworkOperator;
                newreq.MerchantName = payload.MerchantName;
                newreq.MasterPassHolder = "No";
                newreq.NibbsUSSDHolder = "No";

                newreq.NetworkType = payload.PreferedTerminalNetwork;

                if (payload.AccountSegment.Length >= 50)
                {
                    newreq.AccountClass = payload.AccountSegment.Substring(0, 47);
                }
                else
                {
                    newreq.AccountClass = payload.AccountSegment;
                }

                if (payload.MerchantAlternatePhone == string.Empty)
                {
                    newreq.AltPhone = "NIL";
                }
                else
                {
                    newreq.AltPhone = payload.MerchantAlternatePhone;
                }


                // Assign FileName to db field
                newreq.ApplicationForm1 = AppFileName;
                newreq.ApplicationForm2 = AcceptanceFileName;

                string path1 = _configuration["upload"] + AppFileName;
                string path2 = _configuration["upload"] + AcceptanceFileName;

                newreq.SiteVisitationDoc = "";
                newreq.AgreementDoc = "";
                newreq.LGA = payload.LocalGovernmentArea;
                newreq.ReqCode = TransactionRef;
                newreq.EmailAlerts = payload.ReceiveEmailAlerts == true ? "Y" : "N";
                newreq.CustID = payload.CustomerID;

                var  mcid =  GetNewMID(payload.AccountNumber,payload.PhysicalAddress);

                newreq.MerchantID = mcid.Result.Trim();

                newreq.ProfilingStatus = 2;
                newreq.Path1 = path1;
                newreq.Path2 = path2;


                //Confirm if Merchant ID Already Exist

                var checkmid = await getMerchantName(newreq.MerchantID);

                if (checkmid.Result == "NA")
                {
                    newreq.MerchantName = payload.MerchantName;
                }
                else
                {
                    newreq.MerchantName = checkmid.Result;
                }


                if (payload.MerchantNameDifference == true)
                {
                    newreq.AcctMerchantNameFlag ="true";
                    newreq.AcctMNameReason = payload.ReasonForDifferentName;
                }
                else
                {
                    newreq.AcctMNameReason = string.Empty;
                    newreq.AcctMerchantNameFlag = "False";
                }


                newreq.TerminalID = NewTerminalNumber;

                newreq.profillingDate = DateTime.Now;
                newreq.profilingBy = _configuration["ProfileStatus"];
                newreq.EbizComment = "Request carrried out on the mobile app.";
                newreq.EbizAction = "Request Accepted";
                string op = "New  POS  Request  Submitted with ID :" + TransactionRef + "Submitted Successfully";
               


                var history = new ReqHistory();

                history.ActionDateTime = DateTime.Now;
                history.Initiator = _configuration["userid"];
                history.ActionPerformed = "Submit New POS Request";
                history.ReqId = TransactionRef;
              
                    
               var log = new NewAuditLog();

                log.Activity = "New POS   Request Submitted";
                log.MakerDate = DateTime.Now;
                log.MakerID = _configuration["userid"];
                log.MakerIPAddress = IPAddressUtil.GetLocalIPAddress();
                log.MakerName = _configuration["MakerName"];
                log.OldValue = "";
                log.TransRef = TransactionRef;
                log.TransDesc = "New  POS Request Submitted by the Processor";


                await _context.NewAuditLog.AddAsync(log);
                await _context.ReqHistory.AddAsync(history);
                await _context.PosReq.AddAsync(newreq);

                await _context.SaveChangesAsync();
                await writelog(_configuration["userid"], op);


                data.AccountNo = payload.AccountNumber;
                data.MerchantName = payload.MerchantName;
                data.MerchantNumber = newreq.MerchantID;
                data.TerminalID = newreq.TerminalID;
                data.Status = "Created";

                msg.HasError = false;
                msg.Message = "Request Submitted Successfully.";
                msg.StatusCode = CommonResponseMessage.MobileSuccessful;
               
                msg.Result = data;

                /// send email
         //   await  sendText(TransactionRef, newreq.MerchantID, _configuration["NewquestNotification"]);


                return msg;


            }
            catch (Exception ex)
            {
                msg.Message = CommonResponseMessage.InternalError;
                msg.HasError = true;
                msg.Result = null;
                return msg;
            }
        }


        public async Task<ApiResult<List<CustomerRequestReponse>>> GetCustomerRequestByAccountNo(string AccountNo)
        {
            var msg = new ApiResult<List<CustomerRequestReponse>>();
            try
            {

                var dataList = new List<CustomerRequestReponse>();

               


                var getData =  (from k in  _context.PosReq
                               join a in _context.AcctType on k.AcctType equals a.Acctcode
                               join p in _context.RequestStatus on k.ProfilingStatus equals p.ReqStatId

                               where k.AcctNo == AccountNo
                               select new CustomerRequestReponse
                               {
                                   AccountClass = k.AccountClass,
                                   AcctNo= k.AcctNo,
                                   AcctType =a.AcctDesc,
                                   AcctName = k.AcctName,
                                   ContactName= k.ContactName,
                                   ContactTitle= k.ContactTitle,
                                  
                                   MobilePhone= k.MobilePhone,
                                   Comment = k.Comment,
                                   profillingDate = k.profillingDate,
                                   MerchantID= k.MerchantID,
                                   CustID= k.CustID,
                                   Status = p.ReqStatus,
                                   ReqDate= k.ReqDate,
                                   TerminalNo = k.TerminalID == null || k.TerminalID == "" ? "N/A" : k.TerminalID,
                                   MerchantName = k.MerchantName == null || k.MerchantName == "" ? "N/A" : k.MerchantName,
                               }).ToList();




                if (getData == null)
                {
                    msg.Message = CommonResponseMessage.RecordNotExisting.Replace("{0}", " account data ");
                    msg.HasError = true;
                    msg.StatusCode = CommonResponseMessage.MobileFailed;
                }
                else
                {
                    msg.HasError = false;
                    msg.Message = CommonResponseMessage.FetchSuccessMessage;
                    msg.StatusCode = CommonResponseMessage.MobileSuccessful;
                    msg.Result = getData;
                }


                return msg;

            }
            catch (Exception ex)
            {
                msg.Message = CommonResponseMessage.InternalError;
                msg.HasError = true;
                msg.Result = null;
                return msg;
            }
        }


        public async Task<ApiResult<List<CustomerRequestReponse>>> GetPOSRequestByTerminalid(string TerminalId)
        {
            var msg = new ApiResult<List<CustomerRequestReponse>>();
            try
            {

                var dataList = new List<CustomerRequestReponse>();




                var getData = (from k in _context.PosReq
                               join a in _context.AcctType on k.AcctType equals a.Acctcode
                               join p in _context.RequestStatus on k.ProfilingStatus equals p.ReqStatId

                               where k.TerminalID == TerminalId
                               select new CustomerRequestReponse
                               {
                                   AccountClass = k.AccountClass,
                                   AcctNo = k.AcctNo,
                                   AcctType = a.AcctDesc,
                                   AcctName = k.AcctName,
                                   ContactName = k.ContactName,
                                   ContactTitle = k.ContactTitle,
                             
                                   MobilePhone = k.MobilePhone,
                                   Comment = k.Comment,
                                   profillingDate = k.profillingDate,
                                   MerchantID = k.MerchantID,
                                   CustID = k.CustID,
                                   Status = p.ReqStatus,
                                   ReqDate = k.ReqDate,
                                   TerminalNo = k.TerminalID == null || k.TerminalID == "" ? "N/A" : k.TerminalID,
                                   MerchantName = k.MerchantName == null || k.MerchantName == "" ? "N/A" : k.MerchantName,

                               }).ToList();




                if (getData == null)
                {
                    msg.Message = CommonResponseMessage.RecordNotExisting.Replace("{0}", " account data ");
                    msg.HasError = true;
                    msg.StatusCode = CommonResponseMessage.MobileFailed;
                }
                else
                {
                    msg.HasError = false;
                    msg.Message = CommonResponseMessage.FetchSuccessMessage;
                    msg.StatusCode = CommonResponseMessage.MobileSuccessful;
                    msg.Result = getData;
                }


                return msg;

            }
            catch (Exception ex)
            {
                msg.Message = CommonResponseMessage.InternalError;
                msg.HasError = true;
                msg.Result = null;
                return msg;
            }

        }

            public async Task<ApiResult<List<CustomerRequestReponse>>> GetPOSRequestByMerchantId(string MerchantId)
            {
                var msg = new ApiResult<List<CustomerRequestReponse>>();
                try
                {

                    var dataList = new List<CustomerRequestReponse>();




                    var getData = (from k in _context.PosReq
                                   join a in _context.AcctType on k.AcctType equals a.Acctcode
                                   join p in _context.RequestStatus on k.ProfilingStatus equals p.ReqStatId

                                   where k.MerchantID == MerchantId
                                   select new CustomerRequestReponse
                                   {
                                       AccountClass = k.AccountClass,
                                       AcctNo = k.AcctNo,
                                       AcctType = a.AcctDesc,
                                       AcctName = k.AcctName,
                                       ContactName = k.ContactName,
                                       ContactTitle = k.ContactTitle,
                                       MerchantName = k.MerchantName,
                                       MobilePhone = k.MobilePhone,
                                       Comment = k.Comment,
                                       profillingDate = k.profillingDate,
                                       MerchantID = k.MerchantID,
                                       CustID = k.CustID,
                                       Status = p.ReqStatus,
                                       ReqDate = k.ReqDate,
                                       TerminalNo=k.TerminalID

                                   }).ToList();




                    if (getData == null)
                    {
                        msg.Message = CommonResponseMessage.RecordNotExisting.Replace("{0}", " account data ");
                        msg.HasError = true;
                        msg.StatusCode = CommonResponseMessage.MobileFailed;
                    }
                    else
                    {
                        msg.HasError = false;
                        msg.Message = CommonResponseMessage.FetchSuccessMessage;
                        msg.StatusCode = CommonResponseMessage.MobileSuccessful;
                        msg.Result = getData;
                    }


                    return msg;

                }
                catch (Exception ex)
                {
                    msg.Message = CommonResponseMessage.InternalError;
                    msg.HasError = true;
                    msg.Result = null;
                    return msg;
                }
            }
            public async Task<ApiResult<string>> getMerchantName(string MerchantID)
        {

            var msg = new ApiResult<string>();
            string temp = "NA";

            try
            {

                if (MerchantID == "")
                {
                    msg.HasError = true;
                    msg.StatusCode = CommonResponseMessage.MobileFailed;
                    msg.Message = "Merchant Id is a required field.";
                    return msg;
                }

                var getRequest = await _context.PosReq.Where(a => a.MerchantID == MerchantID).FirstOrDefaultAsync();

                if (getRequest != null)
                {
                    temp = getRequest.MerchantName;

                }
                
                msg.HasError = false;
                msg.Message = "Record Saved Successful";
                msg.StatusCode = CommonResponseMessage.MobileSuccessful;
                msg.Result = temp;
                return msg;

            }
            catch (Exception ex)
            {

                msg.Message = CommonResponseMessage.InternalError;
                msg.HasError = true;
                msg.Result = null;
                return msg;
            }
        }

    
        public async Task writelog(string userid, string op)
        {
            try
            {
                var ds = new MerchantAcquirerAPI.Data.Models.Domains.AuditLog();

                ds.UserId = userid;
                ds.OperationsPerformed = op;
                ds.IpAddress = IPAddressUtil.GetLocalIPAddress();
                ds.PageVisited = "Mobile";
                ds.DateAccessed = DateTime.Now;
               await  _context.AuditLog.AddAsync(ds);
                await _context.SaveChangesAsync();
             
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<string> sendText(string mm, string mcid,string fileName)
        {
            Mailer m = new Mailer(_configuration);
            string msg = "";
            string from = _configuration["From"];
            string to = _configuration["HOPEmail"];
            string cc = "";
           // string cc = configuration.ATMPOS + "," + Session["Email"].ToString();
            string bcc = _configuration["RelationshipOfficeEmail"];
            string subject = "New Merchant Onboarding Request Awaiting your Review";
            string dt = DateTime.Now.ToString();

            var rs = await _context.PosReq.Where(a => a.ReqCode == mm).FirstOrDefaultAsync();

              string id = mm;
            string initiator = _configuration["userid"];
            string merchant = rs.MerchantName;
            string acct = rs.AcctNo;
            string mid = mcid;

           
            StringBuilder sb = new StringBuilder();

            string fullPath = Directory.GetCurrentDirectory() + _configuration["htmlFolder"] +fileName;
            
            StreamReader sr = new StreamReader(fullPath);

            string line = sr.ReadToEnd();
            sb.Append(line);

            sb.Replace("{id}", id);
            sb.Replace("{date}", dt);
            sb.Replace("{initiator}", initiator);
            sb.Replace("{MerchantName}", merchant);
            sb.Replace("{AccountNo}", acct);
            sb.Replace("{MerchantID}", mid);

            string body = sb.ToString();
            try
            {
                // Mailer.SendMailMessage(from, to, bcc, cc, subject, body);

                m.sendMail(to, from, cc, bcc, body, subject, "");

                if (to == string.Empty)
                {
                     msg = "Request Submitted Successfully. However,No Active HOP created for your Branch, Please inform your HOP to follow up with Portal Administrator";
                  
                }
                else
                {
                     msg = "Request Submitted Successfully. Please follow up with your HOP";
                  
                }
            }
            catch (Exception ex)
            {

                if (to == string.Empty)
                {
                     msg = "Request Submitted Successfully. However,No Active Authorizer created for your Branch, Please inform your Authorizer to follow up with User Access Management";                  
                }
                else
                {
                     msg = "Request Submitted Successfully. However,No Active Authorizer created for your Branch, Please inform your Authorizer to follow up with User Access Management ";

                }

                return msg;
            }

            return msg;
        }

       

        public async Task<ApiResult<List<POSRequestResponse>>> CheckPOSRequestStatus(string AccountNo)
        {
            var msg = new ApiResult<List<POSRequestResponse>>();
            try
            {

                var dataList = new List<POSRequestResponse>();




                var getData = (from k in _context.PosReq
                               join a in _context.AcctType on k.AcctType equals a.Acctcode
                               join p in _context.RequestStatus on k.ProfilingStatus equals p.ReqStatId

                               where k.AcctNo == AccountNo
                               select new POSRequestResponse
                               {
                                   AccountNo = k.AcctNo,
                                   MerchantNumber = k.MobilePhone,
                                   TerminalID= k.TerminalID ==null || k.TerminalID == ""  ? "N/A" : k.TerminalID,
                                   MerchantName = k.MerchantName == null || k.MerchantName == "" ? "N/A" : k.MerchantName,
                                   Status = p.ReqStatus,


                               }).ToList();




                if (getData == null)
                {
                    msg.Message = CommonResponseMessage.RecordNotExisting.Replace("{0}", " account data ");
                    msg.HasError = true;
                    msg.StatusCode = CommonResponseMessage.MobileFailed;
                }
                else
                {
                    msg.HasError = false;
                    msg.Message = CommonResponseMessage.FetchSuccessMessage;
                    msg.StatusCode = CommonResponseMessage.MobileSuccessful;
                    msg.Result = getData;
                }


                return msg;

            }
            catch (Exception ex)
            {
                msg.Message = CommonResponseMessage.InternalError;
                msg.HasError = true;
                msg.Result = null;
                return msg;
            }
        }
    }
}
