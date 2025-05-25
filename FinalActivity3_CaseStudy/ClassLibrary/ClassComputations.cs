using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class ClassComputations
    {
        /// <summary>  
        /// Calculates the Suggested Retail Price (SRP) by adding a 15% markup to the base price.  
        /// </summary>  
        /// <param name="price">The base price of the product.</param>  
        /// <returns>The SRP value (base price + 15% markup).</returns>  
        public static double CalculateSRP(double price)
        {
            return price * 1.15;
        }

        /// <summary>  
        /// Calculates the Value Added Tax (VAT) at a rate of 10% on the total amount.  
        /// </summary>  
        /// <param name="totalAmount">The total amount before VAT.</param>  
        /// <returns>The VAT amount (10% of the total amount).</returns>  
        public static double CalculateVAT(double totalAmount)
        {
            return totalAmount * 0.10;
        }


        /// <summary>  
        /// Calculates a discount based on the customer's membership type and total purchase amount.  
        /// </summary>  
        /// <param name="totalAmount">The total purchase amount before discounts.</param>  
        /// <param name="memType">The customer's membership type (Platinum, Gold, Silver).</param>  
        /// <returns>The discount amount, or 0 if no discount applies.</returns>  
        /// <remarks>  
        /// Discount rules:  
        /// - No discount if total amount is below 10,000.  
        /// - Platinum members get 15% discount.  
        /// - Gold members get 10% discount.  
        /// - Silver members get 5% discount.  
        /// - No discount for invalid or empty membership types.  
        /// </remarks>  
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


        /// <summary>  
        /// Calculates the final payable amount after applying VAT and membership discounts.  
        /// </summary>  
        /// <param name="totalAmount">The total purchase amount before taxes and discounts.</param>  
        /// <param name="memType">The customer's membership type (Platinum, Gold, Silver).</param>  
        /// <returns>The final amount after adding VAT and subtracting applicable discounts.</returns>  
        public static double CalculateFinalAmount(double totalAmount, string memType)
        {
            double vat = CalculateVAT(totalAmount);
            double discount = CalculateDiscount(totalAmount, memType);
            return (totalAmount + vat) - discount;
        }
    }
}
