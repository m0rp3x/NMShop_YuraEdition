using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NMShop.Shared.Scaffold;

public partial class NMShopContext : DbContext
{
    public NMShopContext()
    {
    }

    public NMShopContext(DbContextOptions<NMShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<BrandGallery> BrandGalleries { get; set; }

    public virtual DbSet<ContactMethod> ContactMethods { get; set; }

    public virtual DbSet<DeliveryType> DeliveryTypes { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<NavigationItem> NavigationItems { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderPart> OrderParts { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<PaymentType> PaymentTypes { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductColor> ProductColors { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<ProductType> ProductTypes { get; set; }

    public virtual DbSet<PromoCode> PromoCodes { get; set; }

    public virtual DbSet<ReferenceContent> ReferenceContents { get; set; }

    public virtual DbSet<ReferenceTopic> ReferenceTopics { get; set; }

    public virtual DbSet<SellingCategory> SellingCategories { get; set; }

    public virtual DbSet<StockInfo> StockInfos { get; set; }

    public virtual DbSet<TextSize> TextSizes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pg_catalog", "adminpack");

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Brands_pkey");
        });

        modelBuilder.Entity<BrandGallery>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BrandGallery_pkey");

            entity.HasOne(d => d.Brand).WithMany(p => p.BrandGalleries)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("BrandGallery_Brand_Id_fkey");
        });

        modelBuilder.Entity<ContactMethod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ContactMethods_pkey");
        });

        modelBuilder.Entity<DeliveryType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DeliveryTypes_pkey");
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Genders_pkey");
        });

        modelBuilder.Entity<NavigationItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("NavigationItems_pkey");

            entity.HasOne(d => d.ParentItem).WithMany(p => p.InverseParentItem).HasConstraintName("NavigationItems_ParentItem_Id_fkey");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Orders_pkey");

            entity.HasOne(d => d.ContactMethod).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Orders_ContactMethod_Id_fkey");

            entity.HasOne(d => d.DeliveryType).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Orders_DeliveryType_Id_fkey");

            entity.HasOne(d => d.OrderStatus).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Orders_OrderStatus_Id_fkey");

            entity.HasOne(d => d.PaymentType).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Orders_PaymentType_Id_fkey");

            entity.HasOne(d => d.PromoCode).WithMany(p => p.Orders).HasConstraintName("Orders_PromoCode_Id_fkey");
        });

        modelBuilder.Entity<OrderPart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OrderParts_pkey");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderParts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_OrderParts_Order_Id");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderParts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_OrderParts_Product_Id");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OrderStatuses_pkey");
        });

        modelBuilder.Entity<PaymentType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PaymentTypes_pkey");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Product_pkey");

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Product_Brand_Id");

            entity.HasOne(d => d.Color).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Product_Color_Id");

            entity.HasOne(d => d.Gender).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Product_Gender_Id");

            entity.HasOne(d => d.ProductType).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Product_ProductType_Id");

            entity.HasOne(d => d.SellingCategory).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Product_SellingCategory_Id");
        });

        modelBuilder.Entity<ProductColor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ProductColors_pkey");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ProductImages_pkey");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductImages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ProductImages_Product_Id");
        });

        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ProductTypes_pkey");

            entity.HasOne(d => d.ParentType).WithMany(p => p.InverseParentType).HasConstraintName("fk_ProductTypes_ParentType_Id");
        });

        modelBuilder.Entity<PromoCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PromoCodes_pkey");
        });

        modelBuilder.Entity<ReferenceContent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ReferenceContent_pkey");

            entity.HasOne(d => d.TextSize).WithMany(p => p.ReferenceContents)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ReferenceContent_TextSize_Id_fkey");

            entity.HasOne(d => d.Topic).WithMany(p => p.ReferenceContents)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ReferenceContent_Topic_Id_fkey");
        });

        modelBuilder.Entity<ReferenceTopic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ReferenceTopics_pkey");

            entity.HasOne(d => d.ParentTopic).WithMany(p => p.InverseParentTopic).HasConstraintName("ReferenceTopics_ParentTopic_Id_fkey");
        });

        modelBuilder.Entity<SellingCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SellingCategories_pkey");
        });

        modelBuilder.Entity<StockInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("StockInfo_pkey");

            entity.HasOne(d => d.Product).WithMany(p => p.StockInfos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_StockInfo_Product_Id");
        });

        modelBuilder.Entity<TextSize>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TextSizes_pkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
