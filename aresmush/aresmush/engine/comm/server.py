# -----------------------------------------------------------------------------
# -- AresMUSH.  Copyright 2010 by Linda Naughton ("Faraday")
# -- See http://www.wordsmyth.org/aresmush for documentation and license info.
# -----------------------------------------------------------------------------

import logging
import SocketServer
import sys
import traceback

from aresmush.engine.commands import commandDispatcher

# Override of the threaded TCP server so it'll handle the SystemExit exception
# and stop listening.  This allows a graceful app shutdown.
class ExitingTCPServer(SocketServer.ThreadingTCPServer):

    def handle_error(self, request, client_address):
        try:
            raise
        except SystemExit:
            logging.info("Shutting down server.")
            self.shutdown()
        except BaseException:
            traceback.print_exception


# Serves requests from a client connection.
class RequestHandler(SocketServer.BaseRequestHandler):
    def send(self, message):
        self.request.send(message)

    def setup(self):
        logging.info("Connection received from %s" % str(self.client_address))
        self.request.send("Welcome to AresMUSH\n")

    def handle(self):
        while 1:
            data = self.request.recv(1024)
            commandDispatcher.RootCommandDispatcher.DispatchCommand(self, data)
               
    def finish(self):
        logging.info("%s disconnected" % str(self.client_address))
        self.request.send("Goodbye!  Please come back soon.")
