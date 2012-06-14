package Extract::Coinet::Customer;

@ISA = qw ( Extract::OutToFile );

use Win32::ODBC;
use strict;

sub getFileNameOut {
    my $self = shift;
    my $dir = ">i:/transfer/";
    my $file = "Customer.txt";
    return $dir . $file;
}

sub sql {
    my $self = shift;
   
    my $sql = qq /
      select
      cm.CustID as CustID,
      cm.CustNum as CustNum,
      cm.Name    as Name,
      cm.Address1  as Address1,
      cm.Address2  as Address2,
      cm.Address3  as Address3,
      cm.City    as City,
      cm.State   as State,
      cm.Zip     as Zip,
      cm.Country as Country,
      cm.TerritoryID as TerritoryID,
      cm.SalesRepCode as SalesRepCode,
      cm.TermsCode as TermsCode,
      cm.ShipViaCode as ShipViaCode,
      cm.GroupCode as GroupCode,
      cg.SalesCatID as channel,
      cm.ResaleID as ResaleID,
      cm.PrintStatements as PrintStatements,
      cm.TaxExempt as TaxExempt,
      cm.CreditHold as CreditHold,
      cm.TaxRegionCode as TaxRegionCode,
 
      cm.CreditLimit as CreditLimit,         -- decimal

      cm.CreditHoldDate as CreditHoldDate,      -- int
      cm.CreditHoldSource as CreditHoldSource,  -- x10
      cm.CreditIncludeOrders as CreditIncludeOrders,  -- smallint

      cm.GlobalCust as GlobalCust,    -- smallint
      cm.GlobalCreditHold  as GlobalCreditHold, -- x12
      cm.GlobalCredIncOrd as GlobalCredIncOrd,      -- smallint
      cm.PhoneNum as PhoneNum,
      cm.CheckBox01 as optiPortMemeber,
      cm.CheckBox02 as visionSourceMemeber,
      cm.CheckBox03 as inactiveFlag,
      cm.ChangedBy as changedBy,
      cm.ChangeDate as changeDate,
      cm.EstDate  as EstDate,
      cm.CreditClearUserID as CreditClearUserID,
      cm.BTName        as  BTName,     
      cm.BTAddress1    as  BTAddress1, 	
      cm.BTAddress2    as  BTAddress2, 	
      cm.BTAddress3    as  BTAddress3, 	
      cm.BTCity	       as  BTCity,     		
      cm.BTCountry     as  BTCountry,  
      cm.BTCountryNum  as  BTCountryNum,
      cm.BTFaxNum      as  BTFaxNum,   	
      cm.BTFormatStr   as  BTFormatStr,
      cm.BTPhoneNum    as  BTPhoneNum, 	
      cm.BTState       as  BTState,    
      cm.BTZip 	       as  BTZip       
     FROM  pub.customer as cm
        left join pub.CustGrup as cg
        on cg.Company = cm.Company and
           cg.GroupCode = cm.GroupCode
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
	my $CreditHoldDate = $row{CREDITHOLDDATE};
	$CreditHoldDate =~ s/-//g;
	my $EstDate = $row{ESTDATE};
	$EstDate =~ s/-//g;
	print OUT  $i . "\t" .
                  $row{CUSTID}      . "\t" . 
                  $row{CUSTNUM}     . "\t" . 
                  $row{NAME}        . "\t" . 
                  $row{ADDRESS1}    . "\t" .
                  $row{ADDRESS2}    . "\t" .
                  $row{ADDRESS3}    . "\t" . 
                  $row{CITY}        . "\t" . 
                  $row{STATE}       . "\t" . 
                  $row{ZIP}         . "\t" . 
                  $row{COUNTRY}     . "\t" . 
                  $row{TERRITORYID}          . "\t" . 
                  $row{SALESREPCODE}         . "\t" . 
                  $row{TERMSCODE}            . "\t" . 
                  $row{SHIPVIACODE}          . "\t" . 
                  $row{GROUPCODE}            . "\t" . 
                  $row{CHANNEL}              . "\t" . 
                  $row{RESALEID}             . "\t" .
                  $row{PRINTSTATEMENTS}      . "\t" . 
                  $row{TAXEXEMPT}            . "\t" . 
                  $row{CREDITHOLD}           . "\t" . 
                  $row{TAXREGIONCODE}        . "\t" . 
                  $row{CREDITLIMIT}          . "\t" . 
                  $CreditHoldDate            . "\t" . 
                  $row{CREDITHOLDSOURCE}     . "\t" . 
                  $row{CREDITINCLUDEORDERS}  . "\t" . 
                  $row{GLOBALCUST}           . "\t" . 
                  $row{GLOBALCREDITHOLD}     . "\t" . 
                  $row{GLOBALCREDINCORD}     . "\t" .
                  $row{PHONENUM}             . "\t" .
                  $row{OPTIPORTMEMEBER}      . "\t" .
                  $row{VISIONSOURCEMEMEBER}  . "\t" .
                  $row{CHANGEDBY}  . "\t" .
                  $row{CHANGEDATE}  . "\t" .
		  $EstDate   . "\t" .
                  $row{CREDITCLEARUSERID} . "\t" .
                  $row{INACTIVEFLAG} . "\t" .
                  $row{BTNAME} . "\t" .
                  $row{BTADDRESS1} . "\t" .
                  $row{BTADDRESS2} . "\t" .
                  $row{BTADDRESS3} . "\t" .
                  $row{BTCITY} . "\t" .
                  $row{BTCOUNTRY} . "\t" .
                  $row{BTCOUNTRYNUM} . "\t" .
                  $row{BTFAXNUM} . "\t" .
                  $row{BTFORMATSTR} . "\t" .
                  $row{BTPHONENUM} . "\t" .
                  $row{BTSTATE} . "\t" .
                  $row{BTZIP} . "\t" .
		  0 . "\n";
    }
    close OUT;
}

1;

