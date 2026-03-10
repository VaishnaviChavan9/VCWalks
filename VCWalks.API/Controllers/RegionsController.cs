using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VCWalks.API.Data;
using VCWalks.API.Models.DTO;
using VCWalks.API.Models.Domain;

namespace VCWalks.API.Controllers
{
    //https: localhost:7035/api/Regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly VCWalksDbContext dbContext;
        public RegionsController(VCWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var regionsDomain = dbContext.Regions.ToList();

            //MAP DOMAIN MODELS TO DTO
            var regionsDto = new List<RegionDto>();

            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });
            }

            //RETURN DTOS
            return Ok(regionsDto);
        }

        //GET SINGLE REGION(get region by id)
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            {
                // Find can be only used for the primary key's
                var region = dbContext.Regions.Find(id);
                //can be used for fields which are not the primary key
                //var region = dbContext.Regions.FirstOrDefault(x => x.Id == id);


                if (region == null)
                {
                    return NotFound();
                }

                return Ok(region);

            }
        }

        //POST to Create New Region4
        //POST : https://localhost:portnumber/api/regions
        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Map or Convert DTO to Domain Model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            //Use Ddomain Model to create Region

            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();

            //Map Domain model back to DTO 
            var regionsDto = new RegionDto()
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl

            };
            return CreatedAtAction(nameof(GetById), new { id = regionsDto.Id }, regionsDto);
        }

        //Update region
        //PUT : https://localhost:portnumber/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequest updateRegionRequest)
        {
            //Check if region exists
            var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if (regionDomainModel == null) { 
                return NotFound();
            }
            //Map DTO to Domain model 
            regionDomainModel.Code = updateRegionRequest.Code;
            regionDomainModel.Name = updateRegionRequest.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequest.RegionImageUrl;

            dbContext.SaveChanges();

            //Convert Domain Model to DTO
            var regionDto = new RegionDto()
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return Ok(regionDto);
        }

        //Delete Region
        //DELETE: https://localhost:port/api/region/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id) {
            var regionDomainModel = dbContext.Regions.FirstOrDefault(x=> x.Id == id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //Delete region
            dbContext.Regions.Remove(regionDomainModel);
            dbContext.SaveChanges();

            //return deleted region back
            //map Domain Model to DTO
            var regionDto = new RegionDto()
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return Ok(regionDto);
        }
    }
}
