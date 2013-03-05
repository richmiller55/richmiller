package Extract::Coinet::PartPlant;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">i:/transfer/";
    my $file = "PartPlant.txt";
    return $dir . $file;
}

sub sql {
    my $self = shift;
   
    my $sql = qq /
      select
      pp.Company as Company, -- char 8 
      pp.Plant  as Plant,  -- char 8
      pp.PartNum  as PartNum,  -- char 50
      pp.PrimWhse as PrimWhse, -- char 8
      pp.MinimumQty as MinimumQty, -- decimal 12,2
      pp.MaximumQty as MaximumQty, -- decimal 12,2
      pp.SafetyQty as SafetyQty,
      pp.MinOrderQty as MinOrderQty,
      pp.AllocQty as AllocQty,
      0 as fill
     FROM  pub.PartPlant as pp
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
                  $row{PLANT}        . "\t" . 
                  $row{PARTNUM}      . "\t" . 
                  $row{PRIMWHSE}     . "\t" . 
                  $row{MINIMUMQTY}   . "\t" . 
                  $row{MAXIMUMQTY}   . "\t" . 
                  $row{SAFETYQTY}    . "\t" . 
                  $row{MINORDERQTY}  . "\t" . 
                  $row{ALLOCQTY}     . "\t" . 
                  $row{FILL}         . "\n";
    }
    close OUT;
}

1;

