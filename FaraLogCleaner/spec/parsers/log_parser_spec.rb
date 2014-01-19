require 'log_parser'

describe LogParser do
  before do
    @format_specs = double
    @line_parser = double
    expect(LogFormatReader).to receive(:read).with("mush").and_return(@format_specs)
    expect(LineParser).to receive(:new).with(@format_specs).and_return(@line_parser)
  end
  
  describe :initialize do
    it "should setup the line parser" do
      LogParser.new("mush")
      # Expectations are in the 'before'
    end
  end
  
  describe :parse do
    it "should parse each line and separate by double newlines" do
      expect(@line_parser).to receive(:parse).with("line1").and_return("new line1")
      expect(@line_parser).to receive(:parse).with("line2").and_return("new line2")
      parser = LogParser.new("mush")
      parsed_lines = parser.parse(["line1", "line2"])
      expect(parsed_lines).to eq "new line1\n\nnew line2"
    end
    
    it "should ignore blank lines" do
      expect(@line_parser).to receive(:parse).with("line1").and_return("new line1")
      expect(@line_parser).to receive(:parse).with("line2").and_return("new line2")
      parser = LogParser.new("mush")
      parsed_lines = parser.parse(["line1", "\n", "\r", "\r\n", "line2"])
      expect(parsed_lines).to eq "new line1\n\nnew line2"
    end
    
    it "should ignore omitted lines" do
      expect(@line_parser).to receive(:parse).with("line1").and_return("new line1")
      expect(@line_parser).to receive(:parse).with("line2").and_return("")
      parser = LogParser.new("mush")
      parsed_lines = parser.parse(["line1", "line2"])
      expect(parsed_lines).to eq "new line1"
    end
    
    it "should strip off line ends" do
      parser = LogParser.new("mush")
      
    end
    
  end
  
end