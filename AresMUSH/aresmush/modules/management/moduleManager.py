# -----------------------------------------------------------------------------
# -- AresMUSH.  Copyright 2010 by Linda Naughton ("Faraday")
# -- See http://www.wordsmyth.org/aresmush for documentation and license info.
# -----------------------------------------------------------------------------

import logging
        
from aresmush.modules.management import moduleFactory

class ModuleManager:
    modules = None
    factory = None
    
    def __init__(self, factory):
        self.modules = dict()
        self.factory = factory
        self.loadModules()

    def isInstalled(self, name):
        return self.modules.has_key(name)
        
    def currentInstance(self, name):
        if (self.isInstalled(name) == False):
            return None
        return self.modules[name].currentInstance
        
    def loadModule(self, module):
        module.currentInstance = module.factoryMethod()
        logging.debug("Loading %s module." % module.name())
        self.modules[module.name()] = module
        
    def loadModules(self):
        logging.info("Loading all modules.")
        allModules = self.factory.allModules()
        for module in allModules:
            self.loadModule(module)
            
    def reloadAll(self):
        logging.info("Reloading all modules.")
        
        for key, module in self.modules.iteritems():
            reload(module.moduleRef)
        self.loadModules()
      
    def reload(self, name):
        if (self.isInstalled(name) == False):
            logging.info("Attempted to reload invalid module %s" % name)
            return
        module = self.modules[name]
        reload(module.moduleRef)
        self.loadModule(module)
        

