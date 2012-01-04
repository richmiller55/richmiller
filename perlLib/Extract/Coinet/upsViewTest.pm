package Extract::Coinet::upsViewTest;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">d:/users/rich/export/co-inet/";
    my $file = "upsViewTest.txt";
    return $dir . $file;
}

sub sql {
    my $self = shift;
   
    my $sql = qq /

    select ups.PackNum,
         ups.COD,
         ups.ShipViaCode,
         ups.Name,
         ups.Address1,
         ups.Address2,
	 ups.Address3,
         ups.City,
         ups.State,
         ups.ZIP,
         ups.Country,
         ups.PhoneNum         
     FROM odbcuser.upsView as ups
     where ups.PackNum > 158160
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
                $row{PACKNUM}            . "\t" .
                $row{COD}          . "\t" . 
                $row{SHIPVIACODE}               . "\t" .
                $row{NAME}               . "\t" .
                $row{ADDRESS1}           . "\t" . 
                $row{ADDRESS2}           . "\t" . 
                $row{ADDRESS3}           . "\t" .
                $row{CITY}               . "\t" .
                $row{STATE}              . "\t" . 
                $row{ZIP}                . "\t" . 
                $row{PHONENUM}           . "\t" . 
                $row{COUNTRY}            . "\t" . 
                1                        . "\n";
    }
    close OUT;
}

1;
