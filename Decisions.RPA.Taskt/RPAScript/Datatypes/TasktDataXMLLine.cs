using DecisionsFramework.Data.ORMapper;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace RPAScript.Datatypes
{
    [ORMEntity]
    [DataContract]
    public class TasktDataXMLLine : BaseORMEntity
    {
        [ORMPrimaryKeyField]
        private string TasktDataXMLLineId;

        [DataMember]
        public string TheLine { get; set; }
        [DataMember]

        public string id { get; set; }
        [DataMember]

        public string variableName { get; set; }
        [DataMember]

        public string VariableValue { get; set; }

    }
}
