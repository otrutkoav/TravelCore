using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourCore.Pricing.Domain.PriceBlocks;
using TourCore.Pricing.Domain.PricePackages;
using TourCore.Pricing.Domain.SPOTypes;
using TourCore.Pricing.Domain.Seasons;

namespace TourCore.Pricing.Infrastructure.Postgres.Configurations;

internal sealed class PriceBlockConfiguration : IEntityTypeConfiguration<PriceBlock>
{
    public void Configure(EntityTypeBuilder<PriceBlock> builder)
    {
        builder.ToTable("PriceBlocks");
        builder.HasKey(item => item.Id);
        builder.Property(item => item.Name).HasMaxLength(PriceBlock.MaximumNameLength).IsRequired();
        builder.Property(item => item.Code).HasMaxLength(PriceBlock.MaximumCodeLength).IsRequired();
        builder.Property(item => item.Description).HasMaxLength(PriceBlock.MaximumDescriptionLength).IsRequired();
        builder.Property(item => item.Comment).HasMaxLength(PriceBlock.MaximumCommentLength);
        builder.Property(item => item.OnlineInfoText).HasMaxLength(PriceBlock.MaximumOnlineInfoTextLength);
        builder.Property(item => item.State).HasConversion<byte>().IsRequired();
        builder.Property(item => item.CreatedByUserId).HasMaxLength(PriceBlock.MaximumUserIdLength).IsRequired();
        builder.Property(item => item.SentForReviewByUserId).HasMaxLength(PriceBlock.MaximumUserIdLength);
        builder.Property(item => item.ActivatedForNetByUserId).HasMaxLength(PriceBlock.MaximumUserIdLength);
        builder.Property(item => item.ActivatedByUserId).HasMaxLength(PriceBlock.MaximumUserIdLength);
        builder.Property(item => item.DeactivatedByUserId).HasMaxLength(PriceBlock.MaximumUserIdLength);

        builder.HasOne<PricePackage>().WithMany().HasForeignKey(item => item.PricePackageId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<SPOType>().WithMany().HasForeignKey(item => item.SPOTypeId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Season>().WithMany().HasForeignKey(item => item.SeasonId).OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(item => item.PricePackageId).HasDatabaseName("IX_PriceBlocks_PricePackageId");
        builder.HasIndex(item => item.SPOTypeId).HasDatabaseName("IX_PriceBlocks_SPOTypeId");
        builder.HasIndex(item => item.SeasonId).HasDatabaseName("IX_PriceBlocks_SeasonId");
        builder.HasIndex(item => new { item.State, item.SaleEndsAt }).HasDatabaseName("IX_PriceBlocks_State_SaleEndsAt");
        builder.HasIndex(item => new { item.ServiceTypeId, item.SaleStartsAt, item.SaleEndsAt, item.State })
            .HasDatabaseName("IX_PriceBlocks_ServiceTypeId_SaleDates_State");
    }
}
