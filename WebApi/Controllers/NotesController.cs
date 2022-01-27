using Core.Exceptions;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.AccessModels;
using WebAPI.Models.NoteModel;
using WebAPI.ViewModels.NotesViewModels;
using WebAPI.ViewModels.NotesViewModels.Extensions;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        #region Dependency Injection

        private readonly UserManager<User> _userManager;
        private readonly IRepository<Notes> _notesRepository;
        private readonly IHttpContextAccessor _accessor;

        public NotesController(IRepository<Notes> notesRepository, UserManager<User> userManager, IHttpContextAccessor accessor)
        {
            _notesRepository = notesRepository;
            _userManager = userManager;
            _accessor = accessor;
        }

        #endregion

        /// <summary>
        /// Notes Insert
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("")]
        public async Task<ActionResult> Insert(NotesAddViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var verifyId = model.PersonalNotes.Select(p => p.Id > 0).ToList();

            if (verifyId.Find(x => x.Equals(true)))
                return BadRequest("Para inserir novos registros, insira com id 0");

            try
            {
                //pego email que vem do token para cada usuário gerenciar suas próprias anotações
                var user = await _userManager.FindByEmailAsync(_accessor.HttpContext.User.Identity.Name);

                if (user == null || user.IsDeleted)
                    return Unauthorized();

                await _notesRepository.InsertUpdate(model.PersonalNotes?.Select(p => p.NotesFront2Entity()).ToList(), user.Id);

                return Created("", "");
            }
            catch (CustomErrorException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Notes Update
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("")]
        public async Task<ActionResult> Update(NotesUpdateViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var verifyId = model.PersonalNotes.Select(p => p.Id <= 0).ToList();

            if (verifyId.Find(x => x.Equals(true)))
                return BadRequest("Informe um id válido");

            try
            {
                var user = await _userManager.FindByEmailAsync(_accessor.HttpContext.User.Identity.Name);

                if (user == null || user.IsDeleted)
                    return Unauthorized();                    

                await _notesRepository.InsertUpdate(model.PersonalNotes?.Select(p => p.NotesFront2Entity()).ToList(), user.Id);

                return Ok();
            }
            catch (CustomErrorException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Notes Get Notes for user
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(long id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values);

            try
            {
                var user = await _userManager.FindByEmailAsync(_accessor.HttpContext.User.Identity.Name);

                if (user == null || user.IsDeleted)
                    return Unauthorized();

                var result = await _notesRepository.GetList(id, user.Id);

                return Ok(result?.Select(n => n.Entity2Front()).ToList());
            }
            catch (CustomErrorException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Notes Get Notes for user
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        [HttpGet("")]
        public async Task<ActionResult> GetList()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values);

            try
            {
                var user = await _userManager.FindByEmailAsync(_accessor.HttpContext.User.Identity.Name);

                if (user == null || user.IsDeleted)
                    return Unauthorized();

                var result = await _notesRepository.GetList(0, user.Id);

                return Ok(result?.Select(n => n.Entity2Front()).ToList());
            }
            catch (CustomErrorException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Notes Delete
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values);

            try
            {
                var user = await _userManager.FindByEmailAsync(_accessor.HttpContext.User.Identity.Name);

                if (user == null || user.IsDeleted)
                    return Unauthorized();

                await _notesRepository.Delete(id, user.Id);

                return Ok();
            }
            catch (CustomErrorException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
