package Extract::Coinet::PLGrupBrk;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">i:/transfer/";
    my $file = "PLGrupBrk.txt";
    return $dir . $file;
}

sub sql {
    my $self = shift;
   
    my $sql = qq /
      select
      pg.Company as Company, -- char 8 
      pg.DiscountPercent as DiscountPercent, -- decimal 7,2
      pg.ListCode as ListCode, -- x 10
      pg.ProdCode  as ProdCode,  -- char 8
      pg.Quantity as Quantity, -- int
      pg.UnitPrice as UnitPrice -- decimal 12,5
     FROM  pub.PLGrupBrk as pg
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
                $row{COMPANY}      . "\t" . 
                $row{DISCOUNTPERCENT}     . "\t" .
                $row{LISTCODE}     . "\t" .
                $row{PRODCODE}     . "\t" .
                $row{QUANTITY}     . "\t" .
                $row{UNITPRICE}    . "\t" .
                1                  . "\n";
    }
    close OUT;
}

1;

