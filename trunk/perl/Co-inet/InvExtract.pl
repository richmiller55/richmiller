use strict;

use lib "../perlLib";
use Extract::OutToFile;
use Extract::Coinet::SalesDetail;
use Extract::Coinet::Customer;
use Extract::Coinet::InvcHead;
use Extract::Coinet::InvcHeadEx;
use Extract::Coinet::InvcDtl;
use Extract::Coinet::InvcMisc;
use Extract::Coinet::PartBin;
use Extract::Coinet::Part;
use Extract::Coinet::OrderDtl;
use Extract::Coinet::OrderHed;
use Extract::Coinet::OrderRel;
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
use Extract::Coinet::ShipVia;
use Extract::Coinet::Terms;
main();

sub main {
#    Extract::Coinet::SalesDetail->new();
#    Extract::Coinet::Customer->new();
#    Extract::Coinet::InvcHead->new();
#    Extract::Coinet::InvcHeadEx->new();
#    Extract::Coinet::InvcDtl->new();
#    Extract::Coinet::InvcMisc->new();
#    Extract::Coinet::PartBin->new();
#    Extract::Coinet::Part->new();

#    Extract::Coinet::OrderHed->new();
#    Extract::Coinet::OrderDtl->new();
#    Extract::Coinet::OrderRel->new();
#    Extract::Coinet::ProdGrup->new();
    Extract::Coinet::CustGrup->new();
#    Extract::Coinet::SalesCat->new();
#    Extract::Coinet::ShipHead->new();
#    Extract::Coinet::ShipDtl->new();
#    Extract::Coinet::PartWhse->new();
#    Extract::Coinet::CustBillTo->new();
#    Extract::Coinet::PORel->new();
#    Extract::Coinet::ContainerHeader->new();
#    Extract::Coinet::ContainerDetail->new();
#    Extract::Coinet::PODetail->new();
#    Extract::Coinet::POHeader->new();
#    Extract::Coinet::ShipTo->new();
#    Extract::Coinet::InvcTax->new();
#    Extract::Coinet::SalesTer->new();
#    Extract::Coinet::SalesRep->new();
#    Extract::Coinet::Region->new();    
#    Extract::Coinet::CustXPrt->new();            
#    Extract::Coinet::PartCost->new();
#    Extract::Coinet::PLGrupBrk->new();
#    Extract::Coinet::PLPartBrk->new();
#    Extract::Coinet::RMADtl->new();
#    Extract::Coinet::PartMtl->new();
#    Extract::Coinet::Fiscal->new();            
#    Extract::Coinet::ShipVia->new();            
#    Extract::Coinet::Terms->new();            
}
