require 'line_parser'
require "spec_helper"

describe LineParser do
  
  it "should return the original line of no matches found" do
    spec = double 
    expect(spec).to receive(:matches?).with("a line").and_return(false)
    expect(spec).not_to receive(:parse)
    parser = LineParser.new([spec])
    parser.parse("a line").should eq "a line"
  end
  
  it "should use the format spec to parse the line if it matches" do
    spec = double 
    expect(spec).to receive(:matches?).with("a line").and_return(true)
    expect(spec).to receive(:parse).with("a line").and_return("a new line")
    parser = LineParser.new([spec])
    parser.parse("a line").should eq "a new line"
  end
  
  it "should stop after the first matching format spec" do
    spec1 = double 
    spec2 = double
    expect(spec1).to receive(:matches?).with("a line").and_return(true)
    expect(spec1).to receive(:parse).with("a line").and_return("a new line")
    expect(spec2).to_not receive(:matches?).with("a line")
    expect(spec2).to_not receive(:parse).with("a line")
    parser = LineParser.new([spec1, spec2])
    parser.parse("a line").should eq "a new line"
  end
  
  it "should try each format spec" do
    spec1 = double 
    spec2 = double
    expect(spec1).to receive(:matches?).with("a line").and_return(false)
    expect(spec1).not_to receive(:parse).with("a line")
    expect(spec2).to receive(:matches?).with("a line").and_return(true)
    expect(spec2).to receive(:parse).with("a line").and_return("a new line")
    parser = LineParser.new([spec1, spec2])
    parser.parse("a line").should eq "a new line"
  end
  
end