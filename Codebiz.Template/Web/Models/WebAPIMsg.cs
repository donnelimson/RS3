using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class WebAPIResult
    {
        public string StatusCode { get; set; } = WebApiMsg.OK;
        public string StatusMessage { get; set; } = WebApiMsg.OKMSG;
        public dynamic DataResult { get; set; }
        public int? TotalRecordCount { get; set; }
        public dynamic Attachments { get; set; }
        public bool CodeExist { get; set; }
    }


    public class WebApiMsg {
        public const string ERROR = "-1";
        public const string ERRORMSG = "ERROR";

        public const string NOTFOUND = "-2";
        public const string NOTFOUNDMSG = "NOT FOUND";


        public const string OK = "0";
        public const string OKMSG = "SUCCESS";
    }
}