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

        public virtual DbSet<TbErrors> TbErrors { get; set; }
        public virtual DbSet<TbFlowed> TbFlowed { get; set; }
        public virtual DbSet<TbInfo> TbInfo { get; set; }
        public virtual DbSet<TbLogins> TbLogins { get; set; }
        public virtual DbSet<TbMarkers> TbMarkers { get; set; }
        public virtual DbSet<TbRelations> TbRelations { get; set; }
        public virtual DbSet<TbScriptVersion> TbScriptVersion { get; set; }
        public virtual DbSet<TbScriptsNames> TbScriptsNames { get; set; }
        public virtual DbSet<TbServerList> TbServerList { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TbErrors>(entity =>
            {
                entity.ToTable("tb_Errors", "sh_Reports");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateAndTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ErrorMessage).HasMaxLength(512);

                entity.Property(e => e.ServerDb)
                    .IsRequired()
                    .HasColumnName("ServerDB")
                    .HasMaxLength(256)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TbFlowed>(entity =>
            {
                entity.ToTable("tb_Flowed", "sh_Reports");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ScriptVersionId).HasColumnName("ScriptVersionID");

                entity.Property(e => e.ServerDbid).HasColumnName("ServerDBID");

                entity.HasOne(d => d.ScriptVersion)
                    .WithMany(p => p.TbFlowed)
                    .HasForeignKey(d => d.ScriptVersionId)
                    .HasConstraintName("FK_ScriptVersionID_Flowed__TO__ScriptVersion_ID");

                entity.HasOne(d => d.ServerDb)
                    .WithMany(p => p.TbFlowed)
                    .HasForeignKey(d => d.ServerDbid)
                    .HasConstraintName("FK_ServerDBID_Flowed__TO__Relations_ID");
            });

            modelBuilder.Entity<TbInfo>(entity =>
            {
                entity.ToTable("tb_Info", "sh_Reports");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DataTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MessageInfo)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.Script)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.ServerDb)
                    .IsRequired()
                    .HasColumnName("ServerDB")
                    .HasMaxLength(1024)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TbLogins>(entity =>
            {
                entity.ToTable("tb_Logins", "sh_Access");

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

            modelBuilder.Entity<TbMarkers>(entity =>
            {
                entity.ToTable("tb_Markers", "sh_Servers");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MarkerName)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<TbRelations>(entity =>
            {
                entity.ToTable("tb_Relations", "sh_Servers");

                entity.HasIndex(e => new { e.ServerListId, e.MarkerId })
                    .HasName("UI")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MarkerId).HasColumnName("MarkerID");

                entity.Property(e => e.ServerListId).HasColumnName("ServerListID");

                entity.HasOne(d => d.Marker)
                    .WithMany(p => p.TbRelations)
                    .HasForeignKey(d => d.MarkerId)
                    .HasConstraintName("FK_Relations_MarkerID");

                entity.HasOne(d => d.ServerList)
                    .WithMany(p => p.TbRelations)
                    .HasForeignKey(d => d.ServerListId)
                    .HasConstraintName("FK_Relations_ServerListID");
            });

            modelBuilder.Entity<TbScriptVersion>(entity =>
            {
                entity.ToTable("tb_ScriptVersion", "sh_Scripts");

                entity.HasIndex(e => e.Virsion)
                    .HasName("UQ_ScriptVersion_Version")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ScriptContent)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.ScriptId).HasColumnName("ScriptID");

                entity.Property(e => e.Touch)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Virsion).HasColumnType("decimal(3, 2)");

                entity.HasOne(d => d.Script)
                    .WithMany(p => p.TbScriptVersion)
                    .HasForeignKey(d => d.ScriptId)
                    .HasConstraintName("FK_ScriptID_ScriptVersion__TO__ScriptsNames_ID");
            });

            modelBuilder.Entity<TbScriptsNames>(entity =>
            {
                entity.ToTable("tb_ScriptsNames", "sh_Scripts");

                entity.HasIndex(e => e.ScriptName)
                    .HasName("UQ_ScriptsNames_ScriptName")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ScriptName)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TbServerList>(entity =>
            {
                entity.ToTable("tb_ServerList", "sh_Servers");

                entity.HasIndex(e => new { e.ServerDomainName, e.DataBaseName })
                    .HasName("UQ_ServerDB")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DataBaseName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.ServerDomainName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.ServerLogin)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.ServerPassword)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
