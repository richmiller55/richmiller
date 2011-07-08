using System;
using System.Collections;
using System.Windows.Forms;
using System.Data;

namespace LockBox
{
    public class CashGrp
    {
        Epicor.Mfg.Core.Session objSess;
        Epicor.Mfg.BO.CashGrp cashGrp;
        Epicor.Mfg.BO.CashGrpDataSet cgDs;
//        Epicor.Mfg.BO.CashRec cashRec;
//        Epicor.Mfg.BO.CashRecDataSet crDs;

        DataTable dataTable;
        Hashtable payments;
        
        BankFile bankFile;
        string groupID;
        ArrayList errorMessages;
        int nPayments;
        DataTable bft;
        public CashGrp(Epicor.Mfg.Core.Session session, DataTable dt,BankFile bankFileIn)
        {
            dataTable = dt;
            this.bankFile = bankFileIn;
            objSess = session;
            payments = new Hashtable();
            nPayments = 0;
            groupID = bankFile.getGroupID();
            CheckGrpName();
            MatchChecks();
        }
        public void MatchChecks()
        {
            Hashtable batches = bankFile.getBankBatches();
            CashRec cashRec = new CashRec(objSess, groupID);
            if (batches.Count == 0)
            {
                // some stupid thing
            }
            else
            {
                ICollection iBatches = iBatches = batches.Keys;
                foreach (string batchID in iBatches)
                {
                    BankBatch batch = (BankBatch)batches[batchID];
                    Hashtable checks = batch.getChecks();
                    ICollection iChecks = iChecks = checks.Keys;
                    foreach (string checkNo in iChecks)
                    {
                        Check check = (Check)checks[checkNo];
                        decimal paidTotal = 0.0M;

                        string custID = "";
                        ArrayList paidInvoices = new ArrayList();
                        Hashtable payments = check.getPayments();
                        ICollection iPayments = payments.Keys;
                        foreach (Int32 nPayment in iPayments)
                        {
                            Payment payment = (Payment)payments[nPayment];
                            string strInvoiceNo = payment.InvoiceNo;
                            Int32 invoiceNo = System.Convert.ToInt32(strInvoiceNo);
                            if (invoiceNo > 0)
                            {
                                InvoiceLookup lookup = new InvoiceLookup(invoiceNo);
                                InvoiceInfo info = lookup.getInfo();
                                if (info.InvoiceFound)
                                {
                                    decimal payAmt = payment.PaymentAmt;
                                    info.DetermineMatchLevel(payAmt);
                                    string matchResult = info.CheckMatch(payAmt);
                                    if (info.InvoiceAmtMatch || info.CustIdMatch)
                                    {
                                        paidTotal += payAmt;
                                        paidInvoices.Add(payment);
                                        custID = info.CustID;
                                    }
                                    else
                                   {
                                        paidTotal += payAmt;
                                        // payment on account
                                        // paidInvoices.Add(payment);
                                        // custID = info.CustID;
                                    }
                                }
                            }
                        }
                        if (paidInvoices.Count == 0)
                        {
                            // no matches
                        }
                        else
                        {
                            cashRec.AddMatchedCheck(custID,check, paidTotal, paidInvoices);
                        }
                    }
                }
                this.errorMessages = cashRec.GetErrorMessages();
            }
        }
        public ArrayList GetErrorMessages()
        {
            return errorMessages;
        }
        public void CashRecMatch()
        {
            foreach (DataRow row in dataTable.Rows)
            {
                CashRec rec = new CashRec(objSess, groupID);
                if (row["MatchStatus"].Equals("Matched"))
                {
                    Int32 invoiceNo = Convert.ToInt32(row["InvoiceNo"]);
                    rec.NewCashDetail(invoiceNo);
                }
            }
        }
        public bool CheckGrpName()
        {
            cashGrp = new Epicor.Mfg.BO.CashGrp(objSess.ConnectionPool);

            cgDs = new Epicor.Mfg.BO.CashGrpDataSet();
            bool found = true;
            try
            {
                cashGrp.GetNewCashGrp(cgDs);
            }
            catch (Exception e)
            {
                Console.WriteLine("group Not found:");
                Console.WriteLine(e.Message);
                found = false;
            }
            Epicor.Mfg.BO.CashGrpDataSet.CashGrpRow row = (Epicor.Mfg.BO.CashGrpDataSet.CashGrpRow)cgDs.CashGrp.Rows[0];
            row.GroupID = groupID;
            
            try
            {
                cashGrp.GetGrpBankInfo("CNB1",cgDs);
            }
            catch (Exception e)
            {
                Console.WriteLine("Bank Data not pulled:");
                Console.WriteLine(e.Message);
                found = false;
            }
            try
            {
                cashGrp.Update(cgDs);
            }
            catch (Exception e)
            {
                Console.WriteLine("Update failed:");
                Console.WriteLine(e.Message);
                found = false;
            }
            return found;
        }
    }
}

