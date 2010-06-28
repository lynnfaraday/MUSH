# -----------------------------------------------------------------------------
# -- AresMUSH.  Copyright 2010 by Linda Naughton ("Faraday")
# -- See http://www.wordsmyth.org/aresmush for documentation and license info.
# -----------------------------------------------------------------------------

from aresmush.modules.management.baseModule import BaseModule

class Movement(BaseModule):

    Name = "Movement"
    
    def ProcessCommand(self, requestHandler, command):
        if (command.Name == "@move"):
           requestHandler.send("Handle move2")
           return True
        return False
