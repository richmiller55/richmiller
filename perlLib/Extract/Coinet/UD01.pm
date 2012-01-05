package Extract::Coinet::UD01;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">i:/transfer/";
    my $file = "UD01.txt";
    return $dir . $file;
}

sub sql {
    my $self = shift;
   
    my $sql = qq /
      select
      t.Company as Company, 
      t.Key1 as PartNum,
      t.Key2 as Key2,
      t.Key3 as Key3,
      t.Key4 as Key4,
      t.Key5 as Key5,
      t.ShortChar01 as PartDescr,
      t.Number01 as Cost,
      t.Number02 as AvgPoCost,
      t.Number03 as Freight,
      t.Number04 as Duty,
      t.Number05 as Burden,
      t.Number06 as Overhead,
      t.Number07 as PrintExpense,
      t.Number08 as LastPOCost,
      t.Number09 as Number09,
      t.Number10 as Number10,
      t.Number11 as Number11,
      t.Number12 as Number12,
      t.Number13 as Number13,
      t.Number14 as Number14,
      t.Number15 as Number15,
      t.Number16 as Number16,
      t.Number17 as Number17,
      t.Number18 as Number18,
      t.Number19 as Number19,
      t.Number20 as Number20,
      t.CheckBox01 as CheckBox01,
      t.CheckBox02 as CheckBox02,
      t.CheckBox03 as CheckBox03,
      t.CheckBox04 as CheckBox04,
      t.CheckBox05 as CheckBox05,
      t.CheckBox06 as CheckBox06,
      t.CheckBox07 as CheckBox07,
      t.CheckBox08 as CheckBox08,
      t.CheckBox09 as CheckBox09,
      t.CheckBox10 as CheckBox10,
      t.CheckBox11 as CheckBox11,
      t.CheckBox12 as CheckBox12,
      t.CheckBox13 as CheckBox13,
      t.CheckBox14 as CheckBox14,
      t.CheckBox15 as CheckBox15,
      t.CheckBox16 as CheckBox16,
      t.CheckBox17 as CheckBox17,
      t.CheckBox18 as CheckBox18,
      t.CheckBox19 as CheckBox19,
      t.CheckBox20 as CheckBox20,
      t.ShortChar02 as ShortChar02,
      t.ShortChar03 as ShortChar03,
      t.ShortChar04 as ShortChar04,
      t.ShortChar05 as ShortChar05,
      t.ShortChar06 as ShortChar06,
      t.ShortChar07 as ShortChar07,
      t.ShortChar08 as ShortChar08,
      t.ShortChar09 as ShortChar09,
      t.ShortChar10 as ShortChar10,
      t.ShortChar11 as ShortChar11,
      t.ShortChar12 as ShortChar12,
      t.ShortChar13 as ShortChar13,
      t.ShortChar14 as ShortChar14,
      t.ShortChar15 as ShortChar15,
      t.ShortChar16 as ShortChar16,
      t.ShortChar17 as ShortChar17,
      t.ShortChar18 as ShortChar18,
      t.ShortChar19 as ShortChar19,
      t.ShortChar20 as ShortChar20,
      t.Character01 as	PoLog,
      t.Character02 as	BomLog,
      t.Character03 as	FrtLog,
      t.Character04 as	PartInfoLog,
     1 as filler
     FROM  pub.UD01 as t
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
	    $row{COMPANY} . "\t" .
	    $row{PARTNUM} . "\t" .
	    $row{KEY2} . "\t" .
	    $row{KEY3} . "\t" .
	    $row{KEY4} . "\t" .
	    $row{KEY5} . "\t" .
	    $row{PARTDESCR} . "\t" .
	    $row{COST} . "\t" .
	    $row{AVGPOCOST} . "\t" .
	    $row{FREIGHT} . "\t" .
	    $row{DUTY} . "\t" .
	    $row{BURDEN} . "\t" .
	    $row{OVERHEAD} . "\t" .
	    $row{PRINTEXPENSE} . "\t" .
	    $row{LASTPOCOST} . "\t" .
	    $row{NUMBER09} . "\t" .
	    $row{NUMBER10} . "\t" .
	    $row{NUMBER11} . "\t" .
	    $row{NUMBER12} . "\t" .
	    $row{NUMBER13} . "\t" .
	    $row{NUMBER14} . "\t" .
	    $row{NUMBER15} . "\t" .
	    $row{NUMBER16} . "\t" .
	    $row{NUMBER17} . "\t" .
	    $row{NUMBER18} . "\t" .
	    $row{NUMBER19} . "\t" .
	    $row{NUMBER20} . "\t" .
	    $row{CHECKBOX01} . "\t" .
	    $row{CHECKBOX02} . "\t" .
	    $row{CHECKBOX03} . "\t" .
	    $row{CHECKBOX04} . "\t" .
	    $row{CHECKBOX05} . "\t" .
	    $row{CHECKBOX06} . "\t" .
	    $row{CHECKBOX07} . "\t" .
	    $row{CHECKBOX08} . "\t" .
	    $row{CHECKBOX09} . "\t" .
	    $row{CHECKBOX10} . "\t" .
	    $row{CHECKBOX11} . "\t" .
	    $row{CHECKBOX12} . "\t" .
	    $row{CHECKBOX13} . "\t" .
	    $row{CHECKBOX14} . "\t" .
	    $row{CHECKBOX15} . "\t" .
	    $row{CHECKBOX16} . "\t" .
	    $row{CHECKBOX17} . "\t" .
	    $row{CHECKBOX18} . "\t" .
	    $row{CHECKBOX19} . "\t" .
	    $row{CHECKBOX20} . "\t" .
	    $row{SHORTCHAR02} . "\t" .
	    $row{SHORTCHAR03} . "\t" .
	    $row{SHORTCHAR04} . "\t" .
	    $row{SHORTCHAR05} . "\t" .
	    $row{SHORTCHAR06} . "\t" .
	    $row{SHORTCHAR07} . "\t" .
	    $row{SHORTCHAR08} . "\t" .
	    $row{SHORTCHAR09} . "\t" .
	    $row{SHORTCHAR10} . "\t" .
	    $row{SHORTCHAR11} . "\t" .
	    $row{SHORTCHAR12} . "\t" .
	    $row{SHORTCHAR13} . "\t" .
	    $row{SHORTCHAR14} . "\t" .
	    $row{SHORTCHAR15} . "\t" .
	    $row{SHORTCHAR16} . "\t" .
	    $row{SHORTCHAR17} . "\t" .
	    $row{SHORTCHAR18} . "\t" .
	    $row{SHORTCHAR19} . "\t" .
	    $row{SHORTCHAR20} . "\t" .
	    $row{POLOG} . "\t" .
	    $row{BOMLOG} . "\t" .
	    $row{FRTLOG} . "\t" .
	    $row{PARTINFOLOG}  . "\t" .
           '1'                  .  "\n";
    }
    close OUT;
}

1;
