use strict;

use lib "../perlLib";
use Extract::OutToFile;
use Extract::Coinet::ContainerHeader;
use Extract::Coinet::ContainerDetail;
use Extract::Coinet::PODetail;
use Extract::Coinet::POHeader;
main();

sub main {
#    Extract::Coinet::PORel->new();
#    Extract::Coinet::ContainerHeader->new();
    Extract::Coinet::ContainerDetail->new();
#    Extract::Coinet::PODetail->new();
#    Extract::Coinet::POHeader->new();
}
