using api.Data;
using api.DTOs; 
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace api.Service.AddressS
{
    public class AddressService : ControllerBase, IAddressService
    {
        private readonly Context _context;

        public AddressService(Context context)
        {
            _context = context;
        }

        public async Task<List<Address>> GetAddressAsync()
        {
            return await _context.Addresses.ToListAsync();
        }

        public async Task<object> CreateAndUpdateAddressAsync(AddressDto addressDto)
        {
            var result = await _context.Addresses.AsNoTracking()
                .FirstOrDefaultAsync(x => x.ID == addressDto.ID);

            if (result == null)
            {
                var address = new Address()
                {
                    Province = addressDto.Province,
                    District = addressDto.District,
                    SubDistrict = addressDto.SubDistrict,
                    PostCode = addressDto.PostCode,
                    HouseNumber = addressDto.HouseNumber,
                    UserId = addressDto.UserId,
                };

                await _context.Addresses.AddAsync(address);
            }
            else
            {
                result.Province = addressDto.Province;
                result.District = addressDto.District;
                result.SubDistrict = addressDto.SubDistrict;
                result.PostCode = addressDto.PostCode;
                result.HouseNumber = addressDto.HouseNumber;
                result.UserId = addressDto.UserId;
            }

            var check = await _context.SaveChangesAsync();

            if (check > 0) return StatusCode(201);

            return StatusCode(400);
        }

        public async Task<object> RemoveAsync(int id)
        {
            var result = await _context.Addresses.FirstOrDefaultAsync(x => x.ID == id);

            if (result == null) return NotFound();

            _context.Addresses.Remove(result);
            var check = await _context.SaveChangesAsync();

            if (check > 0) return StatusCode(201);

            return StatusCode(400);
        }
    }
}
