using Payment.Api.Extensions;
using Payment.Api.Models.Validations;
using Payment.API.Models.EntityModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Payment.API.Models.ViewModel
{
    public class TransactionModel
    {

        [Display(Name = "cardNumber"), JsonRequired, JsonCardNumber]
        public string CardNumber { get; set; }

        [Display(Name = "grossAmount"), JsonRequired, JsonCurrency]
        public decimal GrossAmount { get; set; }

        [Display(Name = "installments"), JsonRequired, JsonInstallments]
        [DefaultValue(1)]
        public int Installments { get; set; }

        public Transaction Map()
        {
            string lastFourDigits = CardNumber.Substring(CardNumber.Length - 4).OnlyNumbers();

            return new Transaction
            {
                GrossAmount = GrossAmount,
                Installments = Installments,
                LastFourDigits = lastFourDigits
            };
        }
    }
}
