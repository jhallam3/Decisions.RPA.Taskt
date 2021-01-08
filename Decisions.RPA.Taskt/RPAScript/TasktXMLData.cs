using DecisionsFramework.Design.Flow;
using RPAScript.Datatypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.XPath;

namespace RPAScript
{
    [AutoRegisterMethodsOnClass(true, "RPA\\Taskt")]
    public class TasktXMLData
    {

        

        public string[] GetTasktVariablesFromByte(byte[] FileContents)
        {
            List<string> ListOfVariables = new List<string>();

            var reader = new StreamReader(new MemoryStream(FileContents), true);


            var docnav = new XPathDocument(reader);
            var length = 100;
            for (int i = 1; i < length; i++)
            {
                var expression = "/Script/Variables[1]/ScriptVariable[" + i + "]/VariableName[1]/text()";
                var nav = docnav.CreateNavigator();

                var selected = nav.Select(expression);
                var movedforward = selected.MoveNext();
                if (movedforward == true)
                {
                    var output = selected.Current.Value;
                   
                    ListOfVariables.Add(output);
                }

            }

            return ListOfVariables.ToArray();

        }
    }
}
