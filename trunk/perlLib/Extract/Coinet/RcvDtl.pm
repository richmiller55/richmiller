package Extract::Coinet::RcvDtl;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;


sub getFileNameOut {
    my $self = shift;
    my $dir = ">i:/transfer/";
    my $file = "RvcDtl.txt";
    return $dir . $file;
}

sub sql {
    my $self = shift;
    my $sql = qq /  
      select
      rh.OurQty as OurQty,
      rh.OurUnitCost as OurUnitCost,
      rh.PackLine as PackLine,
      rh.PackSlip as PackSlip,
      rh.PartDescription as PartDescription,
      rh.PartNum as PartNum,
      rh.PassedQty as PassedQty,
      rh.POLine as POLine,
      rh.PONum as PONum,
      rh.PORelNum as PORelNum,
      rh.PUM as PUM,
      rh.PurchCode as PurchCode,
      rh.PurPoint as PurPoint,
      rh.ReasonCode as ReasonCode,
      rh.ReceiptDate as ReceiptDate,
      rh.ReceiptType as ReceiptType,
      rh.ReceivedComplete as ReceivedComplete,
      rh.ReceivedTo as ReceivedTo,
      rh.RefType as RefType,
      rh.RevisionNum as RevisionNum,
      rh.TranReference as TranReference,
      rh.VendorNum as VendorNum,
      rh.VendorQty as VendorQty,
      rh.VendorUnitCost as VendorUnitCost,
      rh.VenPartNum as VenPartNum,
      rh.Volume as Volume,
      rh.WareHouseCode as WareHouseCode,
      rh.Weight as Weight,
      1 as filler
      from pub.RcvDtl as rh
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

	my $ReceiptDate = $row{RECEIPTDATE};
	$ReceiptDate =~ s/-//g;
	print OUT  $i . "\t" .
	    $row{OURQTY} . "\t" . 
	    $row{OURUNITCOST} . "\t" . 
	    $row{PACKLINE} . "\t" . 
	    $row{PACKSLIP} . "\t" . 
	    $row{PARTDESCRIPTION} . "\t" . 
	    $row{PARTNUM} . "\t" . 
	    $row{PASSEDQTY} . "\t" . 
	    $row{POLINE} . "\t" . 
	    $row{PONUM} . "\t" . 
	    $row{PORELNUM} . "\t" . 
	    $row{PUM} . "\t" . 
	    $row{PURCHCODE} . "\t" . 
	    $row{PURPOINT} . "\t" . 
	    $row{REASONCODE} . "\t" . 
	    $ReceiptDate . "\t" . 
	    $row{RECEIPTTYPE} . "\t" . 
	    $row{RECEIVEDCOMPLETE} . "\t" . 
	    $row{RECEIVEDTO} . "\t" . 
	    $row{REFTYPE} . "\t" . 
	    $row{REVISIONNUM} . "\t" . 
	    $row{TRANREFERENCE} . "\t" . 
	    $row{VENDORNUM} . "\t" . 
	    $row{VENDORQTY} . "\t" . 
	    $row{VENDORUNITCOST} . "\t" . 
	    $row{VENPARTNUM} . "\t" . 
	    $row{VOLUME} . "\t" . 
	    $row{WAREHOUSECODE} . "\t" . 
	    $row{WEIGHT} . "\t" . 
            1                 . "\n";
    }
    close OUT;
}

1;
