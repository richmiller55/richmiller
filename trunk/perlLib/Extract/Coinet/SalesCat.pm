package Extract::Coinet::SalesCat;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">i:/transfer/";
    my $file = "SalesCat.txt";
    return $dir . $file;
}

sub sql {
    my $self = shift;
   
    my $sql = qq /
      select
      sc.Company as Company, -- char 8 
      sc.Description  as Description,  -- char 15
      sc.GLDept as GLDept, -- char 50
      sc.SalesCatID as SalesCatID -- char 4
     FROM  pub.SalesCat as sc
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
                $row{DESCRIPTION}  . "\t" .
                $row{GLDEPT}    . "\t" .
                $row{SALESCATID}   . "\t" . 
                1                  . "\n";
    }
    close OUT;
}

1;

