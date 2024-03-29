﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cross.Cutting.Helper
{
    public class ConstantsHelper
    {
        
    }

    public static class ErrorMessages
    {
        public static readonly string InternalServerError = "Attention! The Service is unavailable.";
        public static readonly string NullArgument = "Attention! Submit all the data necessary to complete the request.";
        public static readonly string KeyNotFound = "Attention! The requested data was not found.";
        public static readonly string NoData = "Attention! There is no data in the database.";
        public static readonly string InvalidPageSelected = "Attention! The selected page does not exist.";
        public static readonly string LaunchApiEndPointError = "Attention! The SpaceDevs API endpoint returned an error.";
        public static readonly string DeserializingContentError = "Attention! An error ocurred when retrieving a JSON data from Space Devs API. Contatc the sys admin to get support.";
        public static readonly string NoDataFromSpaceDevApi = "Attention! There is no data received from Space Devs Api. Check the service disponibility and try again.";
        public static readonly string UpdateJobError = "Attention! The update job has failed.";
        public static readonly string StoredProcedurePublishedRoutineError = "Attention! The update to published stored procedure has failed.";
        public static readonly string ViewNotExists = "Attention! The launch view not exists. Contact the sys admin to get support.";
    }

    public static class SuccessMessages
    {
        public static readonly string DeletedEntity = "The selected entity has deleted with success.";
        public static readonly string PartialImportSuccess = "The Data offset has loaded with success.";
        public static readonly string ImportedDataSuccess = "The Data loading from Space Devs Api was successfull.";
        public static readonly string UpdateJob = "The Data loading from Space Devs Api by the update job was successfull.";
    }

    public static class EndPoints
    {
        public static readonly string TheSpaceDevsLaunchEndPoint = "https://ll.thespacedevs.com/2.2.0/launch/";
    }
}
