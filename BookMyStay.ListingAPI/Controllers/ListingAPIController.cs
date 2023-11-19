using AutoMapper;
using BookMyStay.ListingAPI.Data;
using BookMyStay.ListingAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookMyStay.ListingAPI.Controllers
{
    [Route("api/listing")]
    [ApiController]
    public class ListingAPIController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly APIResponseDTO _responseDTO;
        private IMapper _mapper;

        public ListingAPIController(ApplicationDBContext ctx, IMapper mapper)
        {
            _dbContext = ctx;
            _mapper = mapper;
            _responseDTO = new APIResponseDTO();
        }

        [HttpGet]
        [Route("")]
        public APIResponseDTO Get()
        {
            try
            {
                object queryResult = _dbContext.Listings.ToList();

                _responseDTO.Result = _mapper.Map<IEnumerable<ListingDTO>>(queryResult);
                _responseDTO.Info = "Success";
                _responseDTO.HasError = false;
            }
            catch (Exception ex)
            {
                _responseDTO.Info = ex.Message;
                _responseDTO.HasError = true;
            }
            return _responseDTO;
        }

        [HttpGet]
        [Route("{id:int}")]
        public APIResponseDTO GetById(int id)
        {

            try
            {
                Listing queryResult = _dbContext.Listings.First(x => x.ListingId == id);
                _responseDTO.Result = _mapper.Map<ListingDTO>(queryResult);
                _responseDTO.Info = "Success";
                _responseDTO.HasError = false;
            }
            catch (Exception ex)
            {
                _responseDTO.Info = ex.Message;
                _responseDTO.HasError = true;
            }
            return _responseDTO;
        }

        [HttpPost]
        [Route("")]
        [Authorize(Roles = "Admin")]
        public APIResponseDTO Post([FromBody] ListingDTO ListingDto)
        {
            try
            {
                Listing Listing = _mapper.Map<Listing>(ListingDto);
                _dbContext.Listings.Add(Listing);
                _dbContext.SaveChanges();

                _responseDTO.Result = _mapper.Map<ListingDTO>(Listing);
                _responseDTO.Info = "Success";
                _responseDTO.HasError = false;
            }
            catch (Exception ex)
            {
                _responseDTO.Info = ex.Message;
                _responseDTO.HasError = true;
            }
            return _responseDTO;
        }

        [HttpPut]
        [Route("")]
        [Authorize(Roles = "Admin")]
        public APIResponseDTO Put([FromBody] ListingDTO ListingDto)
        {
            try
            {
                Listing Listing = _mapper.Map<Listing>(ListingDto);
                _dbContext.Listings.Update(Listing);
                _dbContext.SaveChanges();

                _responseDTO.Result = _mapper.Map<ListingDTO>(Listing);
                _responseDTO.Info = "Success";
                _responseDTO.HasError = false;
            }
            catch (Exception ex)
            {
                _responseDTO.Info = ex.Message;
                _responseDTO.HasError = true;
            }
            return _responseDTO;
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public APIResponseDTO Delete(int id)
        {
            try
            {
                Listing Listing = _dbContext.Listings.First(x=> x.ListingId == id);
                _dbContext.Listings.Remove(Listing);
                _dbContext.SaveChanges();

                _responseDTO.Result = _mapper.Map<ListingDTO>(Listing);
                _responseDTO.Info = "Success";
                _responseDTO.HasError = false;
            }
            catch (Exception ex)
            {
                _responseDTO.Info = ex.Message;
                _responseDTO.HasError = true;
            }
            return _responseDTO;
        }
    }
}
