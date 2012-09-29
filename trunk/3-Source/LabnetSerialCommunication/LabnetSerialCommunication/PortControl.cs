using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace LabnetSerialCommunication
{
    public class PortControl
    {
        public PortControl()
        {
        }

        public void OpenAU600Port(SerialPort portAu600, DataGridView dataGridInstrumentTable, List<bool> lstValidInstruments, int row)
        {
            try
            {
                if (!portAu600.IsOpen)
                {
                    portAu600.Open();
                    dataGridInstrumentTable.Rows[row].Cells[4].Value = IConstant.OPENED;
                    lstValidInstruments[row] = true;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void OpenCellDyn1700Port(SerialPort portCellDyn1700, DataGridView dataGridInstrumentTable, List<bool> lstValidInstruments, int row)
        {
            try
            {
                if (!portCellDyn1700.IsOpen)
                {
                    portCellDyn1700.Open();
                    dataGridInstrumentTable.Rows[row].Cells[4].Value = IConstant.OPENED;
                    lstValidInstruments[row] = true;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void OpenCellDyn3200Port(SerialPort portCellDyn3200, DataGridView dataGridInstrumentTable, List<bool> lstValidInstruments, int row)
        {
            try
            {
                if (!portCellDyn3200.IsOpen)
                {
                    portCellDyn3200.Open();
                    dataGridInstrumentTable.Rows[row].Cells[4].Value = IConstant.OPENED;
                    lstValidInstruments[row] = true;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void OpenClinitekPort(SerialPort portClinitek, DataGridView dataGridInstrumentTable, List<bool> lstValidInstruments, int row)
        {
            try
            {
                if (!portClinitek.IsOpen)
                {
                    portClinitek.Open();
                    dataGridInstrumentTable.Rows[row].Cells[4].Value = IConstant.OPENED;
                    lstValidInstruments[row] = true;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
