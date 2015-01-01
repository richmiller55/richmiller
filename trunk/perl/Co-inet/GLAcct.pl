use strict;

use lib "../perlLib";
use Extract::OutToFile;
use Extract::Coinet::GLAcct;
main();

sub main {
    Extract::Coinet::GLAcct->new();            
}
