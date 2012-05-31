package Extract::Coinet::OrderDtl;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">i:/transfer/";
    my $file = "OrderDtl.txt";
    return $dir . $file;
}

sub sql {
    my $self = shift;
   
    my $sql = qq /
      select
      od.Company as Company, -- char 8 
od.OrderLine as OrderLine,  -- int
od.OrderNum as OrderNum,    -- int
od.OrderQty as OrderQty,    -- decimal 12,2
od.OpenLine as OpenLine,    -- smallint 
od.OverridePriceList as OverridePriceList, -- smallint
od.PartNum as PartNum,               -- char 50
od.PriceGroupCode as PriceGroupCode, -- char 10
od.PriceListCode as PriceListCode,   -- char 10
od.PricingQty as PricingQty,   -- decimal 12,2
od.ProdCode as ProdCode,       -- char 8
od.NeedByDate as NeedByDate, -- int after conversion
od.RequestDate as RequestDate, -- int after conversion
od.SalesCatID as SalesCatID,   -- char 4
od.ShortChar01 as ShortChar01, -- char 50 
od.ShortChar02 as ShortChar02, -- char 50
od.ShortChar03 as ShortChar03, -- char 50
od.UnitPrice as UnitPrice,     -- decimal 12,4
od.VoidLine as VoidLine,       -- int
od.SellingFactor as SellingFactor
     FROM  pub.OrderDtl as od
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
	my $RequestDate = $row{REQUESTDATE};
	$RequestDate =~ s/-//g;

	my $NeedByDate = $row{NEEDBYDATE};
	$NeedByDate =~ s/-//g;
        
	print OUT  $i . "\t" .
                  $row{COMPANY}           . "\t" . 
                  $row{ORDERLINE}         . "\t" . 
                  $row{ORDERNUM}          . "\t" . 
                  $row{ORDERQTY}          . "\t" . 
                  $row{OPENLINE}          . "\t" . 
                  $row{OVERRIDEPRICELIST} . "\t" . 
                  $row{PARTNUM}           . "\t" . 
                  $row{PRICEGROUPCODE}    . "\t" . 
                  $row{PRICELISTCODE}     . "\t" . 
                  $row{PRICINGQTY}        . "\t" . 
                  $row{PRODCODE}          . "\t" . 
                  $RequestDate            . "\t" . 
		  $NeedByDate             . "\t" . 
                  $row{SALESCATID}        . "\t" . 
                  $row{SHORTCHAR01}       . "\t" . 
                  $row{SHORTCHAR02}       . "\t" . 
                  $row{SHORTCHAR03}       . "\t" . 
                  $row{UNITPRICE}         . "\t" . 
                  $row{VOIDLINE}          . "\t" . 
                  $row{SELLINGFACTOR}     . "\n";
    }
    close OUT;
}

1;

