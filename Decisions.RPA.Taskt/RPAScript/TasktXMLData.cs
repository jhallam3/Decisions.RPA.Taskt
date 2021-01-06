using DecisionsFramework.Design.Flow;
using RPAScript.Datatypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPAScript
{
    [AutoRegisterMethodsOnClass(true, "Integration\\RPA\\Taskt")]
    public class TasktXMLData
    {

        public TasktDataXMLLine[] GetXMLVariables(string FileContents)
        {

            List<TasktDataXMLLine> LTaskTDataLines = new List<TasktDataXMLLine>();


            //string path = "pingwithvariable.xml";
            //string path = "variables.xml";

            var filescontents = FileContents.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var FileContentLines = filescontents.Where(x => x.Contains("CommandName=\"VariableCommand\"")).ToArray();
            foreach (var s in FileContentLines)
            {
                var result = new TasktDataXMLLine();
                result.TheLine = s;

                var splitthem = s.Split("\" ");
                //variablename 
                var variablename = splitthem.Where(c => c.Contains("v_userVariableName="));
                result.variableName = variablename.ElementAt(0).Split('=').Last().ToString().Trim('"');

                //variablevalue
                var variablevalue = splitthem.Where(c => c.Contains("v_Input=")).FirstOrDefault();
                if (variablevalue == null)
                {
                    result.VariableValue = "";
                }
                else
                {
                    result.VariableValue = variablevalue;
                }


                var id = splitthem.Where(c => c.Contains("CommandID=")).ElementAt(0) + "\"";
                result.id = id;
                LTaskTDataLines.Add(result);
            }

            return LTaskTDataLines.ToArray();
        }
    }
}
