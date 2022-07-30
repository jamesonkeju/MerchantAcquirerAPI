using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantAcquirerAPI.Data.Models.Domains
{
	public class UserInfo
	{
		[Key]
		public int sn { get; set; }
		public string UserId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string EmailAddress { get; set; }
		public int AccessID { get; set; }
		public DateTime LastLogin { get; set; }
		public string IpAddress { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime DateLastModified { get; set; }
		public string BranchID { get; set; }
		public DateTime RegDate { get; set; }
		public string UserStatus { get; set; }
		public string approveFlag { get; set; }
		public string OnlineStatus { get; set; }
		public string ReqCode { get; set; }
		public string RMCode { get; set; }
		public string CodeStatus { get; set; }
	}
}
