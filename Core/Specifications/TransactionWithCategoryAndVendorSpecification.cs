﻿using System;
using System.Linq.Expressions;
using Core.DataFilters;
using Core.Entities;

namespace Core.Specifications
{
    public class TransactionWithCategoryAndVendorSpecification : BaseSpecification<TransactionDetail>
    {
        public TransactionWithCategoryAndVendorSpecification(TransactionsFilter spec, bool forCount = false) :
            base(txn =>
                (!spec.Month.HasValue || txn.TransactionDate.Month == spec.Month) &&
                (!spec.Year.HasValue || txn.TransactionDate.Year == spec.Year) &&
                (!spec.PaidBy.HasValue || txn.PaidPartyId == spec.PaidBy) &&
                (!spec.PaidTo.HasValue || txn.PaidToId == spec.PaidTo) &&
                (string.IsNullOrEmpty(spec.Search) || txn.Name.ToLower().Contains(spec.Search)) &&
                (string.IsNullOrEmpty(spec.CategoryName) || txn.Category.Name.ToLower().Contains(spec.CategoryName))
            )
        {
            if (!forCount)
            {
                AddInclude(t => t.Category);
                AddInclude(t => t.PaidParty);
                AddOrderByDesc(t => t.TransactionDate);
                AddPagination((spec.PageIndex - 1) * spec.PageSize, spec.PageSize);
            }
        }

        public TransactionWithCategoryAndVendorSpecification(int id, bool trackingNeeded = false) :
            base(t => t.Id == id)
        {
            AddInclude(t => t.Category);
            AddInclude(t => t.PaidParty);
            if (!trackingNeeded) AddNoTracking();
        }

    }
}