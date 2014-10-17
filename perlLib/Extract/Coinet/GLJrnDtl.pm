package Extract::Coinet::GLJrnDtl;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">I:/transfer/";
    my $file = "GLJrnDtl.txt";
    return $dir . $file;
}

sub sql {
    my $self = shift;
   
    my $sql = qq /
      select
gl.APInvoiceNum as APInvoiceNum,
gl.ARInvoiceNum as ARInvoiceNum,
gl.BankAcctID as BankAcctID,
gl.BankSlip as BankSlip,
gl.BankTranNum as BankTranNum,
gl.CheckNum as CheckNum,
gl.CommentText as CommentText,
gl.Company as Company,
gl.CRHeadNum as CRHeadNum,
gl.Description as Description,
gl.ExtCompanyID as ExtCompanyID,
gl.ExtGLChart as ExtGLChart,
gl.ExtGLDept as ExtGLDept,
gl.ExtGLDiv as ExtGLDiv,
gl.ExtRefCode as ExtRefCode,
gl.ExtRefType as ExtRefType,
gl.FiscalPeriod as FiscalPeriod,
gl.FiscalYear as FiscalYear,
gl.GlbAPInvoiceNum as GlbAPInvoiceNum,
gl.GlbCompanyID as GlbCompanyID,
gl.GlbFiscalPeriod as GlbFiscalPeriod,
gl.GlbFiscalYear as GlbFiscalYear,
gl.GlbGroupID as GlbGroupID,
gl.GlbJournalCode as GlbJournalCode,
gl.GlbJournalLine as GlbJournalLine,
gl.GlbJournalNum as GlbJournalNum,
gl.GlbVendorNum as GlbVendorNum,
gl.GLChart as GLChart,
gl.GLDept as GLDept,
gl.GLDiv as GLDiv,
gl.GroupID as GroupID,
gl.JEDate as JEDate,
gl.JournalCode as JournalCode,
gl.JournalLine as JournalLine,
gl.JournalNum as JournalNum,
gl.Linked as Linked,
gl.MultiCompany as MultiCompany,
gl.Posted as Posted,
gl.PostedBy as PostedBy,
gl.PostedDate as PostedDate,
gl.RefCode as RefCode,
gl.RefType as RefType,
gl.Reverse as Reverse,
gl.SourceModule as SourceModule,
gl.TransAmt as TransAmt,
gl.VendorNum as VendorNum,
4 as fill
FROM pub.GLJrnDtl as gl
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
#	my $sysDate = $row{SYSDATEX};
#	$sysDate =~ s/-//g;

	my $JEDate = $row{JEDATE};
	$JEDate =~ s/-//g;
        
	my $PostedDate = $row{POSTEDDATE};
	$PostedDate =~ s/-//g;
        
	print OUT  $i . "\t" .
	    $row{RECONCILENUM} . "\t" .
	    $row{APINVOICENUM} . "\t" .
	    $row{ARINVOICENUM} . "\t" .
	    $row{BANKACCTID} . "\t" .
	    $row{BANKSLIP} . "\t" .
	    $row{BANKTRANNUM} . "\t" .
	    $row{CHECKNUM} . "\t" .
	    $row{COMMENTTEXT} . "\t" .
	    $row{COMPANY} . "\t" .
	    $row{CRHEADNUM} . "\t" .
	    $row{DESCRIPTION} . "\t" .
	    $row{EXTCOMPANYID} . "\t" .
	    $row{EXTGLCHART} . "\t" .
	    $row{EXTGLDEPT} . "\t" .
	    $row{EXTGLDIV} . "\t" .
	    $row{EXTREFCODE} . "\t" .
	    $row{EXTREFTYPE} . "\t" .
	    $row{FISCALPERIOD} . "\t" .
	    $row{FISCALYEAR} . "\t" .
	    $row{GLBAPINVOICENUM} . "\t" .
	    $row{GLBCOMPANYID} . "\t" .
	    $row{GLBFISCALPERIOD} . "\t" .
	    $row{GLBFISCALYEAR} . "\t" .
	    $row{GLBGROUPID} . "\t" .
	    $row{GLBJOURNALCODE} . "\t" .
	    $row{GLBJOURNALLINE} . "\t" .
	    $row{GLBJOURNALNUM} . "\t" .
	    $row{GLBVENDORNUM} . "\t" .
	    $row{GLCHART} . "\t" .
	    $row{GLDEPT} . "\t" .
	    $row{GLDIV} . "\t" .
	    $row{GROUPID} . "\t" .
	    $JEDate . "\t" .
	    $row{JOURNALCODE} . "\t" .
	    $row{JOURNALLINE} . "\t" .
	    $row{JOURNALNUM} . "\t" .
	    $row{LINKED} . "\t" .
	    $row{MULTICOMPANY} . "\t" .
	    $row{POSTED} . "\t" .
	    $row{POSTEDBY} . "\t" .
	    $PostedDate . "\t" .
	    $row{REFCODE} . "\t" .
	    $row{REFTYPE} . "\t" .
	    $row{REVERSE} . "\t" .
	    $row{SOURCEMODULE} . "\t" .
	    $row{TRANSAMT} . "\t" .
	    $row{VENDORNUM} . "\t" .
	    1                   . "\n";
    }
    close OUT;
}

1;

