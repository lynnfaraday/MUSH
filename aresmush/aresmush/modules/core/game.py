# -----------------------------------------------------------------------------
# -- AresMUSH.  Copyright 2010 by Linda Naughton ("Faraday")
# -- See http://www.wordsmyth.org/aresmush for documentation and license info.
# -----------------------------------------------------------------------------

import logging
import sys
from aresmush.modules.management.baseModule import BaseModule
import aresmush.modules.management.moduleManager
from aresmush.engine.models.player import Player


class Game(BaseModule):
    name = "Game"

    def processCommand(self, connection, command):
        # No idea why the import from won't work for this guy.
        rootModule = aresmush.modules.management.moduleManager.rootModuleManager
        
        if (command.name == "@shutdown"):
            connection.send("Handle shutdown")
            self.shutdown()
            return True
        elif (command.name == "@reload"):
            if (command.switch == "all"):
                rootModule.reloadAll()
                return True
            moduleName = command.args
            if (moduleName == ""):
                connection.send("No module specified.")
                return True
            if (rootModule.isInstalled(moduleName) == False):
                connection.send("That module is not installed.")
                return True
            rootModule.reload(moduleName)
        elif (command.name == "QUIT"):
            connection.disconnect = True
            return True        
        return False

    def processAnonCommand(self, connection, command):
        if (command.name == "connect"):
            connection.player = Player(command.args)
            return True
        elif (command.name == "QUIT"):
            connection.disconnect = True
            return True        
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

            
