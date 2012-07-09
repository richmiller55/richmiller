package Extract::Coinet::OrderRel;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">i:/transfer/";
    my $file = "OrderRel.txt";
    return $dir . $file;
}

sub sql {
    my $self = shift;
   
    my $sql = qq /
      select
      r.Company as Company, -- char 8 
      r.OrderNum  as OrderNum,  -- integer
      r.OrderLine as OrderLine, -- integer
      r.OrderRelNum as OrderRelNum, 
      r.OpenRelease as OpenRelease,
      r.WarehouseCode as WarehouseCode,
      r.OurJobShippedQty as OurJobShippedQty,
      r.SellingStockQty as SellingStockQty,
      r.SellingJobQty as SellingJobQty,
      r.SellingStockShippedQty as SellingStockShippedQty,
      0 as filler
     FROM  pub.OrderRel as r
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
                  $row{COMPANY}          . "\t" . 
                  $row{ORDERNUM}         . "\t" . 
                  $row{ORDERLINE}        . "\t" . 
		  $row{ORDERRELNUM}      . "\t" . 
                  $row{WAREHOUSECODE}    . "\t" .
                  $row{OPENRELEASE}      . "\t" .
                  $row{OURJOBSHIPPEDQTY} . "\t" .
                  $row{SELLINGSTOCKQTY}  . "\t" .
                  $row{SELLINGJOBQTY}    . "\t" .
                  $row{SELLINGSTOCKSHIPPEDQTY}  . "\t" .
                   0 .  "\n";

    }
    close OUT;
}

1;
