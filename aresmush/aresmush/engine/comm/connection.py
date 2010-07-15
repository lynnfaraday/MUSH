import gettext
import logging
import os
import SocketServer
import sys
import traceback

from aresmush.engine.commands import commandDispatcher


# Serves requests from a client connection.
class Connection(SocketServer.BaseRequestHandler):
    
    translator = None
    langCode = "en_US"
    disconnect = False
    
    def send(self, message, args = []):
        # Little bit of magic going on here.
        # We want to be able to send unicode to the client to support localization.
        # We can't send a 'unicode' type to the TCP request directly - we have to encode it
        #   back to 'str' (which is really just a byte array)
        # In order to encode it, the string has to be of 'unicode' type in the first place!
        # Normally we get back a 'unicode' type when we pass it through translation.
        # BUT 'message' may contain some user-input unicode characters.
        # When this happens, 'message' is of type 'str' with unicode chars in it, which
        #   will cause the translation to barf. (BIZARRE half-baked Python unicode support).
        #   Here we trap that exception and then manually convert that string to a unicode-string.
        try:
            print message
            translatedMsg = self._(message)
            print translatedMsg
            translatedMsg = translatedMsg % args
            print translatedMsg
        except UnicodeDecodeError:
            print "Unicode decode error"
            print traceback.print_exc()
            translatedMsg = unicode(message, "utf8")
            
        self.request.send(translatedMsg.encode("utf8"))
        self.request.send("\r\n")

    def setupLocale(self, languageList):
        logging.info("Setting up locale %s" % languageList)
        # Locale files are stored locally.
        localePath = os.path.realpath(os.path.dirname(sys.argv[0]))
        localePath += '/aresmush/locale'
        
        print localePath
        self.translator = gettext.translation('AresMUSH', localePath, languages=languageList)
        self._ = self.translator.ugettext
        self.langCode = languageList[0]

    def setup(self):
        logging.info("Connection received from %s" % str(self.client_address))
        
        # TODO: Get server language preference
        self.setupLocale(["de_DE"])
        
        self.send("Welcome to AresMUSH")
        

    def handle(self):
        try:
            self.server.connections.append(self)
            while self.disconnect == False:
                data = self.request.recv(1024)
                cmd = unicode(data, 'utf8')
                commandDispatcher.rootCommandDispatcher.dispatchCommand(self, cmd)
        finally:
            self.server.connections.remove(self)
        
               
    def finish(self):
        self.server.connections.remove(self)
        logging.info("%s disconnected" % str(self.client_address))
        self.send("Goodbye!  Please come back soon.")