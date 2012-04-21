using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DomainModel
{
    public static class Constant
    {
        public static string DomainUrl = "labnet.vn";
    }

    public enum ConnectionStateEnum
    {
        Available =0,
        Connected =1,
        ConnectionRemoveByLab = 2,
        ConnectionRemoveByDoctor = 3,
        
    }
}