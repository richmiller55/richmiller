package Extract::Coinet::CustXPrt;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">i:/transfer/";
    my $file = "CustXPrt.txt";
    return $dir . $file;
}

sub sql {
    my $self = shift;
   
    my $sql = qq /
      select
      xp.Company as Company, -- char 8 
      xp.BasePartNum as BasePartNum, --  x50
      xp.ChangeDate as ChangeDate,	-- int 
      xp.ChangedBy as ChangedBy, -- 	varchar(20)
      xp.CustNum  as CustNum,  -- int
      xp.PartDescription  as PartDescription, -- varchar(100)
      xp.PartNum as PartNum, -- varchar(50)
      xp.XPartNum as XPartNum, -- varchar(50)
      xp.XRevisionNum	as XRevisionNum -- varchar(12)
     FROM  pub.CustXPrt as xp
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
                $row{BASEPARTNUM}  . "\t" .
                $row{CHANGEDATE}   . "\t" .
                $row{CHANGEDBY}    . "\t" .
                $row{CUSTNUM}     . "\t" .
                $row{PARTDESCRIPTION}     . "\t" .
                $row{PARTNUM}     . "\t" .
                $row{XPARTNUM}     . "\t" .
                $row{XREVISIONNUM}     . "\t" .
                1                  . "\n";
    }
    close OUT;
}

1;

