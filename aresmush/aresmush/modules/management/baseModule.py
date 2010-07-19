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
        
    prefixMap = {'+': 'plus', 
                 '@': 'at', 
                 '' : "noprefix",
                 '.': "period",
                 ',': "comma",
                 '_': "under",
                 '=': "equals",
                 '-': "dash",
                 ';': "semi",
                 ':': "colon",
                 '"': "quote"
                }
        
    
    # Derived classes just need to define methods of the form:
    #    command_foo - matches any foo command (+foo/@foo/.foo)
    #    command_noprefix_foo - matches only "foo"
    #    command_<prefix>_foo - matches "foo" with a specific prefix.  Due to 
    #       method name limits we can't use the non-alphanumeric prefixes directly.
    #       We use aliases (see the prefixMap list above).  So command_at_foo would
    #       handle only "@foo".  If you want more advanced handling, you'll need to
    #       override handleCommand yourself.
    # Returns true if handled, false if not.
    def handleCommand(self, command):
        handler = self.getHandler(command, "command")
        if (handler == None):
            return False
        return handler(command)

    def handleAnonCommand(self, command):
        handler = self.getHandler(command, "anoncommand")
        if (handler == None):
            return False
        return handler(command)
    
    def getHandler(self, command, methodPrefix):
        # Check the generic case first.
        methodName = "%s_%s" % (methodPrefix, command.name)
        if (hasattr(self, methodName) == True):
            return getattr(self, methodName)
        
        # Check the case with a special prefix.
        if (command.prefix in self.prefixMap):
            methodName = "%s_%s_%s" % (methodPrefix, self.prefixMap[command.prefix], command.name)
            if (hasattr(self, methodName) == True):
                return getattr(self, methodName)
        
        return None
        
        