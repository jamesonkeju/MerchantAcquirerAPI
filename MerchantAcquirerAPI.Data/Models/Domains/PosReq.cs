using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantAcquirerAPI.Data.Models.Domains
{
	public class PosReq
	{
		[Key]
		public long ReqID { get; set; }
		public DateTime ReqDate { get; set; }
		public string UserId { get; set; }
		public string AcctNo { get; set; }
		public string AccountClass { get; set; }
		public string AcctName { get; set; }
		public string MerchantID { get; set; }
		public string MerchantName { get; set; }
		public string ContactTitle { get; set; }
		public string ContactName { get; set; }
		public string MobilePhone { get; set; }
		public string MerchantEmail { get; set; }
		public string EmailAlerts { get; set; }
		public string PhyAddress { get; set; }
		public string TermCode { get; set; }
		public string TerminalID { get; set; }
		public string BankCode { get; set; }
		public string AcctType { get; set; }
		public string BusOcpCode { get; set; }
		public string MccCode { get; set; }
		public string StateCode { get; set; }
		public string TermOwnerCode { get; set; }
		public string MandateAttach { get; set; }
		public string AcctMerchantNameFlag { get; set; }
		public string AcctMNameReason { get; set; }
		public int ReqStatId { get; set; }
		public DateTime ReqAcceptDate { get; set; }
		public string ReqAcceptedBy { get; set; }
		public string ReqBranch { get; set; }
		public DateTime ReqDeclinedDate { get; set; }
		public string ReqDeclinedBy { get; set; }
		public string LGA { get; set; }
		public string MerchantURL { get; set; }
		public string ApplicationForm1 { get; set; }
		public string ApplicationForm2 { get; set; }
		public string SiteVisitationDoc { get; set; }
		public string AgreementDoc { get; set; }
		public string SlipHeader { get; set; }
		public string AcctBranch { get; set; }
		public string ReqCode { get; set; }
		public string Comment { get; set; }
		public string BatchId { get; set; }
		public string CustID { get; set; }
		public string EbizProcessor { get; set; }
		public string EbizAction { get; set; }
		public DateTime EbizDateReview { get; set; }
		public string EbizComment { get; set; }
		public string PTSP { get; set; }
		public DateTime DateDownloaded { get; set; }
		public string DownloadedBy { get; set; }
		public string AcknowledgedBy { get; set; }
		public DateTime AknowledgedDate { get; set; }
		public int DaysInterval { get; set; }
		public DateTime ReqProfilingDate { get; set; }
		public int ProfilingStatus { get; set; }
		public DateTime profillingDate { get; set; }
		public string profilingBy { get; set; }
		public string BatchAkw { get; set; }
		public string AltPhone { get; set; }
		public string ROId { get; set; }
		public string Path1 { get; set; }
		public string Path2 { get; set; }
		public string Path3 { get; set; }
		public string Path4 { get; set; }
		public int Reminder { get; set; }
		public string Sec_contactName { get; set; }
		public string Sec_contact_mobile { get; set; }
		public string PostalCode { get; set; }
		public string DeviceName { get; set; }
		public string DeviceNo { get; set; }
		public string AppName { get; set; }
		public string AppVersion { get; set; }
		public string NetworkType { get; set; }
		public string Network_Operator { get; set; }
		public string SerialNo { get; set; }
		public string DeviceBrand { get; set; }
		public string DeviceModel { get; set; }
		public string Sec_Operator { get; set; }
		public string Sec_Network { get; set; }
		public string MerchantAcquirerType { get; set; }
		public string BranchName { get; set; }
		public string BusCatName { get; set; }
		public string BusOcpName { get; set; }
		public string MasterPassHolder { get; set; }
		public string NibbsUSSDHolder { get; set; }
	}
}