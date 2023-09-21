using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDoAPI.DTOs;
using ToDoAPI.Interfaces;

namespace ToDoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoService _todoService;
        public ToDoController(IToDoService todoService)
        {
            _todoService = todoService;
        }

        public async Task<IActionResult> CreateToDo(ToDoDTO toDoDTO)
        {
            try
            {
                var response = _todoService.CreateToDo(toDoDTO);
                if(response.Code == "00")
                {
                    return Ok(response);
                }
                else if (response.Code == "01")
                {
                    return BadRequest(response);
                }
                else
                {
                    return StatusCode(500, response);
                }
              
            }
            catch (System.Exception)
            {

                throw;
            }

        }

        public async Task<IActionResult> GetToDos()
        {
            try
            {
                var response = _todoService.GetAllToDos();
                if (response.Code == "00")
                {
                    return Ok(response);
                }
                else if (response.Code == "01")
                {
                    return BadRequest(response);
                }
                else
                {
                    return StatusCode(500, response);
                }

            }
            catch (System.Exception ex)
            {

                return StatusCode(500, ex.Message);
                
            }

        }

        public async Task<IActionResult> DeleteToDo([FromQuery]string Id)
        {
            try
            {
                var response = _todoService.DeleteToDo(Id);
                if (response.Code == "00")
                {
                    return Ok(response);
                }
                else if (response.Code == "01")
                {
                    return BadRequest(response);
                }
                else
                {
                    return StatusCode(500, response);
                }

            }
            catch (System.Exception ex)
            {

                return StatusCode(500, ex.Message);

            }

        }


        public async Task<IActionResult> GetToDoById([FromQuery] string Id)
        {
            try
            {
                var response = _todoService.GetToDoById(Id);
                if (response.Code == "00")
                {
                    return Ok(response);
                }
                else if (response.Code == "01")
                {
                    return BadRequest(response);
                }
                else
                {
                    return StatusCode(500, response);
                }

            }
            catch (System.Exception ex)
            {

                return StatusCode(500, ex.Message);

            }

        }

        public async Task<IActionResult> UpdateToDoItem(ToDoDTO toDoDTO)
        {
            try
            {
                var response = _todoService.UpdateToDo(toDoDTO);
                if (response.Code == "00")
                {
                    return Ok(response);
                }
                else if (response.Code == "01")
                {
                    return BadRequest(response);
                }
                else
                {
                    return StatusCode(500, response);
                }

            }
            catch (System.Exception ex)
            {

                return StatusCode(500, ex.Message);

            }

        }

    }
}
