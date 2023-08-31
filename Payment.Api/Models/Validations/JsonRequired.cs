using Payment.Api.Models.EntityModel.Enum;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Payment.Api.Models.Validations
{
    public class JsonRequired : RequiredAttribute
    {
        public JsonRequired()
        {
            ErrorMessage = "{0}: Required.";
        }
    }

    public class JsonCurrency : RangeAttribute
    {
        private const double JsonMinimum = 0;
        private const double JsonMaximum = 999999.99;

        public JsonCurrency() : base(JsonMinimum, JsonMaximum)
        {
            ErrorMessage = "{0}: Between {1} and {2}.";
        }
    }

    public class JsonCardNumber : ValidationAttribute
    {
        protected override ValidationResult IsValid([NotNull] object value, ValidationContext validationContext)
        {
            if (value is string cardNumber && !IsValidCardNumber(cardNumber))
            {
                return new ValidationResult("Card number is invalid.");
            }

            return ValidationResult.Success;
        }

        private bool IsValidCardNumber(string cardNumber)
        {
            return cardNumber.Length == 16 && !(cardNumber.StartsWith("5999"));
        }
    }

    public class JsonAnticipationResult : ValidationAttribute
    {
        protected override ValidationResult IsValid([NotNull] object value, ValidationContext validationContext)
        {
            if (value is AnticipationAnalysisResult analysisResult)
            {
                if (analysisResult != AnticipationAnalysisResult.Approved &&
                    analysisResult != AnticipationAnalysisResult.PartiallyApproved &&
                    analysisResult != AnticipationAnalysisResult.Rejected)
                {
                    return new ValidationResult("Invalid anticipation analysis result.");
                }
            }

            return ValidationResult.Success;
        }
    }

    public class JsonInstallments : RangeAttribute
    {
        public JsonInstallments() : base(1, 999)
        {
            ErrorMessage = "{0}: Between {1} and {2}.";
        }
    }
}
