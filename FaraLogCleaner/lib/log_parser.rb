require 'log_format_reader'
require 'line_parser'

class LogParser
  def initialize(format_name)
    @format_specs = LogFormatReader.read(format_name)
    @line_parser = LineParser.new(@format_specs)
  end
  
  def parse(log_lines)
    parsed_lines = []
    
    log_lines.each do |l|
      l = l.rstrip.chomp
      if (!l.empty?)        
        l = @line_parser.parse(l)
      end
      if (!l.empty?)
        parsed_lines << l
      end
    end
    parsed_lines.join("\n\n")
  end
  
end