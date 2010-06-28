# -----------------------------------------------------------------------------
# -- AresMUSH.  Copyright 2010 by Linda Naughton ("Faraday")
# -- See http://www.wordsmyth.org/aresmush for documentation and license info.
# -----------------------------------------------------------------------------

class BaseModule():
    def __init__(self):
        print "Loading module %s" % self.Name

    # Empty in base class.  Derived classes may implement additional init options.
    def Init(self):
        pass
    
    # Derived classes should override.  Returns true if handled, false if not.
    def ProcessCommand(self, requestHandler, command):
        return False
    