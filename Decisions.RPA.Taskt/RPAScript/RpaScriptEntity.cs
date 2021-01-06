using System;
using System.Runtime.Serialization;
using DecisionsFramework.Data.ORMapper;

namespace RPAScript
{
    [ORMEntity]
    [DataContract]
    public class RpaScriptEntity : BaseORMEntity
    {
        [ORMPrimaryKeyField]
        private string rpaScriptEntityId;

        [ORMField]
        private string name;


        [ORMField]
        private string version;

        [ORMField]
        private string commaSeparatedVariables;

        [ORMField]
        private string commaSeparatedVersions;

        [DataMember]
        public string Name
        {
            get => name;
            set => name = value;
        }

        [DataMember]
        public string Version
        {
            get => version;
            set => version = value;
        }

        [DataMember]
        public string CommaSeparatedVariables
        {
            get => commaSeparatedVariables;
            set => commaSeparatedVariables = value;
        }

        [DataMember]
        public string CommaSeparatedVersions
        {
            get => commaSeparatedVersions;
            set => commaSeparatedVersions = value;
        }
    }
}