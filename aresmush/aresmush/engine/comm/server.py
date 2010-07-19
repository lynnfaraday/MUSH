# -----------------------------------------------------------------------------
# -- AresMUSH.  Copyright 2010 by Linda Naughton ("Faraday")
# -- See http://www.wordsmyth.org/aresmush for documentation and license info.
# -----------------------------------------------------------------------------
# DESCRIPTION:
# This class is the server that handles client connections.  It inherits from
# ThreadingTCPServer so that each client connection will get its own thread.
# It handles the SystemExit exception to allow a graceful app shutdown, and
# also tracks connections.  (the registration of connections is done by the
# clients themselves; this is just a central repository for them)
# -----------------------------------------------------------------------------

import logging
import SocketServer
import sys
import traceback
from SocketServer import ThreadingTCPServer

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
