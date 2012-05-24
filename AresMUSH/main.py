# -----------------------------------------------------------------------------
# -- AresMUSH.  Copyright 2010 by Linda Naughton ("Faraday")
# -- See http://www.wordsmyth.org/aresmush for documentation and license info.
# -----------------------------------------------------------------------------

import locale
import logging
import logging.handlers
import SocketServer

from aresmush.engine.comm import server
from aresmush.engine.comm import connection
from aresmush.engine.events import dispatcher
from aresmush.modules.management import moduleManager
from aresmush.modules.management import moduleFactory

def SetupLogging():
    LOG_FILENAME = 'log.txt'
        
    rootLogger = logging.getLogger('')
    rootLogger.setLevel(logging.DEBUG)
       
    formatter = logging.Formatter("%(asctime)s %(levelname)-8s %(filename)-25s %(funcName)-20s | %(message)s ")
    
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
   
   factory = moduleFactory.ModuleFactory()
   moduleManager.rootModuleManager = moduleManager.ModuleManager(factory)
   dispatcher.rootDispatcher = dispatcher.Dispatcher()

   HOST, PORT = "localhost", 7207

   logging.info("Server starting up.")

   #server host is a tuple ('host', port)
   # allow_reuse_address lets you immediately restart after shutting down the server
   # Otherwise you'll have to wait till the OS's TCP stack releases the connection,
   # which could take an unpredictably long time.
   SocketServer.ThreadingTCPServer.allow_reuse_address = True
   # Daemon Threads makes the individual connection threads into 'daemons' which means
   # they won't hold up the application from exiting.
   server.daemon_threads = True
   server = server.AresServer((HOST, PORT), connection.Connection)
   server.serve_forever()
