use strict;

use lib "../perlLib";
use Extract::OutToFile;
use Extract::Coinet::ShipToEx;
use Extract::Coinet::ShipTo;

main();

sub main {
    Extract::Coinet::ShipToEx->new();
    # Extract::Coinet::ShipTo->new();
}
