using FarmTradeDataLayer.Repository;
using FarmTradeEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmBusiness.Services
{
    public class AddressService
    {
        IAddressRepo _addressRepo;
        public AddressService(IAddressRepo addressRepo) {
            _addressRepo=addressRepo;
        }
        public void AddAddress(Address address)
        {
            _addressRepo.AddAddress(address);
        }
        public void UpdateAddress(Address address)
        {
            _addressRepo.UpdateAddress(address);
        }
        public IEnumerable<Address> GetAddresses() 
        {
            return _addressRepo.GetAddress();
        }
        public void DeleteAddress(int addressId)
        {
            _addressRepo.DeleteAddress(addressId);
        }
        public Address GetAddressById(int addressId)
        {
            return _addressRepo.GetAddressById(addressId);
        }
    }
}
