import gettext
import logging
import os
import SocketServer
import sys

from aresmush.engine.commands import commandDispatcher


# Serves requests from a client connection.
class Connection(SocketServer.BaseRequestHandler):
    
    translator = None
    langCode = "en_US"
    
    def send(self, message, args = []):
        self.request.send(self._(message) % args)
        self.request.send("\r\n")

    def setupLocale(self, languageList):
        logging.info("Setting up locale %s" % languageList)
        # Locale files are stored locally.
        localePath = os.path.realpath(os.path.dirname(sys.argv[0]))
        localePath += '/locale'
        
        self.translator = gettext.translation('AresMUSH', localePath, languages=languageList, fallback=True)
        self._ = self.translator.ugettext
        self.langCode = languageList[0]

    def setup(self):
        logging.info("Connection received from %s" % str(self.client_address))
        
        # TODO: Get server language preference
        self.setupLocale(["en_US"])
        
        self.send("Welcome to AresMUSH")
        

    def handle(self):
        while 1:
            data = self.request.recv(1024)
            commandDispatcher.rootCommandDispatcher.dispatchCommand(self, data)
               
    def finish(self):
        logging.info("%s disconnected" % str(self.client_address))
        self.request.send("Goodbye!  Please come back soon.")