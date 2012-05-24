# -----------------------------------------------------------------------------
# -- AresMUSH.  Copyright 2010 by Linda Naughton ("Faraday")
# -- See http://www.wordsmyth.org/aresmush for documentation and license info.
# -----------------------------------------------------------------------------

class AresModule:
    
    # Reference to the Python module that contains the code for this Ares module.
    moduleRef = None
    
    # Currently-instantiated instance of this module
    currentInstance = None
    
    # Method used to generate a new instance of this module
    factoryMethod = None
    
    def __init__(self, moduleRef, currentInstance, factoryMethod):
        self.moduleRef = moduleRef
        self.currentInstance = currentInstance
        self.factoryMethod = factoryMethod
    
    def name(self):
        if (self.currentInstance == None):
            return ""
        return self.currentInstance.name