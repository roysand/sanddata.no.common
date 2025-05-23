﻿using DataLayer.Domain.Common.Entities;

namespace DataLayer.Domain.Entities;

public class Location : AuditableEntity
{
    public Guid LocationId { get; set; }

    public string LocationName { get; set; } = null!;

    public string? LocationAddress { get; set; }

    public string? SerialNumber { get; set; }

    public Guid? ApiKeyId { get; set; }

    public virtual ApiKey? ApiKey { get; set; }
}