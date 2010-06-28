# -----------------------------------------------------------------------------
# -- AresMUSH.  Copyright 2010 by Linda Naughton ("Faraday")
# -- See http://www.wordsmyth.org/aresmush for documentation and license info.
# -----------------------------------------------------------------------------

from aresmush.modules.management.baseModule import BaseModule

class Descriptions(BaseModule):
 
    Name = "Descriptions"  
  
    def ProcessCommand(self, requestHandler, command):
        if (command.Name == "@desc"):
           requestHandler.send("Handle desc")
           return True
        return False

 