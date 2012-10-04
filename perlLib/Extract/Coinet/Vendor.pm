package Extract::Coinet::Vendor;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">i:/transfer/";
    my $file = "Vendor.txt";
    return $dir . $file;
}

sub sql {
    my $self = shift;
   
    my $sql = qq /
      select
      v.Company as Company, -- char 8 
      v.VendorID as VendorID,    -- char 8
      v.Name   as Name,          -- varchar(50)
      v.VendorNum as VendorNum,  -- int
      v.Address1 as Address1,   -- varchar(50)
      v.Address2 as Address2,  -- varchar(50)
      v.Address3 as Address3,  -- varchar(50)
      v.City     as City,     -- varchar(50)
      v.State    as State, -- varchar(50)  
      v.ZIP    as Zip,     -- varchar(10)  
      v.Country  as Country, -- varchar(50)  
      v.APAcctID as APAcctID -- varchar(5)
     FROM  pub.Vendor as v
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
                  $row{VENDORID}     . "\t" . 
                  $row{NAME}         . "\t" . 
                  $row{VENDORNUM}    . "\t" . 
                  $row{ADDRESS1}     . "\t" . 
                  $row{ADDRESS2}     . "\t" . 
                  $row{ADDRESS3}     . "\t" . 
                  $row{CITY}         . "\t" . 
                  $row{STATE}        . "\t" . 
                  $row{ZIP}          . "\t" . 
                  $row{COUNTRY}      . "\t",
                  $row{APACCTID}     . "\n";
    }
    close OUT;
}

1;
