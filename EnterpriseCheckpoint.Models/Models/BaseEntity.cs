using System.ComponentModel.DataAnnotations;

namespace EnterpriseCheckpoint.Models.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
