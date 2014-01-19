require 'log_parser'
require 'log_format_reader'

class HomeController < ApplicationController
  
  def parse
    uploaded_file = params[:file]
    format = params[:log_format]
    begin
      parser = LogParser.new(format)
      begin
        lines = []
        uploaded_file.open.each do |l|
          lines << l
        end        
        @parsed_log = parser.parse(lines)
        @error = nil
      rescue Exception => e
        @parsed_log = ""
        @error = "Error reading file.  Please make sure it's a plain text log.  If it is, lease send this error to faraday@aresmush.com: #{e} #{e.backtrace}"
      end
    rescue Exception => e
      @parsed_log = ""
      @error = "Sorry!  Something went wrong.  Please send this error to faraday@aresmush.com: #{e}"
    end
  
    
    render "parse"
  end
end
