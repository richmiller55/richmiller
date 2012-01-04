package Extract::Coinet::SalesTer;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">i:/transfer/";
    my $file = "SalesTer.txt";
    return $dir . $file;
}

sub sql {
    my $self = shift;
   
    my $sql = qq /
      select
      st.Company as Company,               -- char 8 
      st.ConsToPrim as ConsToPrim,         -- smallint
      st.DefTaskSetID as DefTaskSetID,     -- Char 8
      st.Inactive as Inactive,             -- smallint
      st.PrimeSalesRepCode as PrimeSalesRepCode,  -- char 8
      st.RegionCode as RegionCode,                -- x 12
      st.TerritoryDesc as TerritoryDesc,          -- x 30
      st.TerritoryID as TerritoryID,              -- x 8
      st.Number01 as terrCommRate
     FROM  pub.SalesTer as st
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

	print OUT  $i                           . "\t" .
                  $row{COMPANY}                 . "\t" . 
                  $row{CONSTOPRIM}              . "\t" . 
                  $row{DEFTASKSETID}            . "\t" . 
                  $row{INACTIVE}                . "\t" . 
                  $row{PRIMESALESREPCODE}       . "\t" . 
                  $row{REGIONCODE}              . "\t" . 
                  $row{TERRITORYDESC}           . "\t" . 
                  $row{TERRITORYID}             . "\t" ,
		  $row{TERRCOMMRATE}            . "\t" ,
                  1                             . "\n";
    }
    close OUT;
}

1;

