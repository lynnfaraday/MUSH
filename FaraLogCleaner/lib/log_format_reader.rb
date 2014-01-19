require 'format_spec'

class LogFormatReader
  def self.read(format_name)
    format_specs = read_file("common")
    format_specs.concat(read_file("#{format_name}"))
    format_specs
  end
  
  def self.read_file(format_name)
    format_specs = []
    file_name = format_name.downcase.gsub(' ','_') + ".txt"
    format_path = File.join(Rails.root, 'config', 'formats', file_name)
     File.open(format_path).each_line do |line|
       line = line.chomp
       if !line.start_with?("#") && !line.empty?
         format_specs << FormatSpec.new(line)
       end
     end
     format_specs
   end
end