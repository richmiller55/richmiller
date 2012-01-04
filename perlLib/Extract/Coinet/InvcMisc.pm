package Extract::Coinet::InvcMisc;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">i:/transfer/";
    my $file = "InvcMisc.txt";
    return $dir . $file;
}

sub sql {
    my $self = shift;
    my $sql = qq /  
        select
            im.Company as Company,  -- char 8
            im.InvoiceNum as InvoiceNum, -- int
            im.InvoiceLine as InvoiceLine, -- int
            im.MiscCode as MiscCode,  -- char 4
            im.Description as Description,  -- char 30
            im.CheckBox01 as CheckBox01,   -- smallint
            im.DocMiscAmt as DocMiscAmt,   -- 12,2 decimal
            im.MiscAmt as MiscAmt,          -- 12,2 decimal
            im.Number01 as Number01,         -- int
            im.ShortChar01 as ShortChar01   -- char 50
      from pub.InvcMisc as im
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
                  $row{COMPANY}    . "\t" . 
                  $row{INVOICENUM}    . "\t" . 
                  $row{INVOICELINE}    . "\t" . 
                  $row{MISCCODE}    . "\t" . 
                  $row{DESCRIPTION}    . "\t" . 
                  $row{CHECKBOX01}    . "\t" . 
                  $row{DOCMISCAMT}    . "\t" . 
                  $row{MISCAMT}    . "\t" . 
                  $row{NUMBER01}    . "\t" . 
                  $row{SHORTCHAR01}    . "\n";
    }
    close OUT;
}

1;
