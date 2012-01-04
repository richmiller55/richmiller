package Extract::Coinet::RMADtl;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">i:/transfer/";
    my $file = "RMADtl.txt";
    return $dir . $file;
}

sub sql {
    my $self = shift;
   
    my $sql = qq /
      select
      rm.ChangeDate as ChangeDate,  -- int
      rm.Company as Company,                    -- char 8 
      rm.CustNum         as CustNum,            -- int
      rm.OpenDtl         as OpenDtl,            -- int
      rm.OpenRMA         as OpenRMA,            -- int
      rm.OrderLine       as OrderLine,          -- int
      rm.OrderNum        as OrderNum,           -- int
      rm.OrderRelNum     as OrderRelNum,        -- smallint
      rm.OurReturnQty    as OurReturnQty,       -- decimal 12,2
      rm.PartNum         as PartNum,            -- char 50     
      rm.RefInvoiceLine  as RefInvoiceLine,     -- smallint
      rm.RefInvoiceNum   as RefInvoiceNum,      -- int
      rm.ReturnReasonCode as ReturnReasonCode, -- char(5)
      rm.RMALine          as RMALine,          --  smallint,
      rm.RMANum           as RMANum,           --  int,
      rm.ShipToNum as ShipToNum,  -- varchar 14
      0 as filler
     FROM  pub.RMADtl as rm
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
	my $ChangeDate = $row{CHANGEDATE};
	$ChangeDate =~ s/-//g;

	print OUT  $i . "\t" .
                  $ChangeDate             . "\t" . 
                  $row{COMPANY}           . "\t" . 
                  $row{CUSTNUM}           . "\t" . 
                  $row{OPENDTL}           . "\t" . 
                  $row{OPENRMA}           . "\t" . 
                  $row{ORDERLINE}         . "\t" . 
                  $row{ORDERNUM}          . "\t" . 
                  $row{ORDERRELNUM}       . "\t" . 
                  $row{OURRETURNQTY}      . "\t" . 
                  $row{PARTNUM}           . "\t" . 
                  $row{REFINVOICELINE}    . "\t" . 
                  $row{REFINVOICENUM}     . "\t" . 
                  $row{RETURNREASONCODE}  . "\t" . 
                  $row{RMALINE}           . "\t" . 
                  $row{RMANUM}            . "\t" . 
                  $row{SHIPTONUM}         . "\t" .
                     0                    . "\n";
    }
    close OUT;
}

1;

