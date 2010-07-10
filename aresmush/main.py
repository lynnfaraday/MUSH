# -----------------------------------------------------------------------------
# -- AresMUSH.  Copyright 2010 by Linda Naughton ("Faraday")
# -- See http://www.wordsmyth.org/aresmush for documentation and license info.
# -----------------------------------------------------------------------------

import logging
import logging.handlers
import SocketServer

from aresmush.engine.comm import server
from aresmush.engine.commands import commandDispatcher
from aresmush.modules.management import moduleManager


def SetupLogging():
    LOG_FILENAME = 'log.txt'
        
    rootLogger = logging.getLogger('')
    rootLogger.setLevel(logging.DEBUG)
       
    formatter = logging.Formatter("%(asctime)s %(levelname)-8s %(filename)s %(funcName)s | %(message)s")
    
    # Add a handler to the logger to print to a file.
    fileHandler = logging.handlers.RotatingFileHandler(
              LOG_FILENAME, maxBytes=10000, backupCount=5)
    fileHandler.setFormatter(formatter)
    rootLogger.addHandler(fileHandler)
    
    # Then add a handler that prints to the screen
    consoleHandler = logging.StreamHandler()
    consoleHandler.setLevel(logging.DEBUG)
    consoleHandler.setFormatter(formatter)
    
    rootLogger.addHandler(consoleHandler)

    
if __name__ == "__main__":
    
   SetupLogging()

   logging.info("System starting up.")

   factory = ModuleFactory()
   moduleManager.RootModuleManager = moduleManager.ModuleManager(factory)
   commandDispatcher.RootCommandDispatcher = commandDispatcher.CommandDispatcher()

   HOST, PORT = "localhost", 9999

   logging.debug("Server starting up.")

   #server host is a tuple ('host', port)
   server = server.ExitingTCPServer((HOST, PORT), server.RequestHandler)
   server.serve_forever()
