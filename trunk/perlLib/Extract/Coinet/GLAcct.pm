package Extract::Coinet::GLAcct;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">I:/transfer/";
    my $file = "GLAcct.txt";
    return $dir . $file;
}

sub sql {
    my $self = shift;
   
    my $sql = qq /
      select
     gl.AcctDesc as AcctDesc,
     gl.Active as Active,
     gl.Category as Category,
gl.Chart as Chart,
gl.Company as Company,
gl.DisplayAccount as DisplayAccount,
gl.Division as Division,
gl.GLDept as GLDept,
gl.MultiCompany as MultiCompany,
gl.NormalBalance as NormalBalance,
gl.OverrideAcctDesc as OverrideAcctDesc,
gl.ParentChart as ParentChart,
gl.ParentDept as ParentDept,
gl.ParentDiv as ParentDiv,
gl.Segment1 as Segment1,
gl.Segment10 as Segment10,
gl.Segment11 as Segment11,
gl.Segment12 as Segment12,
gl.Segment13 as Segment13,
gl.Segment14 as Segment14,
gl.Segment15 as Segment15,
gl.Segment16 as Segment16,
gl.Segment2 as Segment2,
gl.Segment3 as Segment3,
gl.Segment4 as Segment4,
gl.Segment5 as Segment5,
gl.Segment6 as Segment6,
gl.Segment7 as Segment7,
gl.Segment8 as Segment8,
gl.Segment9 as Segment9,
gl.SubCat as SubCat


FROM pub.GLAcct as gl
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
	    $row{ACCTDESC} . "\t" .
	    $row{ACTIVE} . "\t" .
	    $row{CATEGORY} . "\t" .
	    $row{CHART} . "\t" .
	    $row{COMPANY} . "\t" .
	    $row{DISPLAYACCOUNT} . "\t" .
	    $row{DIVISION} . "\t" .
	    $row{GLDEPT} . "\t" .
	    $row{MULTICOMPANY} . "\t" .
	    $row{NORMALBALANCE} . "\t" .
	    $row{OVERRIDEACCTDESC} . "\t" .
	    $row{PARENTCHART} . "\t" .
	    $row{PARENTDEPT} . "\t" .
	    $row{PARENTDIV} . "\t" .
	    $row{SUBCAT} . "\t" .
	    1                   . "\n";
    }
    close OUT;
}

1;

