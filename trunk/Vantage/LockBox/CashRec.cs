using System;
using System.Collections;
using System.Windows.Forms;
using System.Data;

namespace LockBox
{
    public class CashRec
    {
        Epicor.Mfg.Core.Session objSess;
        Epicor.Mfg.BO.CashRec cashRec;
        Epicor.Mfg.BO.CashRecDataSet crDs; 
        Epicor.Mfg.BO.CashRecDataSet.CashHeadRow headRow;
        ArrayList errorMessages = new ArrayList();
        Epicor.Mfg.BO.CashRecDataSet.CashDtlRow detailRow; // issue: there can be more than one detail line
        int headNum;
        int nextCashDetail; 
        string groupID;

        public CashRec(Epicor.Mfg.Core.Session sess, string grpId)
        {
            objSess = sess;
            nextCashDetail = 0;
            groupID = grpId;
            cashRec = new Epicor.Mfg.BO.CashRec(objSess.ConnectionPool);
            
            // Epicor.Mfg.BO.CashRecDataSet.CashHeadRow headRow = (Epicor.Mfg.BO.CashRecDataSet.CashHeadRow)crDs.CashHead.Rows[0];
            // Epicor.Mfg.BO.CashRecDataSet.CashDtlRow detailRow = (Epicor.Mfg.BO.CashRecDataSet.CashDtlRow)crDs.CashDtl.Rows[0];
        }
        public void AddCashPayment()
        {

        }
        public void Update()
        { 

        }
        public void AddMatchedCheck(string custID, Check check, decimal matchedTotal, ArrayList matchedInvoices)
        {
            Cursor.Current = Cursors.WaitCursor;
            crDs = new Epicor.Mfg.BO.CashRecDataSet();
            headNum = 0;
            bool goOn = true;
            try
            {
                cashRec.GetNewCashHead(crDs, groupID);
            }
            catch (Exception e)
            {
                string message = "Msg 8: cashRec.GetNewCashHead failed: " + e.Message;
                this.errorMessages.Add(message);
                goOn = false;
            }
            if (goOn)
            {
                try
                {
                    cashRec.GetCustomerInfo(custID, crDs);
                }
                catch (Exception e)
                {
                    string message = "Msg 7: Customer Not Found: " + e.Message;
                    this.errorMessages.Add(message);
                    goOn = false;
                }
            }
            if (goOn)
            {
                Epicor.Mfg.BO.CashRecDataSet.CashHeadRow hRow = (Epicor.Mfg.BO.CashRecDataSet.CashHeadRow)crDs.CashHead.Rows[0];
                hRow.Company = "CA";
                hRow.TranType = "PayInv";
                
                hRow.DocTranAmt = matchedTotal;
                
                hRow.CheckRef = check.CheckNo;
                try
                {
                    cashRec.Update(crDs);
                }
                catch (Exception e)
                {
                    string message = "Msg 6: Cash Rec Group Not found: " +  e.Message;
                    this.errorMessages.Add(message);
                    goOn = false;
                }
                hRow = (Epicor.Mfg.BO.CashRecDataSet.CashHeadRow)crDs.CashHead.Rows[0];
                headNum = hRow.HeadNum;
            }
            if (goOn)
            {
                // Int32 nPayments = 0;
                foreach (Payment payment in matchedInvoices)
                {

                    Epicor.Mfg.BO.CashRecDataSet.CashHeadRow hRow = (Epicor.Mfg.BO.CashRecDataSet.CashHeadRow)crDs.CashHead.Rows[0];
                    hRow.Company = "CA";
                    hRow.TranType = "PayInv";
                                   
                    string strInvoiceNo = payment.InvoiceNo;
                    Int32 invoiceNo = System.Convert.ToInt32(strInvoiceNo);
                    hRow.InvoiceNum = invoiceNo;
                    decimal payAmount = payment.PaymentAmt;
                    // bool takeDiscount = false;
                    
                    try
                    {
                        cashRec.GetNewCashDtl(crDs, groupID, headNum, invoiceNo);
                    }
                    catch (Exception e)
                    {
                        string idMsg = payment.TransNo + ":" + payment.StubSeqStr + ":" + payment.InvoiceNo + ": Amt  " + payment.PaymentAmt;
                        string message = "Msg 1: " + idMsg + "  :getNewCashDtl failed:" + e.Message;
                        this.errorMessages.Add(message);
                        goOn = false;
                    }
                    try
                    {
                        cashRec.GetDtlInvoiceInfo(invoiceNo, crDs);
                        headRow = (Epicor.Mfg.BO.CashRecDataSet.CashHeadRow)crDs.CashHead.Rows[0];
                        detailRow = (Epicor.Mfg.BO.CashRecDataSet.CashDtlRow)crDs.CashDtl.Rows[0];
                        detailRow.DocDispTranAmt = payAmount;
                        detailRow.DocTranAmt = payAmount;
                    }
                    catch (Exception e)
                    {
                        string idMsg = payment.TransNo + ":" + payment.StubSeqStr + ":" + payment.InvoiceNo + ": Amt  " + payment.PaymentAmt;
                        string message = "Msg 2: " + idMsg + "  :GetDtlInvoiceInfo failed:" + e.Message;
                        this.errorMessages.Add(message);
                        goOn = false;
                    }
                    try
                    {
                        cashRec.GetDtlTranAmtInfo(payAmount, crDs);
                    }
                    catch (Exception e)
                    {
                        string idMsg = payment.TransNo + ":" + payment.StubSeqStr + ":" + payment.InvoiceNo + ": Amt  " + payment.PaymentAmt;
                        string message = "Msg 3: " + idMsg + "  :GetDtlTranAmtInfo failed:" + e.Message;
                        this.errorMessages.Add(message);
                        goOn = false;
                    }
                    
                    /*
                    try
                    {
                        Epicor.Mfg.BO.CashRecDataSet.CashDtlRow dRow = (Epicor.Mfg.BO.CashRecDataSet.CashDtlRow)crDs.CashDtl.Rows[nPayments++];
                        dRow.DocTranAmt = payAmount;
                        dRow.TranAmt = payAmount;
                    }
                    catch (Exception e)
                    {
                        string idMsg = payment.TransNo + ":" + payment.StubSeqStr + ":" + payment.InvoiceNo + ": Amt  "+ payment.PaymentAmt;
                        string message = "Msg 2: " + idMsg + "  :CashDtlRow failed " + e.Message;
                        this.errorMessages.Add(message);
                        goOn = false;
                    }
                    */
                    try
                    {
                        cashRec.Update(crDs);
                    }
                    catch (Exception e)
                    {
                        string idMsg = payment.TransNo + ":" + payment.StubSeqStr + ":" + payment.InvoiceNo + ": Amt  " + payment.PaymentAmt;
                        string message = "Msg 4: " + idMsg + "  :cashRec.Update failed " + e.Message;
                        this.errorMessages.Add(message);
                        goOn = false;
                    }
                }
            }
            Cursor.Current = Cursors.Default;
        }
        public void NewCashHead()
        {
            cashRec.GetNewCashHead(crDs, groupID);
            headRow = (Epicor.Mfg.BO.CashRecDataSet.CashHeadRow)crDs.CashHead.Rows[0];
            headNum = headRow.HeadNum;
        }
        public ArrayList GetErrorMessages()
        {
            return errorMessages;
        }
        public void NewCashDetail(int invoiceNum)
        {
            cashRec.GetNewCashDtl(crDs, groupID, headNum, invoiceNum);
            Epicor.Mfg.BO.CashRecDataSet.CashDtlRow detailRow = (Epicor.Mfg.BO.CashRecDataSet.CashDtlRow)crDs.CashDtl.Rows[nextCashDetail++];
        }
    }
}
		

