using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ServiceCockpit.Models
{
    // Sie können Profildaten für den Benutzer hinzufügen, indem Sie der ApplicationUser-Klasse weitere Eigenschaften hinzufügen. Weitere Informationen finden Sie unter https://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Beachten Sie, dass der "authenticationType" mit dem in "CookieAuthenticationOptions.AuthenticationType" definierten Typ übereinstimmen muss.
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Benutzerdefinierte Benutzeransprüche hier hinzufügen
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        //Stammddaten--------------------------------------------------------------------------
        public DbSet<Mitarbeiter> Mitarbeiter { get; set; }
        public DbSet<Ausführungsadresse> Ausführungsadresse { get; set; }
        public DbSet<Rechnungsadresse> Rechnungsadresse { get; set; }
        public DbSet<Eigentuemeradresse> Eigentuemeradresse { get; set; }
        public DbSet<Organisation> Organisation { get; set; }
        public DbSet<Projekt> Projekt { get; set; }
        public DbSet<Verrechnungsart> Verrechnungsart { get; set; }
        public DbSet<WochenrapportUeberzeitFaktor> WochenrapportUeberzeitFaktor { get; set; }
        public DbSet<Material> Material { get; set; }

        //Servicrapport------------------------------------------------------------------------
        public DbSet<Servicerapport> Servicerapport { get; set; }
        public DbSet<MaterialKosten> MaterialKosten { get; set; }
        public DbSet<ZeitKosten> ZeitKosten { get; set; }

        //Wochenrapport------------------------------------------------------------------------
        public DbSet<Wochenrapport> Wochenrapport { get; set; }
        public DbSet<WochenrapportSpesenEintrag> WochenrapportSpesenEintrag { get; set; }
        public DbSet<WochenrapportZeitEintrag> WochenrapportZeitEintrag { get; set; }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}