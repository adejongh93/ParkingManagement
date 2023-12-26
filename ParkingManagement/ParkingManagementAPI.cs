using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace ParkingManagement
{
    public class ParkingManagementAPI
    {
        private readonly ILogger<ParkingManagementAPI> logger;
        private readonly IParkingManager parkingManager;

        public ParkingManagementAPI(IParkingManager parkingManager, ILogger<ParkingManagementAPI> logger)
        {
            this.parkingManager = parkingManager;
            this.logger = logger;
        }


        [FunctionName("RegisterEntry")]
        [OpenApiOperation(operationId: "RegisterEntry", tags: new[] { "Parking Accesses" })]
        [OpenApiParameter(name: "licensePlate", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **License Plate** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> RegisterEntryAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req)
        {
            string licensePlate = req.Query["licensePlate"];

            try
            {
                await parkingManager.RegisterEntryAsync(licensePlate);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }

            return new OkObjectResult($"Vehicle with license plate: {licensePlate} has just entered the parking.");
        }


        [FunctionName("RegisterExit")]
        [OpenApiOperation(operationId: "RegisterExit", tags: new[] { "Parking Accesses" })]
        [OpenApiParameter(name: "licensePlate", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **License Plate** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> RegisterExitAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req)
        {
            string licensePlate = req.Query["licensePlate"];

            try
            {
                var invoice = await parkingManager.RegisterExitAsync(licensePlate);
                if (invoice is not null)
                {
                    return new OkObjectResult(invoice);
                }
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }

            return new OkObjectResult($"Vehicle with license plate: {licensePlate} has just exited the parking. No invoice required.");
        }


        [FunctionName("RegisterVehicleAsync")]
        [OpenApiOperation(operationId: "RegisterVehicleAsync", tags: new[] { "Vehicles Registration" })]
        [OpenApiParameter(name: "licensePlate", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **License Plate** parameter")]
        [OpenApiParameter(name: "vehicleType", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Vehicle Type** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> RegisterVehicleAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req)
        {
            string licensePlate = req.Query["licensePlate"];
            string vehicleType = req.Query["vehicleType"];
            string vehicleTypeToUpper = vehicleType.ToUpperInvariant();

            try
            {
                await parkingManager.RegisterVehicleAsync(licensePlate, vehicleTypeToUpper);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }

            return new OkObjectResult($"A new vehicle of type {vehicleTypeToUpper} has just been registered in the system with license plate: {licensePlate}.");
        }


        [FunctionName("Reset")]
        [OpenApiOperation(operationId: "Reset", tags: new[] { "System Reset" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> ResetAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = null)] HttpRequest req)
        {
            await parkingManager.ExecutePartialResetAsync();
                    
            return new OkObjectResult("A new month has just started. All accesses and counters have been reset.");
        }


        [FunctionName("ResidentsPayments")]
        [OpenApiOperation(operationId: "ResidentsPayments", tags: new[] { "Payments" })]
        [OpenApiParameter(name: "fileName", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **File Name** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> ResidentsPaymentsAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {
            string fileName = req.Query["fileName"];

            return await parkingManager.GenerateResidentsPaymentsAsync(fileName);
        }


        [FunctionName("RegisterOfficialVehicle")]
        [OpenApiOperation(operationId: "RegisterOfficialVehicle", tags: new[] { "Labs" })]
        [OpenApiParameter(name: "licensePlate", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **License Plate** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> RegisterOfficialVehicleAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req)
        {
            string licensePlate = req.Query["licensePlate"];

            try
            {
                await parkingManager.RegisterOfficialVehicleAsync(licensePlate);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }

            return new OkObjectResult($"A new official vehicle has just been registered in the system with license plate: {licensePlate}.");
        }


        [FunctionName("RegisterResidentVehicle")]
        [OpenApiOperation(operationId: "RegisterResidentVehicle", tags: new[] { "Labs" })]
        [OpenApiParameter(name: "licensePlate", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **License Plate** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> RegisterResidentVehicleAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req)
        {
            string licensePlate = req.Query["licensePlate"];

            try
            {
                await parkingManager.RegisterResidentVehicleAsync(licensePlate);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }

            return new OkObjectResult($"A new resident vehicle has just been registered in the system with license plate: {licensePlate}.");
        }


        [FunctionName("GetAllVehicles")]
        [OpenApiOperation(operationId: "GetAllVehicles", tags: new[] { "Labs" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> GetAllVehiclesAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {
            var vehicles = await parkingManager.GetAllVehiclesAsync();

            return new OkObjectResult(vehicles);
        }


        [FunctionName("GetAllVehiclesInParking")]
        [OpenApiOperation(operationId: "GetAllVehiclesInParking", tags: new[] { "Labs" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> GetAllVehiclesInParkingAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {
            var vehiclesInParking = await parkingManager.GetAllVehiclesInParkingAsync();

            return new OkObjectResult(vehiclesInParking);
        }


        [FunctionName("GetAllVehiclesStays")]
        [OpenApiOperation(operationId: "GetAllVehiclesStays", tags: new[] { "Labs" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> GetAllVehiclesStaysAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {
            var vehiclesInParking = await parkingManager.GetAllVehicleStaysAsync();

            return new OkObjectResult(vehiclesInParking);
        }


        [FunctionName("GetVehiclesCount")]
        [OpenApiOperation(operationId: "GetVehiclesCount", tags: new[] { "Labs" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> GetVehiclesCountAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {
            var vehiclesCount = await parkingManager.GetVehiclesCountAsync();

            return new OkObjectResult(vehiclesCount);
        }


        [FunctionName("FullReset")]
        [OpenApiOperation(operationId: "FullReset", tags: new[] { "Labs" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> FullResetAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = null)] HttpRequest req)
        {
            await parkingManager.ExecuteFullResetAsync();

            return new OkObjectResult("Full System Reset done.");
        }
    }
}

