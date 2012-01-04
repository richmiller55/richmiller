package Extract::Coinet::ContainerHeader;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">i:/transfer/";
    my $file = "ContainerHeader.txt";
    return $dir . $file;
}

sub sql {
    my $self = shift;
   
    my $sql = qq /
      select
      p.Company as Company, -- char 8 
      p.ContainerID as ContainerID,  --- int 
      p.ShipDate as ShipDate, --  int after conversion
      p.Shipped as Shipped,  -- int
      p.ContainerClass as ContainerClass, -- x 10
      p.ContainerCost as ContainerCost, -- decimal
      p.ShippingDays as ShippingDays, -- integer
      p.ContainerDescription as ContainerDescription, -- x 50
      p.Volume as Volume, -- decimal 2
      p.ContainerReference as ContainerReference -- x 50
     FROM  pub.ContainerHeader as p
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

	my $ShipDate = $row{SHIPDATE};
	$ShipDate =~ s/-//g;
        
	print OUT  $i . "\t" .
                  $row{COMPANY}       . "\t" . 
                  $row{CONTAINERID}     . "\t" . 
                  $ShipDate   . "\t" . 
                  $row{SHIPPED}    . "\t" .
                  $row{CONTAINERCLASS}   . "\t" . 
                  $row{CONTAINERCOST}         . "\t" . 
                  $row{SHIPPINGDAYS}       . "\t" . 
                  $row{CONTAINERDESCRIPTION}     . "\t" . 
                  $row{VOLUME}    . "\t" .
                  $row{CONTAINERREFERENCE}  . "\n";

    }
    close OUT;
}

1;
