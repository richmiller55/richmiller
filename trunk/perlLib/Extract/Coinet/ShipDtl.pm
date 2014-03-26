package Extract::Coinet::ShipDtl;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">i:/transfer/";
    my $file = "ShipDtl.txt";
    return $dir . $file;
}

sub sql {
    my $self = shift;
   
    my $sql = qq /
      select
      sd.Company as Company,               -- char 8 
      sd.PackNum as PackNum,               -- int
      sd.PackLine as PackLine,             -- int
      sd.OrderNum as OrderNum,             -- int
      sd.OrderLine as OrderLine,           -- int
      sd.OrderRelNum as OrderRelNum,       -- int
      sd.OurInventoryShipQty as OurInventoryShipQty, -- decimal 12 2
      sd.OurJobShipQty as OurJobShipQty,   -- decimal 12 2
      sd.JobNum as JobNum,                 -- char 14
      sd.PartNum as PartNum,               -- char 50
      sd.ShipCmpl as ShipCmpl,              -- smallint
      sd.WarehouseCode as WarehouseCode,   -- char 8
      sd.BinNum   as BinNum,               -- char 10
      sd.UpdatedInventory as UpdatedInventory, -- smallint
      sd.Invoiced as Invoiced,              -- smallint
      sd.CustNum as CustNum,               -- int
      sd.ShipToNum  as ShipToNum,          -- char 14
      sd.ReadyToInvoice as ReadyToInvoice,  -- smallint
      sd.ChangedBy as ChangedBy,            -- char 20
      sd.ChangeDate as ChangeDate          -- int
     FROM  pub.ShipDtl as sd
     where sd.PackNum > 410231
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

	my $changeDate = $row{CHANGEDATE};
	$changeDate =~ s/-//g;

	print OUT  $i                         . "\t" .
                  $row{COMPANY}               . "\t" . 
                  $row{PACKNUM}               . "\t" . 
                  $row{PACKLINE}              . "\t" . 
                  $row{ORDERNUM}              . "\t" . 
                  $row{ORDERLINE}             . "\t" . 
                  $row{ORDERRELNUM}           . "\t" . 
                  $row{OURINVENTORYSHIPQTY}   . "\t" . 
                  $row{OURJOBSHIPQTY}         . "\t" . 
                  $row{JOBNUM}                . "\t" . 
                  $row{PARTNUM}               . "\t" . 
                  $row{SHIPCMPL}              . "\t" . 
                  $row{WAREHOUSECODE}         . "\t" . 
                  $row{BINNUM}                . "\t" . 
                  $row{UPDATEDINVENTORY}      . "\t" . 
                  $row{INVOICED}              . "\t" . 
                  $row{CUSTNUM}               . "\t" . 
                  $row{SHIPTONUM}             . "\t" . 
                  $row{READYTOINVOICE}        . "\t" . 
                  $row{CHANGEDBY}             . "\t" . 
                  $changeDate                 . "\n";

    }
    close OUT;
}
1;

