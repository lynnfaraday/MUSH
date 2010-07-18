# -----------------------------------------------------------------------------
# -- AresMUSH.  Copyright 2010 by Linda Naughton ("Faraday")
# -- See http://www.wordsmyth.org/aresmush for documentation and license info.
# -----------------------------------------------------------------------------

import sys
import logging
import traceback

from aresmush.modules.management import moduleManager
from aresmush.engine.commands import command

class CommandDispatcher:
    
    def __init__(self):
        logging.info("Creating command dispatcher.")

    # TODO: Ultimately this will need to dump into a thread-safe queue and have
    # a separate loop for dispatching
    def dispatchCommand(self, connection, commandString):
        try:
            logging.debug("Got command %s from %s" % (commandString, str(connection.client_address)))
            
            cmd = command.Command(commandString)
                        
            for key, module in moduleManager.rootModuleManager.modules.iteritems():
            	# Only one module is allowed to handle a command so end once we found one.
				# Call either the anon command or regular version depending on whether the
				# person is logged in.
                if (connection.player == None):
                    handled = module.currentInstance.processAnonCommand(connection, cmd)
                else:
                    handled = module.currentInstance.processCommand(connection, cmd)
               	if (handled):
                   	break
            
            if (handled == False):
                connection.send("%s %s" % ( connection._("<GAME>"), 
                     connection._("I don't recognize that command.") ))
        except SystemExit:
           logging.info("Got system exit request.  Goodbye!")
           raise
        
        except BaseException as detail:
            logging.error("Ooops!  Something went awry with your command: %s" % detail)
            raise
        else:
           return "Processed command %s" % command
       
  