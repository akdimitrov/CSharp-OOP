using System;
using System.Linq;
using T03.Telephony.Cotracts;
using T03.Telephony.Exceptions;

namespace T03.Telephony.Models
{
    public class Smartphone : ICallable, IBrowsable
    {
        public string Call(string phoneNumber)
        {
            if (!phoneNumber.All(x => char.IsDigit(x)))
            {
                throw new ArgumentException(ExceptionMessages.InvalidNumberException);
            }

            return $"Calling... {phoneNumber}";
        }

        public string Browse(string url)
        {
            if (url.Any(x => char.IsDigit(x)))
            {
                throw new ArgumentException(ExceptionMessages.InvalidUrlException);
            }

            return $"Browsing: {url}!";
        }
    }
}
