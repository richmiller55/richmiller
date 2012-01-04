package Extract::Coinet::ShipVia;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">i:/transfer/";
    my $file = "ShipVia.txt";
    return $dir . $file;
}

sub sql {
    my $self = shift;
   
    my $sql = qq /
      select
      sv.Company as Company, -- char 8 
      sv.Description  as Description,  -- char 30
      sv.ShipViaCode  as ShipViaCode  -- char 4
     FROM  pub.ShipVia as sv
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
                  $row{DESCRIPTION}     . "\t" . 
                  $row{SHIPVIACODE}     . "\t" . 
                  '1'                  .  "\n";
    }
    close OUT;
}

1;

