# -----------------------------------------------------------------------------
# -- AresMUSH.  Copyright 2010 by Linda Naughton ("Faraday")
# -- See http://www.wordsmyth.org/aresmush for documentation and license info.
# -----------------------------------------------------------------------------

import logging
import SocketServer
import sys
import traceback
from SocketServer import ThreadingTCPServer

# Override of the threaded TCP server so it'll handle the SystemExit exception
# and stop listening.  This allows a graceful app shutdown.
class AresServer(ThreadingTCPServer):

    connections = []
    
    def handle_error(self, request, client_address):
        try:
            raise
        except SystemExit:
            logging.info("Shutting down server.")
            self.shutdown()
        except BaseException:
            traceback.print_exc
