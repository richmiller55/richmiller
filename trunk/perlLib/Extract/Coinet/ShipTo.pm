package Extract::Coinet::ShipTo;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">i:/transfer/";
    my $file = "ShipTo.txt";
    return $dir . $file;
}

sub sql {
    my $self = shift;
   
    my $sql = qq /
      select
      st.Company              as Company, -- char 8 
      st.CustNum              as CustNum,  -- int
      st.ShipToNum            as ShipToNum, -- x14
      st.Name                 as Name, -- x50
      st.Address1             as Address1, -- x50
      st.Address2             as Address2, -- x50
      st.Address3             as Address3, -- x50
      st.City                 as City, -- x50
      st.State                as State, -- x50
      st.ZIP                  as ZIP, -- x10
      st.PhoneNum             as PhoneNum, -- x20
      st.Country              as Country, -- x50
      st.AddressVal           as AddressVal, -- smallint
      st.TerritoryID          as TerritoryID, -- x8
      st.CountryNum           as CountryNum, -- int
      st.CreatedByEDI         as CreatedByEDI, -- smallint
      st.EDICode              as EDICode, -- x15  GLN
      st.EDIShipNum           as EDIShipNum, -- x15 or this GLN
      st.ShipViaCode          as ShipViaCode, -- x4
      st.TaxAuthorityCode     as TaxAuthorityCode, -- x8
      st.TaxRegionCode        as TaxRegionCode, -- x4
      st.TerritorySelect      as TerritorySelect, -- x4
      st.TradingPartnerName   as TradingPartnerName, -- x20
      st.ChangedBy            as ChangedBy, -- x20
      st.ChangeDate           as ChangeDate,  -- int after conversion
      st.ShortChar01          as ShortChar01, --  x50 
      st.ShortChar02          as ShortChar02, --  x50 
      st.SalesRepCode         as SalesRepCode,
      st.RefNotes             as RefNotes,
      st.ResDelivery          as ResDelivery,
      st.NotifyFlag           as NotifyFlag,
      st.NotifyEmail          as NotifyEmail,
      st.COD                  as COD,
      st.SatDelivery          as SatDelivery
     FROM  pub.ShipTo as st
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
	my $ChangeDate = $row{CHANGEDATE};
	$ChangeDate =~ s/-//g;
        
	print OUT  $i . "\t" .
                $row{COMPANY}            . "\t" . 
                $row{CUSTNUM}            . "\t" .
                $row{SHIPTONUM}          . "\t" . 
                $row{NAME}               . "\t" .
                $row{ADDRESS1}           . "\t" . 
                $row{ADDRESS2}           . "\t" . 
                $row{ADDRESS3}           . "\t" .
                $row{CITY}               . "\t" .
                $row{STATE}              . "\t" . 
                $row{ZIP}                . "\t" . 
                $row{PHONENUM}           . "\t" . 
                $row{COUNTRY}            . "\t" . 
                $row{ADDRESSVAL}         . "\t" . 
                $row{TERRITORYID}        . "\t" . 
                $row{COUNTRYNUM}         . "\t" . 
                $row{CREATEDBYEDI}       . "\t" . 
                $row{EDICODE}            . "\t" .
                $row{EDISHIPNUM}         . "\t" . 
                $row{SHIPVIACODE}        . "\t" . 
                $row{TAXAUTHORITYCODE}   . "\t" .
                $row{TAXREGIONCODE}      . "\t" . 
                $row{TERRITORYSELECT}    . "\t" .
                $row{TRADINGPARTNERNAME} . "\t" . 
                $row{CHANGEDBY}          . "\t" . 
                $ChangeDate              . "\t" . 
                $row{SHORTCHAR01}        . "\t" . 
                $row{SHORTCHAR02}        . "\t" . 
                $row{SALESREPCODE}       . "\t" . 
                $row{REFNOTES}           . "\t" . 
                $row{RESDELIVERY}        . "\t" . 
                $row{NOTIFYFLAG}         . "\t" . 
                $row{NOTIFYEMAIL}        . "\t" . 
                $row{COD}                . "\t" . 
                $row{SATDELIVERY}        . "\t" . 
                1                        . "\n";
    }
    close OUT;
}

1;

