using System;
using System.Collections.Generic;
using System.Text;

namespace RPAScript.Datatypes
{
    class RPATask
    {
    }

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Parameters
    {

        private ParametersRpatask rpataskField;

        /// <remarks/>
        public ParametersRpatask rpatask
        {
            get
            {
                return this.rpataskField;
            }
            set
            {
                this.rpataskField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ParametersRpatask
    {

        private string customLongDescriptionField;

        private string customShortDescriptionField;

        private bool deletedField;

        private string deletedByField;

        private System.DateTime deletedOnField;

        private string extensionIdField;

        private string entityIdField;

        private string entityTypeNameField;

        private bool isPreferredField;

        private string[] variablesField;

        private ParametersRpataskTasktDataXMLLine[] tasktDataXMLLineToReplaceField;

        private string descriptionField;

        private string nameField;

        private byte versionField;

        private string workWithField;

        private ParametersRpataskRPAScript rPAScriptField;

        private ParametersRpataskFileContent fileContentField;

        /// <remarks/>
        public string CustomLongDescription
        {
            get
            {
                return this.customLongDescriptionField;
            }
            set
            {
                this.customLongDescriptionField = value;
            }
        }

        /// <remarks/>
        public string CustomShortDescription
        {
            get
            {
                return this.customShortDescriptionField;
            }
            set
            {
                this.customShortDescriptionField = value;
            }
        }

        /// <remarks/>
        public bool Deleted
        {
            get
            {
                return this.deletedField;
            }
            set
            {
                this.deletedField = value;
            }
        }

        /// <remarks/>
        public string DeletedBy
        {
            get
            {
                return this.deletedByField;
            }
            set
            {
                this.deletedByField = value;
            }
        }

        /// <remarks/>
        public System.DateTime DeletedOn
        {
            get
            {
                return this.deletedOnField;
            }
            set
            {
                this.deletedOnField = value;
            }
        }

        /// <remarks/>
        public string ExtensionId
        {
            get
            {
                return this.extensionIdField;
            }
            set
            {
                this.extensionIdField = value;
            }
        }

        /// <remarks/>
        public string EntityId
        {
            get
            {
                return this.entityIdField;
            }
            set
            {
                this.entityIdField = value;
            }
        }

        /// <remarks/>
        public string EntityTypeName
        {
            get
            {
                return this.entityTypeNameField;
            }
            set
            {
                this.entityTypeNameField = value;
            }
        }

        /// <remarks/>
        public bool IsPreferred
        {
            get
            {
                return this.isPreferredField;
            }
            set
            {
                this.isPreferredField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public string[] Variables
        {
            get
            {
                return this.variablesField;
            }
            set
            {
                this.variablesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("TasktDataXMLLine", IsNullable = false)]
        public ParametersRpataskTasktDataXMLLine[] TasktDataXMLLineToReplace
        {
            get
            {
                return this.tasktDataXMLLineToReplaceField;
            }
            set
            {
                this.tasktDataXMLLineToReplaceField = value;
            }
        }

        /// <remarks/>
        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public byte Version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }

        /// <remarks/>
        public string WorkWith
        {
            get
            {
                return this.workWithField;
            }
            set
            {
                this.workWithField = value;
            }
        }

        /// <remarks/>
        public ParametersRpataskRPAScript RPAScript
        {
            get
            {
                return this.rPAScriptField;
            }
            set
            {
                this.rPAScriptField = value;
            }
        }

        /// <remarks/>
        public ParametersRpataskFileContent FileContent
        {
            get
            {
                return this.fileContentField;
            }
            set
            {
                this.fileContentField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ParametersRpataskTasktDataXMLLine
    {

        private string customLongDescriptionField;

        private string customShortDescriptionField;

        private string theLineField;

        private string idField;

        private string variableNameField;

        private string variableValueField;

        /// <remarks/>
        public string CustomLongDescription
        {
            get
            {
                return this.customLongDescriptionField;
            }
            set
            {
                this.customLongDescriptionField = value;
            }
        }

        /// <remarks/>
        public string CustomShortDescription
        {
            get
            {
                return this.customShortDescriptionField;
            }
            set
            {
                this.customShortDescriptionField = value;
            }
        }

        /// <remarks/>
        public string TheLine
        {
            get
            {
                return this.theLineField;
            }
            set
            {
                this.theLineField = value;
            }
        }

        /// <remarks/>
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public string variableName
        {
            get
            {
                return this.variableNameField;
            }
            set
            {
                this.variableNameField = value;
            }
        }

        /// <remarks/>
        public string VariableValue
        {
            get
            {
                return this.variableValueField;
            }
            set
            {
                this.variableValueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ParametersRpataskRPAScript
    {

        private string customLongDescriptionField;

        private string customShortDescriptionField;

        private string nameField;

        private string versionField;

        private string commaSeparatedVariablesField;

        /// <remarks/>
        public string CustomLongDescription
        {
            get
            {
                return this.customLongDescriptionField;
            }
            set
            {
                this.customLongDescriptionField = value;
            }
        }

        /// <remarks/>
        public string CustomShortDescription
        {
            get
            {
                return this.customShortDescriptionField;
            }
            set
            {
                this.customShortDescriptionField = value;
            }
        }

        /// <remarks/>
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public string Version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }

        /// <remarks/>
        public string CommaSeparatedVariables
        {
            get
            {
                return this.commaSeparatedVariablesField;
            }
            set
            {
                this.commaSeparatedVariablesField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ParametersRpataskFileContent
    {

        private string idField;

        private string fileNameField;

        private string contentsField;

        /// <remarks/>
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public string FileName
        {
            get
            {
                return this.fileNameField;
            }
            set
            {
                this.fileNameField = value;
            }
        }

        /// <remarks/>
        public string Contents
        {
            get
            {
                return this.contentsField;
            }
            set
            {
                this.contentsField = value;
            }
        }
    }


}
