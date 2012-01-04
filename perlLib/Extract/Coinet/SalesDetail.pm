package Extract::Coinet::SalesDetail;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">i:/transfer/";
    my $file = "SalesDtl.txt";
    return $dir . $file;
}


sub sql {
    my $self = shift;
    my $sql = qq /  
      select
      ih.fiscalYear as wyear,
      ih.fiscalPeriod as wper,
      ih.InvoiceNum as invNum,
      ih.OrderNum as iord,
      id.InvoiceLine as ilin,
      ih.InvoiceDate as idte,
      id.ShipDate as itdte,
      id.SellingShipQty as itsh,
      id.UnitPrice as iprc,
      id.SellingShipQty * id.UnitPrice as idlr,
      id.ExtPrice as ilprc,
      cmSt.CustID  as CustID,
      id.ShipToNum as shipTo,
      cmBt.CustID as billTo,
      id.PartNum as partNum,
      pt.PartDescription as partDesc,
      ih.SalesRepList as isls1,
      ih.CustNum as CustNum,  -- bill to custNum buygroup
      ih.SoldToCustNum as SoldToCustNum
      
      from pub.InvcHead as ih
       left join pub.InvcDtl as id
         on id.Company = ih.Company and
            id.InvoiceNum = ih.InvoiceNum
       left join pub.Customer as cmBt
         on ih.Company = cmBt.Company and
            cmBt.CustNum = ih.CustNum
       left join pub.Customer as cmSt
         on ih.Company = cmSt.Company and
            ih.SoldToCustNum = cmSt.CustNum
--       left join pub.CustGrup as cg
--         on cg.Company = cmSt.Company and
--            cg.GroupCode = cmSt.GroupCode 
--       left join pub.SalesCat as sc
--         on cg.Company = sc.Company and
--            cg.SalesCatID = sc.SalesCatID
        left join pub.Part as pt
         on pt.Company = id.Company and
            pt.PartNum = id.PartNum
        where
	-- ih.invoiceNum > 609580 and
             ih.fiscalPeriod in (7,8)
             and ih.fiscalYear = 2008
             -- and id.OurShipQty > 0
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
	my $idte = $row{IDTE};
	$idte =~ s/-//g;
	my $itdte = $row{ITDTE};
	$itdte =~ s/-//g;
	print OUT  $i . "\t" .
                  $row{WYEAR} . "\t" . 
                  $row{WPER}     . "\t" . 
                  $row{INVNUM}   . "\t" . 
                  $row{IORD}     . "\t" . 
                  $row{ILIN}     . "\t" . 
                  $idte          . "\t" . 
                  $itdte         . "\t" . 
                  $row{ITSH}     . "\t" . 
                  $row{IPRC}     . "\t" . 
                  $row{IDLR}     . "\t" . 
                  $row{ILPRC}     . "\t" . 
                  $row{CUSTID}     . "\t" . 
                  $row{SHIPTO}     . "\t" . 
                  $row{BILLTO}     . "\t" . 
                  $row{PARTNUM}     . "\t" . 
                  $row{PARTDESC}     . "\t" .
                  $row{ISLS1}     . "\t" . 
                  $row{CUSTNUM}     . "\t" . 
                  $row{SOLDTOCUSTNUM}    . "\n";

    }
    close OUT;
}

1;
