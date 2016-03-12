# 7th Sea Die Roller
# by Faraday (faraday@aresmush.com)
#
# Based on the mechanics described in the [7th Sea Quickstart Rules](https://www.kickstarter.com/projects/johnwickpresents/7th-sea-second-edition) by John Wick.
#
# Usage:  ruby 7sea.rb <num dice> <explode 10s for rank 5? true or false>  <double raises for rank 4? true or false>
#
# Note:  This program uses 'pair' to refer to dice PAIRS that combine to a raise (like 8+2 making 10 or 6+9 making 15) 
#        and 'junk' to refer to any random set of dice that add up to a raise (like 7+1+1+1)

# Rolls the specified number of 10-sided dice.  
#
# Returns an array of the die results.
#
# dice = number of dice to roll
# explode = true or false to allow 10's to explode
def rollDice(dice, explode)
   return [] if dice == 0
   roll = dice.times.map { |d| rand(10) + 1 }
   puts "Rolled #{roll.inspect}"
   if (explode)
     rerollTens(roll)
   end
   return roll
end

# Finds any 10's in the original die roll and allows them to 'explode'
#
# Returns a modified array containing the original die results AND any exploded dice
#
# roll = array containing the original die results; rerolled dice will be appended
# Note: a 10 on a reroll will explode again
def rerollTens(roll)
   tens = roll.select { |d| d == 10 }
   numTens = tens.count

   return roll if numTens == 0

   puts "Exploding #{tens.count} dice"
   explode = rollDice(tens.count, true)
   roll.concat explode
   return roll
end

# Counts pairs of the specified numbers that add up to a raise and removes them from the die list.   
#
# Returns the number of raises.
#
# roll = array containing original die results; after calling, any dice that combine will be removed
# first = the first number of the pair
# second = the second number of the pair
#
# Example:  Given a roll of [6, 6, 4, 4, 4, 3] and first/second of 6 and 4, it will return 2 raises
# and 'roll' will be the unmatched numbers [4, 3]
def countPairs(roll, first, second)
  raises = 0

  firsts = roll.select { |d| d == first }
  seconds = roll.select { |d| d == second }
  roll.delete_if { |d| d == first }  
  roll.delete_if { |d| d == second }

  firsts.each do |n|
    if (seconds.count > 0)
      firsts.pop
      seconds.pop

      puts "Found a pair! #{first}+#{second}"
      raises += 1
    end
  end
  
  # Put any unmatched numbers back in our roll list.
  roll.concat firsts
  roll.concat seconds
  return raises
end
   
# Stupidly complicated method to match up all possible pairs of dice that add up to the 
# specified target number
#
# Returns the number of raises found.
#
# roll = Original roll results; after calling, any matched pairs will be removed
# target = The target number for a raise (10 or 15 -- if 15, raises count as double)
#
# Example: Given a roll of [10, 8, 6, 2, 1] and a target of 10, it would return 2 raises
#   (10 and 8+2) and the 'roll' array would contain the unmatched [6, 1]
def findRaisesInPairs(roll, target)
  raises = 0
   
  if (target == 10)
     # Count tens
     tens = roll.select { |d| d == 10 }
     raises += tens.count
     roll.delete_if { |d| d == 10 }
     puts "#{tens.count} tens"

    raises += countPairs(roll, 9, 1)
    raises += countPairs(roll, 8, 2)
    raises += countPairs(roll, 7, 3)
    raises += countPairs(roll, 6, 4)
  elsif (target == 15)
    # Count fifteens; they count as double raises
    raises += (countPairs(roll, 10, 5) * 2)
    raises += (countPairs(roll, 9, 6) * 2)
    raises += (countPairs(roll, 8, 7) * 2)
  else
    raise "Invalid target specified - use 10 or 15."
  end
    
  # Count groups of either 2 or 3 fives.  countPairs doesn't work if the first and second numbers are the same :/
  divisor = target == 10 ? 2 : 3
  multiplier = target == 10 ? 1 : 2

  fives = roll.select { |d| d == 5 }
  roll.delete_if { |d| d == 5 }
  raises += ((fives.count / divisor) * multiplier)
  puts "#{fives.count / divisor} groups of fives"
  (fives.count % divisor).times do |f|
     roll << 5
  end

   return raises
end

# Another stupidly complicated method to sum up dice trying to find the target number for a raise.
# Tracks the dice that were used AND the sum of those dice.
#
# Returns the sum, even if no raise happened.
# 
# junk = dice that didn't match into pairs; any dice that added up to a raise will be removed when it returns.
# target = target number for a raise
#
# Example:  Given junk dice of [6, 5, 3, 1, 1] it will return a sum of 10 (for the 6+1+3) and 'junk' will be the
#   unmatched [5, 1] afterward.
def countJunk(junk, target)
 sum = junk.shift
 diceUsed = [ sum ]
 return 0 if !sum
 while (sum < target && junk.count > 0)
   die = junk.pop
   sum += die
   diceUsed << die
 end

 diceUsed = diceUsed.sort.reverse
 
 if (sum < target)
   junk.concat diceUsed
 else 
   sum = minimizeJunk(junk, diceUsed, sum, target)
 end

 puts "Adding up junk: #{diceUsed.inspect} to #{sum}"
 return sum
end

# Sometimes you end up adding too many dice.  This will see if that's true and remove the extra
# ones.
#
# junk = dice that didn't match into pairs; any extra dice will be put back into this list when it returns
# diceUsed = dice that made up a raise
# sum = what the diceUsed added up to
# target = target number for a raise
#
# Example:  Because we add the little numbers first, the algorithm could have come up with [6, 3, 1, 1] 
# but only needed [6, 3, 1].  We need to put the extra '1' back.
def minimizeJunk(junk, diceUsed, sum, target)
  while (true)
    die = diceUsed.pop
    return sum if die == nil
    sum = sum - die
    if (sum >= target)
      # Die was unnecessary
      junk << die
    else
      # Die was needed after all
      diceUsed << die
      sum += die
      #puts "Adjusted junk to #{junk.inspect} -- #{diceUsed.inspect} -- #{sum}"
      return sum
    end
  end
end

# Loops through all the junk dice (the ones that didn't match into pairs) and makes as many raises as possible.
#
# Returns the number of raises found.
#
# junk = dice that didn't match into pairs; any dice that added up to a raise will be removed when it returns.
# target = target number for a raise
def findRaisesInJunk(junk, target)
  raises = 0
  while true
    y = countJunk(junk, target)
    if (y < target)
      # If the sum of remaining dice was less than our raise target, we've found all possible raises
      break
    else
       if (target == 15)
         puts "Double Raise!"
         raises += 2
       else
         puts "Raise!"
         raises += 1
       end
    end
  end
  return raises
end

def printTitle(title)
   puts "\n============================="
   puts title
   puts "============================="
end

## Parse the command line arguments

if (ARGV.count != 3)
  raise  "Usage:  ruby 7sea.rb <num dice> <explode 10s? true or false>  <double raises? true or false>"
end

if (ARGV[1] != 'true' && ARGV[1] != 'false')
  raise "explode must be true or false"
end

if (ARGV[2] != 'true' && ARGV[2] != 'false')
  raise "double raises must be true or false"
end

if ARGV[0].to_i == 0
  raise "dice must be an integer"
end

dice = ARGV[0].to_i
explode = ARGV[1] == 'true'
double_raises = ARGV[2] == 'true'
raises = 0

printTitle "Rolling Dice"

roll = rollDice(dice, explode)
puts "Final roll: #{roll.sort.inspect}"

# If you're allowing double raises, we have to find all pairs and junk adding up to 15 first,
# then do the same for pairs and junk adding up to 10.
if (double_raises)

   printTitle "Counting Pairs Making 15"

   raises += findRaisesInPairs(roll, 15)
   puts "#{raises} raises so far"

   printTitle "Counting Junk Making 15"

   roll = roll.sort.reverse
   puts "Junk dice: #{roll.inspect}"

   raises += findRaisesInJunk(roll, 15)
   puts "#{raises} raises so far"
end

printTitle "Counting Pairs Making 10"

puts "Left over: #{roll.inspect}"

raises += findRaisesInPairs(roll, 10)
puts "#{raises} raises so far"

printTitle "Counting Junk Making 10"


roll = roll.sort.reverse
puts "Junk dice: #{roll.inspect}"

raises += findRaisesInJunk(roll, 10)
puts "#{raises} raises so far"

puts "-------------------"

puts "#{raises} Raises Total!"
puts "#{roll.inspect} left over"
