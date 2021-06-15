using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.ViewModels.NotesViewModels
{
    public class NoteUpdateViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "O campo PersonalNotes é obrigatório")]
        public string PersonalNotes { get; set; }

        [DefaultValue(false)]
        public bool isDeleted { get; set; }
    }
}
