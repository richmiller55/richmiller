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
      jd.Company as Company,               -- char 8 
      jd.ActBurCost as ActBurCost,         -- decimal
      jd.ActLabCost as ActLabCost,         -- decimal
      jd.ActProdHours as ActProdHours,      -- decimal
      jd.ActProdRwkHours as ActProdRwkHours,   -- decimal
      jd.AssemblySeq as AssemblySeq,          -- int
      jd.CapabilityID as CapabilityID,        -- char 8
      jd.JobNum as JobNum,                   -- char?
      jd.OpDtlDesc as OpDtlDesc,           --  char 30
      jd.OpDtlSeq  as OpDtlSeq,            -- int
      jd.OprSeq as OprSeq,                 -- int
      jd.SysCreateDate as SysCreateDate,   -- date
      jd.SysCreateTime as SysCreateTime
     FROM  pub.JobOpDtl as jd
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

	my $SysCreateDate = $row{SYSCREATEDATE};
	$SysCreateDate =~ s/-//g;

	print OUT  $i                            . "\t" .
                  $row{COMPANY}                  . "\t" . 
                  $row{JOBNUM}                   . "\t" . 
                  $row{ACTBURCOST}               . "\t" . 
                  $row{ACTLABCOST}               . "\t" . 
                  $row{ACTPRODHOURS}             . "\t" . 
                  $row{ACTPRODRWKHOURS}          . "\t" . 
                  $row{ASSEMBLYSEQ}              . "\t" . 
                  $row{CAPABILITYID}             . "\t" . 
                  $row{OPDTLDESC}                . "\t" . 
                  $row{OPDTLSEQ}                 . "\t" . 
                  $row{OPRSEQ}                   . "\t" . 
                  $row{SYSCREATETIME}            . "\t" . 
                  $SysCreateDate                 . "\n";

    }
    close OUT;
}
1;

