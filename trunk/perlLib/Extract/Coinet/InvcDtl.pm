package Extract::Coinet::InvcDtl;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">i:/transfer/";
    my $file = "InvcDtl.txt";
    return $dir . $file;
}


sub sql {
    my $self = shift;
    my $sql = qq /  
      select
      ih.fiscalYear as FiscalYear,
      ih.fiscalPeriod as FiscalPeriod,
      ih.InvoiceNum as InvoiceNum,
      id.InvoiceLine as InvoiceLine,
      ih.InvoiceDate as InvoiceDate,
      id.PackNum as PackNum,
      id.PackLine as PackLine,
      ih.OrderNum as OrderNum,
      id.OrderLine as OrderLine,
      id.ShipDate as ShipDate,
      id.SellingShipQty as SellingShipQty,
      id.UnitPrice as UnitPrice,
      id.SellingShipQty * id.UnitPrice as TotalDollars,
      id.ExtPrice as ExtPrice,
      cmSt.CustID  as SoldToCustID,
      id.ShipToNum as ShipToNum,
      cmBt.CustID as billToCustID,
      id.PartNum as PartNum,
      pt.PartDescription as PartDescription,
      ih.SalesRepList as SalesRepList,
      ih.CustNum as BillToCustNum,  -- bill to custNum buygroup
      ih.SoldToCustNum as SoldToCustNum,
      id.SalesChart as SalesChart,
      id.SalesDept as SalesDept,
      id.SalesDiv as SalesDiv,
      id.MtlUnitCost as MtlUnitCost,
      id.LbrUnitCost as LbrUnitCost,
      id.BurUnitCost as BurUnitCost,
      id.SubUnitCost as SubUnitCost,
      id.MtlBurUnitCost as MtlBurUnitCost,
      id.Discount  as Discount,
      id.DiscountPercent as DiscountPercent,
      id.SellingFactor as SellingFactor,
      id.SellingFactorDirection as SellingFactorDirection
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
	my $ShipDate = $row{SHIPDATE};
	$ShipDate =~ s/-//g;
	print OUT  $i . "\t" .
                  $row{FISCALYEAR} . "\t" . 
                  $row{FISCALPERIOD}     . "\t" . 
                  $row{INVOICENUM}   . "\t" . 
                  $row{INVOICELINE}     . "\t" . 
                  $InvoiceDate   . "\t" . 
                  $row{PACKNUM}     . "\t" . 
                  $row{PACKLINE}     . "\t" . 
                  $row{ORDERNUM}     . "\t" . 
                  $row{ORDERLINE}     . "\t" . 
                  $ShipDate         . "\t" . 
                  $row{SELLINGSHIPQTY}     . "\t" . 
                  $row{UNITPRICE}     . "\t" . 
                  $row{TOTALDOLLARS}     . "\t" . 
                  $row{EXTPRICE}     . "\t" . 
                  $row{SOLDTOCUSTID}      . "\t" . 
                  $row{SHIPTONUM}         . "\t" . 
                  $row{BILLTOCUSTID}      . "\t" . 
                  $row{PARTNUM}           . "\t" . 
                  $row{PARTDESCRIPTION}   . "\t" .
                  $row{SALESREPLIST}      . "\t" . 
                  $row{BILLTOCUSTNUM}     . "\t" .
                  $row{SOLDTOCUSTNUM}     . "\t" . 
                  $row{SALESCHART}        . "\t" . 
                  $row{SALESDEPT}         . "\t" . 
                  $row{SALESDIV}          . "\t" . 
                  $row{MTLUNITCOST}       . "\t" . 
                  $row{LBRUNITCOST}       . "\t" . 
                  $row{BURUNITCOST}       . "\t" . 
                  $row{SUBUNITCOST}       . "\t" . 
                  $row{MTLBURUNITCOST}    . "\t" .
                  $row{DISCOUNT}          . "\t" . 
                  $row{DISCOUNTPERCENT}   . "\t" . 
                  $row{SELLINGFACTOR}   . "\t" . 
                  $row{SELLINGFACTORDIRECTION}   . "\t" . 

		  0                       . "\n";

    }
    close OUT;
}

1;
