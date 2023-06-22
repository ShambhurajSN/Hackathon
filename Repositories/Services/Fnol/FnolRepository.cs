using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FnolApiVersion2.O.Data;
using FnolApiVersion2.O.Models.DTO;
using FnolApiVersion2.O.Models.Domains;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FnolApiVersion2.O.Repositories.Services.Fnol
{
    public class FnolRepository : IFnolRepository
    {
        private readonly FnolDbContext _context;
        private readonly IMapper _mapper;

        public FnolRepository(FnolDbContext context,IMapper mapper)
        {
            _context=context;
            _mapper=mapper;
        }
        //Method used for generating FNOL ID
        public string GenerateFNOLID()
        {
            string num ="123456789";
            int len = num.Length;
            string ID = string.Empty;
            int IDdigit = 5;
            string finaldigit;
            int getindex;
            string fnolID = "FNOL-";
            for(int i=0;i < IDdigit;i++)
            {
                do{
                    getindex = new Random().Next(0,len);
                    finaldigit = num.ToCharArray()[getindex].ToString();
                }
                while(ID.IndexOf(finaldigit) != -1);
                ID += finaldigit;
            }
            return fnolID+ID;
        }
        //Method used for generating Incident ID
        public string GenerateIncidentID()
        {
            string num ="123456789";
            int len = num.Length;
            string ID = string.Empty;
            int IDdigit = 4;
            string finaldigit;
            int getindex;
            string incidentID = "INC-";
            for(int i=0;i < IDdigit;i++)
            {
                do{
                    getindex = new Random().Next(0,len);
                    finaldigit = num.ToCharArray()[getindex].ToString();
                }
                while(ID.IndexOf(finaldigit) != -1);
                ID += finaldigit;
            }
            return incidentID+ID;
        }
        //Method used for generating Driver ID
        public string GenerateDriverID()
        {
            string num ="123456789";
            int len = num.Length;
            string ID = string.Empty;
            int IDdigit = 4;
            string finaldigit;
            int getindex;
            string driverID = "DRIVER-";
            for(int i=0;i < IDdigit;i++)
            {
                do{
                    getindex = new Random().Next(0,len);
                    finaldigit = num.ToCharArray()[getindex].ToString();
                }
                while(ID.IndexOf(finaldigit) != -1);
                ID += finaldigit;
            }
            return driverID+ID;
        }
        //Method used for generating Vehicle ID
        public string GenerateVehicleID()
        {
            string num ="123456789";
            int len = num.Length;
            string ID = string.Empty;
            int IDdigit = 4;
            string finaldigit;
            int getindex;
            string vehicleID = "VehicleCase-";
            for(int i=0;i < IDdigit;i++)
            {
                do{
                    getindex = new Random().Next(0,len);
                    finaldigit = num.ToCharArray()[getindex].ToString();
                }
                while(ID.IndexOf(finaldigit) != -1);
                ID += finaldigit;
            }
            return vehicleID+ID;
        }
        //Method used for generating Picture ID
        public string GeneratePicID()
        {
            string num ="123456789";
            int len = num.Length;
            string ID = string.Empty;
            int IDdigit = 4;
            string finaldigit;
            int getindex;
            string picID = "Pic-";
            for(int i=0;i < IDdigit;i++)
            {
                do{
                    getindex = new Random().Next(0,len);
                    finaldigit = num.ToCharArray()[getindex].ToString();
                }
                while(ID.IndexOf(finaldigit) != -1);
                ID += finaldigit;
            }
            return picID+ID;
        }
        //Method used for conversion of file to byte array
        public byte[] ImageToByteArray(IFormFile file)
        {
                using(var stream = new MemoryStream())
                {
                    file.CopyTo(stream);
                    
                    return stream.ToArray();
                }
        }
        //Method used for Conversion of Fnol Dto to FnolDetailsDto
        public FnolDetailsDto ConversionOfFnolToFnolDetailsDto(FnolDto fnolDto)
        {
            return _mapper.Map<FnolDetailsDto>(fnolDto);
        }
        //Method used for Conversion of Fnol Dto to IncidentDetailsDto
        public IncidentDetailsDto ConversionOfFnolToIncidentDetailsDto(FnolDto fnolDto)
        {
            return _mapper.Map<IncidentDetailsDto>(fnolDto);
        }
        //Method used for Conversion of Fnol Dto to DriverDetailsDto
        public DriverDetailsDto ConversionOfFnolToDriverDetailsDto(FnolDto fnolDto)
        {
            return _mapper.Map<DriverDetailsDto>(fnolDto);
        }
        //Method used for Conversion of Fnol Dto to VehicleDetailsDto
        public VehicleDetailsDto ConversionOfFnolToVehicleDetailsDto(FnolDto fnolDto)
        {
            return _mapper.Map<VehicleDetailsDto>(fnolDto);
        }
        //Method used for Conversion of Fnol Dto to IncidentPictureDto
        public IncidentPictureDto ConversionOfFnolToIncidentPictureDto(FnolDto fnolDto)
        {
            var incidentPictureDto = new IncidentPictureDto();
            if(fnolDto.FrontImage != null)
            {
                    incidentPictureDto.FrontImage=ImageToByteArray(fnolDto.FrontImage);
            }
            if(fnolDto.BackImage != null)
            {
                    incidentPictureDto.BackImage=ImageToByteArray(fnolDto.BackImage);
                    
            }
            if(fnolDto.LeftImage != null)
            {
                    incidentPictureDto.LeftImage=ImageToByteArray(fnolDto.LeftImage);
                    
            }
            if(fnolDto.RightImage != null)
            {
                    incidentPictureDto.RightImage=ImageToByteArray(fnolDto.RightImage);
                    
            }
            return incidentPictureDto;
        }
        //Method that generated list of FnolDetailsAdminDto
        public async Task<List<FnolDetailsAdminDto>> GetAllFnolDetailsAsync()
        {
            List<FnolDetailsAdminDto> FnolDetails = new List<FnolDetailsAdminDto>();
            FnolDetailsAdminDto fnolDetails;
            IncidentDetails incidentDetails;
            DriverDetails driverDetails;
            VehicleDetails vehicleDetails;
            foreach(var item in await _context.fnolDetails.ToListAsync())
            {
                if(item != null)
                {
                    fnolDetails = new FnolDetailsAdminDto();
                    fnolDetails.FnolID=item.FnolID;
                    fnolDetails.PolicyID=item.PolicyID;
                    fnolDetails.ReporterName=item.ReporterName;
                    fnolDetails.LossDate=item.ReportedDate;
                    incidentDetails = GetIncidentDetails(item.FnolID);
                    fnolDetails.IncidentID=incidentDetails.IncidentID;
                    fnolDetails.CauseOfIncident=incidentDetails.CauseOfIncident;
                    fnolDetails.DamagedParts=incidentDetails.DamagedParts;
                    fnolDetails.AddressOfIncident=incidentDetails.AddressOfIncident;
                    fnolDetails.IncidentDate=incidentDetails.IncidentDate;
                    fnolDetails.Description= incidentDetails.Description;
                    fnolDetails.Landmark = incidentDetails.Landmark;
                    driverDetails= GetDriverDetails(incidentDetails.IncidentID);
                    fnolDetails.DriverID=driverDetails.DriverID;
                    fnolDetails.DriverName=driverDetails.DriverName;
                    fnolDetails.DriverLicenseType=driverDetails.DriverLicenseType;
                    fnolDetails.DriverLicenseNumber=driverDetails.DriverLicenseNumber;
                    vehicleDetails= GetVehicleDetails(driverDetails.DriverID);
                    fnolDetails.VehicleId=vehicleDetails.VehicleID;
                    fnolDetails.VehicleMaker=vehicleDetails.VehicleMaker;
                    fnolDetails.VehicleType=vehicleDetails.VehicleType;
                    fnolDetails.VehicleModel=vehicleDetails.VehicleModel;
                    fnolDetails.VehicleRegNumber=vehicleDetails.VehicleRegNumber;
                    fnolDetails.RCNumber=vehicleDetails.RCNumber;
                    fnolDetails.PictureID = GetIncidentPictureID(incidentDetails.IncidentID);
                    FnolDetails.Add(fnolDetails);

                }
                
            }
            return FnolDetails;
        }
        //Method that generates IncidentPictureModel from IncidentID
        public IncidentPicture PicturesOfIncidents(string IncidentID)
        {
            var incidentPicture = _context.incidentPictures.Where(x => x.IncidentID == IncidentID).Select(x => new IncidentPicture()
            {
                FrontImage=x.FrontImage,
                BackImage=x.BackImage,
                LeftImage=x.LeftImage,
                RightImage=x.RightImage
            }).FirstOrDefault();
            return incidentPicture;
        }
        //Method that generates file from the byte array data
        public FileStreamResult arrayToPic(byte[] binaryPicture)
        {
            MemoryStream ms = new MemoryStream(binaryPicture);
            Stream stream  = ms;
            string mimeType = "image/png";
            return new FileStreamResult(stream, mimeType)
            {
                FileDownloadName = "image.png"
            };
        }
        public string GetIncidentPictureID(string IncidentID)
        {
            var incidentPictures = _context.incidentPictures.Where(x=> x.IncidentID == IncidentID).Select( x=> new IncidentPicture()
            {
                IncidentID = IncidentID,
                PictureID=x.PictureID,
                FrontImage=x.FrontImage,
                BackImage=x.BackImage,
                LeftImage=x.LeftImage,
                RightImage=x.RightImage

            }).FirstOrDefault();
            return incidentPictures.PictureID;
        }
        //Method that returns VehicleDetails Related to Driver
        public VehicleDetails GetVehicleDetails(string DriverID)
        {
            var vehicleDetails = _context.vehicleDetails.Where(x => x.DriverID == DriverID).Select(x => new VehicleDetails()
            {
                DriverID =DriverID,
                VehicleID=x.VehicleID,
                VehicleMaker=x.VehicleMaker,
                VehicleModel=x.VehicleModel,
                VehicleType=x.VehicleType,
                VehicleRegNumber=x.VehicleRegNumber,
                RCNumber=x.RCNumber
                
            }).FirstOrDefault();

            return vehicleDetails;
        }
        //Method that returns DriverDetails Related to Incident
        public DriverDetails GetDriverDetails(string IncidentID)
        {
            var driverDetails = _context.driverDetails.Where(x=> x.IncidentID == IncidentID).Select(x=> new DriverDetails()
            {
                IncidentID = IncidentID,
                DriverID=x.DriverID,
                DriverName=x.DriverName,
                DriverLicenseNumber=x.DriverLicenseNumber,
                DriverLicenseType=x.DriverLicenseType
            }).FirstOrDefault();
            return driverDetails;
        }
        //Method that returns IncidentDetails Related to Fnol
        public  IncidentDetails GetIncidentDetails(string FnolID)
        {
            var incidentDetails =_context.incidentDetails.Where(x => x.FnolID == FnolID).Select(x => new IncidentDetails()
            {
                FnolID=FnolID,
                IncidentID = x.IncidentID,
                CauseOfIncident = x.CauseOfIncident,
                DamagedParts = x.DamagedParts,
                AddressOfIncident = x.AddressOfIncident,
                Description = x.Description,
                Landmark = x.Landmark,
                IncidentDate = x.IncidentDate
            }).FirstOrDefault();
            return incidentDetails;
        }
        //Method that adds fnol details to Database
        public async Task<string> AddFnolDetailsToDbAsync(FnolDto fnolDto,Claim userID)
        {
            var FnolDetailsRecords = _mapper.Map<FnolDetails>(ConversionOfFnolToFnolDetailsDto(fnolDto));
            FnolDetailsRecords.FnolID=GenerateFNOLID();
           FnolDetailsRecords.UserID= Guid.Parse(userID.Value);
            var IncidentDetailsRecords = _mapper.Map<IncidentDetails>(ConversionOfFnolToIncidentDetailsDto(fnolDto));
            IncidentDetailsRecords.IncidentID = GenerateIncidentID();
            IncidentDetailsRecords.FnolID = FnolDetailsRecords.FnolID;
            var DriverDetailsRecords = _mapper.Map<DriverDetails>(ConversionOfFnolToDriverDetailsDto(fnolDto));
            DriverDetailsRecords.DriverID=GenerateDriverID();
            DriverDetailsRecords.IncidentID=IncidentDetailsRecords.IncidentID;
            var VehicleDetailsRecords = _mapper.Map<VehicleDetails>(ConversionOfFnolToVehicleDetailsDto(fnolDto));
            VehicleDetailsRecords.VehicleID=GenerateVehicleID();
            VehicleDetailsRecords.DriverID=DriverDetailsRecords.DriverID;
            var IncidentPictureRecords = _mapper.Map<IncidentPicture>(ConversionOfFnolToIncidentPictureDto(fnolDto));
            IncidentPictureRecords.PictureID=GeneratePicID();
            IncidentPictureRecords.IncidentID=IncidentDetailsRecords.IncidentID;
            _context.fnolDetails.Add(FnolDetailsRecords);
            _context.incidentDetails.Add(IncidentDetailsRecords);
            _context.driverDetails.Add(DriverDetailsRecords);
            _context.vehicleDetails.Add(VehicleDetailsRecords);
            _context.incidentPictures.Add(IncidentPictureRecords);
            await _context.SaveChangesAsync();

            return  FnolDetailsRecords.FnolID;

        }
        //Method that returns Fnol details by ID
        public async Task<FnolCustomerDto> GetFnolByIDAsync(string FnolID)
        {
            FnolCustomerDto fnolDetails = new FnolCustomerDto();
            IncidentDetails incidentDetails;
            DriverDetails driverDetails;
            VehicleDetails vehicleDetails;
            var FnolDetails = await _context.fnolDetails.Where(x => x.FnolID == FnolID).Select(x => new FnolDetails()
            // var FnolDetails = await _context.fnolDetails.Where(x => x.FnolID == FnolID || x.UserID.ToString() ==userID.Value).Select(x => new FnolDetails()
            {
                FnolID = x.FnolID,
                PolicyID=x.PolicyID,
                ReportedDate=x.ReportedDate,
                ReporterName=x.ReporterName

            }).FirstOrDefaultAsync();
            if(FnolDetails != null)
            {
                fnolDetails.PolicyID=FnolDetails.PolicyID;
                fnolDetails.ReporterName=FnolDetails.ReporterName;
                fnolDetails.ReportedDate=FnolDetails.ReportedDate;
                incidentDetails = GetIncidentDetails(FnolDetails.FnolID);
                fnolDetails.CauseOfIncident=incidentDetails.CauseOfIncident;
                fnolDetails.DamagedParts=incidentDetails.DamagedParts;
                fnolDetails.AddressOfIncident=incidentDetails.AddressOfIncident;
                fnolDetails.IncidentDate=incidentDetails.IncidentDate;
                fnolDetails.Description= incidentDetails.Description;
                fnolDetails.Landmark = incidentDetails.Landmark;
                driverDetails= GetDriverDetails(incidentDetails.IncidentID);
                fnolDetails.DriverName=driverDetails.DriverName;
                fnolDetails.DriverLicenseType=driverDetails.DriverLicenseType;
                fnolDetails.DriverLicenseNumber=driverDetails.DriverLicenseNumber;
                vehicleDetails= GetVehicleDetails(driverDetails.DriverID);
                fnolDetails.VehicleMaker=vehicleDetails.VehicleMaker;
                fnolDetails.VehicleType=vehicleDetails.VehicleType;
                fnolDetails.VehicleModel=vehicleDetails.VehicleModel;
                fnolDetails.VehicleRegNumber=vehicleDetails.VehicleRegNumber;
                fnolDetails.RCNumber=vehicleDetails.RCNumber;
                return fnolDetails;
            }
            return null;
            
        }
        //Method that deletes FnolDetails by ID
        public async Task<bool> DeleteFnolByIDAsync(string FnolID)
        {
            var record = await _context.fnolDetails.Where(x => x.FnolID == FnolID).Select( x=> new FnolDetails()
            {
                FnolID=x.FnolID,
                PolicyID=x.PolicyID,
                ReportedDate=x.ReportedDate,
                ReporterName=x.ReporterName
            }).FirstOrDefaultAsync();

            if(record != null)
            {
                 _context.fnolDetails.Remove(record);
                 _context.SaveChangesAsync();
                 return true;

            }
            else
            {
                return false;
            }
        }
        //Method that updates details of Fnol by ID
        public async Task<bool> UpdateFnolByIDAsync(string FnolID,FnolDto fnolDto)
        {
            var fnolDetails = await _context.fnolDetails.FirstOrDefaultAsync(x=> x.FnolID==FnolID);
            if(fnolDetails != null)
            {
                fnolDetails.FnolID=FnolID;
                fnolDetails.PolicyID=fnolDto.PolicyID;
                fnolDetails.ReporterName=fnolDto.ReporterName;
                fnolDetails.ReportedDate=fnolDto.ReportedDate;
                var Incident = await _context.incidentDetails.FirstOrDefaultAsync(x => x.FnolID == FnolID);
                Incident.CauseOfIncident=fnolDto.CauseOfIncident;
                Incident.AddressOfIncident=fnolDto.AddressOfIncident;
                Incident.IncidentDate=fnolDto.IncidentDate;
                Incident.Description=fnolDto.Description;
                Incident.Landmark=fnolDto.Landmark;
                var Driver = await _context.driverDetails.FirstOrDefaultAsync(x => x.IncidentID == Incident.IncidentID);
                Driver.DriverName = fnolDto.DriverName;
                Driver.DriverLicenseType=fnolDto.DriverLicenseType;
                Driver.DriverLicenseNumber=fnolDto.DriverLicenseNumber;
                var Vehicle = await _context.vehicleDetails.FirstOrDefaultAsync(x => x.DriverID == Driver.DriverID);
                Vehicle.VehicleMaker=fnolDto.VehicleMaker;
                Vehicle.VehicleModel=fnolDto.VehicleModel;
                Vehicle.VehicleRegNumber=fnolDto.VehicleRegNumber;
                Vehicle.VehicleType=fnolDto.VehicleType;
                var Incidentpictures= await _context.incidentPictures.FirstOrDefaultAsync(x => x.IncidentID == Incident.IncidentID);
                Incidentpictures.FrontImage= ImageToByteArray(fnolDto.FrontImage);
                Incidentpictures.BackImage= ImageToByteArray(fnolDto.BackImage);
                Incidentpictures.LeftImage= ImageToByteArray(fnolDto.LeftImage);
                Incidentpictures.RightImage= ImageToByteArray(fnolDto.RightImage);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
           
        }
        //Method that returns all Fnol details from Database
        public async Task<List<CaseDetailsDto>> GetAllOnlyFnolDetailsAsync()
        {
            var records = await _context.fnolDetails.ToListAsync();
            List<CaseDetailsDto> caseDetails = new List<CaseDetailsDto>();
            CaseDetailsDto caseDetailsDto;
            foreach(var record in records)
            {
                caseDetailsDto = new CaseDetailsDto();

                caseDetailsDto = _mapper.Map<CaseDetailsDto>(record);

                caseDetails.Add(caseDetailsDto);

            }
            return caseDetails;
        }

        //Method that returns all Incident Pictures related to Case
        public async Task<IncidentPicture> GetIncidentPicturesByID(string FnolID)
        {
            var incident = GetIncidentDetails(FnolID);
            if(incident ==null)
            {
                return null;
            }
            var records = await _context.incidentPictures.Where(x => x.IncidentID == incident.IncidentID).Select(x => new IncidentPicture()
            {
                PictureID = x.PictureID,
                FrontImage = x.FrontImage,
                BackImage=x.BackImage,
                LeftImage=x.LeftImage,
                RightImage=x.RightImage,
                IncidentID=x.IncidentID

            }).FirstOrDefaultAsync();
            if(records == null)
            {
                return null;
            }
            return records;
        }
    
    }
}