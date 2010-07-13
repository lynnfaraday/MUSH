# -----------------------------------------------------------------------------
# -- AresMUSH.  Copyright 2010 by Linda Naughton ("Faraday")
# -- See http://www.wordsmyth.org/aresmush for documentation and license info.
# -----------------------------------------------------------------------------

from aresmush.modules.management.baseModule import BaseModule

class Movement(BaseModule):

    name = "Movement"
    
    def processCommand(self, requestHandler, command):
        if (command.name == "@move"):
           requestHandler.send("Handle move2")
           return True
        return False
