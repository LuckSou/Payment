using System.ComponentModel.DataAnnotations;

namespace Payment.Api.Models.ViewModel
{
    public class AnticipationModel
    {

        [Display(Name = "nsu")]
        public List<string> Nsu { get; set; }
    }
}
