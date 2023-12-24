using System;
using System.IO;
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
        private readonly ILogger<ParkingManagementAPI> _logger;
        private readonly IParkingManager _parkingManager;

        public ParkingManagementAPI(IParkingManager parkingManager, ILogger<ParkingManagementAPI> log)
        {
            _parkingManager = parkingManager;
            _logger = log;
        }

        [FunctionName("RegisterEntry")]
        [OpenApiOperation(operationId: "RegisterEntry", tags: new[] { "Parking Accesses" })]
        [OpenApiParameter(name: "licensePlate", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **License Plate** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> RegisterEntryAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string licensePlate = req.Query["licensePlate"];

            try
            {
                await _parkingManager.RegisterEntryAsync(licensePlate);
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
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string licensePlate = req.Query["licensePlate"];

            try
            {
                await _parkingManager.RegisterExitAsync(licensePlate);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }

            return new OkObjectResult($"Vehicle with license plate: {licensePlate} has just exited the parking.");
        }

        [FunctionName("RegisterOfficialVehicle")]
        [OpenApiOperation(operationId: "RegisterOfficialVehicle", tags: new[] { "Vehicles Registration" })]
        [OpenApiParameter(name: "licensePlate", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **License Plate** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> RegisterOfficialVehicleAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string licensePlate = req.Query["licensePlate"];

            try
            {
                await _parkingManager.RegisterOfficialVehicleAsync(licensePlate);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }

            return new OkObjectResult($"A new official vehicle has just been registered in the system with license plate: {licensePlate}.");
        }

        [FunctionName("RegisterResidentVehicle")]
        [OpenApiOperation(operationId: "RegisterResidentVehicle", tags: new[] { "Vehicles Registration" })]
        [OpenApiParameter(name: "licensePlate", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **License Plate** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> RegisterResidentVehicleAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string licensePlate = req.Query["licensePlate"];

            try
            {
                await _parkingManager.RegisterResidentVehicleAsync(licensePlate);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }

            return new OkObjectResult($"A new resident vehicle has just been registered in the system with license plate: {licensePlate}.");
        }

        [FunctionName("Reset")]
        [OpenApiOperation(operationId: "Reset", tags: new[] { "System Reset" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public IActionResult ResetAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string responseMessage = "A new month has just started. All accesses and counters have been reset.";

            return new OkObjectResult(responseMessage);
        }

        [FunctionName("ResidentsPayments")]
        [OpenApiOperation(operationId: "ResidentsPayments", tags: new[] { "Payments" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public IActionResult ResidentsPaymentsAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string responseMessage = "Generating payment data for residents...";

            return new OkObjectResult(responseMessage);
        }

        [FunctionName("GetAllVehicles")]
        [OpenApiOperation(operationId: "GetAllVehicles", tags: new[] { "Labs" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> GetAllVehiclesAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var vehicles = await _parkingManager.GetAllVehiclesAsync();

            return new OkObjectResult(vehicles);
        }

        [FunctionName("GetVehiclesCount")]
        [OpenApiOperation(operationId: "GetVehiclesCount", tags: new[] { "Labs" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> GetVehiclesCountAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var vehiclesCount = await _parkingManager.GetVehiclesCountAsync();

            return new OkObjectResult(vehiclesCount);
        }
    }
}

