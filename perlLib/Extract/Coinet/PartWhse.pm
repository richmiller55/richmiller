package Extract::Coinet::PartWhse;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">i:/transfer/";
    my $file = "PartWhse.txt";
    return $dir . $file;
}

sub sql {
    my $self = shift;
   
    my $sql = qq /
      select
      pw.Company as Company, -- char 8 
      pw.PartNum  as PartNum,  -- char 50
      pw.WarehouseCode as WarehouseCode, -- char 8
      pw.AllocQty as AllocQty,   --  decimal 12,2
      pw.CountedDate as CountedDate, -- int after convert
      pw.OnHandQty as OnHandQty,  -- decimal 12,2
      pw.NonNettableQty  as NonNettableQty,-- decimal 12,2
      pw.SalesAllocQty as SalesAllocQty, -- decimal 12,2
      pw.SalesReservedQty as SalesReservedQty,-- decimal 12,2
      pw.SalesPickingQty as SalesPickingQty, -- decimal 12,2
      pw.SalesPickedQty as SalesPickedQty, -- decimal 12,2
      pw.JobAllocQty as JobAllocQty,  -- decimal 12,2
      pw.JobReservedQty as JobReservedQty, -- decimal 12,2
      pw.MinimumQty as MinimumQty,  -- decimal 12,2
      pw.MaximumQty as MaximumQty,  -- decimal 12,2
      pw.SafetyQty as SafetyQty   -- decimal 12,2
     FROM  pub.PartWhse as pw
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
                  $row{COMPANY}      . "\t" . 
                  $row{PARTNUM}     . "\t" . 
                  $row{WAREHOUSECODE}     . "\t" . 
                  $row{ALLOCQTY}     . "\t" . 
                  $row{COUNTEDDATE}     . "\t" . 
                  $row{ONHANDQTY}     . "\t" . 
                  $row{NONNETTABLEQTY}     . "\t" . 
                  $row{SALESALLOCQTY}     . "\t" . 
                  $row{SALESRESERVEDQTY}     . "\t" . 
                  $row{SALESPICKINGQTY}     . "\t" . 
                  $row{SALESPICKEDQTY}     . "\t" . 
                  $row{JOBALLOCQTY}     . "\t" . 
                  $row{JOBRESERVEDQTY}     . "\t" . 
                  $row{MINIMUMQTY}     . "\t" . 
                  $row{MAXIMUMQTY}     . "\t" . 
                  $row{SAFETYQTY}     . "\n";
    }
    close OUT;
}

1;

