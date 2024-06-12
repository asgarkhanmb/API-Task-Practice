using System.ComponentModel.DataAnnotations;

namespace API_Practice_Task.Models
{
    public class Category : BaseEntity
    {
        [StringLength(10)]
        public string Name { get; set; }
    }

}
