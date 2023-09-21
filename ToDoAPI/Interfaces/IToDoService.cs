using ToDoAPI.DTOs;

namespace ToDoAPI.Interfaces
{
    public interface IToDoService
    {
        APIResponse CreateToDo(ToDoDTO toDoDTO);
        APIResponse GetAllToDos();
        APIResponse DeleteToDo(string Id);
        APIResponse GetToDoById(string Id);

        APIResponse UpdateToDo(ToDoDTO todoDTO);
    }
}
