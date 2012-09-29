using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataRepository;
using System.Text.RegularExpressions;

namespace LabnetSerialCommunication
{
    public class CellDyn3200
    {
        public bool isConnectAvailable = true;
        public IDataRepository CellDyn3200Repository;

        public CellDyn3200()
        {
            CellDyn3200Repository = new Repository();
        }

        public List<string[]> SplitOutputData(string output)
        {
            //string[] strArrays = output.Split('D');

            //byte[] byteToSplit = { 02, 22, 20, 20, 22 };
            string hexValues = "022220202022"; // begin of text + spaces sequence
            char[] values = output.ToCharArray();
            string strHex = "";

            foreach (char letter in values)
            {
                // Get the integral value of the character.
                int value = Convert.ToInt32(letter);
                // Convert the decimal value to a hexadecimal value in string form.
                string hexOutput = String.Format("{0:X2}", value);
                strHex += hexOutput;
                //Console.WriteLine("Hexadecimal value of {0} is {1}", letter, hexOutput);
            }

            String[] lstStrHex = Regex.Split(strHex, hexValues);
            List<string> lstStrResult = new List<string>();

            for (int i = 0; i < lstStrHex.Length; i++)
            {
                string temp = ConvertHexToString(lstStrHex[i], System.Text.Encoding.UTF8);
                lstStrResult.Add(temp);
            }

            List<string[]> strResults = new List<string[]>();
            for (int i = 0; i < lstStrResult.Count; i++)
            {
                if (isValidData(lstStrResult[i]))
                {
                    List<string> lstTemp = GetResultFromSplitData(lstStrResult[i]);
                    if (isValidList(lstTemp))
                    {
                        bool isConnect = true;
                        string[] strTemp = new string[lstTemp.Count];
                        for (int j = 18; j < lstTemp.Count; j++)
                        {
                            int testId = CellDyn3200Repository.GetTestIdByInstrumentAndTestCode(IConstant.CELLDYN3200ID, j.ToString());
                            if (testId == 0)
                            {
                                //WriteToFileToTest(lstStrResult[i]);
                                //isConnect = false;
                                //isConnectAvailable = false;
                                //break;
                            }
                            if (testId != 0)
                            {
                                strTemp[j] = testId.ToString();
                            }
                        }

                        if (isConnect)
                        {
                            strResults.Add(strTemp);
                            InsertToInstrumentResult(strResults[strResults.Count - 1], lstStrResult[i]);
                        }
                    }
                }
            }
            return strResults;
        }

        public bool InsertToInstrumentResult(string[] strResults, string lstStrResult)
        {
            string[] patients = lstStrResult.Split(',');
            string patientId = patients[3];
            string orderNumber = patients[7];
            orderNumber = orderNumber.Trim();
            orderNumber = orderNumber.TrimStart(' ');
            orderNumber = orderNumber.TrimEnd(' ');
            orderNumber = orderNumber.Replace("\"", "");
            int pos = orderNumber.Length - 1;
            for (int i = orderNumber.Length - 1; i >= 0; i--)
            {
                char s = orderNumber[i];
                int ascii = (int)s;

                if (ascii == 32)
                {
                    pos = i;
                }

                if (ascii != 32)
                {
                    break;
                }
            }
            orderNumber = orderNumber.Substring(0, pos);

            for (int j = 18; j < patients.Length; j++)
            {
                if (strResults[j] != null)
                {
                    if (!CellDyn3200Repository.InstrumentResult(orderNumber, Int32.Parse(strResults[j]), float.Parse(patients[j]).ToString(), patientId, IConstant.CELLDYN3200ID))
                    {
                        //WriteToFileToTest(lstStrResult);
                        isConnectAvailable = false;
                        return false; ;
                    }
                }
            }

            return true;
        }

        public static string ConvertHexToString(String hexInput, System.Text.Encoding encoding)
        {
            int numberChars = hexInput.Length;
            byte[] bytes = new byte[numberChars / 2];
            for (int i = 0; i < numberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hexInput.Substring(i, 2), 16);
            }
            return encoding.GetString(bytes);
        }
        
        private bool isValidList(List<string> lstTemp)
        {
            if (lstTemp.Count > 50)
            {
                return true;
            }
            return false;
        }

        private bool isValidData(string str)
        {
            if (str.Replace(" ", "").Length < 15)
                return false;
            return true;
        }

        private List<string> GetResultFromSplitData(string strData)
        {
            string[] strSplit = strData.Split(',');

            List<string> strResult = new List<string>();
            strResult = strSplit.ToList<string>();

            return strResult;
        }
    }
}
