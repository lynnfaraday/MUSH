# -----------------------------------------------------------------------------
# -- AresMUSH.  Copyright 2010 by Linda Naughton ("Faraday")
# -- See http://www.wordsmyth.org/aresmush for documentation and license info.
# -----------------------------------------------------------------------------

class Command:

    name = ""
    args = ""
    switch = ""
    
    def __init__(self, commandString):
        self.data = []
        self.name = ""
        self.args = ""
        self.switch = ""
        self.crack(commandString)
    
    def crack(self, commandString):
        # TODO: I'm sure there's a way to make this into a more elegant regex!
        
        # Get rid of leading and trailing whitespace.
        commandString = commandString.strip()
        
        # Parses in the form command/switch args, where both /switch and args are optional.
        splitOnSpace = commandString.split(" ", 1)
        numSplits = len(splitOnSpace)
        
        if (numSplits < 2):
           self.name = splitOnSpace[0]
           self.args = ""
        else:
           self.name = splitOnSpace[0]
           self.args = splitOnSpace[1]
        
        splitOnSlash = self.name.split("/", 1)
        numSplits = len(splitOnSlash)
        
        if (numSplits < 2):
           self.name = splitOnSlash[0]
           self.switch = ""
        else:
           self.name = splitOnSlash[0]
           self.switch = splitOnSlash[1]
