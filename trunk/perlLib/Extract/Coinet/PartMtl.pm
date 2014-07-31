package Extract::Coinet::PartMtl;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">i:/transfer/";
    my $file = "PartMtl.txt";
    return $dir . $file;
}

sub sql {
    my $self = shift;
   
    my $sql = qq /
      select
      pm.Company as Company, -- char 8 
      pm.PartNum as ParentPart, -- char50
      pm.MtlSeq as MtlSeq, -- int
      pm.MtlPartNum as MtlPartNum, -- char 50
      pm.QtyPer as QtyPer,
      pm.RevisionNum as RevisionNum,
      0 as filler
     FROM  pub.PartMtl as pm
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
        
	print OUT  $i             . "\t" .
                $row{COMPANY}     . "\t" . 
                $row{PARENTPART}  . "\t" .
                $row{MTLSEQ}      . "\t" .
                $row{MTLPARTNUM}  . "\t" .
                $row{QTYPER}      . "\t" .
                $row{REVISIONNUM} . "\t" .
                1                 . "\n";
    }
    close OUT;
}

1;

