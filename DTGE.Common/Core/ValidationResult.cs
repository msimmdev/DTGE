using System;
using System.Collections.Generic;
using System.Text;

namespace DTGE.Common.Core
{
    public class ValidationResult
    {
        private ValidationResult()
        {

        }

        public ValidationResult(bool success, string error)
        {
            Success = success;
            Error = error;
        }

        public bool Success { get; private set; }
        public string Error { get; private set; }

        public static ValidationResult NewSuccess() {
            return new ValidationResult()
            {
                Success = true
            };
        }

        public static ValidationResult NewError(string error)
        {
            return new ValidationResult()
            {
                Success = false,
                Error = error
            };
        }
    }
}
