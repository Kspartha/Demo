﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Usermngt.BL;
using Usermngt.Entities;

namespace UserMgmt
{
    public partial class Suggestionfrom : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        List<District> Districts = new List<District>();
        List<ThanaMaster> Thana = new List<ThanaMaster>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (ViewState["Records"] == null)
                {
                    dt.Columns.Add("Status");
                    dt.Columns.Add("Doc_Name");
                    dt.Columns.Add("Discription");
                    dt.Columns.Add("Doc_Path");
                    dt.Columns.Add("Doc_id");
                    ViewState["Records"] = dt;
                }
                //List<State> statelist = new List<State>();
                //statelist = BL_User_Mgnt.GetListValues(Session["UserID"].ToString());
                //ddlState.DataSource = statelist;
                //ddlState.DataTextField = "State_Name";
                //ddlState.DataValueField = "State_Code";
                //ddlState.DataBind();
                //ddlState.Items.Insert(0, "Select");
                if (Session["rtype"] != null)
                {
                    if (Session["rtype"].ToString() != "0")
                    {
                        Complaint cmp = new Complaint();
                        cmp = BL_Complaint.GetDetails(Convert.ToInt32(Session["Complaint_ID"].ToString()));
                        txtcomplainantname.Text = cmp.complainant_name;
                        txtmobile.Text = cmp.mobile_no.ToString();
                        txtAddress.Text = cmp.address;
                        if ((Session["rtype"].ToString() == "1"))
                        {
                          
                            txtcomplainantname.ReadOnly = true;
                            txtmobile.ReadOnly = true;
                            txtAddress.ReadOnly = true;
                            btnCancel.Visible = false;
                            btnSaveasDraft.Visible = false;
                            btnSubmit.Visible = false;
                        }

                        else
                        {
                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            sb.Append("<script type = 'text/javascript'>");
                            sb.Append("window.onload=function(){");
                            sb.Append("alert('");
                            sb.Append("Database Server Not Connecting");
                            sb.Append("')};");
                            sb.Append("</script>");
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                        }
                    }

                    else
                    {
                        Response.Redirect("~/User_Mgmt");
                    }
                }

            }

        }

        protected void ShowRecord_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("ComplaintList.aspx");

        }



        protected void btnSaveasDraft_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Complaint cmp = new Complaint();
                cmp.complainant_name = txtcomplainantname.Text;

                cmp.mobile_no = Convert.ToDouble(txtmobile.Text);
                cmp.address = txtAddress.Text;
                cmp.record_status = "N";
                string val;
                //if (Session["rtype"].ToString() == "0")
                    val = BL_Complaint.SuggestionInsert(cmp);
                //else
                //{
                //    cmp.complaint_id = Convert.ToInt32(Session["Complaint_ID"].ToString());
                //    val = BL_Complaint.Update(cmp);
                //}
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("https://prohibitionbihar.in/#");
                }
                else
                {

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(val);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                }

            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Complaint cmp = new Complaint();
                cmp.complainant_name = txtcomplainantname.Text;

              cmp.mobile_no = Convert.ToDouble(txtmobile.Text);
                cmp.address = txtAddress.Text;
                cmp.record_status = "N";
                string val;
                //if (Session["rtype"].ToString() == "0")
                val = BL_Complaint.SuggestionInsert(cmp);
                //else
                //{
                //    cmp.complaint_id = Convert.ToInt32(Session["Complaint_ID"].ToString());
                //    val = BL_Complaint.Update(cmp);
                //}
                if (val == "0")
                {
                    Session["UserID"] = Session["UserID"];
                    Response.Redirect("https://prohibitionbihar.in/#");
                }
                else
                {

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(val);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", sb.ToString());
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["UserID"] = Session["UserID"];
            Response.Redirect("https://prohibitionbihar.in/#");
        }




        protected string Generate_otp()
        {
            char[] charArr = "0123456789".ToCharArray();
            string strrandom = string.Empty;
            Random objran = new Random();
            for (int i = 0; i < 4; i++)
            {
                //It will not allow Repetation of Characters
                int pos = objran.Next(1, charArr.Length);
                if (!strrandom.Contains(charArr.GetValue(pos).ToString())) strrandom += charArr.GetValue(pos);
                else i--;
            }
            return strrandom;
        }
        // End OTP Generation function
        // Start Send OTP code on button click
        protected void btnSendOTP_Click(object sender, EventArgs e)
        {
            string otp = Generate_otp();
            string mobileNo = txtmobile.Text.Trim();
            string SMSContents = "", smsResult = "";
            SMSContents = otp + " is your One-Time Password, valid for 10 minutes only, Please do not share your OTP with anyone.";
            //sendSingleSMS("BIHAREDISTRICT-excise", "Excise@123", "BRGOVT", mobileNo, SMSContents, "584fdb0f-6287-490f-a071-6102880a9327", "1307161012079589890");
            //smsResult = SendSMS(mobileNo, SMSContents);


            var request = (HttpWebRequest)WebRequest.Create("http://sms.hspsms.com/sendSMS");
            var postData = "&username=yogibaluragi@datacontech.com";
            postData += "&message=Dear User,RAVI B S(" + otp + ") Entry Allowed at DATACON HEAD OFFICE on 21 / 03 / 2022 10:30:49 AM   DATACON TECHNOLOGIES";
            postData += "&sendername=DATACO";
            postData += "&smstype=TRANS";
            postData += "&numbers=8095346146";

            postData += "&apikey=13a25db6-1fc1-4b5a-acb9-1e5b2cf1bb90";




            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();


            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("controlroom@prohibitionbihar.in");
            msg.To.Add("charan@datacontech.com");
            msg.Subject = "Sending OTP";
            //if (idupDocument.HasFile)
            //{
            //    string FileName = Path.GetFileName(idupDocument.PostedFile.FileName);
            //    msg.Attachments.Add(new Attachment(idupDocument.PostedFile.InputStream, FileName));
            //}
            msg.Body = SMSContents;
            SmtpClient smt = new SmtpClient();
            smt.Host = "smtpout.asia.secureserver.net";
            // smt.Host = "smtp.gmail.com";
            System.Net.NetworkCredential ntwd = new NetworkCredential();
            ntwd.UserName = "controlroom@prohibitionbihar.in"; //Your Email ID  
            ntwd.Password = "IEMS@123"; // Your Password  
            smt.UseDefaultCredentials = true;
            smt.Credentials = ntwd;
            smt.Port = 587;
            smt.EnableSsl = true;
            try
            {
                smt.Send(msg);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Email sent.');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Email not sent.');", true);
            }
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "window.close()", true);
            ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopup", "window.close();window.opener.location.href=window.opener.location.href;", true);
            txtmobile.Focus();
            lblMsg.Text = " OTP is sent to your registered mobile no.";
            lblMsg.CssClass = "green";
            mobileNo = string.Empty;
            txtmobile.Focus();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "countdown()", true);
        }

        public static string SendSMS(string MblNo, string Msg)
        {
            string MainUrl = "smtpout.asia.secureserver.net"; //Here need to give SMS API URL
            string UserName = "controlroom@prohibitionbihar.in"; //Here need to give username
            string Password = "IEMS@123"; //Here need to give Password
            string SenderId = "controlroom@prohibitionbihar.in";
            string strMobileno = MblNo;
            string URL = "";
            URL = MainUrl + "username=" + UserName + "&msg_token=" + Password + "&sender_id=" + SenderId + "&message=" + HttpUtility.UrlEncode(Msg).Trim() + "&mobile=" + strMobileno.Trim() + "";
            string strResponce = GetResponse(URL);
            string msg = "";
            if (strResponce.Equals("Fail"))
            {
                msg = "Fail";
            }
            else
            {
                msg = strResponce;
            }
            return msg;



            //val = BL_HelpDesk.Insert(Helpdesk);
            //MailMessage msg = new MailMessage();
            //msg.From = new MailAddress("controlroom@prohibitionbihar.in");
            //msg.To.Add("charan.ccsd@gmail.com");
            //msg.Subject = "Issues";
            ////if (idupDocument.HasFile)
            ////{
            ////    string FileName = Path.GetFileName(idupDocument.PostedFile.FileName);
            ////    msg.Attachments.Add(new Attachment(idupDocument.PostedFile.InputStream, FileName));
            ////}
            //msg.Body = "Resolve Time";
            //SmtpClient smt = new SmtpClient();
            //smt.Host = "smtpout.asia.secureserver.net";
            //// smt.Host = "smtp.gmail.com";
            //System.Net.NetworkCredential ntwd = new NetworkCredential();
            //ntwd.UserName = "controlroom@prohibitionbihar.in"; //Your Email ID  
            //ntwd.Password = "IEMS@123"; // Your Password  
            //smt.UseDefaultCredentials = true;
            //smt.Credentials = ntwd;
            //smt.Port = 587;
            //smt.EnableSsl = true;
            //try
            //{
            //    smt.Send(msg);
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Email sent.');", true);
            //}
            //catch (Exception ex)
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Email not sent.');", true);
            //}
            ////ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "window.close()", true);
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopup", "window.close();window.opener.location.href=window.opener.location.href;", true);
        }

        public String sendSingleSMS(String username, String password, String senderid, String mobileNo, String message, String secureKey, String templateid)
        {
            //Latest Generated Secure Key
            Stream dataStream;

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; //forcing .Net framework to use TLSv1.2

            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(" ");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://msdgweb.mgov.gov.in/esms/sendsmsrequestDLT");
            request.ProtocolVersion = HttpVersion.Version10;
            request.KeepAlive = false;
            request.ServicePoint.ConnectionLimit = 1;

            //((HttpWebRequest)request).UserAgent = ".NET Framework Example Client";
            ((HttpWebRequest)request).UserAgent = "Mozilla/4.0 (compatible; MSIE 5.0; Windows 98; DigExt)";

            request.Method = "POST";

            System.Net.ServicePointManager.CertificatePolicy = new MyPolicy();

            String encryptedPassword = encryptedPasswod(password);
            String NewsecureKey = hashGenerator(username.Trim(), senderid.Trim(), message.Trim(), secureKey.Trim());
            String smsservicetype = "singlemsg"; //For single message.

            //String query = "username=" + HttpUtility.UrlEncode(username.Trim()) +
            //    "&password=" + HttpUtility.UrlEncode(encryptedPassword) +

            //    "&smsservicetype=" + HttpUtility.UrlEncode(smsservicetype) +

            //    "&content=" + HttpUtility.UrlEncode(message.Trim()) +

            //    "&mobileno=" + HttpUtility.UrlEncode(mobileNo) +

            //    "&senderid=" + HttpUtility.UrlEncode(senderid.Trim()) +
            //  "&key=" + HttpUtility.UrlEncode(NewsecureKey.Trim());

            String query = "username=" + HttpUtility.UrlEncode(username.Trim()) +

             "&password=" + HttpUtility.UrlEncode(encryptedPassword) +

             "&smsservicetype=" + HttpUtility.UrlEncode(smsservicetype) +

             "&content=" + HttpUtility.UrlEncode(message.Trim()) +

             "&bulkmobno=" + HttpUtility.UrlEncode(mobileNo) +

             "&senderid=" + HttpUtility.UrlEncode(senderid.Trim()) +

            "&key=" + HttpUtility.UrlEncode(NewsecureKey.Trim()) +

        "&templateid=" + HttpUtility.UrlEncode(templateid.Trim());

            byte[] byteArray = Encoding.ASCII.GetBytes(query);

            request.ContentType = "application/x-www-form-urlencoded";

            request.ContentLength = byteArray.Length;



            dataStream = request.GetRequestStream();

            dataStream.Write(byteArray, 0, byteArray.Length);

            dataStream.Close();

            WebResponse response = request.GetResponse();

            String Status = ((HttpWebResponse)response).StatusDescription;

            dataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);

            String responseFromServer = reader.ReadToEnd();

            reader.Close();

            dataStream.Close();

            response.Close();
            return responseFromServer;

        }


        public static string GetResponse(string smsURL)
        {
            try
            {
                WebClient objWebClient = new WebClient();
                System.IO.StreamReader reader = new System.IO.StreamReader(objWebClient.OpenRead(smsURL));
                string ResultHTML = reader.ReadToEnd();
                return ResultHTML;
            }
            catch (Exception)
            {
                return "Fail";
            }
        }
        //protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlState.SelectedValue == "BH")
        //    {
        //        Districts = new List<District>();
        //        Districts = BL_User_Mgnt.GetDistricts("");
        //        var org_master1 = from s in Districts
        //                          where s.state_Code == ddlState.SelectedValue
        //                          select s;
        //        ddlDistrict.DataSource = org_master1.ToList();
        //        ddlDistrict.DataTextField = "District_Name";
        //        ddlDistrict.DataValueField = "District_Code";
        //        ddlDistrict.DataBind();
        //        ddlDistrict.Items.Insert(0, "Select");
        //        txtdistrict.Visible = false;
        //        ddlDistrict.Visible = true;
        //        txtthana.Visible = false;
        //        ddlDistrict.Visible = true;
        //        ddlThana.Visible = true;
        //    }
        //    else
        //    {
        //        txtthana.Visible = true;
        //        txtdistrict.Visible = true;
        //        ddlDistrict.Visible = false;
        //        ddlThana.Visible = false;
        //    }
        //}

        //protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Thana = new List<ThanaMaster>();
        //    Thana = BL_Thana.GetThanaList(ddlDistrict.SelectedValue);
        //    var org_master1 = from s in Thana
        //                      where s.district_code == ddlDistrict.SelectedValue
        //                      select s;
        //    ddlThana.DataSource = org_master1.ToList();
        //    ddlThana.DataTextField = "thana_name";
        //    ddlThana.DataValueField = "thana_code";
        //    ddlThana.DataBind();
        //    ddlThana.Items.Insert(0, "Select");
        //}
        protected String encryptedPasswod(String password)
        {

            byte[] encPwd = Encoding.UTF8.GetBytes(password);
            //static byte[] pwd = new byte[encPwd.Length];
            HashAlgorithm sha1 = HashAlgorithm.Create("SHA1");
            byte[] pp = sha1.ComputeHash(encPwd);
            // static string result = System.Text.Encoding.UTF8.GetString(pp);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in pp)
            {

                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();

        }
        protected String hashGenerator(String Username, String sender_id, String message, String secure_key)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append(Username).Append(sender_id).Append(message).Append(secure_key);
            byte[] genkey = Encoding.UTF8.GetBytes(sb.ToString());
            //static byte[] pwd = new byte[encPwd.Length];
            HashAlgorithm sha1 = HashAlgorithm.Create("SHA512");
            byte[] sec_key = sha1.ComputeHash(genkey);

            StringBuilder sb1 = new StringBuilder();
            for (int i = 0; i < sec_key.Length; i++)
            {
                sb1.Append(sec_key[i].ToString("x2"));
            }
            return sb1.ToString();
        }
    }
}