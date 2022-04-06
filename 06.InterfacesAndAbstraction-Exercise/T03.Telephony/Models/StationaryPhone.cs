using System;
using System.Linq;
using T03.Telephony.Cotracts;
using T03.Telephony.Exceptions;

namespace T03.Telephony.Models
{
    public class StationaryPhone : ICallable
    {
        public string Call(string phoneNumber)
        {
            if (!phoneNumber.All(x => char.IsDigit(x)))
            {
                throw new ArgumentException(ExceptionMessages.InvalidNumberException);
            }

            return $"Dialing... {phoneNumber}";
        }
    }
}
