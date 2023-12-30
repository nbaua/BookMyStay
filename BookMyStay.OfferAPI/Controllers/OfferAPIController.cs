using AutoMapper;
using BookMyStay.OfferAPI.Data;
using BookMyStay.OfferAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookMyStay.OfferAPI.Controllers
{
    [Route("api/offer")]
    [ApiController]
    [Authorize]
    public class OfferAPIController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly APIResponseDTO _responseDTO;
        private IMapper _mapper;

        public OfferAPIController(ApplicationDBContext ctx, IMapper mapper)
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
                object queryResult = _dbContext.Offers.ToList();

                _responseDTO.Result = _mapper.Map<IEnumerable<OfferDTO>>(queryResult);
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
                Offer queryResult = _dbContext.Offers.First(x => x.OfferId == id);
                _responseDTO.Result = _mapper.Map<OfferDTO>(queryResult);
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
        [Route("code/{code}")]
        public APIResponseDTO GetByOfferCode(string code)
        {

            try
            {
                Offer queryResult = _dbContext.Offers.FirstOrDefault(x => x.OfferCode.ToUpper() == code.ToUpper());
                _responseDTO.Result = _mapper.Map<OfferDTO>(queryResult);
                _responseDTO.Info = queryResult != null ? "Success" : "No Data Found";
                _responseDTO.HasError = queryResult != null ? false : true;
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
        public APIResponseDTO Post([FromBody] OfferDTO offerDto)
        {
            try
            {
                Offer offer = _mapper.Map<Offer>(offerDto);
                _dbContext.Offers.Add(offer);
                _dbContext.SaveChanges();

                _responseDTO.Result = _mapper.Map<OfferDTO>(offer);
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
        public APIResponseDTO Put([FromBody] OfferDTO offerDto)
        {
            try
            {
                Offer offer = _mapper.Map<Offer>(offerDto);
                _dbContext.Offers.Update(offer);
                _dbContext.SaveChanges();

                _responseDTO.Result = _mapper.Map<OfferDTO>(offer);
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
                Offer offer = _dbContext.Offers.First(x => x.OfferId == id);
                _dbContext.Offers.Remove(offer);
                _dbContext.SaveChanges();

                _responseDTO.Result = _mapper.Map<OfferDTO>(offer);
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
