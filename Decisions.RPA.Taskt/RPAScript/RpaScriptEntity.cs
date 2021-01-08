using System;
using System.Runtime.Serialization;
using DecisionsFramework.Data.DataTypes;
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
        
        private byte[] rpafile;
       

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
        public byte[] Rpafile
        {
            get => rpafile;
            set => rpafile = value;
        }

    }
}