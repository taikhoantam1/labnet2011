using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DomainModel
{
    public static class Constant
    {
        public static string DomainUrl = "http://labnet.vn";
    }

    public static class Domain
    {
        public static string DomainUrl = "http://www.labnet.vn";
    }

    public enum ConnectionStateEnum
    {
        Available =0,
        Connected =1,
        ConnectionRemoveByLab = 2,
        ConnectionRemoveByDoctor = 3,     
    }

    public enum LabClientType
    {
        System = 0,
        ConnectionLab = 1
    }

    public enum ConnectionType
    {
        Doctor = 0,
        Lab = 1
    }
}