using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for UploadFile
/// </summary>
namespace LibraryFuntion
{
    public class UploadFile
    {
        public UploadFile()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        static public string DeleteAttachFile(string strAttachFile)
        {
            try
            {
                System.IO.File.Delete(strAttachFile);
                return "ThanhCong";
            }
            catch (Exception exp)
            {
                return exp.Message;

            }
        }
        static public string UploadAnh(HtmlInputFile pControl, Page page, string strServerFolder, string TenFile)
        {
            string strFiletmp = "";
            try
            {
                if (pControl.Value != "")
                {
                    strFiletmp = TenFile + Path.GetExtension(pControl.Value);


                    if (File.Exists(page.Server.MapPath(".") + strServerFolder + strFiletmp))
                    {
                        DeleteAttachFile(page.Server.MapPath(".") + strServerFolder + strFiletmp);
                    }
                    pControl.PostedFile.SaveAs(page.Server.MapPath(".") + strServerFolder + strFiletmp);
                    System.IO.File.SetAttributes(page.Server.MapPath(".") + strServerFolder + strFiletmp, System.IO.FileAttributes.Normal);
                }
                else
                {
                    return "Vui lòng chọn file!";
                }
            }
            catch (Exception exp)
            {
                string strErr = exp.Message;
                return "Đường dẫn không hợp lệ. Vui lòng xem lại.";
            }
            return "successfully_" + strServerFolder + strFiletmp;
        }
    }
}