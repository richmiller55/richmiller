package Extract::Coinet::Region;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">i:/transfer/";
    my $file = "Region.txt";
    return $dir . $file;
}

sub sql {
    my $self = shift;
   
    my $sql = qq /
    select
      r.Company as Company,          -- x 8 
      r.CountryNum as CountryNum,    -- int
      r.Description as Description,  -- x 30
      r.Inactive as Inactive,        -- small int
      r.RegionCode as RegionCode,         -- x 12
      r.RegionComment as RegionComment,   -- x1000
      r.SalesManagerID as SalesManagerID   -- x 8
     FROM  pub.Region as r
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

	print OUT  $i                          . "\t" .
                  $row{COMPANY}                . "\t" . 
                  $row{COUNTRYNUM}           . "\t" . 
                  $row{DESCRIPTION}           . "\t" . 
                  $row{INACTIVE}           . "\t" . 
                  $row{REGIONCODE}           . "\t" . 
                  $row{REGIONCOMMENT}           . "\t" . 
                  $row{SALESMANAGERID}           . "\t" . 
                  1                            . "\n";
    }
    close OUT;
}

1;
