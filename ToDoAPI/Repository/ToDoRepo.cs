using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using ToDoAPI.DTOs;
using ToDoAPI.Interfaces;
using ToDoAPI.Model;

namespace ToDoAPI.Repository
{
    public class ToDoRepo : IToDoRepo
    {
        //in-Memmory Database
        private readonly List<ToDo> _listOfToDos;

        public ToDoRepo()
        {
            List<ToDo> listOfToDos = new List<ToDo>{
                new ToDo() { Id = Guid.NewGuid() , Name = "House Chores", Description="Doing House Chores", Priority = Enums.Priority.High, Status = Enums.Status.NotStarted, isDeleted = false},
                new ToDo() { Id = Guid.NewGuid() , Name = "Gym", Description="Gyming activities", Priority = Enums.Priority.Low, Status = Enums.Status.InProgress, isDeleted = false},
                new ToDo() { Id = Guid.NewGuid() , Name = "Assessment", Description="School assessment", Priority = Enums.Priority.Medium, Status = Enums.Status.Completed, isDeleted = false}

             };

            this._listOfToDos = listOfToDos;
        }

        public bool CreateToDo(ToDoDTO toDoDTO)
        {
            try
            {
                //map the DTO to the ToDo Entity
                var todo = new ToDo
                {
                    Id = toDoDTO.Id,
                    Name = toDoDTO.Name,
                    Description = toDoDTO.Description,
                    Priority = toDoDTO.Priority,
                    Status = toDoDTO.Status
                };

                _listOfToDos.Add(todo);
                
                return true;
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message;

                throw;
            }
            
        }

        public List<ToDoDTO> getAllToDos()
        {
            try
            {
                var getListofToDos = _listOfToDos.Where(x => x.isDeleted == false).Select(x =>
                 new ToDoDTO
                 {
                     Id = x.Id,
                     Description = x.Description,
                     Name = x.Name,
                     Priority = x.Priority,
                     Status = x.Status
                 }).ToList();

                return getListofToDos;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeleteToDoItem(string Id)
        {
            var todoId = Guid.Parse(Id);
           
            _listOfToDos.Where(x => x.Id == todoId).ToList().ForEach(s => s.isDeleted = true);
            return true;
           
        }

        public ToDoDTO GetToDoItemById(string Id)
        {
            var todoId = Guid.Parse(Id);

            var todo = _listOfToDos.Where(x => x.Id == todoId)
                .Select(x => new ToDoDTO
                {
                    Id = x.Id,
                    Description = x.Description,
                    Name = x.Name,
                    Priority = x.Priority,
                    Status = x.Status,
                    isDeleted = x.isDeleted
                }).FirstOrDefault();
                
            
            return todo;
        }

        public bool UpdateToDoItemById(ToDoDTO toDoDTO)
        {

            var list =_listOfToDos.Where(x => x.Id == toDoDTO.Id).ToList(); //.ForEach(s => s.Name = toDoDTO.Name);
            foreach (var item in list)
            {
                item.Name = toDoDTO.Name;
                item.Priority = toDoDTO.Priority;       
                item.Status = toDoDTO.Status;
                item.Description = toDoDTO.Description;
            }
            return true;

        }

    }
}
