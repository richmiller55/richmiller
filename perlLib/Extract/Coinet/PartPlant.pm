package Extract::Coinet::PartBin;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">i:/transfer/";
    my $file = "PartBin.txt";
    return $dir . $file;
}

sub sql {
    my $self = shift;
   
    my $sql = qq /
      select
      pb.Company as Company, -- char 8 
      pb.partNum  as partNum,  -- char 50
      pb.WarehouseCode as WarehouseCode, -- char 8
      pb.BinNum as BinNum, -- char 10
      pb.OnhandQty as OnhandQty, -- decimal 12,2
      pb.LotNum as LotNum  -- char 30
     FROM  pub.PartBin as pb
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
                  $row{PARTNUM}     . "\t" . 
                  $row{WAREHOUSECODE}     . "\t" . 
                  $row{BINNUM}     . "\t" . 
                  $row{ONHANDQTY}     . "\t" . 
                  $row{LOTNUM}     . "\n";
    }
    close OUT;
}

1;

