using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantAcquirerAPI.Services.CustomerRequest.dto
{
	public class CustomerRequestReponse
	{


		public DateTime? ReqDate { get; set; }

		public string AcctNo { get; set; }
		public string AccountClass { get; set; }
		public string AcctName { get; set; }
		public string MerchantID { get; set; }
		public string TerminalNo { get; set; }
		public string MerchantName { get; set; }
		public string ContactTitle { get; set; }
		public string ContactName { get; set; }
		public string MobilePhone { get; set; }
		public string AcctType { get; set; }
		public string Comment { get; set; }
		public string CustID { get; set; }
		public DateTime? profillingDate { get; set; }
		public string Status { get; set; }


	}


	public class POSRequestResponse
    {
	  public string AccountNo { get; set; }
	public string MerchantName { get; set; }
	public string MerchantNumber { get; set; }
    public string TerminalID { get; set; }
	public string Status { get; set; }
	

	}
}
