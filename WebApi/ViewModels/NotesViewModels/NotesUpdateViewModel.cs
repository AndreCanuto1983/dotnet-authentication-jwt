using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.ViewModels.NotesViewModels
{
    public class NotesUpdateViewModel
    {
        [Required(ErrorMessage = "O campo PersonalNotes é obrigatório")]
        public virtual ICollection<NoteUpdateViewModel> PersonalNotes { get; set; }
    }
}
