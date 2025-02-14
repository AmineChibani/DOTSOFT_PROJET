using ClientService.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Data;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;


namespace ClientService.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        private readonly ILogger<AppDbContext> _logger;
        private readonly IConfiguration _configuration;


        public AppDbContext(DbContextOptions<AppDbContext> options,
                            ILogger<AppDbContext> logger,
                            IConfiguration configuration)
            : base(options)
        {
            _logger = logger;
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Get the connection string from appsettings.json
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            // Use the logger factory
            optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));

            // Enable sensitive data logging (use cautiously in production)
            optionsBuilder.EnableSensitiveDataLogging();

            // Use provider Oracle DbContext options extension with the connection string
            optionsBuilder.UseOracle(connectionString);
        }

        public DbSet<DbClient> Clients { get; set; }

        public DbSet<DbClientFacture> ClientFactures { get; set; }

        public DbSet<DbClientFactureLigne> ClientFatureLignes { get; set; }

        public DbSet<DbClientAdresse> ClientAdresses { get; set; }

        public DbSet<DbEcommerceBa> EcommerceBa { get; set; }

        public DbSet<DbClientAdresseComplement> ClientAdresseComplement { get; set; }

        public DbSet<DbParamCodePostal> ParamCodePostals { get; set; }

        public DbSet<DbParamTypeVoie> ParamTypeVoie { get; set; }

        public DbSet<DbClientOptin> ClientOptin { get; set; }

        public DbSet<DbParamPays> Pays { get; set; }

        public DbSet<DbParamRegion> Regions { get; set; }

        public DbSet<DbFactureTypeReglement> FactureTypeReglement { get; set; }

        public DbSet<DbClientOperation> ClientOperations { get; set; }

        public DbSet<DbParamCategSocioProf> ParamCategSocioProfS { get; set; }

        public DbSet<DbLanguageParamCategSocioProf> LanguageParamCategSocioProfs { get; set; }

        public DbSet<DbMontantCredit> MontantCredits { get; set; }
        public DbSet<DbDroitsSpeciaux> DroitSpeciaux { get; set; }

        // Utilise DbUpdateException.Entries pour récupérer les entités en erreur.
        // Valide manuellement les champs obligatoires avant d'enregistrer (SaveChanges()).
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                var errorMessages = new List<string>();

                foreach (var entry in ex.Entries)
                {
                    foreach (var property in entry.CurrentValues.Properties)
                    {
                        var propertyValue = entry.CurrentValues[property];

                        if (propertyValue == null || string.IsNullOrWhiteSpace(propertyValue.ToString()))
                        {
                            errorMessages.Add($"Le champ {property.Name} ne peut pas être vide.");
                        }
                    }
                }

                throw new DbUpdateException(string.Join("; ", errorMessages), ex);
            }
        }

        public int GetNextSequenceValue()
        {
            int nextSequenceValue = 0;
            DbConnection dbConnection = this.Database.GetDbConnection();
            using (DbCommand command = dbConnection.CreateCommand())
            {
                command.CommandText = "select s_client.nextval as id_client from dual";
                if (dbConnection.State.Equals((object)ConnectionState.Closed))
                    dbConnection.Open();
                nextSequenceValue = Convert.ToInt32(command.ExecuteScalar());
            }
            if (dbConnection.State.Equals((object)ConnectionState.Open))
                dbConnection.Close();
            return nextSequenceValue;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DbDroitsSpeciaux>().HasNoKey();
            modelBuilder.Entity<DbClient>().HasMany<DbClientAdresse>((Expression<Func<DbClient, IEnumerable<DbClientAdresse>>>)(x => x.ClientAdresses)).WithOne((Expression<Func<DbClientAdresse, DbClient>>)(x => x.Client)).HasForeignKey((Expression<Func<DbClientAdresse, object>>)(x => (object)x.ClientId));
            modelBuilder.Entity<DbClientFacture>().HasMany<DbClientFactureLigne>((Expression<Func<DbClientFacture, IEnumerable<DbClientFactureLigne>>>)(x => x.ClientFactureLignes)).WithOne((Expression<Func<DbClientFactureLigne, DbClientFacture>>)(x => x.ClientFacture)).HasForeignKey((Expression<Func<DbClientFactureLigne, object>>)(x => (object)x.IdFactureC));
            modelBuilder.Entity<DbFactureTypeReglement>().HasMany<DbClientOperation>((Expression<Func<DbFactureTypeReglement, IEnumerable<DbClientOperation>>>)(x => x.ClientOperations)).WithOne((Expression<Func<DbClientOperation, DbFactureTypeReglement>>)(x => x.FactureTypeReglement)).HasForeignKey((Expression<Func<DbClientOperation, object>>)(x => (object)x.IdTypeReglement));
            modelBuilder.Entity<DbClient>().HasOne<DbClientOptin>((Expression<Func<DbClient, DbClientOptin>>)(a => a.ClientOptin)).WithOne((Expression<Func<DbClientOptin, DbClient>>)(b => b.Client)).HasForeignKey<DbClientOptin>((Expression<Func<DbClientOptin, object>>)(x => (object)x.ClientId));
            modelBuilder.Entity<DbLanguageParamCategSocioProf>().HasMany<DbParamCategSocioProf>((Expression<Func<DbLanguageParamCategSocioProf, IEnumerable<DbParamCategSocioProf>>>)(x => x.DbParamCategSocioProfs)).WithOne((Expression<Func<DbParamCategSocioProf, DbLanguageParamCategSocioProf>>)(x => x.LanguageParamCategSocioPro)).HasForeignKey((Expression<Func<DbParamCategSocioProf, object>>)(x => (object)x.IdCsp));
            modelBuilder.Entity<DbClient>().HasMany<DbClientAdresseComplement>((Expression<Func<DbClient, IEnumerable<DbClientAdresseComplement>>>)(x => x.ClientAdresseComplement)).WithOne((Expression<Func<DbClientAdresseComplement, DbClient>>)(x => x.Client)).HasForeignKey((Expression<Func<DbClientAdresseComplement, object>>)(x => (object)x.ClientId));
            modelBuilder.Entity<DbClientAdresse>().HasKey((Expression<Func<DbClientAdresse, object>>)(pc => new
            {
                ClientId = pc.ClientId,
                AdresseTypeId = pc.AdresseTypeId
            }));
            modelBuilder.Entity<DbClientOperation>().HasKey((Expression<Func<DbClientOperation, object>>)(pc => new
            {
                IdOperation = pc.IdOperation,
                TypeDocument = pc.TypeDocument
            }));
            modelBuilder.Entity<DbClientFactureLigne>().HasKey((Expression<Func<DbClientFactureLigne, object>>)(pc => new
            {
                IdLigne = pc.IdLigne,
                IdFactureC = pc.IdFactureC,
                IdProduit = pc.IdProduit,
                IdTaxe = pc.IdTaxe
            }));
            modelBuilder.Entity<DbClientAdresseComplement>().HasKey((Expression<Func<DbClientAdresseComplement, object>>)(pc => new
            {
                ClientId = pc.ClientId,
                AdresseTypeId = pc.AdresseTypeId
            }));
        }
    }
}
