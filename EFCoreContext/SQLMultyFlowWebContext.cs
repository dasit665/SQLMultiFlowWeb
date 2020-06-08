using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SQLMultiFlowWeb
{
    public partial class SQLMultyFlowWebContext : DbContext
    {
        public SQLMultyFlowWebContext()
        {
        }

        public SQLMultyFlowWebContext(DbContextOptions<SQLMultyFlowWebContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TbLogins> TbLogins { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-3PLVB9O;User ID=EFC_login_SMFW;Password=open123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TbLogins>(entity =>
            {
                entity.ToTable("tb_Logins");

                entity.HasIndex(e => e.UserName)
                    .HasName("UQ_tb_Logins_UserName")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.HashPasswors)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(48)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
