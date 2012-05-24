# -----------------------------------------------------------------------------
# -- AresMUSH.  Copyright 2010 by Linda Naughton ("Faraday")
# -- See http://www.wordsmyth.org/aresmush for documentation and license info.
# -----------------------------------------------------------------------------
# DESCRIPTION:
# The main dispatcher for system events, including events generated by other
# modules and (most often) commands generated by players.  Events are dumped
# into a queue and sent to the modules one by one.
# -----------------------------------------------------------------------------

import sys
import logging
from Queue import Queue
import threading
import traceback

from aresmush.modules.management import moduleManager
from aresmush.engine.events import command

class DispatchThread(threading.Thread):
    queue = None
    
    def run(self):
        while True:
            # This will block until an event is received.
            logging.debug("Queue blocking.")
            event = self.queue.get()
            try:
                self.dispatchEvent(event)
            finally:
                logging.debug("Queue unblocking.")
                self.queue.task_done()
        
    # TODO: This needs to get smarter about event dispatching and handling special cases like the "anyHandled"
    def dispatchEvent(self, event):
        try:
            anyHandled = False
            logging.debug("Dispatching event.")

            for key, module in moduleManager.rootModuleManager.modules.iteritems():
				# Call either the anon command or regular version depending on whether the
				# person is logged in.
                if (event.connection.player == None):
                    handled = module.currentInstance.handleAnonCommand(event)
                else:
                    handled = module.currentInstance.handleCommand(event)
                anyHandled = anyHandled or handled

            if (anyHandled == False):
                event.connection.send("%s %s" % ( event.connection._("<GAME>"), 
                     event.connection._("I don't recognize that command.") ))

        except BaseException as detail:
            traceback.print_exc
            logging.error("Ooops!  Something went awry with your event: %s" % detail)
    
class Dispatcher:
    
    queue = None
    thread = None
    
    def __init__(self):
        logging.info("Creating dispatcher.")
        self.queue = Queue()
        self.thread = DispatchThread()
        self.thread.daemon = True
        self.thread.queue = self.queue
        self.thread.start()

    # TODO: Ultimately this will need to dump into a thread-safe queue and have
    # a separate loop for dispatching
    def processCommand(self, connection, commandString):
        # TODO: Don't log connect or password commands.
        logging.debug("Got command %s from %s" % (commandString, str(connection.client_address)))
        cmd = command.Command(commandString)
        cmd.connection = connection
        self.queue.put(cmd)
    
    
       
  