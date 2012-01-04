package Extract::Coinet::CustBillTo;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">i:/transfer/";
    my $file = "CustBillTo.txt";
    return $dir . $file;
}

sub sql {
    my $self = shift;
   
    my $sql = qq /
      select
      cb.Company as Company, -- char 8 
      cb.CustNum  as CustNum,  -- int
      cb.BTCustNum  as BTCustNum,  -- int
      cb.DefaultBillTo as DefaultBillTo , -- smallint
      cb.InvoiceAddress as InvoiceAddress, -- smallint
      cb.ChangedBy as ChangedBy, -- char 20
      cb.ShortChar01 as BuyGroupCustID
     FROM  pub.CustBillTo as cb
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
                $row{COMPANY}         . "\t" . 
                $row{CUSTNUM}         . "\t" .
                $row{BTCUSTNUM}       . "\t" .
                $row{DEFAULTBILLTO}   . "\t" . 
                $row{INVOICEADDRESS}  . "\t" . 
                $row{CHANGEDBY}       . "\t" . 
                $row{BUYGROUPCUSTID}  . "\t" . 
                1                     . "\n";
    }
    close OUT;
}

1;

