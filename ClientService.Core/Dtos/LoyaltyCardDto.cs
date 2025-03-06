using Microsoft.EntityFrameworkCore;

namespace ClientService.Core.Dtos
{
    [Keyless]
    public class LoyaltyCardDto
    {
        public long id_carte { get; set; }   
        public DateTime? fdate { get; set; }  
        public long? duree_point { get; set; }  
        public string? Libelle { get; set; }  
        public DateTime? PointsDepuis { get; set; }  
        public decimal? Points { get; set; } 
        public DateTime? adhesion { get; set; }  
        public long? duree_adhesion { get; set; }  
        public long? validite_point_hors_adhesion { get; set; }  
    }
}
