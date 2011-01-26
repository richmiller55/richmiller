using System;
using System.Collections.Generic;
using System.Text;

namespace InvPrt
{
    class BuyGroup
    {
        bool buyGroupMember = false;
        int custNum;
        string custID;
        public BuyGroup(int custNum)
        {
            this.custNum = custNum;
            this.DetermineIfBuyGroup();
        }
        public BuyGroup(string custID)
        {
            this.custID = custID;
            this.DetermineIfBuyGroup();
        }
        private void DetermineIfBuyGroup()
        {

        }
        public bool GetBuyGroupMember()
        {
            return this.buyGroupMember;
        }
    }
}