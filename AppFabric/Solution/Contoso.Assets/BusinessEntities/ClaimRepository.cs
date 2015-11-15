using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    public class ClaimRepository
    {
        private ClaimDataContext db = new ClaimDataContext();

        #region Query Methods
        public IQueryable<Claim> FindAllClaims()
        {
            return db.Claims;
        }


        public IQueryable<Claim> FindPendingClaims()
        {
            return from claim in db.Claims
                   where claim.Status == "Pending"
                   orderby claim.DateCreated
                   select claim;
        }


        public Claim GetLastClaim()
        {
            return db.Claims.OrderByDescending(d => d.ClaimId).FirstOrDefault();
        }



        public Claim GetClaim(int id)
        {
            return db.Claims.FirstOrDefault(d => d.ClaimId == id);
        }

        #endregion

        #region Insert/Delete Methods
        public void Add(Claim claim)
        {
            db.Claims.InsertOnSubmit(claim);
        }

        public void Delete(Claim claim)
        {
            db.Claims.DeleteOnSubmit(claim);
        }

        #endregion

        #region Persistence
        public void Save()
        {
            db.SubmitChanges();
        }
        #endregion

    }
}
