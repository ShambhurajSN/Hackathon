using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using FnolApiVersion2.O.Repositories;
using Microsoft.AspNetCore.Mvc;
using FnolApiVersion2.O.Data;
using FnolApiVersion2.O.Models.DTO;
using FnolApiVersion2.O.Models.Domains;
using System.IO.Compression;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using FnolApiVersion2.O.Repositories.Services.Fnol;

namespace FinalFnolAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class FnolController : ControllerBase
    {
        private readonly IFnolRepository _repository;

        //Constructor that takes IFnolRepository as parameter
        public FnolController(IFnolRepository repository)
        {
            _repository=repository;
        }
        //Api Endpoint that returns a Case Details by accepting FNOL ID
        //AccessToResource Policy is used for customer to ensure that user dont access other users Cases
        //Policy has access for customer and members of organisation
        [Route("ReturnDetailsOfCaseByFnolID")]
        [HttpGet]
        [Authorize(Policy ="AccessToResource")]
        public async Task<IActionResult> GetFnolDetailsByFnolId(string FnolID)
        {
            var userID = HttpContext.User.FindFirst("userID");
            var records = await _repository.GetFnolByIDAsync(FnolID);
            if(records != null)
            {
                return Ok(records);
            }
            else
            {
                return BadRequest("Enter correct Case ID");
            }
           
        }
        //Api Endpoint that returns whole cases with all respective fields related to the case from Database
        //This Endpoint is only Accessed by manager and admin because it consists ID fields that should not be exposed to Customer
        [Route("ReturnAllFnolDetails")]
        [HttpGet]
        [Authorize(Roles ="manager,admin")]
        public async Task<IActionResult> GetAllFnolDetails()
        {
            var records = await _repository.GetAllFnolDetailsAsync();
            return Ok(records);
        }
        //Api Endpoint that returns whole cases with neccessary details from Database
        //This Endpoint is only Accessed by manager and admin because Customer should not know about others cases
        [Route("ReturnAllCaseDetails")]
        [HttpGet]
        [Authorize(Roles ="manager,admin")]
        public async Task<IActionResult> GetAllOnlyFnolDetails()
        {
            var records = await _repository.GetAllOnlyFnolDetailsAsync();
            return Ok(records);
        }
        //Api Endpoint that is used to take Pictures related to the Incident by taking its Case ID
        //Authorization Policy that allows only to the Employees(employee,admin,manager)
        [Route("SavedIncidentPictures")]
        [HttpGet]
        [Authorize(Policy ="AccessToOrg")]
        public async Task<IActionResult> GetImagesInZipFile(string FnolID)
        {
            var records = await _repository.GetIncidentPicturesByID(FnolID);
            if(records != null)
            {
                List<byte[]> IncidentPictureList = new List<byte[]>();

                IncidentPictureList.Add(records.FrontImage);
                IncidentPictureList.Add(records.BackImage);
                IncidentPictureList.Add(records.LeftImage);
                IncidentPictureList.Add(records.RightImage);
                using (var ms = new MemoryStream())
                {
                    using(var zipArchive = new ZipArchive(ms,ZipArchiveMode.Create,true))
                    {
                        var i =1;
                        foreach(var image in IncidentPictureList)
                        {
                            var entry = zipArchive.CreateEntry("image" + i + ".png", CompressionLevel.Fastest);
                            using (var entryStream = entry.Open())
                            {
                                entryStream.Write(image, 0,image.GetLength(0));
                            }
                            i++;
                        }
                        var file = File(ms.GetBuffer(), "application/zip", "Images.zip");
                        return file;
                    }
                }
            }
            return BadRequest("FnolID not Found or Incorrect");

        }
        //Api EndPoint used to raise an FNOL that takes details like incident details,driver details,vehicle details,policy details and incident pictures 
        //The Policy newCaseAccessOnly ensures that a user doesn't have a active case 
        //returns Case ID
        [Route("AddCaseDetails")]
        [HttpPost]
        [Authorize(Policy = "newCaseAccessOnly")]
        public async Task<IActionResult> AddFnolDetails([FromForm]FnolDto fnolDto)
        {
            var userID = HttpContext.User.FindFirst("userID");
            var FnolIDGenerated= await _repository.AddFnolDetailsToDbAsync(fnolDto,userID);
            return Ok(FnolIDGenerated);
        }
        //Api EndPoint that is used to Update the details that are already in database
        //Only manager and admin has the authorization for updating the details once it was submitted by Customer
        [Route("UpdateCaseDetails")]
        [HttpPut]
        [Authorize(Roles ="manager,admin")]
        public async Task<IActionResult> UpdateFnolDetails(string FnolID,[FromForm]FnolDto fnolDto)
        {

            var result = await _repository.UpdateFnolByIDAsync(FnolID,fnolDto);
            var c = await _repository.GetFnolByIDAsync(FnolID);
            if(result)
            {
                return Ok(c);
            }
            else
            {
                return BadRequest("Not Found");
            }
        }
        //Api Endpoint that is used to delete a closed case
        //Only manager and admin has the authorization for deleting the details once the case was closed
        [Route("DeleteCaseDetails")]
        [HttpDelete]
        [Authorize(Roles ="manager,admin")]
        public async Task<IActionResult> DeleteFnolIDByID(string FnolID)
        {
            var result = await _repository.DeleteFnolByIDAsync(FnolID);
            if(result)
            {
                return Ok("Resource Deleted");
            }
            else
            {
                return BadRequest("Resource Not Found");
            }
        }
        
    }
}