using System;
using System.IO;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Text;
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
            string partNumber = split[(int)tagList.partNum];
            StringBuilder query = new StringBuilder(1000);
            query.Append(@"Company = 'CA' And PartNum = '" + partNumber + "\'");
            query.Append(@" And WareHouseCode = '01' And BinNum = '10011'");
            query.Append(@" And DimCode = '' And LotNum = ''");
            query.Append(@" And ( SysDate > '12/31/2010' Or (SysDate = '12/31/2010' And SysTime >= '51690'))");
            query.Append(@" BY PartNum ");
            PartTran partTran = new PartTran(objSess.ConnectionPool);
            bool morePages;
            PartTranListDataSet partList = partTran.GetList(query.ToString(), 0, 0, out morePages);
            StringBuilder tagQuery = new StringBuilder(1000);
            tagQuery.Append("Company = 'CA' And GroupID = 'J11'");
            tagQuery.Append(" And TagNum <> \'" + tagNumber + "\' And PartNum = '1129'");
            tagQuery.Append(" And WareHouseCode = '01' And BinNum = '10011'");
            tagQuery.Append(" And TagNum <> '' BY TagNum ");

            CountTagListDataSet countTagList = tag.GetList(tagQuery.ToString(), 0, 0, out morePages);
            CountTagDataSet ds = tag.GetByID("J11", tagNumber);
            try
            {
                tag.DeleteByID("J11", tagNumber);
                // tag.VoidTag("J11", tagNumber, ds);
            }
            catch (Exception e)
            {
                string message = e.Message;
            }
        }
            
        public void VoidTagOld(string line)
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

     
