use strict;

use lib "../perlLib";
use Extract::OutToFile;
use Extract::Coinet::SalesDetail;
use Extract::Coinet::Customer;
use Extract::Coinet::InvcHead;
use Extract::Coinet::InvcDtl;
use Extract::Coinet::InvcMisc;
use Extract::Coinet::PartBin;
use Extract::Coinet::Part;
use Extract::Coinet::OrderDtl;
use Extract::Coinet::OrderHed;
use Extract::Coinet::ProdGrup;
use Extract::Coinet::CustGrup;
use Extract::Coinet::SalesCat;
use Extract::Coinet::ShipHead;
use Extract::Coinet::ShipDtl;
use Extract::Coinet::ShipTo;
use Extract::Coinet::PartWhse;
use Extract::Coinet::CustBillTo;
use Extract::Coinet::PORel;
use Extract::Coinet::ContainerHeader;
use Extract::Coinet::ContainerDetail;
use Extract::Coinet::PODetail;
use Extract::Coinet::POHeader;
use Extract::Coinet::InvcTax;
use Extract::Coinet::SalesTer;
use Extract::Coinet::SalesRep;
use Extract::Coinet::Region;
use Extract::Coinet::CustXPrt;
use Extract::Coinet::PartCost;
use Extract::Coinet::PLGrupBrk;
use Extract::Coinet::PLPartBrk;
use Extract::Coinet::RMADtl;
use Extract::Coinet::PartMtl;
use Extract::Coinet::Fiscal;
main();

sub main {
    Extract::Coinet::Customer->new();
    Extract::Coinet::CustGrup->new();
    Extract::Coinet::SalesCat->new();
    Extract::Coinet::SalesTer->new();
    Extract::Coinet::SalesRep->new();
    Extract::Coinet::Region->new();    
}
