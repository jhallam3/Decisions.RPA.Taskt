using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using DecisionsFramework.Data.DataTypes;
using DecisionsFramework.Data.ORMapper;
using DecisionsFramework.Design.ConfigurationStorage.Attributes;
using DecisionsFramework.Design.Flow;
using DecisionsFramework.Design.Flow.Mapping;
using DecisionsFramework.Design.Properties;
using DecisionsFramework.ServiceLayer.Services.ContextData;
using RPAScript.Datatypes;
using System.Xml.Serialization;
using System.IO;
using System.Xml.XPath;

namespace RPAScript
{
    [Writable]
    [AutoRegisterStep("Specify RPA Script Variables", "RPA")]
    public class RpaScriptStep : ISyncStep, IDataConsumer, INotifyPropertyChanged
    {
        private const string INPUT = "Script Variables";
        private const string PATH_DONE = "Done";
        private const string RESULT_DATA = "Script Variables";


        [PropertyHidden]
        public static string[] ScriptNames
        {
            get
            {
                ORM<RpaScriptEntity> orm = new ORM<RpaScriptEntity>();
                RpaScriptEntity[] rpaScripts = orm.Fetch();
                List<string> scriptNames = new List<string>();

                foreach (var item in rpaScripts)
                {
                    if (!scriptNames.Contains(item.Name))
                    {
                        scriptNames.Add(item.Name);
                    }
                }

                return scriptNames.ToArray();
            }
        }

        [WritableValue]
        private string script;

        [SelectStringEditor("ScriptNames")]
        public string Script
        {
            get
            {

                return script;
            }
            set
            {
                script = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(InputData));
                OnPropertyChanged(nameof(Version));
            }
        }


        [PropertyHidden]
        public string[] VersionNames
        {
            get
            {
                ORM<RpaScriptEntity> orm = new ORM<RpaScriptEntity>();
                RpaScriptEntity[] rpaScripts = orm.Fetch();
                List<string> scriptNames = new List<string>();
                foreach (var item in rpaScripts.Where(x => x.Name == this.script).ToArray())
                {
                    if (!string.IsNullOrEmpty(item.Version))
                    {
                        scriptNames.Add(item.Version);
                    }
                }

                return scriptNames.ToArray();
            }
        }

        [WritableValue]
        private string version;

        [SelectStringEditor("VersionNames")]
        public string Version
        {
            get
            {
                return version;
            }
            set
            {
                version = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(InputData));

            }
        }

        public DataDescription[] InputData
        {
            get
            {
                List<DataDescription> output = new List<DataDescription>();
                if (string.IsNullOrEmpty(Script))
                {
                    return new DataDescription[0];
                }
                if (string.IsNullOrEmpty(Version))
                {
                    return null;
                }
                else
                {
                    string[] variables = GetVariables(Script, Version);

                    Array.ForEach(variables, s => output.Add(new DataDescription(typeof(string), s)));

                    return output.ToArray();
                }

            }
        }

        private string[] GetVariables(string scriptName, string version)
        {
            ORM<RpaScriptEntity> orm = new ORM<RpaScriptEntity>();
            if (string.IsNullOrEmpty(version))
            {
                return null;
            }
            else
            {


                RpaScriptEntity[] rpaScripts = orm.Fetch(new WhereCondition[]
                {
                new FieldWhereCondition("name", QueryMatchType.EqualsWithoutCase, scriptName)
                });
                rpaScripts = rpaScripts.Where(x => x.Version == version).ToArray();
                if (rpaScripts == null)
                {
                    return null;
                }
                else
                {
                    if(rpaScripts.Length >=1)
                        return rpaScripts[0].CommaSeparatedVariables.Split(',');
                    return new string[] { "" };
                }
            }
        }

        public OutcomeScenarioData[] OutcomeScenarios => new[]
        {
            new OutcomeScenarioData(PATH_DONE, new DataDescription(typeof(SimpleKeyValuePair[]), RESULT_DATA))
        };

        public ResultData Run(StepStartData data)
        {
            SimpleKeyValuePair[] scriptVariables = data[INPUT] as SimpleKeyValuePair[];
            List<SimpleKeyValuePair> LSVP = new List<SimpleKeyValuePair>();

            ORM<RpaScriptEntity> orm = new ORM<RpaScriptEntity>();
            RpaScriptEntity[] rpaScripts = orm.Fetch();



            for (var i = 0; i < data.Keys.Count; i++)
            {
                var _simplekeyvaluepair = new SimpleKeyValuePair() { Key = data.Keys.ElementAt(i), Value = data.Values.ElementAt(i).ToString() };
                LSVP.Add(_simplekeyvaluepair);
            }
            scriptVariables = LSVP.ToArray();

            var item = rpaScripts.Where(x => x.Name == script && x.Version == version).FirstOrDefault();
            var rpaFileContents = System.Text.Encoding.Default.GetString(item.Rpafile);

            var variables = new Variables();
            List<ScriptVariable> LScriptVariable = new List<ScriptVariable>();

            foreach (var s in scriptVariables)
            {
                

                
                
                var vv = new VariableValue() { Text = "value", Type = "xsd:string" };
                LScriptVariable.Add(new ScriptVariable() { VariableName = s.Key, VariableValue = new VariableValue() { Text = s.Value, Type = "xsd:string" } });
            }


            ////// get file variables secion 
            ///

            var startofvariables = rpaFileContents.IndexOf("<Variables>");
            var endofvariables = rpaFileContents.IndexOf("</Variables>");

            var partoffile = rpaFileContents.Substring(startofvariables, (endofvariables-startofvariables)+12);


            var reader = new StreamReader(new MemoryStream(item.Rpafile), true);


            


            variables.ScriptVariable = LScriptVariable;

            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            var stringwriter = new System.IO.StringWriter();
            var serializer = new XmlSerializer(typeof(Variables));
            serializer.Serialize(stringwriter, variables);
            var xmlasString =  stringwriter.ToString();
            xmlasString = xmlasString.Replace(" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", string.Empty);
            xmlasString = xmlasString.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", string.Empty);

            var fileforreturn = rpaFileContents.Replace(partoffile, xmlasString);

            LSVP.Add(new SimpleKeyValuePair() { Key = "script", Value = fileforreturn });
            scriptVariables = LSVP.ToArray();

            return new ResultData(PATH_DONE, new[] { new DataPair(RESULT_DATA, scriptVariables) });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}