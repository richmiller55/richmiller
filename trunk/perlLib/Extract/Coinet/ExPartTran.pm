package Extract::Coinet::ExPartTran;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">I:/transfer/";
    my $file = "ExPartTran.txt";
    return $dir . $file;
}

sub sql {
    my $self = shift;
   
    my $sql = qq /
      select
   pt.Company as Company,
--   pt.SysDate as SysDatex,
--   pt.SysTime as SysTimex,
pt.TranNum as TranNum,
pt.PartNum as PartNum,
pt.WareHouseCode as WareHouseCode,
pt.BinNum as BinNum,
pt.TranClass as TranClass,
pt.TranType as TranType,
pt.InventoryTrans as InventoryTrans,
pt.TranDate as TranDate,
pt.TranQty as TranQty,
pt.UM as UM,
pt.MtlUnitCost as MtlUnitCost,
pt.LbrUnitCost as LbrUnitCost,
pt.BurUnitCost as BurUnitCost,
pt.SubUnitCost as SubUnitCost,
pt.MtlBurUnitCost as MtlBurUnitCost,
pt.ExtCost as ExtCost,
pt.CostMethod as CostMethod,
pt.JobNum as JobNum,
pt.AssemblySeq as AssemblySeq,
pt.JobSeqType as JobSeqType,
pt.JobSeq as JobSeq,
pt.PackType as PackType,
pt.PackNum as PackNum,
pt.PackLine as PackLine,
pt.PONum as PONum,
pt.POLine as POLine,
pt.PORelNum as PORelNum,
pt.WareHouse2 as WareHouse2,
pt.BinNum2 as BinNum2,
pt.OrderNum as OrderNum,
pt.OrderLine as OrderLine,
pt.OrderRelNum as OrderRelNum,
pt.EntryPerson as EntryPerson,
pt.TranReference as TranReference,
pt.PartDescription as PartDescription,
pt.RevisionNum as RevisionNum,
pt.VendorNum as VendorNum,
pt.PurPoint as PurPoint,
pt.POReceiptQty as POReceiptQty,
pt.POUnitCost as POUnitCost,
pt.PackSlip as PackSlip,
pt.InvoiceNum as InvoiceNum,
pt.InvoiceLine as InvoiceLine,
pt.GLDiv as GLDiv,
pt.GLDept as GLDept,
pt.GLChart as GLChart,
pt.InvAdjSrc as InvAdjSrc,
pt.InvAdjReason as InvAdjReason,
pt.LotNum as LotNum,
pt.DimCode as DimCode,
pt.DUM as DUM,
pt.DimConvFactor as DimConvFactor,
pt.LotNum2 as LotNum2,
pt.DimCode2 as DimCode2,
pt.DUM2 as DUM2,
pt.DimConvFactor2 as DimConvFactor2,
pt.PostedToGL as PostedToGL,
pt.FiscalYear as FiscalYear,
pt.FiscalPeriod as FiscalPeriod,
pt.JournalNum as JournalNum,
pt.Costed as Costed,
pt.DMRNum as DMRNum,
pt.ActionNum as ActionNum,
pt.RMANum as RMANum,
pt.COSPostingReqd as COSPostingReqd,
pt.JournalCode as JournalCode,
pt.Plant as Plant,
pt.Plant2 as Plant2,
pt.CallNum as CallNum,
pt.CallLine as CallLine,
pt.MatNum as MatNum,
pt.JobNum2 as JobNum2,
pt.AssemblySeq2 as AssemblySeq2,
pt.JobSeq2 as JobSeq2,
pt.CustNum as CustNum,
pt.RMALine as RMALine,
pt.RMAReceipt as RMAReceipt,
pt.RMADisp as RMADisp,
pt.OtherDivValue as OtherDivValue,
pt.PlantTranNum as PlantTranNum,
pt.NonConfID as NonConfID,
pt.RefType as RefType,
pt.RefCode as RefCode,
pt.LegalNumber as LegalNumber,
pt.BeginQty as BeginQty,
pt.AfterQty as AfterQty,
pt.PlantCostValue as PlantCostValue,
pt.EmpID as EmpID,
pt.ReconcileNum as ReconcileNum
     FROM  pub.PartTran as pt
where 
pt.TranNum >1508326

   /;
    return $sql;
}

sub printData {
    my $self = shift;
    my $fh = $self->getFileNameOut();

    open OUT, $fh or die "Cannot create $fh: $!";
    my $i = 0;
    my $db = $self->{db};
    while ($db->FetchRow() ) {
	$i++;
	my %row = $db->DataHash();
#	my $sysDate = $row{SYSDATEX};
#	$sysDate =~ s/-//g;

	my $tranDate = $row{TRANDATE};
	$tranDate =~ s/-//g;
        
	print OUT  $i . "\t" .
$row{COMPANY} . "\t" .
# $sysDate . "\t" . 
# $row{SYSTIMEX} . "\t" .
$row{TRANNUM} . "\t" .
$row{PARTNUM} . "\t" .
$row{WAREHOUSECODE} . "\t" .
$row{BINNUM} . "\t" .
$row{TRANCLASS} . "\t" .
$row{TRANTYPE} . "\t" .
$row{INVENTORYTRANS} . "\t" .
$tranDate . "\t" .
$row{TRANQTY} . "\t" .
$row{UM} . "\t" .
$row{MTLUNITCOST} . "\t" .
$row{LBRUNITCOST} . "\t" .
$row{BURUNITCOST} . "\t" .
$row{SUBUNITCOST} . "\t" .
$row{MTLBURUNITCOST} . "\t" .
$row{EXTCOST} . "\t" .
$row{COSTMETHOD} . "\t" .
$row{JOBNUM} . "\t" .
$row{ASSEMBLYSEQ} . "\t" .
$row{JOBSEQTYPE} . "\t" .
$row{JOBSEQ} . "\t" .
$row{PACKTYPE} . "\t" .
$row{PACKNUM} . "\t" .
$row{PACKLINE} . "\t" .
$row{PONUM} . "\t" .
$row{POLINE} . "\t" .
$row{PORELNUM} . "\t" .
$row{WAREHOUSE2} . "\t" .
$row{BINNUM2} . "\t" .
$row{ORDERNUM} . "\t" .
$row{ORDERLINE} . "\t" .
$row{ORDERRELNUM} . "\t" .
$row{ENTRYPERSON} . "\t" .
$row{TRANREFERENCE} . "\t" .
$row{PARTDESCRIPTION} . "\t" .
$row{REVISIONNUM} . "\t" .
$row{VENDORNUM} . "\t" .
$row{PURPOINT} . "\t" .
$row{PORECEIPTQTY} . "\t" .
$row{POUNITCOST} . "\t" .
$row{PACKSLIP} . "\t" .
$row{INVOICENUM} . "\t" .
$row{INVOICELINE} . "\t" .
$row{GLDIV} . "\t" .
$row{GLDEPT} . "\t" .
$row{GLCHART} . "\t" .
$row{INVADJSRC} . "\t" .
$row{INVADJREASON} . "\t" .
$row{LOTNUM} . "\t" .
$row{DIMCODE} . "\t" .
$row{DUM} . "\t" .
$row{DIMCONVFACTOR} . "\t" .
$row{LOTNUM2} . "\t" .
$row{DIMCODE2} . "\t" .
$row{DUM2} . "\t" .
$row{DIMCONVFACTOR2} . "\t" .
$row{POSTEDTOGL} . "\t" .
$row{FISCALYEAR} . "\t" .
$row{FISCALPERIOD} . "\t" .
$row{JOURNALNUM} . "\t" .
$row{COSTED} . "\t" .
$row{DMRNUM} . "\t" .
$row{ACTIONNUM} . "\t" .
$row{RMANUM} . "\t" .
$row{COSPOSTINGREQD} . "\t" .
$row{JOURNALCODE} . "\t" .
$row{PLANT} . "\t" .
$row{PLANT2} . "\t" .
$row{CALLNUM} . "\t" .
$row{CALLLINE} . "\t" .
$row{MATNUM} . "\t" .
$row{JOBNUM2} . "\t" .
$row{ASSEMBLYSEQ2} . "\t" .
$row{JOBSEQ2} . "\t" .
$row{CUSTNUM} . "\t" .
$row{RMALINE} . "\t" .
$row{RMARECEIPT} . "\t" .
$row{RMADISP} . "\t" .
$row{OTHERDIVVALUE} . "\t" .
$row{PLANTTRANNUM} . "\t" .
$row{NONCONFID} . "\t" .
$row{REFTYPE} . "\t" .
$row{REFCODE} . "\t" .
$row{LEGALNUMBER} . "\t" .
$row{BEGINQTY} . "\t" .
$row{AFTERQTY} . "\t" .
$row{PLANTCOSTVALUE} . "\t" .
$row{EMPID} . "\t" .
$row{RECONCILENUM} . "\t" .
  1                   . "\n";
    }
    close OUT;
}

1;

