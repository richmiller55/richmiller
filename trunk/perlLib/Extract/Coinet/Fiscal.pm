package Extract::Coinet::Fiscal;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">i:/transfer/";
    my $file = "Fiscal.txt";
    return $dir . $file;
}

sub sql {
    my $self = shift;
   
    my $sql = qq /
      select
      f.ClosedPeriod as ClosedPeriod, -- smallint
      f.Company as Company, -- char 8 
      f.EndDate  as EndDate,  -- int
      f.FAClosedPeriod as FAClosedPeriod, -- smallint
      f.FiscalPeriod as FiscalPeriod,  -- smallint
      f.FiscalYear   as FiscalYear, -- smallint
      f.StartDate as StartDate   -- int
      
     FROM  pub.Fiscal as f
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
	my $endDate = $row{ENDDATE};
	$endDate =~ s/-//g;

	my $startDate = $row{STARTDATE};
	$startDate =~ s/-//g;
        
	print OUT  $i                . "\t" .
                $row{COMPANY}        . "\t" . 
                $row{CLOSEDPERIOD}   . "\t" .
                $startDate           . "\t" . 
                $endDate             . "\t" . 
                $row{FISCALPERIOD}   . "\t" . 
                $row{FISCALYEAR}     . "\t" . 
                $row{FACLOSEDPERIOD} . "\t" .
                1                    . "\n";
    }
    close OUT;
}

1;
