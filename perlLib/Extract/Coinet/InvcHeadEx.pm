package Extract::Coinet::InvcHeadEx;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">i:/transfer/";
    my $file = "InvcHeadEx.txt";
    return $dir . $file;
}


sub sql {
    my $self = shift;
    my $sql = qq /  
      select
      ih.fiscalYear as wyear,
      ih.fiscalPeriod as wper,
      ih.SoldToCustNum as SoldToCustNum,
      ih.CustNum as BillToCustNum,
      ih.InvoiceNum as invNum,
      ih.OrderNum as orderNum,
      ih.InvoiceDate as InvoiceDate,
      ih.CreditMemo as CreditMemo,
      ih.DocInvoiceAmt as DocInvoiceAmt,
      ih.InvoiceAmt as InvoiceAmt,
      ih.EntryPerson as EntryPerson,
      ih.SalesRepList as SalesRepList,
      ih.StartUp as StartUp,
      ih.Posted as Posted,
      ih.InvoiceBal as InvoiceBal,
      ih.UnappliedCash as UnappliedCash,
      ih.DebitNote as DebitNote,
      ih.InvoiceSuffix as InvoiceSuffix,
      ih.CheckBox01 as CheckBox01,
      1 as filler
      from pub.InvcHead as ih
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
	my $InvoiceDate = $row{INVOICEDATE};
	$InvoiceDate =~ s/-//g;
	print OUT  $i . "\t" .
                  $row{WYEAR} . "\t" . 
                  $row{WPER}     . "\t" . 
                  $row{SOLDTOCUSTNUM}  . "\t" . 
                  $row{BILLTOCUSTNUM}  . "\t" . 
                  $row{INVNUM}     . "\t" . 
                  $row{ORDERNUM}     . "\t" . 
                  $InvoiceDate     . "\t" . 
                  $row{CREDITMEMO}     . "\t" . 
                  $row{DOCINVOICEAMT}     . "\t" . 
                  $row{INVOICEAMT}     . "\t" . 
                  $row{ENTRYPERSON}     . "\t" . 
                  $row{SALESREPLIST}     . "\t" . 
                  $row{STARTUP}     . "\t" . 
                  $row{POSTED}      . "\t" .
                  $row{INVOICEBAL}  . "\t" .
                  $row{UNAPPLIEDCASH}  . "\t" .
                  $row{DEBITNOTE}  . "\t" .
                  $row{INVOICESUFFIX}  . "\t" .
                  $row{CHECKBOX01}  . "\t" .
		  1                 . "\n";
    }
    close OUT;
}

1;
