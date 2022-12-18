using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace noteTaking.Models
{
    public class Comments
    {
        public int Id { get; set; }
        [Display(Name = "Comment")]
        [Required]
        public string Comment { get; set; }

        [ForeignKey("Posts")]
        [Display(Name = "Posts")]
        public int PostId { get; set; }
        public Posts? Post { get; set; }
    }
}
