using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.ViewModels.NotesViewModels
{
    public class NotesAddViewModel
    {
        [Required(ErrorMessage = "O campo PersonalNotes é obrigatório")]
        public virtual ICollection<NoteAddViewModel> PersonalNotes { get; set; }
    }
}
