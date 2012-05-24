#!/usr/bin/perl
use strict;
use POSIX;
use File::Copy;

my $line;
my $lastLineBlank;
my $omitLine;
my $configFileName = @ARGV[0];
my $inFileName = @ARGV[1];
my $outFileName = @ARGV[2];
my @createTime;
my $timeString;
my @fileStats;
my @omitMatches;
my $omitMatch;

if (@ARGV < 2 || @ARGV > 3)
   {
   print "Usage:  faralogedit.pl <configfile> <raw file name> <edited name>\n";
   print "\n\n";
   print "- Edited name will automatically receive a .txt extension and\n";
   print "  a prefix based on the current date/time.\n";
   print "- Must use double quotes around multi-word filenames.\n";
   print "- See sample config file for details of what to put in it.\n";
   exit;
   }

open(IN_FILE, "$inFileName") or die "ERROR: Cannot open $inFileName\n";

open(CONFIG_FILE, "$configFileName") or die "ERROR: Cannot open $configFileName\n";

@fileStats = stat(IN_FILE);
@createTime = localtime(@fileStats[10]);

# In the filename we only care about the date.  Prepend that
# to the log name.

$timeString = strftime("%Y-%m-%d", @createTime);

my @lines = <IN_FILE>;
close(IN_FILE);

foreach (@lines)
{
  if (/\((\d\d)\/(\d\d)\/(\d\d)\)/)
    {
     $timeString = "20$3-$1-$2";
     print "\n***Alternate date found!: $timeString\n";
    }
  if (/\((\d\d)\/(\d\d)\/(\d\d\d\d)\)/)
    {
     $timeString = "$3-$1-$2";
     print "\n***Alternate date found!: $timeString\n";
    }
}

if (!defined($outFileName))
{
    $outFileName = $inFileName;
}
$outFileName = "$timeString -- $outFileName";

# If I forgot the .txt extension, add it.

if ($outFileName !~ /\.txt/)
   {
   $outFileName = $outFileName . ".txt";
   }   

if ($outFileName eq $inFileName)
   {
    die "ERROR: Output file can't be the same as input file.\n";
   }

open(OUT_FILE, ">$outFileName") or die "ERROR: Cannot open $outFileName\n";
print "Parsing $inFileName to $outFileName.\n";

# Parse the config file to figure out what we need to omit from the log.

while ($line = <CONFIG_FILE>)
   {

     # Trim \n's from our lines because they're not part of the match.
   
     chomp($line);

     # Ignore comments.

     if ($line =~ /^\#/)
     {
     }

     # Ignore blank lines.

     elsif ($line !~ /[\S]/)
     {
     }

     else
     {
     push(@omitMatches, "$line");
     }
   }
      

# In the log itself we want to know date and time

$timeString = strftime("%Y-%m-%d %H:%M", @createTime);
print OUT_FILE "File created: $timeString\n";

$lastLineBlank = 0;

foreach $line (@lines)
   {

   $omitLine = 0;

   # Check each omission (from the config file), and if 
   # the line matches, skip it.

   foreach $omitMatch (@omitMatches)
   {
   if ($line =~ /$omitMatch/)
      {
      $omitLine = 1;
      }
   }

   if (!$omitLine)
      {

      # Don't print an extra blank line for a blank line.
      
      if ($line !~ /[\S]/)
         {

         # And don't print another blank line if the last one was blank

         if (!$lastLineBlank)
            {
            print OUT_FILE "$line";
	    $lastLineBlank = 1;
            }

         }

      # Also don't put an extra blank line for two in a row.

      elsif ($lastLineBlank)
         {	 
         print OUT_FILE "$line";
          $lastLineBlank = 0;
	  }
      else
         {
         print OUT_FILE "\n$line";
	 $lastLineBlank = 0;
         }
      }
   }

close OUT_FILE;
close IN_FILE;
close CONFIG_FILE;

copy($inFileName, "Originals/" . $inFileName);
unlink($inFileName);

print "Parsing complete.\n"
