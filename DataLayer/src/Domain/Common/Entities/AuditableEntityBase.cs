﻿namespace DataLayer.Domain.Common.Entities;

public abstract class AuditableEntityBase
{
    public DateTime ModifiedDate { get; set; }
}