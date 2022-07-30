using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantAcquirerAPI.Data.Models.Domains
{
    public class DeviceTab
    {
        [Key]
        public int Branchid { get; set; }
        public string SerialNo { get; set; }
        public string DeviceName { get; set; }
        public string DeviceModel { get; set; }
        public string DeviceBrand { get; set; }
        public DateTime DateUploaded { get; set; }
        public string UploadedBy { get; set; }
        public string BatchID { get; set; }
        public string Status { get; set; }

    }
}
