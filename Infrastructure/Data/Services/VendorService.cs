﻿using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Services
{
    public class VendorService : IVendorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public VendorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddVendor(Vendor vendor)
        {
            _unitOfWork.Repository<Vendor>().Add(vendor);
            return await _unitOfWork.Complete() > 0;
        }

        public async Task<IReadOnlyList<Vendor>> GetVendors()
        {
            return await _unitOfWork.Repository<Vendor>().FindAllAsync();
        }
    }
}
