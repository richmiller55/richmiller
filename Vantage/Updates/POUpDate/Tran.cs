using System;
using System.Collections;

namespace POUpDate
{
    public class Tran
    {
        string transType;
        string poId;
        string poDateStr;
        System.DateTime poDate; // dont know if exAsia Date or proforma or others
        string typeOfDate;
        int poNum;
        int poLine;
        int seq;

        public Tran(Hashtable htTran)
        {
            TransType = htTran["TransType"].ToString();
            TypeOfDate = htTran["TypeOfDate"].ToString();
            PODateStr = htTran["PODate"].ToString();
            POId = htTran["POId"].ToString();
            seq = Convert.ToInt32(htTran["seq"]);
            SetPONum();
            SetDate();
        }
        public void SetPONum()
        {
            string poSide = POId.Split('_')[0];
            this.PONum = Convert.ToInt32(poSide);
            string lineSide = poId.Split('_')[1];
            this.POLine = Convert.ToInt32(lineSide);
        }
        public string TypeOfDate
        {
            get
            {
                return typeOfDate;
            }
            set
            {
                typeOfDate = value;
            }
        }
        public string TransType
        {
            get
            {
                return transType;
            }
            set
            {
                transType = value;
            }
        }
        public System.DateTime PODate
        {
            get
            {
                return poDate;
            }
            set
            {
                poDate = value;
            }
        }
        public string PODateStr
        {
            get
            {
                return poDateStr;
            }
            set
            {
                poDateStr = value;
            }
        }
        public string POId
        {
            get
            {
                return poId;
            }
            set
            {
                poId = value;
            }
        }
        public int PONum
        {
            get
            {
                return poNum;
            }
            set
            {
                poNum = value;
            }
        }
        public int POLine
        {
            get
            {
                return poLine;
            }
            set
            {
                poLine = value;
            }
        }
        public void SetDate()
        {
            string dateStr = PODateStr;
            string year = dateStr.Substring(0, 4);
            string month = dateStr.Substring(4, 2);
            string day = dateStr.Substring(6, 2);

            System.DateTime dateObj = new DateTime(Convert.ToInt32(year),
                Convert.ToInt32(month), Convert.ToInt32(day));
            this.PODate = dateObj;
        }

    }
}