using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class ClassComputations
    {
        public static double CalculateSRP(double price)
        {
            return price * 1.15;
        }

        public static double CalculateVAT(double totalAmount)
        {
            return totalAmount * 0.10;
        }

        public static double CalculateDiscount(double totalAmount, string memType)
        {
            if (string.IsNullOrEmpty(memType))
                return 0;

            if (totalAmount < 10000)
                return 0;

            memType = memType.Trim().ToLower();
            double discountRate = 0;

            if (memType == "platinum") discountRate = 0.15;
            else if (memType == "gold") discountRate = 0.10;
            else if (memType == "silver") discountRate = 0.05;

            return totalAmount * discountRate;
        }

        public static double CalculateFinalAmount(double totalAmount, string memType)
        {
            double vat = CalculateVAT(totalAmount);
            double discount = CalculateDiscount(totalAmount, memType);
            return (totalAmount + vat) - discount;
        }
    }
}
