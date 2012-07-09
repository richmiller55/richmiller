package Extract::Coinet::Part;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">i:/transfer/";
    my $file = "Part.txt";
    return $dir . $file;
}

sub sql {
    my $self = shift;
   
    my $sql = qq /
      select
      p.Company as Company, -- char 8 
      p.partNum  as partNum,  -- char 50
      p.PartDescription as PartDescription, -- char 50
      p.ProdCode as ProdCode,   -- char 8
      substring(p.PartDescription,1,19) as basePart, -- char 20
      substring(p.PartDescription,21,3) as printType,  -- 3
      p.UnitPrice as unitPrice, -- 12,4
      p.ShortChar02 as loc,  -- varchar 50
      p.RunOut  as RunOut,   -- int
      p.SearchWord as SearchWord,  -- varchar 8
      p.TypeCode as TypeCode, -- char 1
      p.Number01 as CasePack, -- decimal 10,2
      p.ShortChar03 as flyer, -- x 50
      p.ShortChar04 as flyerNickname, -- x 50
      p.CheckBox01 as oneTimeBuy, -- smallint
      p.UserChar1 as aicDescr,     -- x30
      p.Inactive as Inactive,
      p.Number02 as minWOS,
      p.Number03 as maxWOS,
      p.Number04 as retailPrice,
      p.Number08 as listPrice,
      p.CheckBox02 as arCoating,        -- smallint
      p.CheckBox03 as rxAdaptable,      -- smallint
      p.CheckBox04 as springHinge,      -- smallint
      p.CheckBox05 as coordinatingCase, -- smallint
      p.Character01 as character01,
      p.Character02 as character02,
      p.Number05 as frameDim,
      p.Number06 as bridgeDim,
      p.Number07 as templeDim,
      p.Number09 as avgCost,
      p.Number10 as avgPOCost,
      p.Number11 as freightCost,
      p.Number12 as customsCost,
      p.Number13 as matlBurden,	
      p.Number14 as lastPOCost,	
      p.ShortChar05 as OrderingType,
      p.ShortChar06 as PrintOptions,
      p.ClassID     as ClassID,
      1  as filler
     FROM  pub.Part as p
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
                  $row{COMPANY}       . "\t" . 
                  $row{PARTNUM}       . "\t" . 
                  $row{PARTDESCRIPTION}     . "\t" . 
                  $row{PRODCODE}    . "\t" . 
                  $row{BASEPART}    . "\t" .
                  $row{PRINTTYPE}   . "\t" . 
                  $row{UNITPRICE}   . "\t" . 
                  $row{LOC}         . "\t" . 
                  $row{RUNOUT}      . "\t" . 
                  $row{SEARCHWORD}  . "\t" . 
                  $row{TYPECODE}    . "\t" . 
                  $row{CASEPACK}    . "\t" . 
                  $row{FLYER}       . "\t" . 
                  $row{FLYERNICKNAME}    . "\t" . 
                  $row{ONETIMEBUY}  . "\t" . 
                  $row{AICDESCR}    . "\t" . 
                  $row{INACTIVE}    . "\t" . 
                  $row{MINWOS}      . "\t" . 
                  $row{MAXWOS}      . "\t" . 
                  $row{RETAILPRICE} . "\t" . 
                  $row{LISTPRICE}   . "\t" . 
                  $row{ARCOATING}   . "\t" . 
                  $row{RXADAPTABLE} . "\t" . 
                  $row{SPRINGHINGE} . "\t" . 
                  $row{COORDINATINGCASE} . "\t" . 
                  $row{CHARACTER01} . "\t" . 
                  $row{CHARACTER02} . "\t" . 
                  $row{FRAMEDIM} . "\t" . 
                  $row{BRIDGEDIM} . "\t" . 
                  $row{TEMPLEDIM} . "\t" . 
                  $row{AVGCOST} . "\t" . 
                  $row{AVGPOCOST} . "\t" . 
                  $row{FREIGHTCOST} . "\t" . 
                  $row{CUSTOMSCOST} . "\t" . 
                  $row{MATLBURDEN} . "\t" . 
                  $row{LASTPOCOST} . "\t" . 
                  $row{ORDERINGTYPE} . "\t" . 
                  $row{PRINTOPTIONS} . "\t" . 
                  $row{CLASSID}      . "\t" . 
	           0    . "\n";

    }
    close OUT;
}

1;

