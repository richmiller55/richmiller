package Extract::Coinet::SalesRep;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">i:/transfer/";
    my $file = "SalesRep.txt";
    return $dir . $file;
}

sub sql {
    my $self = shift;
   
    my $sql = qq /
    select
      sr.Company as Company,                       -- x 8 
      sr.SalesRepCode as SalesRepCode,             -- x 8
      sr.Name as Name,                             -- x 30
      sr.CommissionPercent as CommissionPercent,   -- decimal 
      sr.CommissionEarnedAt as CommissionEarnedAt,  -- x 1 , I or P  
      sr.AlertFlag as AlertFlag,                    -- small int
      sr.EMailAddress as EMailAddress,              -- x 50
      sr.WebSaleGetsCommission as WebSaleGetsCommission,   -- small int
      sr.Number01 as bgCommRate,                      -- decimal
      sr.Number02 as Number02,                      -- decimal
      sr.Number03 as Number03,                      -- decimal
      sr.InActive as InActive,                       -- small int
      sr.CheckBox01  as runReport,                    -- small int run report
      sr.OfficePhoneNum as OfficePhoneNum,
      sr.CellPhoneNum as CellPhoneNum,
      sr.HomePhoneNum as HomePhoneNum
     FROM  pub.SalesRep as sr
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

	print OUT  $i                          . "\t" .
                  $row{COMPANY}                . "\t" . 
                  $row{SALESREPCODE}           . "\t" . 
                  $row{NAME}                   . "\t" . 
                  $row{COMMISSIONPERCENT}      . "\t" . 
                  $row{COMMISSIONEARNEDAT}     . "\t" . 
                  $row{ALERTFLAG}              . "\t" . 
                  $row{EMAILADDRESS}           . "\t" . 
                  $row{WEBSALEGETSCOMMISSION}  . "\t" . 
                  $row{BGCOMMRATE}             . "\t" . 
                  $row{NUMBER02}               . "\t" . 
                  $row{NUMBER03}               . "\t" . 
                  $row{INACTIVE}               . "\t" ,
                  $row{RUNREPORT}              . "\t" ,		  
                  $row{OFFICEPHONENUM}         . "\t" ,		  
                  $row{CELLPHONENUM}           . "\t" ,		  
                  $row{HOMEPHONENUM}           . "\t" ,		  
                  1                            . "\n";
    }
    close OUT;
}

1;

