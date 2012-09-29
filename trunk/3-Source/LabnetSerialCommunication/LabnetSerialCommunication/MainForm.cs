using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataRepository;
using System.IO;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;
using System.Net;

namespace LabnetSerialCommunication
{
    public partial class MainForm : Form
    {
        public IDataRepository Repository;
        List<Instrument> lstInstruments;
        List<bool> lstValidInstruments;
        PortControl port;
        AU600 instrumentAU600;
        string dataAU600 = "";
        string dataCellDyn3200 = "";
        CellDyn3200 instrumentCD3200;

        public MainForm()
        {          
            Repository = new Repository();
            port = new PortControl();
            instrumentAU600 = new AU600();
            instrumentCD3200 = new CellDyn3200();

            lstInstruments = Repository.GetInstruments();
            lstValidInstruments = new List<bool>(lstInstruments.Count);
            for (int i = 0; i < lstInstruments.Count; i++)
            {
                lstValidInstruments.Add(false);
            }

            //Check data in temporatory file
            List<int> lstPosition = instrumentAU600.ReadFile();
            instrumentAU600.RemoveLineInFile(lstPosition);

            InitializeComponent();
            
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
           
            for (int i = 0; i < lstInstruments.Count; i++)
            {
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(dataGridInstrumentTable);
                newRow.Cells[0].Value = false;
                newRow.Cells[1].Value = lstInstruments[i].Name;
                newRow.Cells[2].Value = lstInstruments[i].COMName.ToString() + ", " + lstInstruments[i].BaudRate.ToString() + ", " + lstInstruments[i].Databits.ToString()
                                        + ", " + lstInstruments[i].Parity.ToString() + ", " + lstInstruments[i].Stopbit.ToString();
                newRow.Cells[3].Value = "Cài đặt";
                newRow.Cells[4].Value = IConstant.CLOSED;
                newRow.Cells[5].Value = lstInstruments[i].Id;
                dataGridInstrumentTable.Rows.Add(newRow);
            }

            //For test only

            timerConnect = new Timer();

            timerConnect.Interval = (300000) * (1);             // Timer will tick
            timerConnect.Enabled = true;                       // Enable the timer
            timerConnect.Tick += new EventHandler(timerConnect_Tick); // Everytime timer ticks, timer_Tick will be called
            timerConnect.Start();                              // Start the timer
            
            
            try
            {
                using (StreamReader sr = new StreamReader("E:\\LabNetProject\\SVN\\3-Source\\LabnetSerialCommunication\\LabnetSerialCommunication\\CD3200.txt"))
                {
                    String line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        dataCellDyn3200 += line;
                    }
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(exp.Message);
            }
            //WriteToFileToTest(dataAU600);
            
            List<string[]> strArrays = instrumentCD3200.SplitOutputData(dataCellDyn3200);
            //instrumentAU600.InsertToInstrumentResult(strArrays);
        }

        private void timerConnect_Tick(object sender, EventArgs e)
        {
            if (!instrumentAU600.isConnectAvailable)
            {
                List<int> lstPosition = instrumentAU600.ReadFile();
                instrumentAU600.RemoveLineInFile(lstPosition);
                instrumentAU600.isConnectAvailable = true;
            }
        }

        private void dataGridInstrumentTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                int col = this.dataGridInstrumentTable.CurrentCell.ColumnIndex;
                int row = this.dataGridInstrumentTable.CurrentCell.RowIndex;
                string id = this.dataGridInstrumentTable.Rows[row].Cells[5].Value.ToString();
                //MessageBox.Show("Button in Cell[" +
                //    col.ToString() + "," +
                //    row.ToString() + "] has been clicked" +
                //   " & Id = " + id.ToString());
            }
        }

        private void btnOpenPort_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridInstrumentTable.RowCount; i++)
            {
                if (Boolean.Parse(dataGridInstrumentTable.Rows[i].Cells[0].Value.ToString()) == true && lstValidInstruments[i] == true)
                {
                    string strShow = "Cổng máy " + dataGridInstrumentTable.Rows[i].Cells[1].Value.ToString() + " đã mở, vui lòng chọn cổng khác";
                    MessageBox.Show(strShow, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                if (Boolean.Parse(dataGridInstrumentTable.Rows[i].Cells[0].Value.ToString()) == true && lstValidInstruments[i] == false)
                {
                    string name = lstInstruments[i].Name.Replace(" ", "");
                    if (name.ToUpper() == IConstant.AU600NAME)
                    {
                        port.OpenAU600Port(serialPort_AU600, dataGridInstrumentTable, lstValidInstruments, i);
                    }

                    if (name.ToUpper() == IConstant.CELLDYN1700NAME)
                    {
                        port.OpenCellDyn1700Port(serialPort_CellDyn1700, dataGridInstrumentTable, lstValidInstruments, i);
                    }

                    if (name.ToUpper() == IConstant.CELLDYN3200NAME)
                    {
                        port.OpenCellDyn1700Port(serialPort_CellDyn3200, dataGridInstrumentTable, lstValidInstruments, i);
                    }
                }
            }
        }

        private void btnClosePort_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridInstrumentTable.RowCount; i++)
            {
                if (Boolean.Parse(dataGridInstrumentTable.Rows[i].Cells[0].Value.ToString()) == true && lstValidInstruments[i] == true)
                {
                    string name = lstInstruments[i].Name.Replace(" ", "");
                    if (name.ToUpper() == IConstant.AU600NAME)
                    {
                        serialPort_AU600.Close();
                    }

                    if (name.ToUpper() == IConstant.CELLDYN1700NAME)
                    {
                        serialPort_CellDyn1700.Close();
                    }

                    if (name.ToUpper() == IConstant.CELLDYN3200NAME)
                    {
                        serialPort_CellDyn3200.Close();
                    }

                    dataGridInstrumentTable.Rows[i].Cells[4].Value = IConstant.CLOSED;
                    lstValidInstruments[i] = false;
                }
            }
        }

        private void serialPort_AU600_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            //dataAU600 = "";
            dataAU600 += serialPort_AU600.ReadExisting();
            //string data = serialPort_AU600.ReadExisting();
            if (isValidData(dataAU600))
            {
                WriteToFileToTest(dataAU600);
                List<string[]> strArrays = instrumentAU600.SplitOutputData(dataAU600);
                dataAU600 = "";
            }
            //List<string[]> strArrays = instrumentAU600.SplitOutputData(data);
            //instrumentAU600.InsertToInstrumentResult(strArrays);
        }


        private bool isValidData(string data)
        {
            string hexValues = "03"; // end of text
            char[] values = data.ToCharArray();
            string strHex = "";

            foreach (char letter in values)
            {
                // Get the integral value of the character.
                int value = Convert.ToInt32(letter);
                // Convert the decimal value to a hexadecimal value in string form.
                string hexOutput = String.Format("{0:X2}", value);
                strHex += hexOutput;
            }

            if (strHex.EndsWith(hexValues))
            {
                return true;
            }

            return false;
        }

        private void WriteToFileToTest(string data)
        {
            string filePath = IConstant.PATHTOFILE;
            StringBuilder sb = new StringBuilder();

            using (StreamReader sr = new StreamReader(filePath))
            {
                sb.Append(sr.ReadToEnd());
                sb.AppendLine();
                sb.AppendLine();
            }


            sb.AppendLine("= = = = = =");
            sb.Append(dataAU600);
            sb.AppendLine();

            using (StreamWriter outfile =
                new StreamWriter(filePath))
            {
                outfile.Write(sb.ToString());
            }
        }

        public bool isConnectionAvailable()
        {
            bool _success = true;
            //build a list of sites to ping, you can use your own
            string[] sitesList = { "www.google.com", "www.labnet.vn"};
            //create an instance of the System.Net.NetworkInformation Namespace
            Ping ping = new Ping();
            //Create an instance of the PingReply object from the same Namespace
            PingReply reply;
            //int variable to hold # of pings not successful
            int notReturned = 0;
            try
            {
                //start a loop that is the lentgh of th string array we
                //created above
                for (int i = 0; i < sitesList.Length; i++)
                {
                    //use the Send Method of the Ping object to send the
                    //Ping request
                    reply = ping.Send(sitesList[i], 10);
                    //now we check the status, looking for,
                    //of course a Success status
                    if (reply.Status != IPStatus.Success)
                    {
                        //now valid ping so increment
                        notReturned += 1;
                    }
                    //check to see if any pings came back
                    if (notReturned == sitesList.Length)
                    {
                        _success = false;
                        //comment this back in if you have your own excerption
                        //library you use for you applications (use you own
                        //exception names)
                        //throw new ConnectivityNotFoundException(@"There doest seem to be a network/internet connection.\r\n
                        //Please contact your system administrator");
                        //use this is if you don't your own custom exception library
                        throw new Exception(@"There doest seem to be a network/internet connection.\r\n
                    Please contact your system administrator");
                    }
                    else
                    {
                        _success = true;
                    }
                }
            }
            //comment this back in if you have your own excerption
            //library you use for you applications (use you own
            //exception names)
            //catch (ConnectivityNotFoundException ex)
            //use this line if you don't have your own custom exception
            //library
            catch (Exception ex)
            {
                _success = false;
                MessageBox.Show("Vui lòng kiểm tra kết nối mạng", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return _success;
        }

        private void checkAutomaticUpdate_CheckedChanged(object sender, EventArgs e)
        {
            if (checkAutomaticUpdate.Checked)
            {
                timerPeriod = new Timer();
                
                timerPeriod.Interval = (30000) * (1);              // Timer will tick
                timerPeriod.Enabled = true;                       // Enable the timer
                timerPeriod.Tick += new EventHandler(timerPeriod_Tick); // Everytime timer ticks, timer_Tick will be called
                timerPeriod.Start();                              // Start the timer
            }

            if (!checkAutomaticUpdate.Checked)
            {
                timerPeriod.Stop();
                timerPeriod.Enabled = false;
            }
        }

        private void timerPeriod_Tick(object sender, EventArgs e)
        {
            List<InstrumentResult> lstInstrumentResults = Repository.GetAllValidInstrumentResultByCondition(DateTime.Now.Date, null, null);

            if (lstInstrumentResults.Count > 0)
            {
                foreach (InstrumentResult ins in lstInstrumentResults)
                {
                    int orderNumber;
                    if (int.TryParse(ins.OrderNumber, out orderNumber))
                    {
                        Repository.InsertToResult(orderNumber, DateTime.Now.Date, ins.TestId, ins.Result, ins.Id);
                    }
                }
            }
        }

        private void serialPort_CellDyn3200_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {

        }

        /*public void OpenAU600Port(int row)
        {
            try
            {
                if (!serialPort_AU600.IsOpen)
                {
                    serialPort_AU600.Open();
                    dataGridInstrumentTable.Rows[row].Cells[4].Value = IConstant.OPENED;
                    lstValidInstruments[row] = true;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }*/

        /*public void OpenCellDyn1700Port(int row)
        {
            try
            {
                if (!serialPort_CellDyn1700.IsOpen)
                {
                    serialPort_CellDyn1700.Open();
                    dataGridInstrumentTable.Rows[row].Cells[4].Value = IConstant.OPENED;
                    lstValidInstruments[row] = true;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }*/
    }
}
