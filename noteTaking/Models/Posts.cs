using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace noteTaking.Models
{
    public class Posts
    {
        public int Id { get; set; }
        [Display(Name = "Title")]
        [Required]
        public string Title { get; set; }
        [Display(Name = "Content")]
        [Required]
        public string Content { get; set; }

        public List<Comments>? Comments { get; set; }

        [ForeignKey("Category")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }



    }
}
