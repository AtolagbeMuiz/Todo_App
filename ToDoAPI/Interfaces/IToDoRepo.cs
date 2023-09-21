using System.Collections.Generic;
using ToDoAPI.DTOs;

namespace ToDoAPI.Interfaces
{
    public interface IToDoRepo
    {
        bool CreateToDo(ToDoDTO toDoDTO);
        List<ToDoDTO> getAllToDos();
        bool DeleteToDoItem(string Id);
        ToDoDTO GetToDoItemById(string Id);
        bool UpdateToDoItemById(ToDoDTO toDoDTO);
    }
}
