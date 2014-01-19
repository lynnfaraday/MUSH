require 'format_spec'
require "spec_helper"

describe FormatSpec do
  
  describe :initialize do
    it "should default to omit" do
      spec = FormatSpec.new("foo")
      spec.type.should eq "Omit"
      spec.match.should eq "foo"
    end
  
    it "should not try to parse single |'s" do
      spec = FormatSpec.new("foo|bar")
      spec.type.should eq "Omit"
      spec.match.should eq "foo|bar"
    end

    it "should parse appropriately delimeted lines" do
      spec = FormatSpec.new("foo|bar|baz")
      spec.type.should eq "foo"
      spec.match.should eq "bar"
      spec.replace.should eq "baz"
    end
  end

  describe :matches? do
    it "should tell if a line is a match" do
      spec = FormatSpec.new("A.+C")
      spec.matches?("ABBBBBC").should_not be_nil
    end

    it "should tell if a line is not a match" do
      spec = FormatSpec.new("A.+C")
      spec.matches?("ABBBBBX").should be_nil
    end
  end

  describe :parse do
    it "should replace text for a replace action" do
      spec = FormatSpec.new("Replace|A.+C|XY")
      spec.parse("ABBBBBC").should eq "XY"
    end

    it "should use the original text when replacing" do
      spec = FormatSpec.new("Replace|A.+C|X$&Y")
      spec.parse("ABC").should eq "XABCY"
    end
  
    it "should keep text for the keep action" do
      spec = FormatSpec.new("Keep|A.+C|")
      spec.parse("ABC").should eq "ABC"
    end
  
    it "should return empty for an omit action" do
      spec = FormatSpec.new("A.+C")
      spec.parse("ABC").should eq ""
    end
  
    it "should default to an omit action if action unknonwn" do
      spec = FormatSpec.new("XXX|A.+C|X$&Y")
      spec.parse("ABC").should eq ""
    end
  end
end