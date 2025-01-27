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

    public virtual DbSet<CustomOrder> CustomOrders { get; set; }

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
        => optionsBuilder.UseNpgsql("Host=83.222.10.153;Port=5432;Username=m0rp3x;Password=16092009Ba##;Database=nmshop");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BannerCarouselItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BannerCarouselItems_pkey");

            entity.ToTable("BannerCarouselItems", "NMShop");
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Brands_pkey");

            entity.ToTable("Brands", "NMShop");

            entity.HasIndex(e => e.Name, "Brands_Name_key").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<BrandGalleryItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BrandGalleryItems_pkey");

            entity.ToTable("BrandGalleryItems", "NMShop");

            entity.Property(e => e.BrandId).HasColumnName("Brand_Id");

            entity.HasOne(d => d.Brand).WithMany(p => p.BrandGalleryItems)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("BrandGalleryItems_Brand_Id_fkey");
        });

        modelBuilder.Entity<ContactMethod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ContactMethods_pkey");

            entity.ToTable("ContactMethods", "NMShop");

            entity.HasIndex(e => e.Name, "ContactMethods_Name_key").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.ValidationErrorText).HasMaxLength(255);
            entity.Property(e => e.ValidationMask).HasMaxLength(255);
        });

        modelBuilder.Entity<CustomOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CustomOrders_pkey");
            entity.Property(e => e.Id).HasColumnName("Id").ValueGeneratedOnAdd();

            entity.ToTable("CustomOrders", "NMShop");

            entity.Property(e => e.CreatedAt).HasColumnType("timestamp without time zone");
            entity.Property(e => e.Id).HasColumnName("Id");
            entity.Property(e => e.UserName).HasMaxLength(100);
            entity.Property(e => e.UserPhone).HasMaxLength(20);
            
        });

        modelBuilder.Entity<DeliveryType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("DeliveryTypes_pkey");

            entity.ToTable("DeliveryTypes", "NMShop");

            entity.HasIndex(e => e.Name, "DeliveryTypes_Name_key").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Genders_pkey");

            entity.ToTable("Genders", "NMShop");

            entity.HasIndex(e => e.Name, "Genders_Name_key").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<NavigationItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("NavigationItems_pkey");

            entity.ToTable("NavigationItems", "NMShop");

            entity.Property(e => e.Link).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.ParentItemId).HasColumnName("ParentItem_Id");

            entity.HasOne(d => d.ParentItem).WithMany(p => p.InverseParentItem)
                .HasForeignKey(d => d.ParentItemId)
                .HasConstraintName("NavigationItems_ParentItem_Id_fkey");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Orders_pkey");

            entity.ToTable("Orders", "NMShop");

            entity.Property(e => e.ClientFullName).HasMaxLength(250);
            entity.Property(e => e.Comment).HasMaxLength(1000);
            entity.Property(e => e.ContactMethodId).HasColumnName("ContactMethod_Id");
            entity.Property(e => e.ContactValue).HasMaxLength(255);
            entity.Property(e => e.DeliveryAdress).HasMaxLength(500);
            entity.Property(e => e.DeliveryRecipientFullName).HasMaxLength(250);
            entity.Property(e => e.DeliveryRecipientPhone).HasMaxLength(11);
            entity.Property(e => e.DeliveryTypeId).HasColumnName("DeliveryType_Id");
            entity.Property(e => e.EstimatedDeliveryDateRange)
                .HasMaxLength(50)
                .HasDefaultValueSql("'Уточняем'::character varying");
            entity.Property(e => e.OrderStatusId).HasColumnName("OrderStatus_Id");
            entity.Property(e => e.PaymentTypeId).HasColumnName("PaymentType_Id");
            entity.Property(e => e.PromoCodeId).HasColumnName("PromoCode_Id");

            entity.HasOne(d => d.ContactMethod).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ContactMethodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Orders_ContactMethod_Id_fkey");

            entity.HasOne(d => d.DeliveryType).WithMany(p => p.Orders)
                .HasForeignKey(d => d.DeliveryTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Orders_DeliveryType_Id_fkey");

            entity.HasOne(d => d.OrderStatus).WithMany(p => p.Orders)
                .HasForeignKey(d => d.OrderStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Orders_OrderStatus_Id_fkey");

            entity.HasOne(d => d.PaymentType).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PaymentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Orders_PaymentType_Id_fkey");

            entity.HasOne(d => d.PromoCode).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PromoCodeId)
                .HasConstraintName("Orders_PromoCode_Id_fkey");
        });

        modelBuilder.Entity<OrderPart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OrderParts_pkey");

            entity.ToTable("OrderParts", "NMShop");

            entity.Property(e => e.OrderId).HasColumnName("Order_Id");
            entity.Property(e => e.StockInfoId).HasColumnName("StockInfo_Id");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderParts)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("OrderParts_Order_Id_fkey");

            entity.HasOne(d => d.StockInfo).WithMany(p => p.OrderParts)
                .HasForeignKey(d => d.StockInfoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("OrderParts_StockInfo_Id_fkey");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OrderStatuses_pkey");

            entity.ToTable("OrderStatuses", "NMShop");

            entity.HasIndex(e => e.Name, "OrderStatuses_Name_key").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<PaymentType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PaymentTypes_pkey");

            entity.ToTable("PaymentTypes", "NMShop");

            entity.HasIndex(e => e.Name, "PaymentTypes_Name_key").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Product_pkey");

            entity.ToTable("Product", "NMShop");

            entity.HasIndex(e => e.Article, "Product_Article_key").IsUnique();

            entity.Property(e => e.Article).HasMaxLength(150);
            entity.Property(e => e.BrandId).HasColumnName("Brand_Id");
            entity.Property(e => e.ColorId).HasColumnName("Color_Id");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.GenderId).HasColumnName("Gender_Id");
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.ProductTypeId).HasColumnName("ProductType_Id");
            entity.Property(e => e.SellingCategoryId).HasColumnName("SellingCategory_Id");

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Product_Brand_Id_fkey");

            entity.HasOne(d => d.Color).WithMany(p => p.Products)
                .HasForeignKey(d => d.ColorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Product_Color_Id_fkey");

            entity.HasOne(d => d.Gender).WithMany(p => p.Products)
                .HasForeignKey(d => d.GenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Product_Gender_Id_fkey");

            entity.HasOne(d => d.ProductType).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Product_ProductType_Id_fkey");

            entity.HasOne(d => d.SellingCategory).WithMany(p => p.Products)
                .HasForeignKey(d => d.SellingCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Product_SellingCategory_Id_fkey");
        });

        modelBuilder.Entity<ProductColor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ProductColors_pkey");

            entity.ToTable("ProductColors", "NMShop");

            entity.HasIndex(e => e.Name, "ProductColors_Name_key").IsUnique();

            entity.HasIndex(e => e.Value, "ProductColors_Value_key").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.Value).HasMaxLength(6);
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ProductImages_pkey");

            entity.ToTable("ProductImages", "NMShop");

            entity.Property(e => e.ProductId).HasColumnName("Product_Id");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ProductImages_Product_Id_fkey");
        });

        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ProductTypes_pkey");

            entity.ToTable("ProductTypes", "NMShop");

            entity.HasIndex(e => e.Name, "ProductTypes_Name_key").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.ParentTypeId).HasColumnName("ParentType_Id");
            entity.Property(e => e.SizeDisplayType).HasMaxLength(10);

            entity.HasOne(d => d.ParentType).WithMany(p => p.InverseParentType)
                .HasForeignKey(d => d.ParentTypeId)
                .HasConstraintName("ProductTypes_ParentType_Id_fkey");
        });

        modelBuilder.Entity<PromoCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PromoCodes_pkey");

            entity.ToTable("PromoCodes", "NMShop");

            entity.HasIndex(e => e.Code, "PromoCodes_Code_key").IsUnique();

            entity.Property(e => e.Code).HasMaxLength(50);
        });

        modelBuilder.Entity<ReferenceContent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ReferenceContent_pkey");

            entity.ToTable("ReferenceContent", "NMShop");

            entity.Property(e => e.Content).HasMaxLength(1000);
            entity.Property(e => e.TextSizeId).HasColumnName("TextSize_Id");
            entity.Property(e => e.TopicId).HasColumnName("Topic_Id");

            entity.HasOne(d => d.TextSize).WithMany(p => p.ReferenceContents)
                .HasForeignKey(d => d.TextSizeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ReferenceContent_TextSize_Id_fkey");

            entity.HasOne(d => d.Topic).WithMany(p => p.ReferenceContents)
                .HasForeignKey(d => d.TopicId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ReferenceContent_Topic_Id_fkey");
        });

        modelBuilder.Entity<ReferenceTopic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ReferenceTopics_pkey");

            entity.ToTable("ReferenceTopics", "NMShop");

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.ParentTopicId).HasColumnName("ParentTopic_Id");

            entity.HasOne(d => d.ParentTopic).WithMany(p => p.InverseParentTopic)
                .HasForeignKey(d => d.ParentTopicId)
                .HasConstraintName("ReferenceTopics_ParentTopic_Id_fkey");
        });

        modelBuilder.Entity<SellingCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SellingCategories_pkey");

            entity.ToTable("SellingCategories", "NMShop");

            entity.HasIndex(e => e.Name, "SellingCategories_Name_key").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<StockInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("StockInfo_pkey");

            entity.ToTable("StockInfo", "NMShop");

            entity.Property(e => e.DiscountPrice).HasPrecision(10);
            entity.Property(e => e.Price).HasPrecision(10);
            entity.Property(e => e.ProductId).HasColumnName("Product_Id");
            entity.Property(e => e.Size).HasPrecision(10);

            entity.HasOne(d => d.Product).WithMany(p => p.StockInfos)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("StockInfo_Product_Id_fkey");
        });

        modelBuilder.Entity<TextSize>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TextSizes_pkey");

            entity.ToTable("TextSizes", "NMShop");

            entity.HasIndex(e => e.Value, "TextSizes_Value_key").IsUnique();

            entity.Property(e => e.Value).HasMaxLength(50);
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Ticketid).HasName("tickets_pkey");

            entity.ToTable("tickets", "NMShop");

            entity.Property(e => e.Ticketid).HasColumnName("ticketid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("createdat");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValueSql("'Open'::character varying")
                .HasColumnName("status");
            entity.Property(e => e.Subject)
                .HasMaxLength(255)
                .HasColumnName("subject");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("updatedat");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("tickets_userid_fkey");
        });

        modelBuilder.Entity<Ticketmessage>(entity =>
        {
            entity.HasKey(e => e.Messageid).HasName("ticketmessages_pkey");

            entity.ToTable("ticketmessages", "NMShop");

            entity.Property(e => e.Messageid).HasColumnName("messageid");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.Sentat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("sentat");
            entity.Property(e => e.Ticketid).HasColumnName("ticketid");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Ticket).WithMany(p => p.Ticketmessages)
                .HasForeignKey(d => d.Ticketid)
                .HasConstraintName("ticketmessages_ticketid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Ticketmessages)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("ticketmessages_userid_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("users_pkey");

            entity.ToTable("users", "NMShop");

            entity.HasIndex(e => e.Telegramid, "users_telegramid_key").IsUnique();

            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("createdat");
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .HasDefaultValueSql("'Client'::character varying")
                .HasColumnName("role");
            entity.Property(e => e.Telegramid).HasColumnName("telegramid");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
