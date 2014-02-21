use strict;

use lib "../perlLib";
use Extract::OutToFile;
use Extract::Coinet::ShipToEx;

main();

sub main {
    Extract::Coinet::ShipToEx->new();
}
