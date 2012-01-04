package Extract::Coinet::ProdGrup;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">i:/transfer/";
    my $file = "ProdGrup.txt";
    return $dir . $file;
}

sub sql {
    my $self = shift;
   
    my $sql = qq /
      select
      pg.Company as Company, -- char 8 
      pg.ProdCode  as ProdCode,  -- char 8
      pg.Description as Description, -- char 30
      pg.Number01   as dutyRate, -- decimal 12,4
      pg.Number02   as burden, -- decimal 12,4
      pg.ShortChar01 as compRetailOEM
      
     FROM  pub.ProdGrup as pg
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
                $row{PRODCODE}     . "\t" .
                $row{DESCRIPTION}  . "\t" . 
                $row{DUTYRATE}     . "\t" . 
                $row{BURDEN}       . "\t" .
                $row{COMPRETAILOEM}. "\t" .                 
                1                  . "\n";
    }
    close OUT;
}

1;

