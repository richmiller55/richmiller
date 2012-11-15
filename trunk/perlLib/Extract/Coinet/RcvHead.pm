package Extract::Coinet::RcvHead;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">i:/transfer/";
    my $file = "RvcHead.txt";
    return $dir . $file;
}

sub sql {
    my $self = shift;
    my $sql = qq /  
      select
      sd.ContainerID as ContainerID,
      sd.EntryDate as EntryDate,
      sd.EntryPerson as EntryPerson,
      sd.LandedCost as LandedCost,
      sd.PackSlip as PackSlip,
      sd.PONum as PONum,
      sd.PurPoint as PurPoint,
      sd.ReceiptComment	as ReceiptComment,
      sd.ReceiptDate as ReceiptDate,
      sd.ReceivePerson as ReceivePerson,
      sd.VendorNum as VendorNum,
      sd.Weight as Weight,
      1 as filler
      from pub.RcvHead as sd
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
	my $EntryDate = $row{ENTRYDATE};
	$EntryDate =~ s/-//g;

	my $ReceiptDate = $row{RECEIPTDATE};
	$ReceiptDate =~ s/-//g;
	print OUT  $i . "\t" .
	    $row{CONTAINERID} . "\t" . 
            $EntryDate  . "\t" . 
	    $row{ENTRYPERSON}     . "\t" . 
	    $row{LANDEDCOST}  . "\t" . 
	    $row{PACKSLIP}  . "\t" . 
	    $row{PONUM}     . "\t" . 
	    $row{PURPOINT}     . "\t" . 
	    $row{RECEIPTCOMMENT} . "\t" . 
            $ReceiptDate         . "\t" . 
	    $row{RECEIVEPERSON}     . "\t" . 
	    $row{VENDORNUM}     . "\t" . 
	    $row{WEIGHT}     . "\t" . 
            1                 . "\n";
    }
    close OUT;
}

1;
