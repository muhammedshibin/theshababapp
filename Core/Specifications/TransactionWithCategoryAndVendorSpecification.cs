﻿using System;
using System.Linq.Expressions;
using Core.DataFilters;
using Core.Entities;

namespace Core.Specifications
{
    public class TransactionWithCategoryAndVendorSpecification : BaseSpecification<Transaction>
    {
        public TransactionWithCategoryAndVendorSpecification(TransactionsFilter spec) : 
            base(txn =>
                (!spec.Month.HasValue||txn.TransactionDate.Month == spec.Month)&&
                    (!spec.Year.HasValue||txn.TransactionDate.Year == spec.Year)&&
                        (string.IsNullOrEmpty(spec.Search)|| txn.Name.ToLower().Contains(spec.Search))&&
                            (string.IsNullOrEmpty(spec.CategoryName)|| txn.Category.Name.ToLower().Contains(spec.CategoryName))
            )
        {
            AddInclude(t => t.Category);
            AddInclude(t => t.PaidParty);
            AddOrderByDesc(t => t.TransactionDate);
            if(spec.IsPagingNeeded)
                AddPagination(spec.PageIndex - 1 * spec.PageSize,spec.PageSize);
        }
        
        public TransactionWithCategoryAndVendorSpecification(int id) : 
            base(t => t.Id == id)
        {
            AddInclude(t => t.Category);
            AddInclude(t => t.PaidParty);
        }
    }
}