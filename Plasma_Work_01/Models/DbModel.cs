using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Plasma_Work_01.Models
{
    public enum BloodGroup { A, B, AB, O }
    public class PlasmaDonation
    {
        public PlasmaDonation()
        {
            this.CoronaPositivePaitents = new List<CoronaPositivePaitent>();
        }

        [Display(Name = "Donation ID")]
        public int PlasmaDonationId { get; set; }

        [Required, StringLength(40), Display(Name = "Donator Name")]
        public string DonatorName { get; set; }

        [Required, Display(Name = "Blood Group")]
        [EnumDataType(typeof(BloodGroup))]
        public BloodGroup? BloodGroup { get; set; }

        //[Required, Column(TypeName = "date")]
        //[Display(Name = "Recovery date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //public DateTime RecoveryDate { get; set; }

        public virtual ICollection<CoronaPositivePaitent> CoronaPositivePaitents { get; set; }
    }

    public class CoronaPositivePaitent
    {
        public CoronaPositivePaitent()
        {
            this.ReasonsforNotRecoveredCoronaPaitents = new List<ReasonsforNotRecoveredCoronaPaitent>();
        }
        [Display(Name = "Paitent ID")]
        public int CoronaPositivePaitentId { get; set; }

        [Required, StringLength(40), Display(Name = "Paitent Name")]
        public string PaitentName { get; set; }

        [Required, StringLength(50), Display(Name = "Hospital/Home")]
        public string Hospitalorhome { get; set; }

        [Required, StringLength(40), Display(Name = "Contact No")]
        public string ContactNo { get; set; }

        //Fk
        [Required, ForeignKey("PlasmaDonation")]
        public int PlasmaDonationId { get; set; }
        public virtual PlasmaDonation PlasmaDonation { get; set; }
        public virtual ICollection<ReasonsforNotRecoveredCoronaPaitent> ReasonsforNotRecoveredCoronaPaitents { get; set; }
    }
    public class ReasonsforNotRecoveredCoronaPaitent
    {
        [Display(Name = "Reason ID")]
        public int ReasonsforNotRecoveredCoronaPaitentId { get; set; }

        [Required, StringLength(50), Display(Name = "Disease Name")]
        public string DiseaseName { get; set; }

        [Required, StringLength(40), Display(Name = "Error in Collection")]
        public string ImproperPlasma { get; set; }

        [Required, StringLength(30), Display(Name = "Critical State")]
        public string CriticalState { get; set; }

        [Required, ForeignKey("CoronaPositivePaitent")]
        public int CoronaPositivePaitentId { get; set; }
        public virtual CoronaPositivePaitent CoronaPositivePaitent { get; set; }

    }
    public class CoronaPaitentDbContext : DbContext
    {
        public CoronaPaitentDbContext()
        {
            Database.SetInitializer(new InitialCreate());
        }
        public DbSet<PlasmaDonation> PlasmaDonations { get; set; }
        public DbSet<CoronaPositivePaitent> CoronaPositivePaitents { get; set; }
        public DbSet<ReasonsforNotRecoveredCoronaPaitent> ReasonsforNotRecoveredCoronaPaitents { get; set; }
    }
    public class InitialCreate : DropCreateDatabaseIfModelChanges<CoronaPaitentDbContext>
    {
        protected override void Seed(CoronaPaitentDbContext db)
        {
            string[] DonatorName = { "Rouza Chowdhury", "Natasha Rately", "Asif Iqbal", "Binondo Saha", "Trisha Gosh", "Sakib Hasan" };
            Random r = new Random();
            //var i = 1;
            foreach (string s in DonatorName)
            {
                var t = new PlasmaDonation { DonatorName = s, BloodGroup = (BloodGroup)r.Next(0, 4) };
                CoronaPositivePaitent c1 = new CoronaPositivePaitent { PaitentName = (s.Substring(0, 1) + "ilok" + (s.Substring(1, 3))), Hospitalorhome = "Dhaka Medical Colege", ContactNo = "014564", PlasmaDonation = t };
                CoronaPositivePaitent c2 = new CoronaPositivePaitent { PaitentName = (s.Substring(0, 1) + "ajom" + (s.Substring(1, 3))), Hospitalorhome = "Home", ContactNo = "019874", PlasmaDonation = t };

                t.CoronaPositivePaitents.Add(c1);
                t.CoronaPositivePaitents.Add(c2);
                db.PlasmaDonations.Add(t);
            }
            db.SaveChanges();


        }
    }
}