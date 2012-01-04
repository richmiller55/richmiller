package Extract::Coinet::PartTran;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">d:/users/rich/export/alternate/";
    my $file = "PartTran15.txt";
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
pt.GL2Div as GL2Div,
pt.GL2Dept as GL2Dept,
pt.GL2Chart as GL2Chart,
pt.GL3Div as GL3Div,
pt.GL3Dept as GL3Dept,
pt.GL3Chart as GL3Chart,
pt.GL4Div as GL4Div,
pt.GL4Dept as GL4Dept,
pt.GL4Chart as GL4Chart,
pt.GL5Div as GL5Div,
pt.GL5Dept as GL5Dept,
pt.GL5Chart as GL5Chart,
pt.GLTrans as GLTrans,
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
pt.GL6Div as GL6Div,
pt.GL6Dept as GL6Dept,
pt.GL6Chart as GL6Chart,
pt.GL7Div as GL7Div,
pt.GL7Dept as GL7Dept,
pt.GL7Chart as GL7Chart,
pt.GL8Div as GL8Div,
pt.GL8Dept as GL8Dept,
pt.GL8Chart as GL8Chart,
pt.GL9Div as GL9Div,
pt.GL9Dept as GL9Dept,
pt.GL9Chart as GL9Chart,
pt.GL10Div as GL10Div,
pt.GL10Dept as GL10Dept,
pt.GL10Chart as GL10Chart,
pt.GL11Div as GL11Div,
pt.GL11Dept as GL11Dept,
pt.GL11Chart as GL11Chart,
pt.RMALine as RMALine,
pt.RMAReceipt as RMAReceipt,
pt.RMADisp as RMADisp,
pt.OtherDivValue as OtherDivValue,
pt.PlantTranNum as PlantTranNum,
pt.NonConfID as NonConfID,
pt.MtlMtlUnitCost as MtlMtlUnitCost,
pt.MtlLabUnitCost as MtlLabUnitCost,
pt.MtlSubUnitCost as MtlSubUnitCost,
pt.MtlBurdenUnitCost as MtlBurdenUnitCost,
pt.GL12Div as GL12Div,
pt.GL12Dept as GL12Dept,
pt.GL12Chart as GL12Chart,
pt.GL13Div as GL13Div,
pt.GL13Dept as GL13Dept,
pt.GL13Chart as GL13Chart,
pt.GL14Div as GL14Div,
pt.GL14Dept as GL14Dept,
pt.GL14Chart as GL14Chart,
pt.GL15Div as GL15Div,
pt.GL15Dept as GL15Dept,
pt.GL15Chart as GL15Chart,
pt.RefType as RefType,
pt.RefCode as RefCode,
pt.LegalNumber as LegalNumber,
pt.BeginQty as BeginQty,
pt.AfterQty as AfterQty,
pt.BegBurUnitCost as BegBurUnitCost,
pt.BegLbrUnitCost as BegLbrUnitCost,
pt.BegMtlBurUnitCost as BegMtlBurUnitCost,
pt.BegMtlUnitCost as BegMtlUnitCost,
pt.BegSubUnitCost as BegSubUnitCost,
pt.AfterBurUnitCost as AfterBurUnitCost,
pt.AfterLbrUnitCost as AfterLbrUnitCost,
pt.AfterMtlBurUnitCost as AfterMtlBurUnitCost,
pt.AfterMtlUnitCost as AfterMtlUnitCost,
pt.AfterSubUnitCost as AfterSubUnitCost,
pt.GL16Div as GL16Div,
pt.GL16Dept as GL16Dept,
pt.GL16Chart as GL16Chart,
pt.PlantCostValue as PlantCostValue,
pt.EmpID as EmpID,
pt.ReconcileNum as ReconcileNum
     FROM  pub.PartTran as pt
where 
-- pt.PartNum = '1057'
pt.TranNum > 809092

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
$row{GL2DIV} . "\t" .
$row{GL2DEPT} . "\t" .
$row{GL2CHART} . "\t" .
$row{GL3DIV} . "\t" .
$row{GL3DEPT} . "\t" .
$row{GL3CHART} . "\t" .
$row{GL4DIV} . "\t" .
$row{GL4DEPT} . "\t" .
$row{GL4CHART} . "\t" .
$row{GL5DIV} . "\t" .
$row{GL5DEPT} . "\t" .
$row{GL5CHART} . "\t" .
$row{GLTRANS} . "\t" .
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
$row{GL6DIV} . "\t" .
$row{GL6DEPT} . "\t" .
$row{GL6CHART} . "\t" .
$row{GL7DIV} . "\t" .
$row{GL7DEPT} . "\t" .
$row{GL7CHART} . "\t" .
$row{GL8DIV} . "\t" .
$row{GL8DEPT} . "\t" .
$row{GL8CHART} . "\t" .
$row{GL9DIV} . "\t" .
$row{GL9DEPT} . "\t" .
$row{GL9CHART} . "\t" .
$row{GL10DIV} . "\t" .
$row{GL10DEPT} . "\t" .
$row{GL10CHART} . "\t" .
$row{GL11DIV} . "\t" .
$row{GL11DEPT} . "\t" .
$row{GL11CHART} . "\t" .
$row{RMALINE} . "\t" .
$row{RMARECEIPT} . "\t" .
$row{RMADISP} . "\t" .
$row{OTHERDIVVALUE} . "\t" .
$row{PLANTTRANNUM} . "\t" .
$row{NONCONFID} . "\t" .
$row{MTLMTLUNITCOST} . "\t" .
$row{MTLLABUNITCOST} . "\t" .
$row{MTLSUBUNITCOST} . "\t" .
$row{MTLBURDENUNITCOST} . "\t" .
$row{GL12DIV} . "\t" .
$row{GL12DEPT} . "\t" .
$row{GL12CHART} . "\t" .
$row{GL13DIV} . "\t" .
$row{GL13DEPT} . "\t" .
$row{GL13CHART} . "\t" .
$row{GL14DIV} . "\t" .
$row{GL14DEPT} . "\t" .
$row{GL14CHART} . "\t" .
$row{GL15DIV} . "\t" .
$row{GL15DEPT} . "\t" .
$row{GL15CHART} . "\t" .
$row{REFTYPE} . "\t" .
$row{REFCODE} . "\t" .
$row{LEGALNUMBER} . "\t" .
$row{BEGINQTY} . "\t" .
$row{AFTERQTY} . "\t" .
$row{BEGBURUNITCOST} . "\t" .
$row{BEGLBRUNITCOST} . "\t" .
$row{BEGMTLBURUNITCOST} . "\t" .
$row{BEGMTLUNITCOST} . "\t" .
$row{BEGSUBUNITCOST} . "\t" .
$row{AFTERBURUNITCOST} . "\t" .
$row{AFTERLBRUNITCOST} . "\t" .
$row{AFTERMTLBURUNITCOST} . "\t" .
$row{AFTERMTLUNITCOST} . "\t" .
$row{AFTERSUBUNITCOST} . "\t" .
$row{GL16DIV} . "\t" .
$row{GL16DEPT} . "\t" .
$row{GL16CHART} . "\t" .
$row{PLANTCOSTVALUE} . "\t" .
$row{EMPID} . "\t" .
$row{RECONCILENUM} . "\t" .
  1                   . "\n";
    }
    close OUT;
}

1;

