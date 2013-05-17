use strict;

use lib "../perlLib";
use Extract::OutToFile;
use Extract::Coinet::JobHead;
use Extract::Coinet::JobOpDtl;

sub main {
    Extract::Coinet::JobHead->new();
    Extract::Coinet::JobOpDtl->new();
}


main();
