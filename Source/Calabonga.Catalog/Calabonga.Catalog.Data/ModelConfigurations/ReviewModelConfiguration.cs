﻿using Calabonga.Catalog.Data.ModelConfigurations.Base;
using Calabonga.Catalog.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Calabonga.Catalog.Data.ModelConfigurations
{
    /// <summary>
    /// Entity Type Configuration for entity <see cref="Review"/>
    /// </summary>
    public class ReviewModelConfiguration: AuditableModelConfigurationBase<Review>
    {
        /// <inheritdoc />
        protected override void AddBuilder(EntityTypeBuilder<Review> builder)
        {
            builder.Property(x => x.UserName).HasMaxLength(128).IsRequired();
            builder.Property(x => x.Content).HasMaxLength(2048);
            builder.Property(x => x.Rating);
            builder.Property(x => x.Visible);
            builder.Property(x => x.ProductId).IsRequired();

            builder.HasOne(x => x.Product);
        }

        /// <inheritdoc />
        protected override string TableName()
        {
            return "Reviews";
        }
    }
}
