using Payment.Api.Models.EntityModel.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payment.Api.Models.EntityModel
{
    public class Anticipation
    {
        public int Id { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? AnalysisStartDate { get; set; }
        public DateTime? AnalysisEndDate { get; set; }
        public AnticipationAnalysisResult AnalysisResult { get; set; }
        public decimal RequestedValue { get; set; }
        public decimal AnticipatedValue { get; set; }

        [InverseProperty("Anticipation")]
        public List<AnticipationTransaction> AnticipationTransactions { get; set; }
    }

}
