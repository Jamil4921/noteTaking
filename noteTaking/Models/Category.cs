using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace noteTaking.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Display(Name = "Subject")]
        [Required]
        public string SubjectName { get; set; }
        public List<Posts>? Posts { get; set; }
    }
}
