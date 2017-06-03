using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

using V6Soft.Common.ModelFactory.Constants;
using V6Soft.Common.Utils.TypeExtensions;

namespace V6Soft.Common.ModelFactory.Managers
{
    internal static class XmlParseExtensions
    {
        /* Expected XML document for this model definition:
         <xsd:complexType>
			<xsd:attribute name="UID" type="sqltypes:uniqueidentifier" use="required" />
			<xsd:attribute name="Ma" type="sqltypes:bigint" />
			<xsd:attribute name="Ten">
				<xsd:simpleType>
					<xsd:restriction base="sqltypes:nvarchar">
						<xsd:maxLength value="50" />
					</xsd:restriction>
				</xsd:simpleType>
			</xsd:attribute>
			<xsd:attribute name="TenKhac" use="required">
				<xsd:simpleType>
					<xsd:restriction base="sqltypes:nvarchar" />
				</xsd:simpleType>
			</xsd:attribute>
         </xsd:complexType>
         */

        /* Expectd XML document for model mapping:
            <mappings name="CustomerGroup">
                <group name="BasicFields">
                    <map name="UID" label="UID" />
                    <map name="MaNhom" label="Code" />
                    <map name="Ten" label="Name" />
                    <map name="TinhTrang" label="Status" />
                </group>
            </mappings>         
         */

        public static FieldMapping ParseFieldMapping(this XmlReader xmlReader)
        {
            /* Expected XML format:
                <map name="..." [label="..."] group="..." code="..." />
             * A field without `label` is used by code only, and never be shown on view.
             */
            if (xmlReader == null) { return null; }

            var model = new FieldMapping();
            if (xmlReader.LocalName != Names.Xml.MapElem)
            {
                return null;
            }

            model.DbName = xmlReader.GetAttribute(Names.Xml.NameAttr);
            model.Label = xmlReader.GetAttribute(Names.Xml.LabelAttr);
            model.AppName = xmlReader.GetAttribute(Names.Xml.CodeAttr);
            
            if (string.IsNullOrEmpty(model.AppName))
            {
                throw new MalformedDefinitionException("Expects attribute \"code\" in tag <map>");
            }

            return model;
        }

        public static ModelMap ParseModelMap(this XmlReader xmlReader)
        {
            /*
             * Expected XML format:
                 <mappings name="...">
                   <group label="...">
                        <map name="..." label="..." group="..." code="..." />
                        ...
                   </group>
                 </mappings>
             */
            if (xmlReader == null
                || !xmlReader.ReadToFollowing(Names.Xml.MappingsElem)
                ) 
            { 
                return null; 
            }

            string modelAppName = xmlReader.GetAttribute(Names.Xml.NameAttr);

            var fieldMappings = new List<FieldMapping>();
            FieldMapping map;
            string currentGroup = null;
            var groups = new List<string>();

            while (xmlReader.Read())
            {
                // If current node is <group name="...">
                if (xmlReader.LocalName == Names.Xml.GroupElem && xmlReader.IsStartElement())
                {
                    currentGroup = xmlReader.GetAttribute(Names.Xml.LabelAttr);

                    if (string.IsNullOrEmpty(currentGroup))
                    {
                        throw new MalformedDefinitionException("Expects attribute \"label\" in tag <group>");
                    }

                    groups.Add(currentGroup);
                }
                else // If current node is <map name="..." label="..." group="..." code="..." />
                {
                    while ((map = xmlReader.ParseFieldMapping()) != null)
                    {
                        map.Group = currentGroup;
                        fieldMappings.Add(map);
                        xmlReader.Read();
                    }
                }
            }

            return new ModelMap()
            {
                FieldMappings = fieldMappings,
                FieldGroups = groups.ToArray(),
                NameMapping = new NameMapping() {  AppName = modelAppName }
            };
        }
    
        public static ModelFieldDefinition ParseFieldDefinition(this XmlReader xmlReader)
        {
            /* Expected XML format:
                <xsd:attribute name="Ma" type="sqltypes:bigint" />
	            <xsd:attribute name="Ten" use="required">
                    ...
	            </xsd:attribute>
             */
            if (xmlReader == null) { return null; }

            if (!xmlReader.ReadToFollowing(Names.Xml.AttributeElem))
            {
                return null;
            }

            string fieldName = xmlReader.ParseFieldName();
            Type fieldType = xmlReader.ParseFieldType();
            IList<IFieldConstraint> constraints = 
                xmlReader.ParseFieldConstraints(ref fieldType);

            if (fieldType == null)
            {
                throw new MalformedDefinitionException("Expects attribute \"type\" in tag <xsd:attribute>" +
                    "or attribute \"base\" in tag <xsd:restriction>.");
            }

            var newField = new ModelFieldDefinition()
            {
                Name = fieldName,
                Type = fieldType
            };

            if (constraints != null && constraints.Any())
            {
                newField.Constraints = constraints;
            }

            return newField;
        }

        public static IList<ModelFieldDefinition> ParseFieldDefinitions(this XmlReader xmlReader)
        {
            var fields = new List<ModelFieldDefinition>();
            ModelFieldDefinition newField;
            
            while (null != (newField = xmlReader.ParseFieldDefinition()))
            {
                fields.Add(newField);
            }

            return (fields.Any() ? fields : null);
        }

        public static IList<IFieldConstraint> ParseFieldConstraints(this XmlReader xmlReader,
            ref Type type)
        {
            /* Expected XML format:
	            <xsd:attribute use="required" ...>
		            <xsd:simpleType>
			            <xsd:restriction base="sqltypes:nvarchar" ...>
				            <xsd:maxLength value="50" />
			            </xsd:restriction>
		            </xsd:simpleType>
	            </xsd:attribute>
             */

            if (xmlReader == null ||
                xmlReader.Name != Names.Xml.AttributeElem)
            {
                return null;
            }

            var constraints = new List<IFieldConstraint>();
            string attrValue = xmlReader.GetAttribute(Names.Xml.UseAttr);

            if (!string.IsNullOrEmpty(attrValue))
            {
                constraints.Add(new NotNullFieldConstraint());
            }
            
            // Looks for type in <xsd:restriction base="sqltypes:...">
            if (xmlReader.ReadToDescendant(Names.Xml.RestrictionElem))
            {
                string sqlTypeName = xmlReader.GetAttribute(Names.Xml.BaseAttr);
                if (!string.IsNullOrEmpty(sqlTypeName))
                {
                    type = TypeExtensions.ParseFromXmlSqlType(sqlTypeName);
                }
            }

            if (xmlReader.ReadToDescendant(Names.Xml.MaxLengthElem))
            {
                int maxLength;
                attrValue = xmlReader.GetAttribute(Names.Xml.ValueAttr);
                if (int.TryParse(attrValue, out maxLength))
                {
                    constraints.Add(new LengthConstraint(maxLength));
                }
            }
            
            return (constraints.Any() ? constraints : null);
        }

        
        private static string ParseFieldName(this XmlReader xmlReader)
        {
            if (xmlReader == null ||
                xmlReader.Name != Names.Xml.AttributeElem) 
            { 
                return null; 
            }

            string name = xmlReader.GetAttribute(Names.Xml.NameAttr);
            if (string.IsNullOrEmpty(name))
            {
                throw new MalformedDefinitionException("Expects attribute \"name\" in tag <xsd:attribute>");
            }
            return name;
        }

        private static Type ParseFieldType(this XmlReader xmlReader)
        {
            /* Expected XML format:
                <xsd:attribute type="sqltypes:bigint" ... />
			    
                or
             
                <xsd:attribute ...>
				    <xsd:simpleType>
					    <xsd:restriction base="sqltypes:nvarchar">
						    ...
					    </xsd:restriction>
				    </xsd:simpleType>
			    </xsd:attribute>
             */
            if (xmlReader == null ||
                xmlReader.Name != Names.Xml.AttributeElem)
            {
                return null;
            }

            // Looks for type in <xsd:attribute name="UID" type="sqltypes:....">
            string sqlTypeName = xmlReader.GetAttribute(Names.Xml.TypeAttr);

            if (!string.IsNullOrEmpty(sqlTypeName))
            {
                return TypeExtensions.ParseFromXmlSqlType(sqlTypeName);
            }
            return null;
        }
    }
}
