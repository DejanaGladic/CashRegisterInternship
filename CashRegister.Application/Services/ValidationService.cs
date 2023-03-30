﻿using System.Text.RegularExpressions;

namespace CashRegister.Application.Services
{
    public class ValidationService
    {
        public bool IsValidBillNumber(string billNumber)
        {
            Regex regex = new Regex(@"^\d{3}-\d{13}-\d{2}$");
            if (!regex.IsMatch(billNumber))
            {
                return false;
            }

            string identificationCode = billNumber.Substring(0, 3);
            string billNum = billNumber.Substring(4, 13);
            string controlNum = billNumber.Substring(18, 2);
            string concatenatedNum = identificationCode + billNum;
            long multipliedNum = long.Parse(concatenatedNum) * 100;
            int controlCalc = 98 - (int)(multipliedNum % 97);
            int controlNumInt = int.Parse(controlNum);
            return controlCalc == controlNumInt;
        }
        public bool isValidCreditCard(string creditCard)
        {
            bool isValid = true;
            if (creditCard == null)
            {
                isValid = false;
                return isValid;
            }
            if (creditCard.Length != 13 && creditCard.Length != 15 && creditCard.Length != 16)
            {
                isValid = false;
            }
            else
            {
                if ((creditCard.Length == 13 || creditCard.Length == 16) && creditCard.StartsWith('4'))
                {
                    isValid = ValidateCreditCard(creditCard);

                }
                else if (creditCard.Length == 15 && (creditCard.StartsWith("34") || creditCard.StartsWith("37")))
                {
                    isValid = ValidateCreditCard(creditCard);
                }
                else if (creditCard.Length == 16 && (creditCard.StartsWith("51") || creditCard.StartsWith("52") || creditCard.StartsWith("53")
                    || creditCard.StartsWith("54") || creditCard.StartsWith("55")))
                {
                    isValid = ValidateCreditCard(creditCard);
                }

                else isValid = false;
            }
            return isValid;
        }
        private bool ValidateCreditCard(string card)
        {
            var cardReverse = card.Reverse();
            var reverseEveryOtherSecondToLast 
                = new string(cardReverse.Where((ch, index) => index % 2 != 0).ToArray());
            string MultiplyDigitsByTwo = "";
            for (int i = 0; i < reverseEveryOtherSecondToLast.Length; i++)
            {
                int num = Int32.Parse(reverseEveryOtherSecondToLast[i].ToString());
                num = num * 2;
                MultiplyDigitsByTwo = MultiplyDigitsByTwo + num.ToString();
            }
            int multipliedDigitsSummed = 0;
            for (int i = 0; i < MultiplyDigitsByTwo.Length; i++)
            {
                int num = Int32.Parse(MultiplyDigitsByTwo[i].ToString());
                multipliedDigitsSummed = multipliedDigitsSummed + num;
            }
            var digitsWerentMultiplied = new string(cardReverse.Where((ch, index) => index % 2 == 0).ToArray());
            var digitsWerentMultipliedToDigits = Int32.Parse(digitsWerentMultiplied);
            int result = 0;
            for (int i = 0; i < digitsWerentMultiplied.Length; i++)
            {
                int num = Int32.Parse((digitsWerentMultiplied[i].ToString()));
                result = result + num;
            }
            int endResult = result + multipliedDigitsSummed;
            bool isValidCard = endResult % 10 == 0;
            return isValidCard;
        }
    }
}
