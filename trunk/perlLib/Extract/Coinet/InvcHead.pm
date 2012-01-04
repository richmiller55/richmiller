package Extract::Coinet::InvcHead;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">i:/transfer/";
    my $file = "InvcHead.txt";
    return $dir . $file;
}


sub sql {
    my $self = shift;
    my $sql = qq /  
      select
      ih.fiscalYear as wyear,
      ih.fiscalPeriod as wper,
      ih.SoldToCustNum as SoldToCustNum,
      ih.CustNum as CustNum,
      cmSt.CustID  as CustID,
      cmBt.CustID as billTo,
      ih.InvoiceNum as invNum,
      ih.OrderNum as orderNum,
      ih.InvoiceDate as InvoiceDate,
      ih.CreditMemo as CreditMemo,
      ih.DocInvoiceAmt as DocInvoiceAmt,
      ih.InvoiceAmt as InvoiceAmt,
      ih.EntryPerson as EntryPerson,
      ih.SalesRepList as SalesRepList,
      ih.StartUp as StartUp,
      im.MiscAmt  as miscAmt,
      im.ShortChar01 as trackingNo,
      ih.Posted as Posted,
      ih.InvoiceBal as InvoiceBal,
      ih.UnappliedCash as UnappliedCash,
      ih.DebitNote as DebitNote,
      ih.InvoiceSuffix as InvoiceSuffix,
      ih.CheckBox01 as CheckBox01,
      1 as filler
      from pub.InvcHead as ih
       left join pub.InvcMisc as im
         on im.Company = ih.Company and
            im.InvoiceNum = ih.InvoiceNum
       left join pub.Customer as cmBt
         on ih.Company = cmBt.Company and
            cmBt.CustNum = ih.CustNum
       left join pub.Customer as cmSt
         on ih.Company = cmSt.Company and
            ih.SoldToCustNum = cmSt.CustNum
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
                  $row{SOLDTOCUSTNUM}     . "\t" . 
                  $row{CUSTNUM}     . "\t" . 
                  $row{CUSTID}   . "\t" . 
                  $row{BILLTO}    . "\t" .
                  $row{INVNUM}     . "\t" . 
                  $row{ORDERNUM}     . "\t" . 
                  $InvoiceDate     . "\t" . 
                  $row{CREDITMEMO}     . "\t" . 
                  $row{DOCINVOICEAMT}     . "\t" . 
                  $row{INVOICEAMT}     . "\t" . 
                  $row{ENTRYPERSON}     . "\t" . 
                  $row{SALESREPLIST}     . "\t" . 
                  $row{STARTUP}     . "\t" . 
                  $row{MISCAMT}     . "\t" . 
                  $row{TRACKINGNO}  . "\t" .
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
