# -----------------------------------------------------------------------------
# -- AresMUSH.  Copyright 2010 by Linda Naughton ("Faraday")
# -- See http://www.wordsmyth.org/aresmush for documentation and license info.
# -----------------------------------------------------------------------------
# DESCRIPTION:
# Represents a player-executed command.  The commands are in the form
#    [<prefix>]<name>[/<switch>][ <args>]
#    i.e.  foo, +foo/all, +foo Whee!
# The format of <args> will vary by command, so there's a separate method that
# will 'crack' the args into a dictionary given a regex.
# -----------------------------------------------------------------------------

import re
class Command:

    prefix = ""
    name = ""
    switch = ""
    args = ""
    connection = None

    def __init__(self, commandString):
        self.empty()
        self.crack(commandString)

    def empty(self):
        self.prefix = ""
        self.name = ""
        self.switch = ""
        self.args = ""
        
    def crack(self, commandString):
        # Get rid of leading and trailing whitespace.
        commandString = commandString.strip()

        # Here's the master regex that does all the work.
        #  prefix = a single non-word character (optional)
        #  name = one-or-more non-whitespace chars (required)
        #  switch = optional '/' followed by optional non-whitespace chars
        #  args = anything else (optional)
        match = re.match("(?P<prefix>[^\w]?)(?P<name>[^\s/]+)(?P<switch>/?[\S]*)(?P<args>.*)", commandString)

        # If we failed to find a command at all, empty out everything
        if (match == None):
            self.empty()
            return
                    
        # Convert name and switch to lowercase for easier matching later.
        self.prefix = match.group('prefix')
        self.name = match.group('name').lower()
        self.switch = match.group('switch').lower()
        self.args = match.group('args')
        
        # Get rid of the "/" on switch
        self.switch = self.switch.replace("/", "")

        # Strip leading/trailing spaces from the resulting fields (except prefix, which is a single special char)
        self.name = self.name.strip()
        self.switch = self.switch.strip()
        self.args = self.args.strip()
    
    # Converts 'args' from a simple string into a dictionary based on the regex string provided (assuming it has group names)
    def crackArgs(self, argRegex):
        match = re.match(argRegex, self.args)
        self.args = match.groupdict()