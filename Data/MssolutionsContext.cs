using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MSS.API.Models;

namespace MSS.API.Data;

public partial class MssolutionsContext : IdentityDbContext<User>
{
    public MssolutionsContext()
    {
    }

    public MssolutionsContext(DbContextOptions<MssolutionsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bank> Banks { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Wallet> Wallets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-TM9KI0C;Database=MSSolutions;Trusted_Connection=True;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bank>(entity =>
        {
            entity.HasKey(e => e.BankCode);

            entity.ToTable("bank");

            entity.Property(e => e.BankCode)
                .ValueGeneratedNever()
                .HasColumnName("bankCode");
            entity.Property(e => e.BankName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("bankName");
            entity.Property(e => e.NbUsers).HasColumnName("nbUsers");
        
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("transaction");

            entity.Property(e => e.Destination)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("destination");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Source)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("source");
            entity.Property(e => e.TransactionDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("transactionDate");
        });

        modelBuilder.Entity<User>(entity =>
        {
      
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.BirthDay)
                .HasColumnType("date")
                .HasColumnName("birthDay");
            entity.Property(e => e.Cin)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("cin");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("firstName");
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("lastName");
            entity.Property(e => e.Mcc)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("mcc");
            entity.Property(e => e.Mf)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("mf");
           
          
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updatedAt");
            entity.Property(e => e.WalletId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("walletId");
        });


        modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            entity.ToTable("AspNetUserLogins"); // Replace with your table name if needed
        });



        // Configure IdentityUserRole
        modelBuilder.Entity<IdentityUserRole<string>>(entity =>
        {
            entity.HasKey(ur => new { ur.UserId, ur.RoleId });
            entity.ToTable("AspNetUserRoles"); // Replace with your table name if needed
        });


        modelBuilder.Entity<IdentityUserToken<string>>(entity =>
        {
            entity.HasKey(ut => new { ut.UserId, ut.LoginProvider, ut.Name });
            entity.ToTable("AspNetUserTokens"); // Replace with your table name if needed
        });


        modelBuilder.Entity<Wallet>(entity =>
        {
            entity.ToTable("wallet");

            entity.Property(e => e.WalletId)
                .ValueGeneratedNever()
                .HasColumnName("walletID");
            entity.Property(e => e.BankCode).HasColumnName("bankCode");
            entity.Property(e => e.Solde)
                .HasDefaultValueSql("((0))")
                .HasColumnType("money")
                .HasColumnName("solde");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.User)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("user");
            entity.Property(e => e.Validity)
                .HasColumnType("date")
                .HasColumnName("validity");

            entity.HasOne(d => d.BankCodeNavigation).WithMany(p => p.Wallets)
                .HasForeignKey(d => d.BankCode)
                .HasConstraintName("FK_bankCode");
        });

        OnModelCreatingPartial(modelBuilder);
    }



    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
