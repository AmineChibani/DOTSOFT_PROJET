using System.ComponentModel.DataAnnotations.Schema;

namespace ClientService.Core.Dtos
{
    public class VenteRequest
    {
        public int IdClient { get; set; }
        public int Abandonnee { get; set; }
        public int? IdStructure { get; set; }   
    }
}
