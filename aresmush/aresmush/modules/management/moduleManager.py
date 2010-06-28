# -----------------------------------------------------------------------------
# -- AresMUSH.  Copyright 2010 by Linda Naughton ("Faraday")
# -- See http://www.wordsmyth.org/aresmush for documentation and license info.
# -----------------------------------------------------------------------------

import logging
        
from aresmush.modules.management import moduleFactory

class ModuleManager:
    def __init__(self):
        self.Modules = dict()
        self.LoadModules()

    def IsInstalled(self, name):
        return self.Modules.has_key(name)
        
    def CurrentInstance(self, name):
        if (self.IsInstalled(name) == False):
            return None
        return self.Modules[name].CurrentInstance
        
    def LoadModule(self, module):
        module.CurrentInstance = module.FactoryMethod()
        logging.debug("Loading %s module." % module.name())
        self.Modules[module.name()] = module
        
    def LoadModules(self):
        logging.info("Loading all modules.")
        allModules = moduleFactory.AllModules()
        for module in allModules:
            self.LoadModule(module)
            
    def ReloadAll(self):
        logging.info("Reloading all modules.")
        
        for key, module in self.Modules.iteritems():
            reload(module.ModuleRef)
        self.LoadModules()
      
    def Reload(self, name):
        if (self.IsInstalled(name) == False):
            logging.info("Attempted to reload invalid module %s" % name)
            return
        module = self.Modules[name]
        reload(module.ModuleRef)
        self.LoadModule(module)
        

