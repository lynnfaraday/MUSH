# -----------------------------------------------------------------------------
# -- AresMUSH.  Copyright 2010 by Linda Naughton ("Faraday")
# -- See http://www.wordsmyth.org/aresmush for documentation and license info.
# -----------------------------------------------------------------------------

import sys
import logging

from aresmush.modules.management import moduleManager
from aresmush.engine.commands import command

class CommandDispatcher:
    
    def __init__(self):
        logging.info("Creating command dispatcher.")

    # TODO: Ultimately this will need to dump into a thread-safe queue and have
    # a separate loop for dispatching
    def dispatchCommand(self, requestHandler, commandString):
        try:
            print "GOT"
            logging.debug("Got command %s from %s" % (commandString, requestHandler.client_address))
            
            cmd = command.Command(commandString)
                        
            for key, module in moduleManager.rootModuleManager.modules.iteritems():
               # Only one module is allowed to handle a command so end once we found one.
               handled = module.currentInstance.processCommand(requestHandler, cmd)
               if (handled):
                   break
            
            requestHandler.send("Got command ~%s~%s~%s\n" % (cmd.name, cmd.switch, cmd.args))
        except SystemExit:
           logging.info("Got system exit request.  Goodbye!")
           raise
        
        except BaseException as detail:
           logging.error("Ooops!  Something went awry with your command: %s" % detail)
        else:
           return "Processed command %s" % command
       
  