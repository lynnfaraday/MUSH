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

    def command_shutdown(self, connection, command):
        connection.send("You have initiated a game shutdown.")
        logging.info("Received shutdown command.")
        sys.exit()
        return True
    
    def command_reload(self, connection, command):
        # No idea why the import from won't work for this guy.
        rootModule = aresmush.modules.management.moduleManager.rootModuleManager
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
    
    def command_quit(self, connection, command):
        connection.disconnect = True
        return True
    
    def command_log(self, connection, command):
        if (command.args == 'info'):
            level = logging.INFO
        elif (command.args == 'debug'):
            level = logging.DEBUG
        else:
            connection.send("'%(level)s' is not a valid logging level.  Use 'info' or 'debug'.", {"level" : command.args})
            return True
        
        rootLogger = logging.getLogger('')
        rootLogger.setLevel(level)
        connection.send("You set the log level to '%(level)s'", str(level))
        logging.info("Setting log level to %s", str(level))
            
    def anoncommand_quit(self, connection, command):
        connection.disconnect = True
        return True
        
    def anoncommand_connect(self, connection, command):
        connection.player = Player(command.args)
        connection.send("Welcome, %(name)s", { 'name' : connection.player.name })
        return True

            
