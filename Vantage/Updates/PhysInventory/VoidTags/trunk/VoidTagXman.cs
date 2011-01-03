using System;
using System.IO;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Epicor.Mfg.BO;
namespace VoidTags
{
    public class VoidTagXman
    {
        public string dataSet;
        Epicor.Mfg.Core.Session objSess;
        CountTag tag;
        //   pilot 8331
        //   sys 8301

        public VoidTagXman()
        {
            AppVars vars = new AppVars();
            string user = vars.User;
            string password = vars.Password;
            string port = vars.DataPort;
            objSess = new Epicor.Mfg.Core.Session(user, password,
                "AppServerDC://VantageDB1:" + port, Epicor.Mfg.Core.Session.LicenseType.Default);
            this.tag = new CountTag(objSess.ConnectionPool);
        }
        public void VoidTag(string line)
        {
            string[] split = line.Split(new Char[] { '\t' });
            string tagNumber = split[(int)tagList.tagNum];
            CountTagDataSet ds = tag.GetByID("J11", tagNumber);
            CountTagDataSet.CountTagRow row = (CountTagDataSet.CountTagRow)ds.CountTag.Rows[0];
            row.TagVoided = true;

            string message;
            try
            {
                tag.Update(ds);
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            /*
            try
            {
                tag.VoidTag("J11", tagNumber, ds);
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            */
        }
    }
}

     
