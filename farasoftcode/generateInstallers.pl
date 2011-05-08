#!/usr/bin/perl

# Usage:  generateInstallers.pl <old version#> <new version #> <wipe?>
#  Directories must already exist in the Installer directory - one for upgrade and one for fresh install
#  Run from the root softcode dir.

use File::Find;
use File::Copy;
use File::Path;
use Cwd;
use File::stat;
use Time::localtime;
use strict;

if (@ARGV < 2)
{
die "Invalid number of arguments.";
}

my $_oldVersion = @ARGV[0];
my $_newVersion = @ARGV[1];
my $_wipe = @ARGV[2];
my $_patch = 0;
my $_fileIndex = 0;
my $_upgradeDir = "Installers/Upgrade v$_oldVersion to v$_newVersion";
my $_freshDir = "Installers/Fresh Install v$_newVersion";

if ($_wipe)
{
print "***Removing old installers.\n";
rmtree($_upgradeDir);
rmtree($_freshDir);

mkdir($_upgradeDir);
mkdir($_freshDir);
}

copy("Installers/_LICENSE", $_freshDir);
copy("Installers/_LICENSE", $_upgradeDir);
copy("Installers/_CHANGELOG", $_freshDir);
copy("Installers/_CHANGELOG", $_upgradeDir);

processAllFiles();

$_patch = 1;
$_fileIndex = 0;
processAllFiles();

close(OUTFILE);

my $freshZip = "Releases/FaraMUSHCode-v$_newVersion.zip";
my $upgradeZip = "Releases/FaraMUSHCode Upgrade v$_oldVersion to v$_newVersion.zip";

unlink $freshZip;
unlink $upgradeZip;

print "*** Creating ZIP files\n";

system("zip -jr \"$freshZip\" \"$_freshDir\"");
system("zip -jr \"$upgradeZip\" \"$_upgradeDir\"");

sub processAllFiles {

if ($_patch)
{
print "***Creating patch files\n";
}
else
{
print "***Creating fresh install files\n";
}

processFile("Core/installManager.dec", "-CORE-");
processFile("Core/help.dec", "-CORE-");
processFile("Core/functions.dec", "-CORE-");
processFile("Core/playerSetup.dec", "-CORE-");
processFile("Core/jobs.dec", "-CORE-");
processFile("Core/faramail.dec", "-CORE-");
processFile("Core/BBS-Myrddin.dec", "-CORE-");
processFile("Core/CRON-Myrddin.dec", "-CORE-");
processFile("Core/hookManager.dec", "-CORE-");
processFile("Core/IC Time.dec", "-CORE-");

find(\&forEachFile, cwd . "/AddOns/");
find(\&forEachFile, cwd . "/FS3/");

processFile("FS3/FS3 Combat Post-Install.dec");

if (!$_patch)
   {
   processFile("FS3/FS3 Chargen.dec");
   }

find(\&copyDocs, cwd . "/Docs/FS3.2/");
}

sub forEachFile{
    my $fileName = $File::Find::name;
   
    if ($fileName !~ /\.dec$/) 
        { return; }
    if ($fileName =~ /FS3 Chargen/) 
        { return; }
    if ($fileName =~ /FS3 Combat Post-Install/) 
        { return; }
    
    processFile($fileName);    
}

sub getOutFileName
{
    my ($fileName, $prefix) = @_;
    my $outFileName;

    my ($shortName) = ($fileName =~ /\/([^\/]+$)/);
    if ($_patch)
    {
	$outFileName = "$_upgradeDir/$prefix$shortName";
    }
    else
    {
	$outFileName = "$_freshDir/$prefix$shortName";
    }
    
# When parsing sub-dirs the script has the CWD as the sub-dir, so we have to go up one to get
# the installers directory.
    my $cwd = cwd;
    if ($cwd !~ /farasoftcode$/)
    {
	$outFileName = "../$outFileName";
    }
    return $outFileName;
}

sub copyDocs {
    my $fileName = $File::Find::name;
    my $outFileName = getOutFileName($fileName);
    if ($fileName =~ /\.(pdf|txt)$/) 
    {
	copy($fileName, "../$outFileName");
	print "Copying document $fileName\n";
    }
}

sub processFile{

my ($fileName, $prefix) = @_;
print "Parsing $fileName\n";

$_fileIndex++;

open(INFILE, "$fileName") or die "Can't open $fileName";
my @contents = <INFILE>;
close(INFILE);

my $indexString = sprintf("%0*d", 2, $_fileIndex);
my $filePrefix;

if ($_patch)
{
    $filePrefix = "$indexString PATCH $prefix ";
}
else
{
    $filePrefix = "$indexString $prefix ";
}

my $outFileName = getOutFileName($fileName, $filePrefix);

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
  
 if ($inDataArea && $_patch)
   {
   # skip this line for patches because it contains DATA 
   }
 elsif ($_patch)
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


