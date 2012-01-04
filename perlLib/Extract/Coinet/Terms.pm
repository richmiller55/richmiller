package Extract::Coinet::Terms;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">i:/transfer/";
    my $file = "Terms.txt";
    return $dir . $file;
}

sub sql {
    my $self = shift;
   
    my $sql = qq /
      select
      t.Company as Company, -- char 8 
      t.Description  as Description,  -- char 30
      t.DiscountDays  as DiscountDays,  -- int
      t.DiscountPercent  as DiscountPercent,  -- decimal
      t.DueDays  as DueDays,   -- int
      t.Monthly  as Monthly,  -- smallint
      t.NumberOfPayments as NumberOfPayments, -- int
      t.TermsCode as TermsCode, -- char 4
     1 as filler
     FROM  pub.Terms as t
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
                  $row{COMPANY}      . "\t" . 
                  $row{DESCRIPTION}     . "\t" . 
                  $row{DISCOUNTDAYS}     . "\t" . 
                  $row{DISCOUNTPERCENT}     . "\t" . 
                  $row{DUEDAYS}     . "\t" . 
                  $row{MONTHLY}     . "\t" . 
                  $row{NUMBEROFPAYMENTS}     . "\t" . 
                  $row{TERMSCODE}     . "\t" . 
                  '1'                  .  "\n";
    }
    close OUT;
}

1;
