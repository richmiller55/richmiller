package Extract::Coinet::ShipHead;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">i:/transfer/";
    my $file = "ShipHead.txt";
    return $dir . $file;
}

sub sql {
    my $self = shift;
   
    my $sql = qq /
      select
      sh.Company as Company,               -- char 8 
      sh.CustNum as CustNum,               -- int
      sh.PackNum as PackNum,               -- int
      sh.ReadyToInvoice as ReadyToInvoice,  -- smallint
      sh.Invoiced as Invoiced,             -- smallint
      sh.FreightedShipViaCode as FreightedShipViaCode, -- char 4
      sh.EntryPerson as EntryPerson,       -- char 20
      sh.ShipDate as ShipDate,             -- int
      sh.Voided as Voided,                  -- smallint
      sh.TrackingNumber as TrackingNumber,
      sh.PayBTAddress1 as PayBTAddress1, 
      sh.PayBTAddress2 as PayBTAddress2, 
      sh.PayBTCity     as PayBTCity,	 
      sh.PayBTState    as PayBTState,	 
      sh.PayBTZip      as PayBTZip,	 
      sh.PayBTCountry  as PayBTCountry,	 
      sh.PayBTPhone    as PayBTPhone,
      sh.ShipToNum     as ShipToNum,
      sh.ShipViaCode   as ShipViaCode,
      1 as filler
     FROM  pub.ShipHead as sh
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

	my $shipDate = $row{SHIPDATE};
	$shipDate =~ s/-//g;

	print OUT  $i                         . "\t" .
                  $row{COMPANY}               . "\t" . 
                  $row{CUSTNUM}               . "\t" . 
                  $row{PACKNUM}               . "\t" . 
                  $row{READYTOINVOICE}        . "\t" . 
                  $row{INVOICED}              . "\t" . 
                  $row{FREIGHTEDSHIPVIACODE}  . "\t" . 
                  $row{ENTRYPERSON}           . "\t" . 
                  $shipDate                   . "\t" . 
                  $row{VOIDED}                . "\t" .
                  $row{TRACKINGNUMBER}        . "\t" .
                  $row{PAYBTADDRESS1}         . "\t" .
                  $row{PAYBTADDRESS2}         . "\t" .
                  $row{PAYBTCITY}             . "\t" .
                  $row{PAYBTSTATE}            . "\t" .
                  $row{PAYBTZIP}              . "\t" .
                  $row{PAYBTCOUNTRY}          . "\t" .
                  $row{PAYBTPHONE}            . "\t" .
                  $row{SHIPTONUM}             . "\t" .
                  $row{SHIPVIACODE}           . "\t" .
		  0 .   "\n";
    }
    close OUT;
}
1;

