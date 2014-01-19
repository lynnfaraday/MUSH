class LineParser
  def initialize(format_specs)
    @specs = format_specs
  end
  
  def parse(line)
    @specs.each do |f|
      if (f.matches?(line))
        return f.parse(line)
      end
    end
    return line
  end
end