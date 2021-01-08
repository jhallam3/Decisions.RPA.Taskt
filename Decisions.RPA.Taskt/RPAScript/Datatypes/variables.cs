using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace RPAScript.Datatypes
{
	

[XmlRoot(ElementName = "VariableValue")]
public class VariableValue
{
	[XmlAttribute(AttributeName = "type", Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
	public string Type { get; set; }
	[XmlText]
	public string Text { get; set; }
}

[XmlRoot(ElementName = "ScriptVariable")]
public class ScriptVariable
{
	[XmlElement(ElementName = "VariableName")]
	public string VariableName { get; set; }
	[XmlElement(ElementName = "VariableValue")]
	public VariableValue VariableValue { get; set; }
}

[XmlRoot(ElementName = "Variables")]
public class Variables
{
	[XmlElement(ElementName = "ScriptVariable")]
	public List<ScriptVariable> ScriptVariable { get; set; }
}

[XmlRoot(ElementName = "Script")]
public class Script
{
	public Variables Variables { get; set; }
	[XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
	public string Xsi { get; set; }
	[XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
	public string Xsd { get; set; }
}

}
