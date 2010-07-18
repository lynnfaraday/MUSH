# -----------------------------------------------------------------------------
# -- AresMUSH.  Copyright 2010 by Linda Naughton ("Faraday")
# -- See http://www.wordsmyth.org/aresmush for documentation and license info.
# -----------------------------------------------------------------------------

from aresmush.modules.management.baseModule import BaseModule

class Foo(BaseModule):
 
    name = "Foo"  
    loadCount = 0
    
    def init(self):
        self.loadCount = self.loadCount + 1
        
    def processCommand(self, connection, command):
        if (command.name == "@foo"):
           return True
        return False
    
    
   
class Bar(BaseModule):
 
    name = "Bar"  
    loadCount = 0
    
    def init(self):
        self.loadCount = self.loadCount + 1
        
    def processCommand(self, connection, command):
        if (command.name == "@bar"):
           return True
        return False