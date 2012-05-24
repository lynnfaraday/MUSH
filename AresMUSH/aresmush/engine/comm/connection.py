# -----------------------------------------------------------------------------
# -- AresMUSH.  Copyright 2010 by Linda Naughton ("Faraday")
# -- See http://www.wordsmyth.org/aresmush for documentation and license info.
# -----------------------------------------------------------------------------
# DESCRIPTION:
# This class implements the connection to the player's client.  It inherits
# from BaseRequestHandler and extends it to implement per-client locale
# support and registration with the server (so it'll know who's connected).
# -----------------------------------------------------------------------------

import gettext
import logging
import os
import select
import SocketServer
import sys
import traceback

from aresmush.engine.events import dispatcher


# Serves requests from a client connection.
class Connection(SocketServer.BaseRequestHandler):
    
    translator = None
    langCode = "en_US"
    disconnect = False
    player = None
    
    def send(self, message, args = []):
        translatedMsg = self._(message) % args
        # Note: Can't send raw unicode to the request, have to encode to a byte stream.
        self.request.send(translatedMsg.encode("utf8"))
        self.request.send("\r\n")

    def setupLocale(self, languageList):
        logging.info("Setting up locale %s" % languageList)
        # Locale files are stored locally.
        localePath = os.path.realpath(os.path.dirname(sys.argv[0]))
        localePath += '/aresmush/locale'
        
        self.translator = gettext.translation('AresMUSH', localePath, languages=languageList, fallback=True)
        self._ = self.translator.ugettext
        self.langCode = languageList[0]

    def setup(self):
        logging.info("Connection received from %s" % str(self.client_address))
                
        # TODO: Get server language preference
        #self.setupLocale(["de_DE"])
        self.setupLocale(["en_US"])
        self.send("Welcome to AresMUSH")
        

    def handle(self):
        self.server.connections.append(self)
        try:
            while self.disconnect == False:
                ready = select.select([self.request], [], [], 1)
                if ready[0]:
                    data = self.request.recv(1024)
                    # Note: data comes in as a string, but we're treating it as UTF8
                    # so we can handle unicode input.  Have to encode it as such before
                    # passing it off to the parsers.
                    cmd = unicode(data, 'utf8')
                    dispatcher.rootDispatcher.processCommand(self, cmd)
        finally:
            self.server.connections.remove(self)
        
               
    def finish(self):
        self.server.connections.remove(self)
        logging.info("%s disconnected" % str(self.client_address))
        self.send("Goodbye!  Please come back soon.")