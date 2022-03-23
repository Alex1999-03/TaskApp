using ApplicationCore.DTO;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ActivityController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ActivityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByListId([FromQuery] int listId)
        {
            var todo = await _unitOfWork.Activities.WhereAsync(x => x.TaskListId == listId);

            if (todo.Count() == 0)
            {
                return NotFound(new ResponseDTO
                {
                    StatusCode = 404,
                    Message = "There are not activities."
                });
            }

            return Ok(new ResponseDTO
            {
                StatusCode = 200,
                Message = "Successful operation.",
                Results = todo
            });
        }

        [HttpPost]
        public async Task<IActionResult> Add(ActivityDTO  todoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseDTO
                {
                    StatusCode = 400,
                    Message = "Parameters are incorrect."
                });
            }
            var activity = new Activity
            {
                CreatedDate = todoDTO.CreatedDate,
                DueDate = todoDTO.DueDate,
                Description = todoDTO.Description,
                TaskListId = todoDTO.TaskListId
            };
            _unitOfWork.Activities.Add(activity);
            await _unitOfWork.SaveChangesAsync();
            return Ok(new ResponseDTO
            {
                StatusCode = 200,
                Message = "Successful operation.",
                Results = activity
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, ActivityDTO todoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseDTO
                {
                    StatusCode = 400,
                    Message = "Parameters are incorrect."
                });
            }

            var todo = await _unitOfWork.Activities.GetByIdAsync(id);
            if (todo == null)
            {
                return NotFound(new ResponseDTO
                {
                    StatusCode = 404,
                    Message = "This task not exist."
                });
            }
            todo.DueDate = todoDTO.DueDate;
            todo.Description = todoDTO.Description;
            _unitOfWork.Activities.Update(todo);
            await _unitOfWork.SaveChangesAsync();
            return Ok(new ResponseDTO
            {
                StatusCode = 200,
                Message = "Successful operation.",
                Results = todo
            });
        }

        [HttpPut("{id}/Check")]
        public async Task<IActionResult> Check(int id)
        {
            var todo = await _unitOfWork.Activities.GetByIdAsync(id);
            if (todo == null)
            {
                return NotFound(new ResponseDTO
                {
                    StatusCode = 404,
                    Message = "This task not exist."
                });
            }
            todo.ChangeIsDone();
            _unitOfWork.Activities.Update(todo);
            await _unitOfWork.SaveChangesAsync();
            return Ok(new ResponseDTO
            {
                StatusCode = 200,
                Message = "Successful operation.",
                Results = todo
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var todo = await _unitOfWork.Activities.GetByIdAsync(id);
            if (todo == null)
            {
                return NotFound(new ResponseDTO
                {
                    StatusCode = 404,
                    Message = "This task not exist."
                });
            }
            _unitOfWork.Activities.Delete(todo);
            await _unitOfWork.SaveChangesAsync();
            return Ok(new ResponseDTO
            {
                StatusCode = 200,
                Message = "Successful operation.",
                Results = todo
            });
        }
    }
}
