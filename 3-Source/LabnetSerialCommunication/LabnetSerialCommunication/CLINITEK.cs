using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataRepository;

namespace LabnetSerialCommunication
{
    public class CLINITEK
    {
        public bool isConnectAvailable = true;
        public IDataRepository ClinitekRepository;

        public CLINITEK()
        {
            ClinitekRepository = new Repository();
        }

        public void SplitOutputData(string output)
        {
            string[] lstResult = output.Split(',');
            List<string> strResults = new List<string>();
            for (int i = 0; i < lstResult.Length; i++)
            {
                if (lstResult[i] != "")
                {
                    strResults.Add(lstResult[i]);
                }

                if (lstResult[i] == "" && lstResult[i - 2] == "LEU")
                {
                    break;
                }
            }

            for (int i = 7; i < strResults.Count; i++)
            {
                bool isConnect = true;
                if (i % 2 != 0)
                {
                    int testId = ClinitekRepository.GetTestIdByInstrumentAndTestCode(IConstant.CLINITEKID, strResults[i]);
                    if (testId == 0)
                    {
                        //WriteToFileToTest(lstStrResult[i]);
                        //isConnect = false;
                        //isConnectAvailable = false;
                        //break;
                    }
                    if (testId != 0)
                    {
                        if (!ClinitekRepository.InstrumentResult(strResults[3], testId, strResults[i + 1], strResults[0], IConstant.CLINITEKID))
                        {
                            //WriteToFileToTest(lstStrResult);
                            isConnectAvailable = false;
                        }
                    }
                }
            }
        }
    }
}
