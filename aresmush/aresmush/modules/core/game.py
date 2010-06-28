# -----------------------------------------------------------------------------
# -- AresMUSH.  Copyright 2010 by Linda Naughton ("Faraday")
# -- See http://www.wordsmyth.org/aresmush for documentation and license info.
# -----------------------------------------------------------------------------

import logging
import sys
from aresmush.modules.management.baseModule import BaseModule
import aresmush.modules.management.moduleManager


class Game(BaseModule):
    Name = "Game"

    def ProcessCommand(self, requestHandler, command):
        # No idea why the import from won't work for this guy.
        rootModule = aresmush.modules.management.moduleManager.RootModuleManager
        
        if (command.Name == "@shutdown"):
            requestHandler.send("Handle shutdown")
            self.Shutdown()
            return True
        elif (command.Name == "@reload"):
            if (command.Switch == "all"):
                rootModule.ReloadAll()
                return True
            moduleName = command.Args
            if (moduleName == ""):
                # TODO: Error msg
                print "No module specified."
                return True
            if (rootModule.IsInstalled(moduleName) == False):
                # TODO: Error msg
                print "Not installed"
                return True
            rootModule.Reload(moduleName)
        
        return False

    def SetLogLevel(self, level):
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
    
    def Shutdown(self):
         logging.info("Received shutdown command.")
         sys.exit()

            
