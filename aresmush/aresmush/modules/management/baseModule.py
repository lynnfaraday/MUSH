# -----------------------------------------------------------------------------
# -- AresMUSH.  Copyright 2010 by Linda Naughton ("Faraday")
# -- See http://www.wordsmyth.org/aresmush for documentation and license info.
# -----------------------------------------------------------------------------

import logging

class BaseModule():
    name = ""
    
    def __init__(self):
        logging.info("Loading module %s" % self.name)
        self.init()

    # Empty in base class.  Derived classes may implement additional init options.
    def init(self):
        pass
    
    # Derived classes should override.  Returns true if handled, false if not.
    def processCommand(self, requestHandler, command):
        return False
    