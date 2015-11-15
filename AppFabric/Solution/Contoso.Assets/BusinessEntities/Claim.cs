using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;


namespace BusinessEntities
{
    public partial class Claim
    {
        public bool IsValid
        {
            get { return (GetRuleViolations().Count() == 0); }
        }


        public int NewClaimId
        {
            get
            {
                ClaimRepository claimRepository = new ClaimRepository();
                Claim lastClaim = claimRepository.GetLastClaim();
                return lastClaim.ClaimId + 1;
            }
        }


        public IEnumerable<RuleViolation> GetRuleViolations()
        {
            if (String.IsNullOrEmpty(DateCreated.ToString()))
                yield return new RuleViolation("Date when claim was created required", "DateCreated");

            DateTime date = new DateTime();
            if( !DateTime.TryParse(DateCreated.ToString(), out date))
                yield return new RuleViolation("Claim creation date is invalid", "DateCreated");

            if (String.IsNullOrEmpty(Description))
                yield return new RuleViolation("Description required", "Description");

            if (String.IsNullOrEmpty(Accidents.ContactPhone))
                yield return new RuleViolation("Contact phone # required", "ContactPhone");

            if (!PhoneValidator.IsValidNumber(Accidents.ContactPhone))
                yield return new RuleViolation("Contact phone is invalid", "ContactPhone");

            if (!PhoneValidator.IsValidCoordinate(Accidents.Latitude.ToString()))
                yield return new RuleViolation("Latitude is invalid", "Latitude");

            if (!PhoneValidator.IsValidCoordinate(Accidents.Longitude.ToString()))
                yield return new RuleViolation("Longitude is invalid", "Longitude");

            yield break;
        }



        public IEnumerable<RuleViolation> SetRuleViolation(string message, string title)
        {
            yield return new RuleViolation(message, title);
        }



        partial void OnValidate(ChangeAction action)
        {
            if (!IsValid)
                throw new ApplicationException("Rule violations prevent saving");
        }



        public bool IsIdentical(Claim claim)
        {
            if (this.ClaimId != claim.ClaimId) return false;
            if (this.Accidents.FName != claim.Accidents.FName) return false;
            if (this.Accidents.LName != claim.Accidents.LName) return false;
            if (this.Accidents.Address != claim.Accidents.Address) return false;
            if (this.Accidents.City != claim.Accidents.City) return false;
            if (this.Accidents.State != claim.Accidents.State) return false;
            if (this.Accidents.Zip != claim.Accidents.Zip) return false;
            if (this.Accidents.ContactPhone != claim.Accidents.ContactPhone) return false;
            if (this.Accidents.Latitude != claim.Accidents.Latitude) return false;
            if (this.Accidents.Longitude != claim.Accidents.Longitude) return false;

            if (this.Description != claim.Description) return false;
            if (this.Status != claim.Status) return false;
            if (this.RentalCar != claim.RentalCar) return false;

            return true;
        }


        public Claim Copy()
        {
            Claim newClaim = new Claim();
            newClaim.ClaimId = this.ClaimId;
            newClaim.Accidents = new Accident();
            newClaim.Accidents.FName = this.Accidents.FName;
            newClaim.Accidents.LName = this.Accidents.LName;
            newClaim.Accidents.Address = this.Accidents.Address;
            newClaim.Accidents.City = this.Accidents.City;
            newClaim.Accidents.State = this.Accidents.State;
            newClaim.Accidents.Zip = this.Accidents.Zip;
            newClaim.Accidents.ContactPhone = this.Accidents.ContactPhone;
            newClaim.Accidents.Latitude = this.Accidents.Latitude;
            newClaim.Accidents.Longitude = this.Accidents.Longitude;

            newClaim.Description = this.Description;
            newClaim.Status = this.Status;
            newClaim.DateCreated = this.DateCreated;
            newClaim.RentalCar = this.RentalCar;

            return newClaim;
        }
    }


    public class RuleViolation
    {
        public string ErrorMessage { get; private set; }
        public string PropertyName { get; private set; }
        
        public RuleViolation(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
        
        public RuleViolation(string errorMessage, string propertyName)
        {
            ErrorMessage = errorMessage;
            PropertyName = propertyName;
        }
    }

}