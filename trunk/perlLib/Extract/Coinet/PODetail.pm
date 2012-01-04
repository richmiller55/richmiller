package Extract::Coinet::PODetail;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">i:/transfer/";
    my $file = "PODetail.txt";
    return $dir . $file;
}

sub sql {
    my $self = shift;
   
    my $sql = qq /
      select
      p.Company as Company, -- char 8 
      p.OpenLine as OpenLine, -- int
      p.VoidLine as VoidLine, -- int
      p.PONUM  as PONum,  -- integer
      p.POLine as POLine, -- integer
      p.LineDesc as LineDesc,   -- x1000
      p.IUM as IUM, -- x2
      p.UnitCost as UnitCost, -- decimal.5
      p.OrderQty as OrderQty, -- decimal 2
      p.XOrderQty as XOrderQty, -- decimal 2
      p.Taxable as Taxable, -- int
      p.PUM as PUM, -- x2      
      p.CostPerCode as CostPerCode, -- char 1
      p.PartNum as PartNum, -- x 50
      p.VenPartNum as VenPartNum, -- int
      p.AdvancePayBal as AdvancePayBal, -- dec 2
      p.Confirmed as Confirmed, -- int
      p.DateChgReq as DateChgReq,  -- int
      p.ConfirmDate as ConfirmDate, -- int after convert
      p.OrderNum as OrderNum, -- int
      p.OrderLine as OrderLine, -- int
      p.Linked as Linked, -- smallint
      p.Date01 as exAsiaDate,  -- int
      p.Date02 as customerShipDate   -- int
    FROM  pub.PODetail as p
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

	my $exAsiaDate = $row{EXASIADATE};
	$exAsiaDate =~ s/-//g;
	my $customerShipDate = $row{CUSTOMERSHIPDATE};
	$customerShipDate =~ s/-//g;
        
	print OUT  $i . "\t" .
                  $row{COMPANY}       . "\t" . 
                  $row{OPENLINE}     . "\t" . 
                  $row{VOIDLINE}     . "\t" . 
                  $row{PONUM}     . "\t" . 
                  $row{POLINE}     . "\t" . 
                  $row{LINEDESC}     . "\t" . 
                  $row{IUM}     . "\t" . 
                  $row{UNITCOST}     . "\t" . 
                  $row{ORDERQTY}     . "\t" . 
                  $row{XORDERQTY}     . "\t" . 
                  $row{TAXABLE}     . "\t" . 
                  $row{PUM}     . "\t" . 
                  $row{COSTPERCODE}     . "\t" . 
                  $row{PARTNUM}     . "\t" . 
                  $row{VENPARTNUM}     . "\t" . 
                  $row{ADVANCEPAYBAL}     . "\t" . 
                  $row{CONFIRMED}     . "\t" . 
                  $row{DATECHGREQ}     . "\t" . 
                  $row{CONFIRMDATE}     . "\t" . 
                  $row{ORDERNUM}     . "\t" . 
                  $row{ORDERLINE}    . "\t" . 
                  $row{LINKED}       . "\t" . 
		  $exAsiaDate        . "\t" . 
		  $customerShipDate . "\t" . 
                  1  . "\n";
    }
    close OUT;
}

1;
