# -----------------------------------------------------------------------------
# -- AresMUSH.  Copyright 2010 by Linda Naughton ("Faraday")
# -- See http://www.wordsmyth.org/aresmush for documentation and license info.
# -----------------------------------------------------------------------------

from aresmush.modules.management.baseModule import BaseModule

class Movement(BaseModule):

    name = "Movement"
    
    # Override default handling because we'll have to do special stuff for exit names.
    def handleCommand(self, connection, command):
        if (command.name == "@move"):
           connection.send("You moved.")
           return True
        return False
