package Extract::Coinet::ContainerDetail;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">i:/transfer/";
    my $file = "ContainerDetail.txt";
    return $dir . $file;
}

sub sql {
    my $self = shift;
   
    my $sql = qq /
      select
      p.Company as Company, -- char 8 
      p.ContainerID as ContainerID,  --- int 
      p.ContainerShipQty as ContainerShipQty, -- decimal
      p.PONum as PONum, -- int
      p.POLine as POLine, -- int
      p.PORelNum as PORelNum, -- int
      p.PackSlip as PackSlip, -- x20
      p.Volume as Volume, -- decimal
      p.VendorNum as VendorNum, -- int
      p.OurUnitCost as OurUnitCost,  -- decimal
      p.LCAmt as LCAmt

     FROM  pub.ContainerDetail as p
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

	print OUT  $i . "\t" .
                  $row{COMPANY}        . "\t" . 
                  $row{CONTAINERID}    . "\t" . 
                  $row{CONTAINERSHIPQTY} . "\t" .
                  $row{PONUM}          . "\t" . 
                  $row{POLINE}         . "\t" . 
                  $row{PORELNUM}       . "\t" . 
                  $row{PACKSLIP}       . "\t" . 
                  $row{VOLUME}         . "\t" .
                  $row{VENDORNUM}      . "\t" . 
                  $row{OURUNITCOST}    . "\t" .
                  $row{LCAMT}    . "\n";

    }
    close OUT;
}

1;
