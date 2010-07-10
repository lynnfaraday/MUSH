# -----------------------------------------------------------------------------
# -- AresMUSH.  Copyright 2010 by Linda Naughton ("Faraday")
# -- See http://www.wordsmyth.org/aresmush for documentation and license info.
# -----------------------------------------------------------------------------

from aresmush.modules.management.baseModule import BaseModule

class Foo(BaseModule):
 
    Name = "Foo"  
    LoadCount = 0
    
    def Init(self):
        self.LoadCount = self.LoadCount + 1
        
    def ProcessCommand(self, requestHandler, command):
        if (command.Name == "@foo"):
           return True
        return False
    
    
   
class Bar(BaseModule):
 
    Name = "Bar"  
    LoadCount = 0
    
    def Init(self):
        self.LoadCount = self.LoadCount + 1
        
    def ProcessCommand(self, requestHandler, command):
        if (command.Name == "@bar"):
           return True
        return False