﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using WebVella.ERP.Api;
using WebVella.ERP.Api.Models;
using System.Net;
using Newtonsoft.Json.Linq;
using WebVella.ERP.Utilities.Dynamic;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebVella.ERP.Web.Controllers
{
    public partial class ApiDevelopersController : ApiControllerBase
    {
        public ApiDevelopersController(IERPService service) : base(service)
        {
        }

		[AcceptVerbs(new[] { "POST" }, Route = "api/v1/en_US/meta/developers/query/create-sample-query-data-structure")]
		public IActionResult CreateSampleQueryDataStructure()
		{
          
            EntityManager em = new EntityManager(service.StorageService);
            EntityRelationManager rm = new EntityRelationManager(service.StorageService);
            
           // create relation
          var userEntity = em.ReadEntity("user");
          var areaEntity = em.ReadEntity("area");
          var roleEntity = em.ReadEntity("role");

          EntityRelation create = new EntityRelation();
          create.Name = "area_user_create_by";
          create.Label = "area_user_create_by label";
          create.Description = "area_user_create_by description";
          create.System = true;
          create.OriginEntityId = areaEntity.Object.Id.Value;
          create.OriginFieldId = areaEntity.Object.Fields.Single(x=>x.Name == "created_by").Id.Value;

          create.TargetEntityId = userEntity.Object.Id.Value;
          create.TargetFieldId = userEntity.Object.Fields.Single(x => x.Name == "id").Id.Value;

          return DoResponse(rm.Create(create));
          

            return DoResponse(rm.Read("area_user_create_by"));

            // Guid recId = Guid.NewGuid();

            // EntityRecord record = new EntityRecord();
            // record["id"] = recId;
            // record["email"] = "test email";
            // RecordManager rm = new RecordManager(service);
            // rm.CreateRecord("user", record);



            // var queryObject = EntityQuery.QueryEQ("id", recId );
            // EntityQuery query = new EntityQuery("user", "id,email", queryObject);
            // var result = rm.Find(query);

            //return Json(result);
        }

        [AcceptVerbs(new[] { "POST" }, Route = "api/v1/en_US/meta/developers/query/execute-sample-query")]
		public IActionResult ExecuteSampleQuery()
		{
			QueryResponse response = new QueryResponse();
			response.Success = true;
			response.Timestamp = DateTime.UtcNow;
			response.Message = "ExecuteSampleQuery:DONE";
			return Json(response);
		}



		//[AcceptVerbs(new[] { "POST" }, Route = "api/v1/en_US/meta/entity")]
		//public IActionResult CreateEntity([FromBody]EntityRecord obj)
		//{
		//	var h = obj.GetProperties();

		//	var t = Json(obj);


		//	return Json(obj);
		//}

		
	}
}