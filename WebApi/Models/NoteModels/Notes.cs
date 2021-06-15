using Core.Models.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Models.AccessModels;

namespace WebAPI.Models.NoteModel
{
    [Table("Notes")]
    public class Notes : ModelBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        //[ForeignKey("UserId")]  // => outra maneira de setar a fk, através do DataAnnotations
        public string UserId { get; set; }        
        public User User { get; set; }        

        [Required]
        public byte[] PersonalNotes { get; set; }
    }
}
