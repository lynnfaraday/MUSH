# -----------------------------------------------------------------------------
# -- AresMUSH.  Copyright 2010 by Linda Naughton ("Faraday")
# -- See http://www.wordsmyth.org/aresmush for documentation and license info.
# -----------------------------------------------------------------------------

class AresModule:
    def __init__(self, moduleRef, currentInstance, factoryMethod):
        self.ModuleRef = moduleRef
        self.CurrentInstance = currentInstance
        self.FactoryMethod = factoryMethod
    
    def name(self):
        if (self.CurrentInstance == None):
            return ""
        return self.CurrentInstance.Name