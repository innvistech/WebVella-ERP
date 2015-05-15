﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using WebVella.ERP.Storage;

namespace WebVella.ERP.Api.Models
{
    public class InputEntity
    {
        [JsonProperty(PropertyName = "id")]
        public Guid? Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }

        [JsonProperty(PropertyName = "labelPlural")]
        public string LabelPlural { get; set; }

        [JsonProperty(PropertyName = "system")]
        public bool? System { get; set; }

        [JsonProperty(PropertyName = "iconName")]
        public string IconName { get; set; }

        [JsonProperty(PropertyName = "weight")]
        public decimal? Weight { get; set; }

        [JsonProperty(PropertyName = "recordPermissions")]
        public RecordPermissions RecordPermissions { get; set; }
    }

    public class Entity
    {
        [JsonProperty(PropertyName = "id")]
        public Guid? Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }

        [JsonProperty(PropertyName = "labelPlural")]
        public string LabelPlural { get; set; }

        [JsonProperty(PropertyName = "system")]
        public bool? System { get; set; }

        [JsonProperty(PropertyName = "iconName")]
        public string IconName { get; set; }

        [JsonProperty(PropertyName = "weight")]
        public decimal? Weight { get; set; }

        [JsonProperty(PropertyName = "recordPermissions")]
        public RecordPermissions RecordPermissions { get; set; }

        [JsonProperty(PropertyName = "fields")]
        public List<Field> Fields { get; set; }

        [JsonProperty(PropertyName = "recordsLists")]
        public List<RecordsList> RecordsLists { get; set; }

        [JsonProperty(PropertyName = "recordViewLists")]
        public List<RecordView> RecordViewLists { get; set; }

        public Entity()
        {

        }

        public Entity(InputEntity entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Label = entity.Label;
            LabelPlural = entity.LabelPlural;
            System = entity.System.Value;
            IconName = entity.IconName;
            Weight = entity.Weight;
            RecordPermissions = entity.RecordPermissions;
            if (RecordPermissions == null)
                RecordPermissions = new RecordPermissions();
        }
    }

    public class RecordPermissions
    {
        [JsonProperty(PropertyName = "canRead")]
        public List<Guid> CanRead { get; set; }

        [JsonProperty(PropertyName = "canCreate")]
        public List<Guid> CanCreate { get; set; }

        [JsonProperty(PropertyName = "canUpdate")]
        public List<Guid> CanUpdate { get; set; }

        [JsonProperty(PropertyName = "canDelete")]
        public List<Guid> CanDelete { get; set; }

        public RecordPermissions()
        {
            CanRead = new List<Guid>();
            CanCreate = new List<Guid>();
            CanUpdate = new List<Guid>();
            CanDelete = new List<Guid>();
        }
    }

    public class EntityList
    {
        [JsonProperty(PropertyName = "entities")]
        public List<Entity> Entities { get; set; }

        public EntityList()
        {
            Entities = new List<Entity>();
        }
    }

    public class EntityResponse : BaseResponseModel
    {
        [JsonProperty(PropertyName = "object")]
        public Entity Object { get; set; }
    }

    public class EntityListResponse : BaseResponseModel
    {
        [JsonProperty(PropertyName = "object")]
        public EntityList Object { get; set; }
    }
}