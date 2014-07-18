<%@ WebHandler Language="C#" Class="ReturnValue" %>

using System;
using System.Web;

public class ReturnValue : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        string FileName = "";
        string length = "0";

        foreach (string file in context.Request.Files)
        {
            HttpPostedFile hpf = context.Request.Files[file] as HttpPostedFile;
            length = hpf.ContentLength.ToString();
            if (hpf.ContentLength == 0)
                continue;
            try
            {
                FileName = hpf.FileName.Replace("''", "");
                FileName = FileName.Replace(" ", "_");
                FileName = context.Server.MapPath(".") + "\\" + FileName;
            }
            catch (Exception ex)
            {
                context.Response.Write("there was an error: " + ex.Message);
                return;
            }

            //hpf.SaveAs(savedFileName);
            if ((System.IO.File.Exists(FileName)))
            {
                System.IO.File.Delete(FileName);
            }
        }
        //context.Response.Write(savedFileName);

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}