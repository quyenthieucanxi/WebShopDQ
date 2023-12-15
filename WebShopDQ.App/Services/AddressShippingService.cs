using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.Repositories.IRepositories;
using WebShopDQ.App.Services.IServices;

namespace WebShopDQ.App.Services
{
    public class AddressShippingService : IAddressShippingService
    {
        private readonly IAddressShippingRepository _addressShippingRepository;
        private readonly IMapper _mapper;
        public AddressShippingService (IAddressShippingRepository addressShippingRepository, IMapper mapper)
        {
            _addressShippingRepository = addressShippingRepository;
            _mapper = mapper;
        }
        
    }
}
