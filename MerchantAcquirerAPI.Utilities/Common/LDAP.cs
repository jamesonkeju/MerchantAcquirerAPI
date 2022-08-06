using MerchantAcquirerAPI.Utilities.LDAPModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DirectoryEntry = System.DirectoryServices.DirectoryEntry;

namespace MerchantAcquirerAPI.Utilities.Common
{
    public class LDAP
    {
        private IConfiguration _config;

        public LDAP(IConfiguration iconfiguration)
        {
            _config = iconfiguration;
        }

        public string _filterAttribute;
        public string msg = "";
        public string fname = "";
        public string sname = "";
        public string mail = "";
        public string id = "";
        public int role = 0;
        public string branchid = "";
     
  

        //private OracleConnection conn = new OracleConnection(_config["OracleConnection"]);
       


        public bool emailIsValid(string email)
        {
            string expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, string.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        public bool Authenticate(string Username, string Password)
        {
            bool authentic = false;

            try
            {
                System.DirectoryServices.DirectoryEntry entry = new System.DirectoryServices.DirectoryEntry("LDAP://" + _config["domain"], Username, Password);
                object nativeObject = entry.NativeObject;
                authentic = true;
            }
            catch (DirectoryServicesCOMException)
            {
                authentic = false;
            }

            return authentic;
        }

        public string GetADUserEmailAddress(string username)
        {
            string tempuser = "";
            string User = _config["ADUsername"];
            string Pass = _config["ADPassword"];
            string domainusername = _config["domain"] + @"\" + User;


            DirectoryEntry entry = new DirectoryEntry(_config["connectAD"], domainusername, Pass);
            //DirectoryEntry entry = new DirectoryEntry();

            //Bind to the native AD object to force authentication
            Object obj = entry.NativeObject;
            DirectorySearcher search = new DirectorySearcher(entry);
            search.Filter = "(SAMAccountName=" + username + ")";

            search.PropertiesToLoad.Add("cn");
            SearchResult rs = search.FindOne();

            //update the new path to the user in the directory
            // _path = result.Path;
            _filterAttribute = (String)rs.Properties["cn"][0];


            if (rs.GetDirectoryEntry().Properties["mail"].Value != null)
            {
                mail = rs.GetDirectoryEntry().Properties["mail"].Value.ToString(); // Email Address
                tempuser = mail;
            }

            return tempuser;

        }


        public bool GetValidEmail(string email)
        {
            bool flag = false;
            try
            {
                string mail = "-1";
                string User = _config["ADUsername"];
                string Pass = _config["ADPassword"];
                string domainusername = _config["domain"] + @"\" + User;
                DirectoryEntry entry = new DirectoryEntry(_config["connectAD"], domainusername, Pass);
                //DirectoryEntry entry = new DirectoryEntry();

                //Bind to the native AD object to force authentication
                Object obj = entry.NativeObject;
                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(mail=" + email + ")";

                search.PropertiesToLoad.Add("cn");
                SearchResult rs = search.FindOne();

                //update the new path to the user in the directory
                // _path = result.Path;
                _filterAttribute = (String)rs.Properties["cn"][0];


                if (rs.GetDirectoryEntry().Properties["mail"].Value != null)
                {
                    mail = rs.GetDirectoryEntry().Properties["mail"].Value.ToString(); // Email Address

                }

                if (mail == "-1")
                {
                    flag = false;
                }
                else
                {
                    flag = true;
                }
            }
            catch (System.Exception ex)
            {
                flag = false;
            }
            return flag;

        }

        public string ADUserInfo(string username)  //Retrieve UserInfo from AD
        {

            string tempuser = "";
            string User = _config["ADUsername"];
            string Pass = _config["ADPassword"];
            string domainusername = _config["domain"] + @"\" + User;

            DirectoryEntry entry = new DirectoryEntry(_config["connectAD"], domainusername, Pass);
            //DirectoryEntry entry = new DirectoryEntry();

            //Bind to the native AD object to force authentication
            Object obj = entry.NativeObject;
            DirectorySearcher search = new DirectorySearcher(entry);
            search.Filter = "(SAMAccountName=" + username + ")";

            search.PropertiesToLoad.Add("cn");
            SearchResult rs = search.FindOne();

            //update the new path to the user in the directory
            // _path = result.Path;
            _filterAttribute = (String)rs.Properties["cn"][0];

            if (rs.GetDirectoryEntry().Properties["samaccountname"].Value != null)
            {
                id = rs.GetDirectoryEntry().Properties["samaccountname"].Value.ToString();  //UserName
            }
            if (rs.GetDirectoryEntry().Properties["givenName"].Value != null)
            {
                fname = rs.GetDirectoryEntry().Properties["givenName"].Value.ToString(); //firstname
                tempuser += fname + ";";
            }
            if (rs.GetDirectoryEntry().Properties["sn"].Value != null)
            {
                sname = rs.GetDirectoryEntry().Properties["sn"].Value.ToString();  // lastname
                tempuser += sname + ";";
            }
            if (rs.GetDirectoryEntry().Properties["mail"].Value != null)
            {
                mail = rs.GetDirectoryEntry().Properties["mail"].Value.ToString(); // Email Address
                tempuser += mail + ";";
            }

            return tempuser;

        }

        public string GetSegmentInfo(string acctno)
        {
            string ret = "Error";
            OracleConnection conn = new OracleConnection(_config["OracleConnection"]);
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                //    Log l = new Log("Connection Open");
                 //   l.SaveError("System");
                    //l.Close();
                }


                string qry = "SELECT  code_desc from fccngn.mitb_class_mapping , fccngn.gltm_mis_code where unit_type ='A' and comp_mis_4= mis_code and branch_code=substr(unit_ref_no,1,3) and unit_ref_no=:acctno and rownum < 2";

                OracleCommand getcmmd = new OracleCommand(qry, conn);

                getcmmd.Parameters.Add(":acctno", acctno);
                OracleDataReader reader = getcmmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ret = reader["code_desc"].ToString();
                     
                   

                    }
                }
                else
                {
                    ret = "Error";
                }
                reader.Close();


            }
            catch (System.Exception ex)
            {

                ret = "Error";


            }
            finally
            {
                conn.Close();
            }
            return ret;
        }

        public string GetInflow(string acctno, DateTime startdate, DateTime enddate)
        {
            string ret = "Error";

            string stdate = startdate.ToString("dd-MMM-yyyy");
            string eddate = enddate.ToString("dd-MMM-yyyy");

            OracleConnection conn = new OracleConnection(_config["OracleConnection"]);
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();

                }


                string qry = "Select ac_no,sum(decode(drcr_ind,'D',lcy_amount,0.00)) tot_Dr, sum(decode(drcr_ind,'C',lcy_amount,0.00)) tot_Cr from fccngn.actb_history where ac_no=" + "'" + acctno + "'" + "And trn_dt between " + "'" + stdate + "'" + "and " + "'" + eddate + "'" + " " + "Group by ac_no";

                OracleCommand getcmmd = new OracleCommand(qry, conn);

                //  getcmmd.Parameters.Add(":acctno", acctno);
                OracleDataReader reader = getcmmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ret = reader["tot_Cr"].ToString() + "|" + reader["tot_Dr"].ToString();


                    }
                }
                else
                {
                    ret = "Error";
                }
                reader.Close();


            }
            catch (System.Exception ex)
            {

                ret = "Error";


            }
            finally
            {
                conn.Close();
            }
            return ret;
        }

     

        public CustomerDetail ValidateAccount_Old(string acctno)
        {
            CustomerDetail cust = new CustomerDetail();
            OracleConnection conn = new OracleConnection(_config["OracleConnection"]);
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();

                }


                //    string qry = "SELECT  distinct a.CUST_AC_NO,a.AC_DESC,a.CUST_NO,a.ACCOUNT_TYPE,a.BRANCH_CODE,a.CCY,ACCOUNT_CLASS,C.E_MAIL,B.UDF_1,B.UDF_2,B.UDF_3,B.UDF_4,B.UDF_5,w.field_val_10 as BVN_NO  FROM FCCNGN.STTM_CUST_ACCOUNT a, FCCNGN.sttm_customer b, FCCNGN.sttm_cust_personal c, FCCNGN.cstm_function_userdef_fields w where a.cust_no = b.customer_no and a.cust_no = c.customer_no(+) and CUST_NO = a.CNO(+) and  a.record_stat = 'O' and a.cust_ac_no =:acctno and rownum < 2";
                //  string qry = "SELECT  distinct a.CUST_AC_NO,a.AC_DESC,a.CUST_NO,a.ACCOUNT_TYPE,a.BRANCH_CODE,a.CCY,ACCOUNT_CLASS,get_account_segment_desc(cust_ac_no, branch_code) acct_seg_desc,C.E_MAIL,B.UDF_1,B.UDF_2,B.UDF_3,B.UDF_4,B.UDF_5,w.BVN_NO  FROM FCCNGN.STTM_CUST_ACCOUNT a, FCCNGN.sttm_customer b, FCCNGN.sttm_cust_personal c, misuser.BVN_TODAY w where a.cust_no = b.customer_no and a.cust_no = c.customer_no(+) AND CUST_NO = CNO(+) and a.record_stat = 'O' and a.cust_ac_no =:acctno";
                string qry = "SELECT Bonig.get_account_segment_desc(cust_ac_no, branch_code) acct_seg_desc ,CUST_AC_NO,AC_DESC,CUST_NO,ACCOUNT_TYPE,BRANCH_CODE,CCY,ACCOUNT_CLASS,C.E_MAIL,B.UDF_1,B.UDF_2,B.UDF_3,B.UDF_4,B.UDF_5 ,BVN_NO FROM FCUBSNIG.STTM_CUST_ACCOUNT a, FCUBSNIG.sttm_customer b, FCUBSNIG.sttm_cust_personal c ,MISUSER.BVN_TODAY W  where a.cust_no = b.customer_no and a.cust_no = c.customer_no(+) AND CUST_NO = CNO(+) and a.record_stat = 'O' and a.cust_ac_no =:acctno";
                //string qry = "SELECT  distinct a.CUST_AC_NO,a.AC_DESC,a.CUST_NO,a.ACCOUNT_TYPE,a.BRANCH_CODE,a.CCY,ACCOUNT_CLASS,get_account_segment_desc(cust_ac_no, branch_code) acct_seg_desc,C.E_MAIL,B.UDF_1,B.UDF_2,B.UDF_3,B.UDF_4,B.UDF_5,w.BVN_NO  FROM FCCNGN.STTM_CUST_ACCOUNT a, FCCNGN.sttm_customer b, FCCNGN.sttm_cust_personal c, misuser.BVN_TODAY w where a.cust_no = b.customer_no and a.cust_no = c.customer_no(+) AND CUST_NO = CNO(+) and a.record_stat = 'O' and a.cust_ac_no =:acctno";


                DataTable dt2 = new DataTable();
                // string qry = "SELECT distinct a.CUST_AC_NO,a.AC_DESC,a.CUST_NO,a.ACCOUNT_TYPE,a.BRANCH_CODE,a.CCY,a.ACCOUNT_CLASS,C.E_MAIL,B.UDF_1,B.UDF_2,B.UDF_3,B.UDF_4,B.UDF_5,e.code_desc  FROM STTM_CUST_ACCOUNT a, sttm_customer b, sttm_cust_personal c ,fccngn.mitb_class_mapping d,fccngn.gltm_mis_code e where mis_code= comp_mis_4 and unit_type = 'A' and unit_ref_no = cust_ac_no  and d.branch_code=a.branch_code and a.cust_no = b.customer_no and a.cust_no = c.customer_no(+) and a.cust_ac_no =:acctno";
                OracleCommand getcmmd = new OracleCommand(qry, conn);

                getcmmd.Parameters.Add(":acctno", acctno);
                OracleDataReader reader2 = getcmmd.ExecuteReader();

                dt2.Load(reader2);

                foreach (DataRow reader in dt2.Rows)

                    if (dt2.Rows.Count > 0)
                    {
                        cust.BranchCode = reader["branch_code"].ToString();

                        // string desc = reader["code_desc"].ToString();

                        cust.AccountClass = reader["ACCOUNT_CLASS"].ToString();
                        cust.AccountNo = reader["cust_ac_no"].ToString();
                        cust.AccountType = reader["account_type"].ToString();
                        cust.Address = reader["UDF_2"].ToString();
                        cust.segment = reader["ACCT_SEG_DESC"].ToString();
                        //  cust.segment = "N/A";
                        cust.Currency = reader["ccy"].ToString();
                        cust.CustomerName = reader["ac_desc"].ToString();
                        cust.CustomerNo = reader["cust_no"].ToString();
                        cust.Email = reader["E_MAIL"].ToString();
                        cust.MobileNo = reader["UDF_5"].ToString();
                        cust.AccountDesc = reader["ac_desc"].ToString();
                        cust.bvn = reader["BVN_NO"].ToString();
                        cust.msg = "Success";

                    }

                    else
                    {
                        cust.CustomerName = "Error";
                        cust.msg = "No Record Found for Account No";
                    }
                reader2.Close();


            }
            catch (System.Exception ex)
            {

                string resp = "Error";
                cust.CustomerName = "Error";
                cust.msg = "Error Occured :" + ex.Message + "|" + ex.StackTrace;

            }
            finally
            {
                conn.Close();
            }

            return cust;
        }

        
        public string GetProperty(SearchResult searchResult, string PropertyName)
        {
            if (searchResult.Properties.Contains(PropertyName))
            {
                return searchResult.Properties[PropertyName][0].ToString();
            }
            else
            {
                return string.Empty;
            }
        }


        public userdetail GetStaffInfo(string username)
        {

            userdetail userRet = new userdetail();
            string User = _config["ADUsername"];
            string Pass = _config["ADPassword"];
            DirectoryEntry de = new DirectoryEntry("LDAP://" + _config["domain"], User, Pass);
            DirectorySearcher deSearch = new DirectorySearcher(de);
            deSearch.Filter = "(&(objectClass=user)(SAMAccountName=" + username.Trim() + "))";
            SearchResult sr = deSearch.FindOne();
            if (sr != null)
            {
                // get DirectoryEntry underlying it 
                // userRet.firstname = sr.Properties["givenName"].ToString();
                userRet.firstname = GetProperty(sr, "givenName");   //firstname

                userRet.lastname = GetProperty(sr, "sn"); //lastname
                userRet.emailaddress = GetProperty(sr, "mail"); //email

                userRet.username = GetProperty(sr, "sAMAccountName");  //username

            }
            else
            {
                userRet = null;
            }

            return userRet;
        }


        // Check Head of Operation


        public string uniqueid()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssFFF");
        }




        public void DataLog(string dataToLog)
        {
            try
            {
                string appLocation = AppDomain.CurrentDomain.BaseDirectory;
                string file = appLocation + "\\ErrorLog.txt";
                if (!File.Exists(file))
                {
                    File.CreateText(file);
                }
                using (StreamWriter writer = File.AppendText(file))
                {
                    string data = "\r\nLog Written at : " + DateTime.Now.ToString() + "\n" + dataToLog;
                    writer.WriteLine(data);
                    writer.WriteLine("==========================================");
                    writer.Flush();
                    writer.Close();
                }
            }
            catch (System.Exception exc)
            {
                throw exc;
            }
        }

        public void ErrorLog(string dataToLog, string filename)
        {
            try
            {

                if (!File.Exists(filename))
                {
                    File.CreateText(filename);
                }
                using (StreamWriter writer = File.AppendText(filename))
                {
                    string data = "\r\nLog Written at : " + DateTime.Now.ToString() + "\n" + dataToLog;
                    writer.WriteLine(data);
                    writer.WriteLine("==========================================");
                    writer.Flush();
                    writer.Close();
                }
            }
            catch (FileNotFoundException exc)
            {
                throw exc;
            }
        }


        public DateTime FirstDayOfMonthFromDateTime(DateTime dateTime)  //Return firstdate of the month
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        public DateTime LastDayOfMonthFromDateTime(DateTime dateTime)
        {
            DateTime firstDayOfTheMonth = new DateTime(dateTime.Year, dateTime.Month, 1);  //Return last date of the month
            return firstDayOfTheMonth.AddMonths(1).AddDays(-1);
        }

        public bool isValidNumber(int k)
        {
            bool flag = false;
            try
            {
                if (Convert.ToInt32(k) > 0)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }
            catch (System.Exception ex)
            {
                flag = false;
            }

            return flag;
        }
        public DataTable GetCardInfo(string acctno)
        {
            DataTable cust = new DataTable();

            OracleConnection conn = new OracleConnection(_config["OracleConnection"]);

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();

                }

                //  string qry2 = "select a.account_no,b.CARD_NUMBER2,b.EXPIRY_DATE,a.FIRST_NAME,a.MIDDLE_NAME,a.LAST_NAME,a.HOME_ADDRESS,a.OFFICE_ADDRESS,a.COUNTRY_OF_RESIDENCE,a.DATE_OF_BIRTH,a.email_address,a.MOBILE_PHONE_NO,a.CUST_ID,a.COLLECTION_BRANCH from indevapp.eco_mc_cust_details a,indevapp.ECO_MC_PROCESSED_CARDS b where a.application_no=b.application_no and a.account_no=:acctno";
                string qry2 = "select * from indevapp.masterpass_view where account_no=:acctno";

                OracleCommand getcmmd = new OracleCommand(qry2, conn);

                getcmmd.Parameters.Add(":acctno", acctno);
                OracleDataReader reader = getcmmd.ExecuteReader();


                cust.Load(reader);


            }
            catch (System.Exception ex)
            {
                DataTable cust1 = new DataTable();

                cust = cust1;


            }

            return cust;
        }
        public string generateQrCodeString(string AccountPan, string MerchantName, string MCCCode, string MerchantCity
           )
        {
            try
            {
                string buildString = "";


                buildString =
                   "11"
                    + "0015"
                    + AccountPan
                    + "0A" + MerchantName.Length.ToString() + MerchantName
                    + "0B0" + MCCCode.Length.ToString() + "6536"
                    + "0C0" + MerchantCity.Length.ToString() + MerchantCity
                    + "0D" + "03" + "566"
                    + "0E" + "03" + "566"
                    + "A904";

                string generateCRC = GetCRC(buildString);
                return buildString + generateCRC;
            }
            catch (System.Exception ex)
            {
                return "-1";

            }
        }

        public static string GetCRC(string inputStr)
        {
            int polynomial = 0x1021;
            int crc = 0xFFFF;

            int[] intArray = new int[inputStr.Length];

            for (int i = 0; i < inputStr.Length; i++)
            {
                intArray[i] = inputStr[i];
            }
            foreach (int b in intArray)
            {
                for (int i = 0; i < 8; i++)
                {
                    bool bit = ((b >> (7 - i) & 1) == 1);
                    bool c15 = ((crc >> 15 & 1) == 1);
                    crc <<= 1;
                    if (c15 ^ bit)
                    {
                        crc ^= polynomial;
                    }
                }
            }

            crc &= 0xFFFF;
            return crc.ToString("X");
        }
   
        public string getDefaultEmail(string inputEmail)
        {
            try
            {
                string[] SplitEamil = inputEmail.Split(',');

                // get the first email if any 
                string SingleEmail = "";
                SingleEmail = SplitEamil[0];

                string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                      @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                      @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

                Regex re = new Regex(strRegex);

                if (re.IsMatch(SingleEmail))
                    return SingleEmail;
                else
                    return "False";
            }
            catch (System.Exception)
            {

                return "False";
            }
        }
        public string getDefaultPhone(string inputPhone)
        {
            try
            {
                string[] SplitPhone = inputPhone.Split(',');

                // get the first email if any 
                string SinglePhoneNo = "";
                SinglePhoneNo = SplitPhone[0];

                string RemovePlus = SinglePhoneNo.Replace("+", "");

                // do a count of the return phone number 
                int TotalCount = RemovePlus.Count();

                if (TotalCount < 11)
                {
                    return "False";
                }
                else
                {
                    return RemovePlus.Trim(); ;
                }
            }
            catch (System.Exception)
            {

                return "False";
            }
        }

        public string FormatExpiryDate(string inputDate)
        {
            try
            {
                string DateTo = inputDate.ToString();

                string[] SplitDate = DateTo.Split(' ');

                // get the first date part if any 
                string[] getDatePart = SplitDate[0].Split('/');

                // format the expire date 
                /// YYYYMM Format
                string getMonth = "";

                if (getDatePart[0].Count() == 1)
                {
                    getMonth = "0" + getDatePart[0];
                }
                else
                {
                    getMonth = getDatePart[0];

                };


                string returnFormat = getDatePart[2] + getMonth;

                return returnFormat;


            }
            catch (System.Exception)
            {

                return "False";
            }
        }

        public string FormatDOBDate(string inputDate)
        {
            try
            {
                string DateTo = inputDate.ToString();

                string[] SplitDate = DateTo.Split(' ');

                // get the first date part if any 
                string[] getDatePart = SplitDate[0].Split('/');

                // format the expire date 
                /// YYYYMM Format
                string getMonth = "";

                if (getDatePart[0].Count() == 1)
                {
                    getMonth = "0" + getDatePart[0];
                }
                else
                {
                    getMonth = getDatePart[0];

                };


                string returnFormat = getDatePart[2] + getMonth + getDatePart[1];

                return returnFormat;


            }
            catch (System.Exception)
            {

                return "False";
            }
        }

       

    }
}
