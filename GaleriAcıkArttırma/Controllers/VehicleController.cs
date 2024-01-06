using BusinessLayer.Abstraction;
using BusinessLayer.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GaleriAcıkArttırma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleservice;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public VehicleController(IVehicleService vehicleService, IWebHostEnvironment _webHostEnvironment)
        {
            _webHostEnvironment = _webHostEnvironment;
            _vehicleservice = vehicleService;
        }


        //Task<ApiResponse> UpdateVehicleResponse(int vehicleId, UpdateVehicleDTO model);
        //Task<ApiResponse> DeleteVehicleResponse(int vehicleId);
        //Task<ApiResponse> GetVehicleById(int vehicleId);
        //Task<ApiResponse> ChangeVehicleStatus(int vehicleId);

        [HttpPost("CreateVehicle")]
        public async Task<IActionResult> AddVehicle([FromForm]CreateVehicleDTO model)
        {
            if (ModelState.IsValid)
            {
                if(model.File == null || model.File.Length == 0)
                {
                    return BadRequest();
                }
                string uploadsFolder = Path.Combine(_webHostEnvironment.ContentRootPath, "Images");
                string fileName = $"{Guid.NewGuid()}{Path.GetExtension(model.File.FileName)}";
                string filePath = Path.Combine(uploadsFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.File.CopyToAsync(fileStream);
                }
                model.Image = fileName;

                var result = await _vehicleservice.CreateVehicle(model);
                if (result.İsSuccess)
                {
                    return Ok(result);
                }
            }
            return BadRequest();
        }
        [HttpGet("GetVehicle")]
        public async Task<IActionResult> GetAllVehicle()
        {
            var vehicles = await _vehicleservice.GetVehicle();
            return Ok(vehicles);
        }
        [HttpPut("UpdateVehicle")]
        public async Task<IActionResult> UpdateVehicle([FromRoute] int vehicleId, UpdateVehicleDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _vehicleservice.UpdateVehicleResponse(vehicleId, model);
                if (result.İsSuccess)
                {
                    return Ok(result);
                }
            }
            return BadRequest();
        }
        [HttpDelete("{vehicleId}")]
        public async Task<IActionResult> DeleteVehicle([FromRoute] int vehicleId)
        {
            var result = await _vehicleservice.DeleteVehicleResponse(vehicleId);
            if (result.İsSuccess)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("{vehicleId}")]
        public async Task<IActionResult> GetVehicleById([FromRoute]int vehicleId)
        {
            var result = await _vehicleservice.GetVehicleById(vehicleId);
            if (result.İsSuccess)

            {
                return Ok(result);
            }
            return BadRequest();    
        }
        [HttpPut("{vehicleId}")]
        public async Task<IActionResult> ChangeStatus([FromRoute] int vehicleId)
        {
            var result = await _vehicleservice.ChangeVehicleStatus(vehicleId);
            if (result.İsSuccess)

            {
                return Ok(result);
            }
            return BadRequest();
        }

    }
}
