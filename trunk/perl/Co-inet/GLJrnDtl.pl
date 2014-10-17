use strict;

use lib "../perlLib";
use Extract::OutToFile;
use Extract::Coinet::GLJrnDtl;
main();

sub main {
    Extract::Coinet::GLJrnDtl->new();            
}
