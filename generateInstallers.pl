#!/usr/bin/perl
use File::Find;
use Cwd;
use File::stat;
use Time::localtime;

if (@ARGV != 2)
{
die "Invalid number of arguments.";
}

my $oldVersion = @ARGV[0];
my $version = @ARGV[1];
my $patch = 0;
my $fileIndex = 0;
open(OUTFILE, ">FaraMUSH v$version.dec")  or die "Can't open install file.";
processAllFiles();
close(OUTFILE);

$patch = 1;
open(OUTFILE, ">FaraMUSH PATCH v$oldVersion to v$version.dec")  or die "Can't open install file.";
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

appendFile("Core/installManager.dec");
appendFile("Core/help.dec");
appendFile("Core/functions.dec");
appendFile("Core/playerSetup.dec");
appendFile("Core/jobs.dec");
appendFile("Core/faramail.dec");

find(\&forEachFile, cwd . "/Addons/");
find(\&forEachFile, cwd . "/FUDGE/");
find(\&forEachFile, cwd . "/FS3/");

appendFile("FS3/FS3 Combat Post-Install.dec");

}

sub forEachFile{
    my $fileName = $File::Find::name;
   
    if ($fileName !~ /\.dec/) 
        { return; }
    if ($fileName =~ /\.svn/) 
        { return; }
    if (($fileName =~ /FS3 Chargen/) && $patch)
        { return; }
    if (($fileName =~ /FUDGE Chargen/) && $patch)
        { return; }
    if ($fileName =~ /FS3 Combat Post-Install/) 
        { return; }
    
    appendFile($fileName);    
}

sub appendFile{

my $fileName = shift;
print "Parsing $fileName\n";

open(INFILE, "$fileName") or die "Can't open $fileName";
@contents = <INFILE>;
print OUTFILE "\n\n\n\n";
($shortName) = ($fileName =~ /\/([^\/]+$)/);

print OUTFILE "@@ ================= SYSTEM: $shortName ==========================";
print OUTFILE "\n\n";

my $inDataArea = 0;
my @output;

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
  
 if ($line =~ /Installation Complete!/)
   {
   # Skip all lines saying the install is complete.
   }
 elsif ($line =~ /\@set me=!QUIET/i)
   {
   # skip all the !quiet lines when doing the mass update.
   }
 elsif ($inDataArea && $patch)
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

if ($patch)
{
  my $patchFileName = "Patches/" . $shortName;
  my $cwd = cwd;
  if ($cwd !~ /trunk$/)
     {
     $patchFileName = "../$patchFileName";
     }
  $patchFileName =~ s/\.dec/ v$oldVersion to v$version\.patch/;
  open(PATCHFILE, ">$patchFileName") or die "Can't open patch file. $!";
  print PATCHFILE @output;
  close(PATCHFILE);
}

close(INFILE);
}


