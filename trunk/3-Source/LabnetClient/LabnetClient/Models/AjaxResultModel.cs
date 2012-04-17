using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabnetClient.Models
{
    public class AjaxResultModel
    {
        public AjaxResultModel()
        {
            IsSuccess = true;
        }

        public string ErrorMessage { set; get; }
        
        public bool IsSuccess { get; set; }

        public object ResponseData { get; set; }
    }
}