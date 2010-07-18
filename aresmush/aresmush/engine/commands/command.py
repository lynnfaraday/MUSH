# -----------------------------------------------------------------------------
# -- AresMUSH.  Copyright 2010 by Linda Naughton ("Faraday")
# -- See http://www.wordsmyth.org/aresmush for documentation and license info.
# -----------------------------------------------------------------------------

class Command:

    prefix = ""
    name = ""
    args = ""
    switch = ""

    def __init__(self, commandString):
        self.name = ""
        self.args = ""
        self.prefix = ""
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

        # Lower and trim the command name and switch.  Prefix is a special char and args we only trim
        self.name = self.name.lower()
        self.name = self.name.strip()
        self.switch = self.switch.lower()
        self.switch = self.switch.strip()
        self.args = self.args.strip()

        # If the command name starts with a non-alpha char, that must be a prefix (@/+/./= are common)
        strlen = len(self.name)
        if (strlen > 0):
            prefix = self.name[0]
            if (prefix.isalpha() == False):
                self.prefix = prefix
                self.name = self.name[1:strlen]

