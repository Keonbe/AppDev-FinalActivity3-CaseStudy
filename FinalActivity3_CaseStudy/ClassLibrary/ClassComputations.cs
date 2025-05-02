using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class ClassComputations
    {
        /// Calculates the Suggested Retail Price as Price + 15%
        public static double CalculateSRP(double price)
        {
            return price * 1.15;
        }

        /// Calculates VAT (10% of total amount)
        public static double CalculateVAT(double totalAmount)
        {
            return totalAmount * 0.10;
        }

        /// Calculates discount based on membership tier
        public static double CalculateDiscount(double totalAmount, int customerTypeIndex)
        {
            if (totalAmount < 10000)
                return 0;

            double discountRate;

            switch (customerTypeIndex)
            {
                case 0: // Platinum
                    discountRate = 0.15;
                    break;
                case 1: // Gold
                    discountRate = 0.10;
                    break;
                case 2: // Silver
                    discountRate = 0.05;
                    break;
                default:
                    throw new ArgumentException("Invalid membership type");
            }

            return totalAmount * discountRate;
        }

        /// Calculates the final amount to be paid: (Total + VAT) - Discount
        public static double CalculateFinalAmount(double totalAmount, int customerTypeIndex)
        {
            double vat = CalculateVAT(totalAmount);
            double discount = CalculateDiscount(totalAmount, customerTypeIndex);
            return (totalAmount + vat) - discount;
        }

        //---

        // Returns the string label for the membership tier index
        public static string CustomerTypeString(int customerTypeIndex)
        {
            switch (customerTypeIndex)
            {
                case 0: return "Platinum";
                case 1: return "Gold";
                case 2: return "Silver";
                default: throw new ArgumentException("Invalid membership type");
            }
        }

    }
}
