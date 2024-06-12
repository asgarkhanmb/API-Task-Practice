using System.ComponentModel.DataAnnotations;

namespace API_Practice_Task.Models
{
    public class Book : BaseEntity
    {
        [StringLength(20)]
        public string Name { get; set; }
        public int Page {  get; set; }

    }
}
