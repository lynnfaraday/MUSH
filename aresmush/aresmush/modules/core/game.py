# -----------------------------------------------------------------------------
# -- AresMUSH.  Copyright 2010 by Linda Naughton ("Faraday")
# -- See http://www.wordsmyth.org/aresmush for documentation and license info.
# -----------------------------------------------------------------------------

import logging
import sys
from aresmush.modules.management.baseModule import BaseModule
import aresmush.modules.management.moduleManager


class Game(BaseModule):
    name = "Game"

    def processCommand(self, requestHandler, command):
        # No idea why the import from won't work for this guy.
        rootModule = aresmush.modules.management.moduleManager.rootModuleManager
        
        if (command.name == "@shutdown"):
            requestHandler.send("Handle shutdown")
            self.shutdown()
            return True
        elif (command.name == "@reload"):
            if (command.switch == "all"):
                rootModule.reloadAll()
                return True
            moduleName = command.args
            if (moduleName == ""):
                requestHandler.send("No module specified.")
                return True
            if (rootModule.isInstalled(moduleName) == False):
                requestHandler.send("That module is not installed.")
                return True
            rootModule.reload(moduleName)
        
        return False

    def setLogLevel(self, level):
        logging.info("Setting log level to %s", level)
        
        if (level == 'info'):
            rootLogger = logging.getLogger('')
            rootLogger.setLevel(logging.INFO)
        elif (level == 'debug'):
            rootLogger = logging.getLogger('')
            rootLogger.setLevel(logging.DEBUG)
        else:
            # TODO: error handling
            pass
    
    def shutdown(self):
         logging.info("Received shutdown command.")
         sys.exit()

            
