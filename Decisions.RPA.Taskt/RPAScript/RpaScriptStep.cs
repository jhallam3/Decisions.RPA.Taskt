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
using DecisionsFramework.Design.Flow.Service.Execution;
namespace RPAScript
{
    [Writable]
    [AutoRegisterStep("Specify RPA Script Variables", "RPA")]
    public class RpaScriptStep : ISyncStep, IDataConsumer, INotifyPropertyChanged
    {
        private const string INPUT = "Script Variables";
        private const string PATH_DONE = "Done";
        private const string RESULT_DATA = "Taskt Response";


        [PropertyHidden]
        public static string[] ScriptNames
        {
            get
            {
                ORM<RpaScriptEntity> orm = new ORM<RpaScriptEntity>();
                RpaScriptEntity[] rpaScripts = orm.Fetch();
                List<string> scriptNames = new List<string>();

                foreach (RpaScriptEntity item in rpaScripts)
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

        [WritableValue]
        private string decisionsServer;

        [StringEditor]
        public string DecisionsServer
        {
            get
            {

                return decisionsServer;
            }
            set
            {
                decisionsServer = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(InputData));
                OnPropertyChanged(nameof(Version));
            }
        }

        [WritableValue]
        private string tasktServer;

        [StringEditor]
        public string TasktServer
        {
            get
            {

                return tasktServer;
            }
            set
            {
                tasktServer = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(InputData));
                OnPropertyChanged(nameof(Version));
            }
        }

        [WritableValue]
        private bool awaitResponse;

        [BooleanEditor]
        public bool AwaitResponse
        {
            get
            {

                return awaitResponse;
            }
            set
            {
                awaitResponse = value;
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
                foreach (RpaScriptEntity item in rpaScripts.Where(x => x.Name == this.script).ToArray())
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
                    if (rpaScripts.Length >= 1)
                        return rpaScripts[0].CommaSeparatedVariables.Split(',');
                    return new string[] { "" };
                }
            }
        }

        public OutcomeScenarioData[] OutcomeScenarios => new[]
        {
            new OutcomeScenarioData(PATH_DONE, new DataDescription(typeof(String), RESULT_DATA))
        };

        public ResultData Run(StepStartData data)
        {
            SimpleKeyValuePair[] scriptVariables = data[INPUT] as SimpleKeyValuePair[];
            List<SimpleKeyValuePair> LSVP = new List<SimpleKeyValuePair>();

            ORM<RpaScriptEntity> orm = new ORM<RpaScriptEntity>();
            RpaScriptEntity[] rpaScripts = orm.Fetch();



            for (int i = 0; i < data.Keys.Count; i++)
            {
                SimpleKeyValuePair _simplekeyvaluepair = new SimpleKeyValuePair() { Key = data.Keys.ElementAt(i), Value = data.Values.ElementAt(i).ToString() };
                LSVP.Add(_simplekeyvaluepair);
            }
            scriptVariables = LSVP.ToArray();

            RpaScriptEntity item = rpaScripts.Where(x => x.Name == script && x.Version == version).FirstOrDefault();
            string rpaFileContents = System.Text.Encoding.Default.GetString(item.Rpafile);

            Variables variables = new Variables();
            List<ScriptVariable> LScriptVariable = new List<ScriptVariable>();

            foreach (SimpleKeyValuePair s in scriptVariables)
            {




                VariableValue vv = new VariableValue() { Text = "value", Type = "xsd:string" };
                LScriptVariable.Add(new ScriptVariable() { VariableName = s.Key, VariableValue = new VariableValue() { Text = s.Value, Type = "xsd:string" } });
            }


            
            //replace variables section of the document
            int startofvariables = rpaFileContents.IndexOf("<Variables>");
            int endofvariables = rpaFileContents.IndexOf("</Variables>");

            string partoffile = rpaFileContents.Substring(startofvariables, (endofvariables - startofvariables) + 12);

    


            variables.ScriptVariable = LScriptVariable;

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            StringWriter stringwriter = new System.IO.StringWriter();
            XmlSerializer serializer = new XmlSerializer(typeof(Variables));
            serializer.Serialize(stringwriter, variables);
            string xmlasString = stringwriter.ToString();
            xmlasString = xmlasString.Replace(" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", string.Empty);
            xmlasString = xmlasString.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", string.Empty);

           
            string TasktFileToRun = rpaFileContents.Replace(partoffile, xmlasString);

           
            
            // Run flow
            List<DataPair> LDP = new List<DataPair>();
            LDP.Add(new DataPair() { Name = "AwaitResult", OutputValue = awaitResponse });
            LDP.Add(new DataPair() { Name = "FileContentsWithChangedParameters", OutputValue = TasktFileToRun.ToString() });
            LDP.Add(new DataPair() { Name = "TasktServerIP", OutputValue = tasktServer });
            LDP.Add(new DataPair() { Name = "DecisionsServerIP", OutputValue = decisionsServer });

            //var Flow_LoadFileContents = FlowEngine.Start("513cc4fb-49e2-11eb-a335-0800277026be", LDP.ToArray());
            FlowCompletedInstruction flowCompletedInstruction = FlowEngine.StartSyncFlow("513cc4fb-49e2-11eb-a335-0800277026be", LDP.ToArray());

            List<SimpleKeyValuePair> ReturnArray = new List<SimpleKeyValuePair>();
           
            ReturnArray.Add(new SimpleKeyValuePair() { Key = "Result", Value = flowCompletedInstruction.ResultData.ElementAt(0).ToString()});

            Dictionary<string, object> resultData = new Dictionary<string, object>();
            resultData.Add("Taskt Response", flowCompletedInstruction.ResultData.ElementAt(0).OutputValue.ToString());
            return new ResultData(PATH_DONE, resultData);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}