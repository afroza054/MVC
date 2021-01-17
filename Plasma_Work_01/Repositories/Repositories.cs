using Plasma_Work_01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Plasma_Work_01.Repositories
{
    public interface IPlasmaRepository
    {
        List<PlasmaDonation> GetAllRelated();
        List<ReasonsforNotRecoveredCoronaPaitent> GetReasonsOfUnrecoverPaitent(int id);
        List<PlasmaDonation> GetDonatorNameForDropDown();
        void InsertCoronaPaitentWithReasons(CoronaPositivePaitent cp);
    }
    public class CoronaPositveRepositories : IPlasmaRepository
    {
        CoronaPaitentDbContext db = new CoronaPaitentDbContext();
        public List<PlasmaDonation> GetAllRelated()
        {
            return db.PlasmaDonations.Include(t => t.CoronaPositivePaitents.Select(cp => cp.ReasonsforNotRecoveredCoronaPaitents)).ToList(); 
         
        }

        public List<PlasmaDonation> GetDonatorNameForDropDown()
        {
            return db.PlasmaDonations.ToList();
        }

        public List<ReasonsforNotRecoveredCoronaPaitent> GetReasonsOfUnrecoverPaitent(int id)
        {
            return db.ReasonsforNotRecoveredCoronaPaitents.Where(x => x.CoronaPositivePaitentId == id).ToList();
        }

        public void InsertCoronaPaitentWithReasons(CoronaPositivePaitent cp)
        {
            db.CoronaPositivePaitents.Add(cp);
            db.SaveChanges();
        }
    }
}