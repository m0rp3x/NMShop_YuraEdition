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

    public virtual DbSet<BannerCarouselItem> BannerCarouselItems { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<BrandGalleryItem> BrandGalleryItems { get; set; }

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

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<Ticketmessage> Ticketmessages { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=194.87.187.147;Port=5432;Username=m0rp3x;Password=16092009##;Database=nmshop");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BannerCarouselItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BannerCarouselItems_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"BannerCarouselItems_Id_seq\"'::regclass)");
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Brands_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"Brands_Id_seq\"'::regclass)");
        });

        modelBuilder.Entity<BrandGalleryItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BrandGalleryItems_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"BrandGalleryItems_Id_seq\"'::regclass)");

            entity.HasOne(d => d.Brand).WithMany(p => p.BrandGalleryItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("BrandGalleryItems_Brand_Id_fkey");
        });

        modelBuilder.Entity<ContactMethod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ContactMethods_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"ContactMethods_Id_seq\"'::regclass)");
        });

        modelBuilder.Entity<DeliveryType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DeliveryTypes_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"DeliveryTypes_Id_seq\"'::regclass)");
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Genders_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"Genders_Id_seq\"'::regclass)");
        });

        modelBuilder.Entity<NavigationItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("NavigationItems_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"NavigationItems_Id_seq\"'::regclass)");

            entity.HasOne(d => d.ParentItem).WithMany(p => p.InverseParentItem).HasConstraintName("NavigationItems_ParentItem_Id_fkey");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Orders_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"Orders_Id_seq\"'::regclass)");
            entity.Property(e => e.EstimatedDeliveryDateRange).HasDefaultValueSql("'Уточняем'::character varying");

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

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"OrderParts_Id_seq\"'::regclass)");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderParts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("OrderParts_Order_Id_fkey");

            entity.HasOne(d => d.StockInfo).WithMany(p => p.OrderParts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("OrderParts_StockInfo_Id_fkey");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OrderStatuses_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"OrderStatuses_Id_seq\"'::regclass)");
        });

        modelBuilder.Entity<PaymentType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PaymentTypes_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"PaymentTypes_Id_seq\"'::regclass)");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Product_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"Product_Id_seq\"'::regclass)");

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Product_Brand_Id_fkey");

            entity.HasOne(d => d.Color).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Product_Color_Id_fkey");

            entity.HasOne(d => d.Gender).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Product_Gender_Id_fkey");

            entity.HasOne(d => d.ProductType).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Product_ProductType_Id_fkey");

            entity.HasOne(d => d.SellingCategory).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Product_SellingCategory_Id_fkey");
        });

        modelBuilder.Entity<ProductColor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ProductColors_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"ProductColors_Id_seq\"'::regclass)");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ProductImages_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"ProductImages_Id_seq\"'::regclass)");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductImages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ProductImages_Product_Id_fkey");
        });

        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ProductTypes_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"ProductTypes_Id_seq\"'::regclass)");

            entity.HasOne(d => d.ParentType).WithMany(p => p.InverseParentType).HasConstraintName("ProductTypes_ParentType_Id_fkey");
        });

        modelBuilder.Entity<PromoCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PromoCodes_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"PromoCodes_Id_seq\"'::regclass)");
        });

        modelBuilder.Entity<ReferenceContent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ReferenceContent_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"ReferenceContent_Id_seq\"'::regclass)");

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

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"ReferenceTopics_Id_seq\"'::regclass)");

            entity.HasOne(d => d.ParentTopic).WithMany(p => p.InverseParentTopic).HasConstraintName("ReferenceTopics_ParentTopic_Id_fkey");
        });

        modelBuilder.Entity<SellingCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SellingCategories_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"SellingCategories_Id_seq\"'::regclass)");
        });

        modelBuilder.Entity<StockInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("StockInfo_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"StockInfo_Id_seq\"'::regclass)");

            entity.HasOne(d => d.Product).WithMany(p => p.StockInfos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("StockInfo_Product_Id_fkey");
        });

        modelBuilder.Entity<TextSize>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TextSizes_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"TextSizes_Id_seq\"'::regclass)");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Ticketid).HasName("tickets_pkey");

            entity.Property(e => e.Ticketid).HasDefaultValueSql("nextval('tickets_ticketid_seq'::regclass)");
            entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Status).HasDefaultValueSql("'Open'::character varying");
            entity.Property(e => e.Updatedat).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.User).WithMany(p => p.Tickets).HasConstraintName("tickets_userid_fkey");
        });

        modelBuilder.Entity<Ticketmessage>(entity =>
        {
            entity.HasKey(e => e.Messageid).HasName("ticketmessages_pkey");

            entity.Property(e => e.Messageid).HasDefaultValueSql("nextval('ticketmessages_messageid_seq'::regclass)");
            entity.Property(e => e.Sentat).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Ticket).WithMany(p => p.Ticketmessages).HasConstraintName("ticketmessages_ticketid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Ticketmessages).HasConstraintName("ticketmessages_userid_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("users_pkey");

            entity.Property(e => e.Userid).HasDefaultValueSql("nextval('users_userid_seq'::regclass)");
            entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Role).HasDefaultValueSql("'Client'::character varying");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
