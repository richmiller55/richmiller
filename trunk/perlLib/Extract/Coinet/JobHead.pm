package Extract::Coinet::JobHead;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">i:/transfer/";
    my $file = "JobHead.txt";
    return $dir . $file;
}

sub sql {
    my $self = shift;
   
    my $sql = qq /
      select
      jh.Company as Company,               -- char 8 
      jh.JobNum  as JobNum,                -- char 25
      jh.JobClosed as JobClosed,           -- C O char I guess
      jh.JobComplete as JobComplete,       -- bool
      jh.JobCompletionDate as JobCompletionDate , -- date
      jh.PartNum as PartNum,               -- char 50
      jh.PartDescription as PartDescription, -- char 50
      jh.PersonId as PersonId,
      jh.PersonList as PersonList,          -- big char
      jh.QtyCompleted   as QtyCompleted,    -- int
      jh.StartDate   as StartDate ,         -- date
      jh.ClosedDate as ClosedDate,          -- date
      1 as filler
     FROM  pub.JobHead as jh
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

	my $JobCompletionDate = $row{JOBCOMPLETIONDATE};
	$JobCompletionDate =~ s/-//g;
	my $StartDate = $row{STARTDATE};
	$StartDate =~ s/-//g;
	my $ClosedDate = $row{CLOSEDDATE};
	$ClosedDate =~ s/-//g;

	print OUT  $i                        . "\t" .
                  $row{COMPANY}              . "\t" . 
                  $row{jobnum}               . "\t" . 
                  $row{jobclosed}            . "\t" . 
                  $row{jobcomplete}          . "\t" . 
                  $JobCompletionDate         . "\t" . 
                  $row{partnum}              . "\t" . 
                  $row{PartDescription}      . "\t" . 
                  $row{PersonId}             . "\t" . 
                  $row{QtyCompleted}         . "\t" . 
                  $StartDate                 . "\t" . 
                  $ClosedDate                . "\t" . 
		  0 .   "\n";
    }
    close OUT;
}
1;

