# -----------------------------------------------------------------------------
# -- AresMUSH.  Copyright 2010 by Linda Naughton ("Faraday")
# -- See http://www.wordsmyth.org/aresmush for documentation and license info.
# -----------------------------------------------------------------------------

class Command:

    def __init__(self, commandString):
        self.data = []
        self.Name = ""
        self.Args = ""
        self.Switch = ""
        self.Crack(commandString)
    
    def Crack(self, commandString):
        # TODO: I'm sure there's a way to make this into a more elegant regex!
        
        # Get rid of leading and trailing whitespace.
        commandString = commandString.strip()
        
        # Parses in the form command/switch args, where both /switch and args are optional.
        splitOnSpace = commandString.split(" ", 1)
        numSplits = len(splitOnSpace)
        
        if (numSplits < 2):
           self.Name = splitOnSpace[0]
           self.Args = ""
        else:
           self.Name = splitOnSpace[0]
           self.Args = splitOnSpace[1]
        
        splitOnSlash = self.Name.split("/", 1)
        numSplits = len(splitOnSlash)
        
        if (numSplits < 2):
           self.Name = splitOnSlash[0]
           self.Switch = ""
        else:
           self.Name = splitOnSlash[0]
           self.Switch = splitOnSlash[1]
