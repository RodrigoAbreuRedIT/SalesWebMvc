using System;

namespace SalesWebMvc.Services.Exceptions {
    public class NotFoundException : ApplicationException {
        public NotFoundException(String message) : base(message) { }
    }
}