using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedDevSample.Domain.Exceptions
{
    public class DomaineException : Exception
    {
        public DomaineException(string message) : base(message) 
        {

        }
    }
}
