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

    def command_shutdown(self, command):
        command.connection.send("You have initiated a game shutdown.")
        logging.info("Received shutdown command.")
        
        for connection in command.connection.server.connections:
            command.connection.send("Game shutdown initiated by %(name)s.  Goodbye for now!" % { 'name' : command.connection.player.name })
            command.connection.disconnect = True
        command.connection.server.shutdown()
        return True
    
    def command_reload(self, command):
        # No idea why the import from won't work for this guy.
        rootModule = aresmush.modules.management.moduleManager.rootModuleManager
        if (command.switch == "all"):
            rootModule.reloadAll()
            return True
        moduleName = command.args
        if (moduleName == ""):
            command.connection.send("No module specified.")
            return True
        if (rootModule.isInstalled(moduleName) == False):
            command.connection.send("That module is not installed.")
            return True
        command.connection.send("Reloading module '%(name)s'." % {'name' : moduleName})
        rootModule.reload(moduleName)
        return True
    
    def command_quit(self, command):
        self.handleQuit(command)
        return True
    
    def command_log(self, command):
        if (command.args == 'info'):
            level = logging.INFO
        elif (command.args == 'debug'):
            level = logging.DEBUG
        else:
            command.connection.send("'%(level)s' is not a valid logging level.  Use 'info' or 'debug'.", {"level" : command.args})
            return True
        
        rootLogger = logging.getLogger('')
        rootLogger.setLevel(level)
        command.connection.send("You set the log level to '%(level)s'", str(level))
        logging.info("Setting log level to %s", str(level))
        return True
            
    def anoncommand_quit(self, command):
        self.handleQuit(command)
        return True
        
    def anoncommand_connect(self, command):
        command.connection.player = Player(command.args)
        command.connection.send("Welcome, %(name)s.", { 'name' : command.connection.player.name })
        return True

    def handleQuit(self, command):
        command.connection.send("Bye!  Please come back soon.")
        command.connection.disconnect = True
