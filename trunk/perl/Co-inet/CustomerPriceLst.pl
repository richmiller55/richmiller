use strict;

use lib "../perlLib";
use Extract::OutToFile;
use Extract::Coinet::CustomerPriceLst;
main();

sub main {
    Extract::Coinet::CustomerPriceLst->new();
}
