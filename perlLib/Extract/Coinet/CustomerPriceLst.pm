package Extract::Coinet::CustomerPriceLst;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">i:/transfer/";
    my $file = "CustomerPriceLst.txt";
    return $dir . $file;
}

sub sql {
    my $self = shift;
   
    my $sql = qq /
      select
      cpl.Company as Company, -- char 8 
      cpl.CustNum  as CustNum,  -- int
      cpl.ListCode  as ListCode,  -- Varchar(10),
      cpl.SeqNum as SeqNum , -- int
      cpl.ShipToNum as ShipToNum -- varchar(14)
     FROM  pub.CustomerPriceLst as cpl
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
                $row{COMPANY}         . "\t" . 
                $row{CUSTNUM}         . "\t" .
                $row{LISTCODE}       . "\t" .
                $row{SEQNUM}       . "\t" .
                $row{SHIPTONUM}       . "\t" .
                1                     . "\n";
    }
    close OUT;
}

1;

