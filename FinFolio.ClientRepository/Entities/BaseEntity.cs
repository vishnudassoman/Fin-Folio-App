using System.ComponentModel.DataAnnotations;

namespace FinFolio.PortFolioRepository.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
