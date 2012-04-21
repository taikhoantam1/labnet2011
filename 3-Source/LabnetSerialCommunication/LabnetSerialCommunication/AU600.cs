using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using DataRepository;
using System.Windows.Forms;
using System.IO;

namespace LabnetSerialCommunication
{
    public class AU600
    {
        public bool isConnectAvailable = true;
        public IDataRepository AU600Repository;
        public AU600()
        {
            AU600Repository = new Repository();
        }

        public List<string[]> SplitOutputData(string output)
        {
            //string[] strArrays = output.Split('D');

            byte[] byteToSplit = {02, 44, 42};
            string hexValues = "0244"; // begin of text +'D'
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
                    bool isTest = false;
                    List<string> lstTemp = GetResultFromSplitData(lstStrResult[i]);
                    if (isValidList(lstTemp))
                    {
                        bool isConnect = true;
                        string[] strTemp = new string[lstTemp.Count];
                        int nextPos = 0;
                        for (int j = 0; j < lstTemp.Count; j++)
                        {
                            if (j > 0)
                            {
                                if (lstTemp[j - 1] == IConstant.AU600NAME)
                                {
                                    isTest = true;
                                    nextPos = j;
                                }
                            }
                            strTemp[j] = lstTemp[j].ToString();

                            if (isTest && nextPos == j)
                            {
                                int testId = AU600Repository.GetTestIdByInstrumentAndTestCode(IConstant.AU600ID, lstTemp[j].ToString());
                                if (testId == 0)
                                {
                                    WriteToFileToTest(lstStrResult[i]);
                                    isConnect = false;
                                    isConnectAvailable = false;
                                    break;
                                }
                                strTemp[j] = testId.ToString();
                                nextPos = j + 2;
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

        private bool isValidList(List<string> lstTemp)
        {
            for (int i = 0; i < lstTemp.Count; i++)
            {
                if (lstTemp[i] == IConstant.AU600NAME && (i + 1) < lstTemp.Count)
                {
                    return true;
                }
            }
            return false;
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

        private bool isValidData(string str)
        {
            if (str.Replace(" ", "").Length < 15)
                return false;
            return true;
        }

        private List<string> GetResultFromSplitData(string strData)
        {
            strData = ReplaceAllInvalidCharacter(strData);
            while (strData.Contains("  ")) strData = strData.Replace("  ", " ");
            strData = strData.TrimStart();
            strData = strData.TrimEnd();
            string[] strSplit = strData.Split(' ');
            
            List<string> strResult = new List<string>();
            strResult = strSplit.ToList<string>();
            //strResult.RemoveAt(2);
            
            return strResult;
        }

        private string ReplaceAllInvalidCharacter(string strData)
        {
            char[] charToSplit = { 'E', '0' };
            string[] strSplit = Regex.Split(strData, "E0");
            string strEnd = Regex.Replace(strSplit[1], @"[^\d.?]", " ");
            if (strEnd.Contains("?"))
            {
                int pos = strEnd.IndexOf("?");
                strEnd = strEnd.Insert(pos + 1, " ");
            }
            strData = strSplit[0] + IConstant.AU600NAME + strEnd;

            return strData;
        }

        public void InsertToInstrumentResult(List<string[]> strResults)
        {
            for (int i = 0; i < strResults.Count; i++)
            {
                int nextPos = 0;
                bool isTest = false;
                string[] patients = strResults[i];
                for (int j = 2; j < patients.Length; j++)
                {
                    if (patients[j - 1] == IConstant.AU600NAME)
                    {
                        isTest = true;
                        nextPos = j;
                    }

                    string patientId = patients[0];
                    string orderNumber = patients[1];
                    if (isTest && j == nextPos)
                    {
                        AU600Repository.InstrumentResult(orderNumber, Int32.Parse(patients[j]), patients[j + 1], patientId, IConstant.AU600ID);
                        nextPos = j + 2;
                    }
                }
            }
        }

        public bool InsertToInstrumentResult(string[] strResults, string lstStrResult)
        {
            int nextPos = 0;
            bool isTest = false;
            string[] patients = strResults;
            for (int j = 2; j < patients.Length; j++)
            {
                if (patients[j - 1] == IConstant.AU600NAME)
                {
                    isTest = true;
                    nextPos = j;
                }

                string patientId = patients[0];
                string orderNumber = patients[1];
                if (isTest && j == nextPos)
                {

                    if (!AU600Repository.InstrumentResult(orderNumber, Int32.Parse(patients[j]), patients[j + 1], patientId, IConstant.AU600ID))
                    {
                        WriteToFileToTest(lstStrResult);
                        isConnectAvailable = false;
                        return false; ;
                    }
                    nextPos = j + 2;
                }
            }

            return true;
        }

        private void WriteToFileToTest(string data)
        {
            string filePath = IConstant.PATHTOFILE;
            StringBuilder sb = new StringBuilder();

            using (StreamReader sr = new StreamReader(filePath))
            {
                sb.Append(sr.ReadToEnd());
            }    
            sb.AppendLine(data);

            using (StreamWriter outfile =
                new StreamWriter(filePath))
            {
                outfile.Write(sb.ToString());
            }
        }

        public List<int> ReadFile()
        {
            List<int> lstPosition = new List<int>();
            string readStr = "";
            try
            {

                using (StreamReader sr = new StreamReader(IConstant.PATHTOFILE))
                {
                    String line;
                    int i = 0;
                    while ((line = sr.ReadLine()) != null)
                    {
                        readStr += line;
                        if (GetDataFromOuput(line))
                        {
                            lstPosition.Add(i);
                        }
                        i += 1;
                    }

                    sr.Close();
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(exp.Message);
            }

            return lstPosition;
        }

        private bool GetDataFromOuput(string strOutput)
        {
            if (isValidData(strOutput))
            {
                bool isTest = false;
                List<string> lstTemp = GetResultFromSplitData(strOutput);
                if (isValidList(lstTemp))
                {
                    bool isConnect = true;
                    string[] strTemp = new string[lstTemp.Count];
                    int nextPos = 0;
                    for (int j = 0; j < lstTemp.Count; j++)
                    {
                        if (j > 0)
                        {
                            if (lstTemp[j - 1] == IConstant.AU600NAME)
                            {
                                isTest = true;
                                nextPos = j;
                            }
                        }
                        strTemp[j] = lstTemp[j].ToString();

                        if (isTest && nextPos == j)
                        {
                            int testId = AU600Repository.GetTestIdByInstrumentAndTestCode(IConstant.AU600ID, lstTemp[j].ToString());
                            if (testId == 0)
                            {
                                WriteToFileToTest(strOutput);
                                isConnect = false;
                                isConnectAvailable = false;
                                break;
                            }
                            strTemp[j] = testId.ToString();
                            nextPos = j + 2;
                        }
                    }

                    if (isConnect)
                    {
                        if (InsertToInstrumentResult(strTemp, strOutput))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public void RemoveLineInFile(List<int> lstPos)
        {
            var file = new List<string>(System.IO.File.ReadAllLines(IConstant.PATHTOFILE));
            var count = lstPos.Count;
            for (int i = 0; i < count; i++)
            {
                file.RemoveAt(lstPos[i]);

                for (int j = i + 1; j < lstPos.Count; j++)
                {
                    lstPos[j] = lstPos[j] - 1;
                }
            }
            File.WriteAllLines(IConstant.PATHTOFILE, file.ToArray());
        }
    }
}
