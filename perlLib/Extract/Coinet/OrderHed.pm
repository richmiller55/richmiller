package Extract::Coinet::OrderHed;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">i:/transfer/";
    my $file = "OrderHed.txt";
    return $dir . $file;
}

sub sql {
    my $self = shift;
   
    my $sql = qq /
      select
      oh.Company as Company,               -- char 8 
      oh.OrderNum as OrderNum,             -- int
      oh.CustNum as CustNum,               -- int
      oh.OpenOrder as OpenOrder,           -- int
      oh.OrderDate as OrderDate,           -- int after conversion
      oh.PONum as PONum,                   -- char 50
      oh.NeedByDate as NeedByDate,         -- int
      oh.RequestDate as RequestDate,       -- int
      oh.ShipToNum as ShipToNum,           -- int
      oh.ShipViaCode as ShipViaCode,       -- char 4
      oh.ShortChar01 as ShortChar01,       -- char 50 
      oh.ShortChar02 as ShortChar02,       -- char 50
      oh.ShortChar03 as ShortChar03,       -- char 50
      oh.TermsCode as TermsCode,           -- char 4
      oh.TotalCharges as TotalCharges,     -- decimal 12,2
      oh.TotalComm as TotalComm,           --  decimal 12,2
      oh.TotalInvoiced as TotalInvoiced,   --  decimal 12,2
      oh.TotalLines as TotalLines,         -- int
      oh.TotalMisc as TotalMisc,           -- decimal 12,2
      oh.TotalReleases as TotalReleases,   -- int
      oh.TotalTax as TotalTax,             -- decimal 12,2
      oh.VoidOrder as VoidOrder,            -- int      
      oh.CheckBox04 as inPrintSetup,        -- int
      oh.CheckBox07 as offShore        -- int
     FROM  pub.OrderHed as oh
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

	my $OrderDate = $row{ORDERDATE};
	$OrderDate =~ s/-//g;

	my $NeedByDate = $row{NEEDBYDATE};
	$NeedByDate =~ s/-//g;

	my $RequestDate = $row{REQUESTDATE};
	$RequestDate =~ s/-//g;
        
	print OUT  $i . "\t" .
                  $row{COMPANY}            . "\t" . 
                  $row{ORDERNUM}           . "\t" . 
                  $row{CUSTNUM}            . "\t" . 
                  $row{OPENORDER}          . "\t" . 
                  $OrderDate               . "\t" . 
                  $row{PONUM}              . "\t" . 
                  $NeedByDate              . "\t" . 
                  $RequestDate             . "\t" . 
                  $row{SHIPTONUM}          . "\t" . 
                  $row{SHIPVIACODE}        . "\t" . 
                  $row{SHORTCHAR01}        . "\t" . 
                  $row{SHORTCHAR02}        . "\t" . 
                  $row{SHORTCHAR03}        . "\t" . 
                  $row{TERMSCODE}          . "\t" . 
                  $row{TOTALCHARGES}       . "\t" . 
                  $row{TOTALCOMM}          . "\t" . 
                  $row{TOTALINVOICED}      . "\t" . 
                  $row{TOTALLINES}         . "\t" . 
                  $row{TOTALMISC}          . "\t" . 
                  $row{TOTALRELEASES}      . "\t" . 
                  $row{TOTALTAX}           . "\t" . 
                  $row{VOIDORDER}          . "\t" .
                  $row{INPRINTSETUP}       . "\t" .
                  $row{OFFSHORE}           . "\t" .
		  0                        . "\n";
    }
    close OUT;
}
1;

