package Extract::Coinet::POHeader;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">i:/transfer/";
    my $file = "POHeader.txt";
    return $dir . $file;
}

sub sql {
    my $self = shift;
   
    my $sql = qq /
      select
      p.Company as Company, -- char 8 
      p.PONum  as PONum,  -- integer
      p.OrderDate as OrderDate, -- int after conversion
      p.ApprovalStatus as ApprovalStatus, -- char 1
      p.Approve as Approve, -- smallint
      p.ApprovedBy as ApprovedBy, -- x 20
      p.ApprovedDate as ApprovedDate, -- int after 
      p.BuyerID as BuyerID,  -- x8
      p.Confirmed as Confirmed, -- smallint
      p.EntryPerson as EntryPerson, -- x 20
      p.Linked as Linked, -- smallint
      p.OpenOrder as OpenOrder, -- smallint
      p.VendorNum as VendorNum
     FROM  pub.POHeader as p
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

	my $OrderDate = $row{ORDERDATE};
	$OrderDate =~ s/-//g;

	my $ApprovedDate = $row{APPROVEDDATE};
	$ApprovedDate =~ s/-//g;
        
	print OUT  $i . "\t" .
                  $row{COMPANY}         . "\t" . 
                  $row{PONUM}           . "\t" . 
                  $OrderDate            . "\t" . 
                  $row{APPROVALSTATUS}  . "\t" . 
                  $row{APPROVE}         . "\t" . 
                  $row{APPROVEDBY}      . "\t" . 
                  $ApprovedDate         . "\t" . 
                  $row{BUYERID}         . "\t" . 
                  $row{CONFIRMED}       . "\t" .
                  $row{ENTRYPERSON}     . "\t" . 
                  $row{LINKED}          . "\t" . 
                  $row{OPENORDER}       . "\t" .
                  $row{VENDORNUM}       . "\n";
    }
    close OUT;
}

1;
