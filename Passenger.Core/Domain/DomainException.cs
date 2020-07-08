using System;
using System.Collections.Generic;
using System.Text;

namespace Passenger.Core.Domain
{
    public class DomainException : PassengerException
    {
        public string Code { get; }

        protected DomainException()
        {

        }

        public DomainException(string code) 
            : base(code)
        {
            Code = code;
        }

        public DomainException(string message, params object[] args) 
            : base(string.Empty, message, args)
        {
        }
        public DomainException(string code, string message, params object[] args) 
            : base(null, code, message, args)
        {
        }
        public DomainException(Exception innerException, string message, params object[] args) 
            : base(innerException, string.Empty, message, args)
        {
        }

        public DomainException(Exception innerException, string code, string message, params object[] args) 
            : base(code, string.Format(message, args), innerException)
        {
        }
    }
}
