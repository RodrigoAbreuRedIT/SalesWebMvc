using System;

namespace SalesWebMvc.Services.Exceptions {
    public class DbConcurrencyException : ApplicationException {
        public DbConcurrencyException(String message) : base(message) { }
    
    }
}
