using System.ComponentModel.DataAnnotations;

namespace WebAPI.ViewModels.NotesViewModels
{
    public class NoteAddViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "O campo PersonalNotes é obrigatório")]
        public string PersonalNotes { get; set; }
    }
}
