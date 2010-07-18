# -----------------------------------------------------------------------------
# -- AresMUSH.  Copyright 2010 by Linda Naughton ("Faraday")
# -- See http://www.wordsmyth.org/aresmush for documentation and license info.
# -----------------------------------------------------------------------------

from aresmush.modules.management.baseModule import BaseModule

class Descriptions(BaseModule):
 
    name = "Descriptions"  
  
    def processCommand(self, connection, command):
        if (command.name == "@desc"):
           connection.send("Handle desc")
           return True
        return False

 