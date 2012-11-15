use strict;

use lib "../perlLib";
use Extract::OutToFile;
use Extract::Coinet::RcvHead;
use Extract::Coinet::RcvDtl;


sub main {
    Extract::Coinet::RcvHead->new();
    Extract::Coinet::RcvDtl->new();
}


main();
