package Extract::Coinet::JobOpDtl;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">i:/transfer/";
    my $file = "JobOpDtl.txt";
    return $dir . $file;
}

sub sql {
    my $self = shift;
   
    my $sql = qq /
      select
      sd.Company as Company,               -- char 8 
     FROM  pub.JobOpDtl as sd
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

