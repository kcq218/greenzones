using Microsoft.EntityFrameworkCore;

namespace GreenZones.Models;

public partial class DbAll01ProdUswest001Context : DbContext
{
    public DbAll01ProdUswest001Context()
    {
    }

    public DbAll01ProdUswest001Context(DbContextOptions<DbAll01ProdUswest001Context> options)
        : base(options)
    {
    }

    public virtual DbSet<GeneratedKey> GeneratedKeys { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<ShotType> ShotTypes { get; set; }

    public virtual DbSet<UrlMapping> UrlMappings { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserBucket> UserBuckets { get; set; }

    public virtual DbSet<UserInfo> UserInfos { get; set; }

    // we are using base context so no longer needs this.
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){ 
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GeneratedKey>(entity =>
        {
            entity.ToTable("generated_keys", "url_shortener");

            entity.Property(e => e.Id)
                .HasComment("pk incremental int")
                .HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValue(true)
                .HasColumnName("active");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_date");
            entity.Property(e => e.HashValue)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("hash_value");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updated_date");
            entity.Property(e => e.UrlId).HasColumnName("url_id");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.ToTable("session", "green_zone", tb => tb.HasComment("each session records shot counts"));

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.Makes).HasColumnName("makes");
            entity.Property(e => e.ShotTypeId).HasColumnName("shot_type_id");
            entity.Property(e => e.Streak).HasColumnName("streak");
            entity.Property(e => e.TotalShots).HasColumnName("total_shots");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");

            entity.HasOne(d => d.ShotType).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.ShotTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_session_shot_type");
        });

        modelBuilder.Entity<ShotType>(entity =>
        {
            entity.ToTable("shot_type", "green_zone", tb => tb.HasComment("type of shot user is shooting for the session"));

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
        });

        modelBuilder.Entity<UrlMapping>(entity =>
        {
            entity.ToTable("url_mapping", "url_shortener");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValue(true)
                .HasColumnName("active");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_date");
            entity.Property(e => e.HashValue)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("hash_value");
            entity.Property(e => e.KeyId).HasColumnName("key_id");
            entity.Property(e => e.LastAccessed).HasColumnName("last_accessed");
            entity.Property(e => e.LongUrl).HasColumnName("long_url");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updated_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users", "green_zone", tb => tb.HasComment("store logged in user information"));

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("display_name");
            entity.Property(e => e.GivenName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("given_name");
            entity.Property(e => e.LoginId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("login_id");
            entity.Property(e => e.Mail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("mail");
            entity.Property(e => e.MobilePhone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("mobile_phone");
            entity.Property(e => e.Surname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("surname");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
            entity.Property(e => e.UserPrincipalName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("user_principal_name");
        });

        modelBuilder.Entity<UserBucket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_app_rate_limiter_user_bucket");

            entity.ToTable("user_bucket", "app_rate_limiter");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BucketCount).HasColumnName("bucket_count");
            entity.Property(e => e.BucketLimit).HasColumnName("bucket_limit");
            entity.Property(e => e.ClientId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("client_id");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ip_address");
            entity.Property(e => e.LastAccessed).HasColumnName("last_accessed");
            entity.Property(e => e.RefillRateSeconds).HasColumnName("refill_rate_seconds");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
        });

        modelBuilder.Entity<UserInfo>(entity =>
        {
            entity.ToTable("user_info", "url_shortener");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValue(true)
                .HasColumnName("active");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_date");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastLogin).HasColumnName("last_login");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.MiddleInitial)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("middle_initial");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("updated_date");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
