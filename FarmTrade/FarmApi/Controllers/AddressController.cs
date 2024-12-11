using FarmBusiness.Services;
using FarmTradeEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FarmApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private AddressService _addressService;
        public AddressController(AddressService addressService)=> _addressService = addressService;
        [HttpPost("AddAddress")]
        public IActionResult AddAddress([FromBody]Address address)
        {
            _addressService.AddAddress(address);
            return Ok("Address Added successfully");
        }
        [HttpPut("UpdateAddress")]
        public IActionResult UpdateAddress([FromBody] Address address)
        {
            _addressService.UpdateAddress(address);
            return Ok("Address Updated Successfully");
        }
        [HttpGet("GetAddressById")]
        public Address GetAddressById(int addressId)
        {
            #region Get Address By Id
            return _addressService.GetAddressById(addressId);
            #endregion
        }
        [HttpGet("GetAddresss")]
        public IEnumerable<Address> GetAddress()
        {
            #region Get Address:
            return _addressService.GetAddresses();
            #endregion
        }
        [HttpDelete("DeleteAddress")]
        public IActionResult DeleteAddress(int addressId)
        {
            #region Delete Address
            _addressService.DeleteAddress(addressId);
            return Ok("Address deleted successfully!!!");
            #endregion
        }
    }
}
