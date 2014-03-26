use strict;

use lib "../perlLib";
use Extract::OutToFile;
use Extract::Coinet::ShipHead;
use Extract::Coinet::ShipDtl;


sub main {
    Extract::Coinet::ShipHead->new();
    Extract::Coinet::ShipDtl->new();
}

main();
