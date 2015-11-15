using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROWF45.CH06.Version.Update.MoviesRental.Workflow.Model
{
    public class CustomerRental
    {
        public CustomerRental()
        {
            Movies = new List<Movie>();
        }
        public Guid RentalId { get; set; }
        public List<Movie> Movies { get; set; }
        public CreditCard PaymentCard { get; set; }
        public string EmailAddress { get; set; }
    }

    [Serializable]
    public class Movie
    {
        public string MovieName { get; set; }
        public string Rating { get; set; }
        public Decimal Price { get; set; }
    }
    public class CreditCard : IEquatable<CreditCard>
    {
        public string CCNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ExpireMonth { get; set; }
        public int ExpireYear { get; set; }
        public int TransactionNumber { get; set; }

        public bool Equals(CreditCard other)
        {
            if (this.CCNumber == other.CCNumber
                && this.ExpireMonth == other.ExpireMonth
                && this.ExpireYear == other.ExpireYear
                && this.FirstName == other.FirstName
                && this.LastName == other.LastName)
                return true;
            else
                return false;
        }
    }
}
