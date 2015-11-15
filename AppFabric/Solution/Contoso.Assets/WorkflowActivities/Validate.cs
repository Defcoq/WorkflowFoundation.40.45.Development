using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Activities;

namespace BusinessEntities
{
    public class Validate : CodeActivity
    {
        static Regex countryRegex = new Regex("^[2-9]\\d{2}-\\d{3}-\\d{4}$");

        public InArgument<Claim> Entity { get; set; }
        public OutArgument<bool> IsValid { get; set; }


        protected override void Execute(CodeActivityContext context)
        {
            Claim claim = Entity.Get<Claim>(context);
            IsValid.Set(context, true);

            if (String.IsNullOrEmpty(claim.DateCreated.ToString()))
                IsValid.Set(context, false);

            DateTime date = new DateTime();
            if (!DateTime.TryParse(claim.DateCreated.ToString(), out date))
                IsValid.Set(context, false);

            if (String.IsNullOrEmpty(claim.Description))
                IsValid.Set(context, false);

            if (String.IsNullOrEmpty(claim.Accidents.ContactPhone))
                IsValid.Set(context, false);

            if (!IsValidNumber(claim.Accidents.ContactPhone))
                IsValid.Set(context, false);

            if (!IsValidCoordinate(claim.Accidents.Latitude.ToString()))
                IsValid.Set(context, false);

            if (!IsValidCoordinate(claim.Accidents.Longitude.ToString()))
                IsValid.Set(context, false);

        }

        private static bool IsValidNumber(string phoneNumber)
        {
            return countryRegex.IsMatch(phoneNumber);
        }

        private static bool IsValidCoordinate(string coord)
        {
            float result;
            return float.TryParse(coord, out result);
        }

    }
}
