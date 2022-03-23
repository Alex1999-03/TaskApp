using ApplicationCore.DTO;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskListController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaskListController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var taskList = await _unitOfWork.TasksLists.GetAllAsync();

            if (taskList.Count() == 0)
            {
                return NotFound(new ResponseDTO
                {
                    StatusCode = 404,
                    Message = "There are not lists."
                }); ;
            }

            return Ok(new ResponseDTO
            {
                StatusCode = 200,
                Message = "Successful operation.",
                Results = taskList
            });
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(TaskListDTO taskListDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseDTO
                {
                    StatusCode = 400,
                    Message = "Parameters are incorrect."
                });
            }
            var taskList = new TaskList
            {
                Title = taskListDTO.Title,
                CreatedDate = DateTime.Now
            };
            _unitOfWork.TasksLists.Add(taskList);
            await _unitOfWork.SaveChangesAsync();
            return Ok(new ResponseDTO
            {
                StatusCode = 200,
                Message = "Successful operation.",
                Results = taskList
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditTitle(int id, TaskListDTO taskListDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseDTO
                {
                    StatusCode = 400,
                    Message = "Parameters are incorrect."
                });
            }

            var taskList = await _unitOfWork.TasksLists.GetByIdAsync(id);
            if (taskList == null)
            {
                return NotFound(new ResponseDTO
                {
                    StatusCode = 404,
                    Message = "This list not exist."
                });
            }

            taskList.Title = taskListDTO.Title;
            _unitOfWork.TasksLists.Update(taskList);
            await _unitOfWork.SaveChangesAsync();
            return Ok(new ResponseDTO
            {
                StatusCode = 200,
                Message = "Successful operation.",
                Results = taskList
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var taskList = await _unitOfWork.TasksLists.GetByIdAsync(id);
            if (taskList == null)
            {
                return NotFound(new ResponseDTO
                {
                    StatusCode = 404,
                    Message = "This list not exist."
                });
            }
            _unitOfWork.TasksLists.Delete(taskList);
            await _unitOfWork.SaveChangesAsync();
            return Ok(new ResponseDTO
            {
                StatusCode = 200,
                Message = "Successful operation.",
                Results = taskList
            });
        }
    }
}
