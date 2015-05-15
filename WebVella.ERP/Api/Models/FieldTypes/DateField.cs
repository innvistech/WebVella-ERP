﻿using Newtonsoft.Json;
using System;

namespace WebVella.ERP.Api.Models
{
    public class DateField : Field
    {
        [JsonProperty(PropertyName = "fieldType")]
        public static FieldType FieldType { get { return FieldType.DateField; } }

        [JsonProperty(PropertyName = "defaultValue")]
        public DateTime? DefaultValue { get; set; }

        [JsonProperty(PropertyName = "format")]
        public string Format { get; set; }

        [JsonProperty(PropertyName = "useCurrentTimeAsDefaultValue")]
        public bool? UseCurrentTimeAsDefaultValue { get; set; }

        public DateField()
        {
        }

        public DateField(Field field) : base(field)
        {
        }
    }

    public class DateFieldMeta : DateField, IFieldMeta
    {
        [JsonProperty(PropertyName = "entityId")]
        public Guid EntityId { get; set; }

        [JsonProperty(PropertyName = "entityName")]
        public string EntityName { get; set; }

        [JsonProperty(PropertyName = "parentFieldName")]
        public string ParentFieldName { get; set; }

        public DateFieldMeta(Guid entityId, string entityName, DateField field, string parentFieldName = null) : base(field)
        {
            EntityId = entityId;
			EntityName = entityName;
			DefaultValue = field.DefaultValue;
			Format = field.Format;
            UseCurrentTimeAsDefaultValue = field.UseCurrentTimeAsDefaultValue;
            ParentFieldName = parentFieldName;
        }
	}
}