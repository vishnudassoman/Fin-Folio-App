using System.ComponentModel.DataAnnotations;

namespace FinFolio.ClientRepository.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
