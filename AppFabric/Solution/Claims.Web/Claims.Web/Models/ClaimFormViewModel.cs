using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Claims.Web.Models;
using BusinessEntities;

namespace Claims.Web.Models
{
    public class ClaimFormViewModel
    {
        //Properties
        public Claim Claim { get; private set; }
        public SelectList States { get; private set; }

        //Constructor
        public ClaimFormViewModel(Claim claim)
        {
            Claim = claim;
            States = new SelectList(USStates, claim.Accidents.State);
        }


        public static IEnumerable<string> USStates
        {
            get
            {
                return new List<string>() { "AL", "AK", "AZ", "AR", "CA", "CO", "CT", "DE", "DC", "FL",
                                            "GA", "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME",
                                            "MD", "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH",
                                            "NJ", "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA", "RI",
                                            "SC", "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI",
                                            "WY"};
            }
        }
    }
}