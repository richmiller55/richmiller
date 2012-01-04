package Extract::Coinet::PORel;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">i:/transfer/";
    my $file = "PORel.txt";
    return $dir . $file;
}

sub sql {
    my $self = shift;
   
    my $sql = qq /
      select
      p.Company as Company, -- char 8 
      p.PONum  as PONum,  -- integer
      p.POLine as POLine, -- integer
      p.PORelNum as PORelNum,   -- integer
      p.DueDate as DueDate, -- int after conversion
      p.XRelQty as XRelQty, -- decimal
      p.RelQty  as RelQty, --  decimal
      p.WarehouseCode as WarehouseCode, -- char 8
      p.ReceivedQty as ReceivedQty, -- decimal  rec to date
      p.PromiseDt as PromiseDt, -- int after conversion
      p.ShippedQty as ShippedQty, -- decimal
      p.ReqChgDate as ReqChgDate, -- int after
      p.ShippedDate as ShippedDate, -- int after conversion
      p.ContainerID as ContainerID,
      p.Date01 as Date01
     FROM  pub.PORel as p
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

	my $DueDate = $row{DUEDATE};
	$DueDate =~ s/-//g;

	my $PromiseDt = $row{PROMISEDT};
	$PromiseDt =~ s/-//g;

	my $ReqChgDate = $row{REQCHGDATE};
	$ReqChgDate =~ s/-//g;

	my $ShippedDate = $row{SHIPPEDDATE};
	$ShippedDate =~ s/-//g;

	my $ExAsiaDate = $row{DATE01};
	$ExAsiaDate =~ s/-//g;
        
	print OUT  $i . "\t" .
                  $row{COMPANY}       . "\t" . 
                  $row{PONUM}     . "\t" . 
                  $row{POLINE}    . "\t" . 
                  $row{PORELNUM}    . "\t" .
                  $DueDate   . "\t" . 
                  $row{XRELQTY}   . "\t" . 
                  $row{RELQTY}         . "\t" . 
                  $row{WAREHOUSECODE}       . "\t" . 
                  $row{RECEIVEDQTY}     . "\t" . 
                  $PromiseDt    . "\t" . 
                  $row{SHIPPEDQTY}    . "\t" .
                  $ReqChgDate   . "\t" . 
                  $ShippedDate  . "\t" . 
                  $row{CONTAINERID}  . "\t" .
                  $ExAsiaDate  .  "\t" .
                   7 .  "\n";

    }
    close OUT;
}

1;
