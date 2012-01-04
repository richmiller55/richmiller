package Extract::Coinet::PartCost;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">i:/transfer/";
    my $file = "PartCost.txt";
    return $dir . $file;
}

sub sql {
    my $self = shift;
   
    my $sql = qq /
      select
      p.Company as Company,  -- x8
      p.PartNum  as PartNum,  -- char 50
      p.AvgBurdenCost as AvgBurdenCost, -- decimal 14,5
      p.AvgLaborCost as AvgLaborCost,
      p.AvgMaterialCost as AvgMaterialCost,
      p.AvgMtlBurCost as AvgMtlBurCost,
      p.AvgSubContCost as AvgSubContCost,
      p.CostID as CostID, -- x8
      p.LastBurdenCost as LastBurdenCost, -- decimal 14,5
      p.LastLaborCost as LastLaborCost, 
      p.LastMaterialCost as LastMaterialCost,
      p.LastMtlBurCost as LastMtlBurCost,
      p.LastSubContCost as LastSubContCost,
      p.Number01        as Number01,
      p.Number02        as Number02,
      p.Number03        as Number03,
      p.Number04        as Number04,
      p.Number05        as Number05,
      p.Number06        as Number06,
      p.Number07        as Number07,
      p.Number08        as Number08,
      p.Number09        as Number09,
      p.Number10        as Number10,
      p.Date01          as Date01,
      p.Date02          as Date02,
      p.Date03          as Date03,
      p.Date04          as Date04,
      p.Date05          as Date05,
      p.CheckBox01      as CheckBox01,
      p.CheckBox02      as CheckBox02,
      p.CheckBox03      as CheckBox03,
      p.CheckBox04      as CheckBox04,
      p.CheckBox05      as CheckBox05
     FROM  pub.PartCost as p
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

	my $date01 = $row{DATE01};
	my $date02 = $row{DATE02};
	my $date03 = $row{DATE03};
	my $date04 = $row{DATE04};
	my $date05 = $row{DATE05};
	$date01 =~ s/-//g;
	$date02 =~ s/-//g;
	$date03 =~ s/-//g;
	$date04 =~ s/-//g;
	$date05 =~ s/-//g;

        
	print OUT  $i . "\t" .
                  $row{COMPANY}       . "\t" . 
                  $row{PARTNUM}       . "\t" . 
                  $row{AVGBURDENCOST}     . "\t" . 
                  $row{AVGLABORCOST}    . "\t" . 
                  $row{AVGMATERIALCOST}     . "\t" . 
                  $row{AVGMTLBURCOST}    . "\t" . 
                  $row{AVGSUBCONTCOST}     . "\t" . 
                  $row{COSTID}     . "\t" . 
                  $row{LASTBURDENCOST}    . "\t" . 
                  $row{LASTLABORCOST}     . "\t" . 
                  $row{LASTMATERIALCOST}    . "\t" . 
                  $row{LASTMTLBURCOST}     . "\t" . 
                  $row{LASTSUBCONTCOST}    . "\t" .
                  $row{NUMBER01}    . "\t" .
                  $row{NUMBER02}    . "\t" .
                  $row{NUMBER03}    . "\t" .
                  $row{NUMBER04}    . "\t" .
                  $row{NUMBER05}    . "\t" .
                  $row{NUMBER06}    . "\t" .
                  $row{NUMBER07}    . "\t" .
                  $row{NUMBER08}    . "\t" .
                  $row{NUMBER09}    . "\t" .
                  $row{NUMBER10}    . "\t" .
                  $date01           . "\t" .
                  $date02           . "\t" .
                  $date03           . "\t" .
                  $date04           . "\t" .
                  $date05           . "\t" .
                  $row{CHECKBOX01}  . "\t" .
                  $row{CHECKBOX02}  . "\t" .
                  $row{CHECKBOX03}  . "\t" .
                  $row{CHECKBOX04}  . "\t" .
                  $row{CHECKBOX05}  . "\t" .
		  0  . "\t" . 0 . "\t" . 0 . "\n";
    }
    close OUT;
}

1;
