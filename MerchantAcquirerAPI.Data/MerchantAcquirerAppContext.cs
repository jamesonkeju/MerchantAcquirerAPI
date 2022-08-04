
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MerchantAcquirerAPI.Data.Models;
using MerchantAcquirerAPI.Data.Models.Domains;

using MerchantAcquirerAPI.Data.Models.Indentity;

namespace MerchantAcquirerAPI.Data
{
    public class MerchantAcquirerAPIAppContext : IdentityDbContext<ApplicationUser,
        ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole,
        IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>

    {

        public MerchantAcquirerAPIAppContext(DbContextOptions<MerchantAcquirerAPIAppContext> options) : base(options)
        {
            //Database.Migrate();

        }


        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MerchantAcquirerAPIAppContext>
        {

            public MerchantAcquirerAPIAppContext CreateDbContext(string[] args)
            {

                var optionsBuilder = new DbContextOptionsBuilder<MerchantAcquirerAPIAppContext>();

                var config = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", optional: false)
                  .Build();



                var connect = config.GetSection("ConnectionStrings").Get<List<string>>().FirstOrDefault();
                // optionsBuilder.UseSqlServer(connect);
                optionsBuilder.UseSqlServer(connect, options => options.MigrationsAssembly("MerchantAcquirerAPI.Data"));

                return new MerchantAcquirerAPIAppContext(optionsBuilder.Options);
            }
        }

        public MerchantAcquirerAPIAppContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();



            var connect = config.GetSection("ConnectionStrings").Get<List<string>>().FirstOrDefault();
            // optionsBuilder.UseSqlServer(connect);
            optionsBuilder.UseSqlServer(connect,options=>options.MigrationsAssembly("MerchantAcquirerAPI.Data"));
        }
        /// <summary>
        /// https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-3.1&tabs=visual-studio
        /// </summary>
        /// 

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            //builder.Seed();

            builder.Ignore<ApplicationUserLogin>();
            //  builder.Ignore<ApplicationUserRole>();
            builder.Ignore<UserLoginHistory>();



            builder.Entity<ApplicationUser>(b => {
                b.HasKey(x => x.Id);
                b.Property(x => x.Id).ValueGeneratedOnAdd();
                b.HasMany(x => x.Roles).WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
            });

            builder.Entity<ApplicationUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.Users)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.Roles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            //  builder.Entity<ApplicationUserRole>().HasNoKey();
            //  base.OnModelCreating(builder);
        }

        #region StartUp

        public virtual DbSet<ActivityLog> ActivityLog { get; set; }
        public DbSet<SystemSetting> SystemSettings { get; set; }

        public DbSet<AccessRight> AccessRight { get; set; }
        public DbSet<AcctType> AcctType { get; set; }
        public DbSet<AuditLog> AuditLog { get; set; }
        public DbSet<Branch> Branch { get; set; }
        public DbSet<BusinessCategory> BusinessCategory { get; set; }
        public DbSet<BusinessOccupation> BusinessOccupation { get; set; }
        public DbSet<DeviceTab> DeviceTab { get; set; }
        public DbSet<MccInfo> MccInfo { get; set; }
        public DbSet<MerchantIDTab> MerchantIDTab { get; set; }
        public DbSet<NetworkTab> NetworkTab { get; set; }

        public DbSet<PosReq> PosReq { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<Lga> LGA { get; set; }
        public DbSet<TerminalModel> TerminalModel { get; set; }
        public DbSet<TerminalOwner> TerminalOwner { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }

        public DbSet<RequestStatus> RequestStatus { get; set; }
        #endregion





        #region Errorlog::
        public DbSet<ErrorLog> ErrorLogs { get; set; }


        #endregion

       

        public async Task<bool> TrySaveChangesAsync(ILogger logger)
        {
            try
            {
                await SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException e)
            {
                logger.LogError($"unable to add  >>>>> {e.Message}");
                logger.LogError($"DB add  Inner Exception Message >>>>> {e.InnerException?.Message}");
                return false;
            }
        }

    }
}

