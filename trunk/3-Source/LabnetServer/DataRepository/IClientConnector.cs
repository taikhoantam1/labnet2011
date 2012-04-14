using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataRepository
{
    public interface IClientConnector
    {
        string SetupConnectionWithLab(string connectionCode, int serverDoctorId,int clientDoctorId,string clientUrl); 
    }
}
