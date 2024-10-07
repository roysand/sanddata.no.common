﻿using DataLayer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Application.Interface;

public interface IApplicationDbContext
{
    DbSet<RawData> RawSet { get; set; }
    DbSet<Detail> DetailSet { get; set; }
    DbSet<Minute> MinuteSet { get; set; }
    DbSet<Hour> HourSet { get; set; }
    DbSet<Price> PriceSet { get; set; }
    DbSet<PriceDetail> PriceDetailSet { get; set; }
    DbSet<ExchangeRate> ExchangeRateSet { get; set; }
    
        
    // Authorization
    DbSet<Account> AccountSet { get; set; }
    DbSet<AccountContact> AccountContactSet { get; set; }
    DbSet<ApiKey> ApiKeySet { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}