package Extract::Coinet::CustGrup;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">i:/transfer/";
    my $file = "CustGrup.txt";
    return $dir . $file;
}

sub sql {
    my $self = shift;
   
    my $sql = qq /
      select
      cg.Company as Company, -- char 8 
      cg.GroupCode  as GroupCode,  -- char 4
      cg.GroupDesc as GroupDesc, -- char 20
      cg.SalesCatID as SalesCatID, -- char 4
      cg.Character01 as Character01, -- char 1000 super group
      cg.CheckBox01 as CheckBox01, -- int?  supress line in reporting
      cg.Number01 as Number01,   -- int sort order
      0 as filler
     FROM  pub.CustGrup as cg
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
        
	print OUT  $i              . "\t" .
                $row{COMPANY}      . "\t" . 
                $row{GROUPCODE}    . "\t" .
                $row{GROUPDESC}    . "\t" .
                $row{SALESCATID}   . "\t" . 
                $row{CHARACTER01}  . "\t" . 
                $row{CHECKBOX01}   . "\t" . 
                $row{NUMBER01}     . "\t" . 
                1                  . "\n";
    }
    close OUT;
}

1;

