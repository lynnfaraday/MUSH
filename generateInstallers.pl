#!/usr/bin/perl
use File::Find;
use Cwd;
use File::stat;
use Time::localtime;
use strict;

if (@ARGV != 2)
{
die "Invalid number of arguments.";
}

my $oldVersion = @ARGV[0];
my $version = @ARGV[1];
my $patch = 0;
my $fileIndex = 0;
processAllFiles();

$patch = 1;
$fileIndex = 0;
processAllFiles();

close(OUTFILE);

sub processAllFiles {

if ($patch)
{
print OUTFILE "\@pemit %#=ansi(hc,Upgrading all Faraday systems.)";
}
else
{
print OUTFILE "\@pemit %#=ansi(hc,Installing all Faraday systems.)";
}

processFile("Core/installManager.dec", "-CORE-");
processFile("Core/help.dec", "-CORE-");
processFile("Core/functions.dec", "-CORE-");
processFile("Core/playerSetup.dec", "-CORE-");
processFile("Core/jobs.dec", "-CORE-");
processFile("Core/faramail.dec", "-CORE-");

find(\&forEachFile, cwd . "/Addons/");
find(\&forEachFile, cwd . "/FUDGE/");
find(\&forEachFile, cwd . "/FS3/");

processFile("FS3/FS3 Combat Post-Install.dec");

if (!$patch)
   {
   processFile("FS3/FS3 Chargen.dec");
   processFile("FUDGE/FUDGE Chargen.dec");
   }
}

sub forEachFile{
    my $fileName = $File::Find::name;
   
    if ($fileName !~ /\.dec$/) 
        { return; }
    if ($fileName =~ /FS3 Chargen/) 
        { return; }
    if ($fileName =~ /FUDGE Chargen/) 
        { return; }
    if ($fileName =~ /FS3 Combat Post-Install/) 
        { return; }
    
    processFile($fileName);    
}

sub processFile{

my ($fileName, $prefix) = @_;
print "Parsing $fileName\n";

$fileIndex++;

open(INFILE, "$fileName") or die "Can't open $fileName";
my @contents = <INFILE>;
close(INFILE);

my ($shortName) = ($fileName =~ /\/([^\/]+$)/);

my $outFileName;
my $indexString = sprintf("%0*d", 2, $fileIndex);

if ($patch)
{
  $outFileName = "Installers/Patch v$oldVersion to v$version/$indexString PATCH $prefix $shortName";
}
else
{
  $outFileName = "Installers/v$version/$indexString $prefix $shortName";
}

my $cwd = cwd;
if ($cwd !~ /trunk$/)
   {
   $outFileName = "../$outFileName";
   }

open(OUTFILE, ">$outFileName") or die "Can't open $outFileName $!";

my $inDataArea = 0;
my @output;
my $line;

while ($line = shift(@contents))
{
 if ($line =~ /END DATA SECTION/)
   {
   $inDataArea = 0;
   }
 elsif ($line =~ /DATA SECTION/)
   {
   $inDataArea = 1;
   }
  
 if ($inDataArea && $patch)
   {
   # skip this line for patches because it contains DATA 
   }
 elsif ($patch)
   {
   # replace instances of install_create with install_patch
   # so we don't try to halfway-install objects
   $line =~ s/install_create\(/install_patch\(/gi;
   push(@output, $line);
   }
 else
   {
   push(@output, $line);
   }
}

print OUTFILE @output;

close(OUTFILE);
}


