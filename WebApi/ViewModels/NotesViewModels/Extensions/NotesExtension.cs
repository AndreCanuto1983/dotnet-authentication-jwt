using Core.Helpers;
using WebAPI.Models.NoteModel;

namespace WebAPI.ViewModels.NotesViewModels.Extensions
{
    public static class NotesExtension
    {
        public static Notes NotesFront2Entity(this NoteAddViewModel entity)
        {
            return new Notes()
            {
                Id = entity.Id,
                PersonalNotes = Util.ByteEncode(entity.PersonalNotes)
            };
        }

        public static Notes NotesFront2Entity(this NoteUpdateViewModel entity)
        {
            return new Notes()
            {
                Id = entity.Id,
                PersonalNotes = Util.ByteEncode(entity.PersonalNotes),
                IsDeleted = entity.isDeleted
            };
        }

        public static NoteAddViewModel Entity2Front(this Notes entity)
        {
            return new NoteAddViewModel()
            {
                Id = entity.Id,
                PersonalNotes = Util.ByteDecode(entity.PersonalNotes)
            };
        }
    }
}
