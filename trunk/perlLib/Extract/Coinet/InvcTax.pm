package Extract::Coinet::InvcTax;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">i:/transfer/";
    my $file = "InvcTax.txt";
    return $dir . $file;
}

sub sql {
    my $self = shift;
    my $sql = qq /  
        select
            it.Company          as Company,  -- char 8
            it.InvoiceNum       as InvoiceNum, -- int
            it.InvoiceLine      as InvoiceLine, -- int
            it.TaxCode          as TaxCode ,  -- x10 
            it.ReportableAmt    as ReportableAmt,  -- dec 12 
            it.DocReportableAmt as DocReportableAmt,  --  dec 12 
            it.TaxableAmt       as TaxableAmt,  -- 
            it.Percent          as Percent,  -- dec 6, 3 
            it.TaxAmt           as TaxAmt,  -- dec 12 , 2
            it.TaxDivision      as TaxDivision,  -- x 50
            it.Manual           as Manual  --  smallint
      from pub.InvcTax as it
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
                  $row{COMPANY}          . "\t" . 
                  $row{INVOICENUM}       . "\t" . 
                  $row{INVOICELINE}      . "\t" . 
                  $row{TAXCODE}          . "\t" . 
                  $row{REPORTABLEAMT}    . "\t" . 
                  $row{DOCREPORTABLEAMT} . "\t" . 
                  $row{TAXABLEAMT}       . "\t" . 
                  $row{PERCENT}          . "\t" . 
                  $row{TAXAMT}           . "\t" . 
                  $row{TAXDIVISION}      . "\t" . 
                  $row{MANUAL}           . "\n";

    }
    close OUT;
}

1;
