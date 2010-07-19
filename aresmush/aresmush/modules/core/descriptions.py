# -----------------------------------------------------------------------------
# -- AresMUSH.  Copyright 2010 by Linda Naughton ("Faraday")
# -- See http://www.wordsmyth.org/aresmush for documentation and license info.
# -----------------------------------------------------------------------------

from aresmush.modules.management.baseModule import BaseModule

class Descriptions(BaseModule):
 
    name = "Descriptions"  
  
    def command_desc(self, command):
        command.connection.send("This would be a desc.")
        return True

 