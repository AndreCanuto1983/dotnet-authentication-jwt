using Core.Exceptions;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Context;
using WebAPI.Models.NoteModel;

namespace WebAPI.Repository
{
    public class NotesRepository : IRepository<Notes>
    {

        #region Dependency Injection

        private readonly ApplicationDbContext _context;

        public NotesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        public async Task InsertUpdate(ICollection<Notes> model, string userId)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    foreach (var item in model)
                    {
                        item.UserId = userId;
                    }

                    foreach (var item in model)
                    {
                        _context.Entry(item).State = item.Id == 0 ? EntityState.Added : EntityState.Modified;

                        if (item.Id > 0)
                        {
                            _context.Entry(item).Property(p => p.CreationDate).IsModified = false;
                            item.UpdateDate = DateTime.UtcNow;
                        }

                        await _context.SaveChangesAsync();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new CustomErrorException("Erro ao salvar registro(s)", ex);
                }
            }
        }

        public async Task<ICollection<Notes>> GetList(long id, string userId)
        {
            try
            {
                if (id == 0)
                    return await _context.Notes.AsNoTracking().Where(n => n.UserId == userId && !n.IsDeleted).ToListAsync();
                else
                    return await _context.Notes.AsNoTracking().Where(n => n.Id == id && n.UserId == userId && !n.IsDeleted).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new CustomErrorException("Erro ao buscar registro(s)", ex);
            }
        }

        public async Task Delete(long id, string userId)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var note = await _context.Notes.AsNoTracking().Where(n => n.Id == id && n.UserId == userId && !n.IsDeleted).FirstOrDefaultAsync();

                    _context.Entry(note).State = EntityState.Modified;
                    _context.Entry(note).Property(p => p.CreationDate).IsModified = false;
                    note.IsDeleted = true;
                    note.UpdateDate = DateTime.UtcNow;
                    await _context.SaveChangesAsync();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new CustomErrorException("Erro ao excluir registro(s)", ex);
                }
            }
        }
    }
}
