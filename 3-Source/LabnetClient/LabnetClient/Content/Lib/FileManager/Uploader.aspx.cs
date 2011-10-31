using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
public partial class Controls_ACMSTextBox_JavaScript_tiny_mce_plugins_FileManager_Uploader : System.Web.UI.Page
{
   // public string str_cheat_link = "D:\\LaCaQuanXa\\3-Source\\TinThanhNien\\TinThanhNien\\Image\\";
    protected void Page_Load(object sender, EventArgs e)
    {
        //AuthenticateFileManager();
        if (Request.Params["exception"] != null)
        {
            spanerror.InnerHtml = "<br/>" + Request.Params["exception"].ToString();
        }
    }



    public void AuthenticateFileManager()
    {
        /* Edit this funcation to  AuthenticateFileManager*/
        /*string SessionID = Request["sessionid"].ToString();

        if (Request.Cookies[SessionID] != null)
        {

        }
        else
        {
            Response.Clear();
            Response.Write("Access Denied");
            Response.End();
        }*/
       
    }

    protected void bntUpload_Click(object sender, EventArgs e)
    {
        try
        {
            txtPath.Text = txtPath.Text.Replace("%20", " ");
            string FilePath = Server.MapPath(txtPath.Text.Replace("//", "/") + FileUpload1.FileName.ToString());
            if (FileUpload1.HasFile)
            {
                if (System.IO.File.Exists(FilePath))
                {
                    spanerror.InnerHtml = "<br/> " + FileUpload1.FileName.ToString() + " File Exists ";
                }
                else
                {
                    //if (FileUpload1.FileName.EndsWith(".bmp") || FileUpload1.FileName.EndsWith(".gif") || FileUpload1.FileName.EndsWith(".jpg") || FileUpload1.FileName.EndsWith(".png") || FileUpload1.FileName.EndsWith(".icon")|| FileUpload1.FileName.EndsWith(".swf"))
                    {
                      //  string folder_cheat = txtPath.Text.Replace(@"../../Image/Upload/","");
                        FileUpload1.SaveAs(Server.MapPath(txtPath.Text + FileUpload1.FileName.ToString()));
                        spanerror.InnerHtml = "";
                      //  FileUpload1.SaveAs(str_cheat_link + folder_cheat + FileUpload1.FileName.ToString());
                    }
                    //else
                    //    spanerror.InnerHtml = "<br/> File is not an image file";
                }
               
            }
            else
            {
                spanerror.InnerHtml = "<br/> Please specify the file to upload";
            }
        }
        catch (Exception ex)
        {
            spanerror.InnerHtml = "<br/>" + ex.Message;
        }
                 
    }
    

}