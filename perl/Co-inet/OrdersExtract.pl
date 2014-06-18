use strict;

use lib "../perlLib";
use Extract::OutToFile;
use Extract::Coinet::OrderDtl;
use Extract::Coinet::OrderHed;
use Extract::Coinet::OrderRel;
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
    Extract::Coinet::OrderRel->new();
#    Extract::Coinet::ProdGrup->new();
#    Extract::Coinet::CustGrup->new();
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
