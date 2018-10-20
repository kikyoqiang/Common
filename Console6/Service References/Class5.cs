using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console6
{
    class Class5
    {
        static void Main22()
        {

        }
    }
    public class Movie
    {
        private Price _price;
        public const int CHILDRENS = 2;
        public const int REGULAR = 0;
        public const int NEW_RELEASE = 1;
        private string _title;
        //private int _priceCode;
        public Movie(string title, int priceCode)
        {
            _title = title;
            SetPriceCode(priceCode);
        }
        //public int GetPriceCode()
        //{
        //    return _priceCode;
        //}
        //public void SetPriceCode(int arg)
        //{
        //    _priceCode = arg;
        //}
        public int GetPriceCode()
        {
            return _price.GetPriceCode();
        }
        public void SetPriceCode(int arg)
        {
            switch (arg)
            {
                case REGULAR:
                    _price = new RegularPrice();
                    break;
                case CHILDRENS:
                    _price = new ChildrensPrice();
                    break;
                case NEW_RELEASE:
                    _price = new NewReleasePrice();
                    break;
                default:
                    throw new ArgumentException("Incorrect Price Code");
            }
        }
        public string GetTitle()
        {
            return _title;
        }
        public double GetCharge(int daysRented)
        {
            return _price.GetCharge(daysRented);
        }

        public int GetFrequentRenterPoints(int daysRented)
        {
            return _price.GetFrequentRenterPoints(daysRented);
        }
    }
    public class Rental
    {
        private Movie _movie;
        private int _daysRented;
        public Rental(Movie movie, int daysRented)
        {
            _movie = movie;
            _daysRented = daysRented;
        }
        public int getDaysRented()
        {
            return _daysRented;
        }
        public Movie getMovie()
        {
            return _movie;
        }

        public double GetCharge()
        {
            return _movie.GetCharge(_daysRented);
        }

        public int GetFrequentRenterPoints()
        {
            return _movie.GetFrequentRenterPoints(_daysRented);
        }
    }
    public class Customer
    {
        private string _name;
        private List<Rental> _rentals = new List<Rental>();
        public Customer(string name)
        {
            _name = name;
        }
        public void AddRental(Rental arg)
        {
            _rentals.Add(arg);
        }
        public string GetName()
        {
            return _name;
        }
        public string Statement()
        {
            double totalAmount = 0;
            //int frequentRenterPoints = 0;
            List<Rental> rentals = this._rentals;
            string result = $"Rental Record for {GetName()} \n";
            foreach (Rental rental in rentals)
            {
                double thisAmount = rental.GetCharge();
                //frequentRenterPoints += rental.GetFrequentRenterPoints();
                result += string.Format("\t{0}\t{1}\n", rental.getMovie().GetTitle(), thisAmount);
                totalAmount += thisAmount;
            }
            result += $"Amount owed is {GetTotalCharge(rentals)} \n";
            result += $"You earned {GetTotaIFrequentRenterPoints(rentals)} frequent renter points";
            return result;
        }
        public String htmlStatement()
        {
            double totalAmount = 0;
            List<Rental> rentals = this._rentals;
            string result = "<H1>Rentals for <EM>" + GetName() + "</EM></H1><P>\n";
            foreach (Rental rental in rentals)
            {
                double thisAmount = rental.GetCharge();
                result += string.Format("\t{0}\t{1}\n", rental.getMovie().GetTitle(), thisAmount);
                totalAmount += thisAmount;
            }
            result += $"<P>You owe <EM> {GetTotalCharge(rentals)} </EM><P>\n";
            result += $"On this rental you earned <EM> {GetTotaIFrequentRenterPoints(rentals)} </EM> frequent renter points<P>";
            return result;
        }
        private double GetTotalCharge(List<Rental> rentals)
        {
            double result = 0;
            foreach (Rental item in rentals)
            {
                result += item.GetCharge();
            }
            return result;
        }
        private double GetTotaIFrequentRenterPoints(List<Rental> rentals)
        {
            double result = 0;
            foreach (Rental item in rentals)
            {
                result += item.GetFrequentRenterPoints();
            }
            return result;
        }
        #region statement2
        public string statement2()
        {
            double totalAmount = 0;
            int frequentRenterPoints = 0;
            List<Rental> rentals = this._rentals;
            String result = "Rental Record for " + GetName() + "\n";
            foreach (var item in rentals)
            {
                double thisAmount = 0;
                Rental each = item;
                //determine amounts for each line
                switch (each.getMovie().GetPriceCode())
                {
                    case Movie.REGULAR:
                        thisAmount += 2;
                        if (each.getDaysRented() > 2)
                            thisAmount += (each.getDaysRented() - 2) * 1.5;
                        break;
                    case Movie.NEW_RELEASE:
                        thisAmount += each.getDaysRented() * 3;
                        break;
                    case Movie.CHILDRENS:
                        thisAmount += 1.5;
                        if (each.getDaysRented() > 3)
                            thisAmount += (each.getDaysRented() - 3) * 1.5;
                        break;
                }
                // add frequent renter points
                frequentRenterPoints++;
                // add bonus for a two day new release rental
                if ((each.getMovie().GetPriceCode() == Movie.NEW_RELEASE) &&
                each.getDaysRented() > 1) frequentRenterPoints++;
                //show figures for this rental
                result += "\t" + each.getMovie().GetTitle() + "\t" + thisAmount + "\n";
                totalAmount += thisAmount;
            }
            //add footer lines
            result += "Amount owed is " + totalAmount + "\n";
            result += "You earned " + frequentRenterPoints +
            " frequent renter points";
            return result;
        }
        #endregion
    }
    public abstract class Price
    {
        public abstract int GetPriceCode();
        public virtual double GetCharge(int daysRented)
        {
            double result = 0;
            switch (GetPriceCode())
            {
                case Movie.REGULAR:
                    result += 2;
                    if (daysRented > 2)
                        result += (daysRented - 2) * 1.5;
                    break;
                case Movie.NEW_RELEASE:
                    result += daysRented * 3;
                    break;
                case Movie.CHILDRENS:
                    result += 1.5;
                    if (daysRented > 3)
                        result += (daysRented - 3) * 1.5;
                    break;
            }
            return result;
        }
        public virtual int GetFrequentRenterPoints(int daysRented)
        {
            return 1;
        }
    }
    public class ChildrensPrice : Price
    {
        public override int GetPriceCode()
        {
            return Movie.CHILDRENS;
        }
        public override double GetCharge(int daysRented)
        {
            double result = 1.5;
            if (daysRented > 3)
                result += (daysRented - 3) * 1.5;
            return result;
        }
    }
    public class NewReleasePrice : Price
    {
        public override int GetPriceCode()
        {
            return Movie.NEW_RELEASE;
        }
        public override double GetCharge(int daysRented)
        {
            return daysRented * 3;
        }
        public override int GetFrequentRenterPoints(int daysRented)
        {
            return (daysRented > 1) ? 2 : 1;
        }
    }
    public class RegularPrice : Price
    {
        public override int GetPriceCode()
        {
            return Movie.REGULAR;
        }
        public override double GetCharge(int daysRented)
        {
            double result = 2;
            if (daysRented > 2)
                result += (daysRented - 2) * 1.5;
            return result;
        }
    }
}
