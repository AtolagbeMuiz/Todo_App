using System;
using ToDoAPI.DTOs;
using ToDoAPI.Interfaces;

namespace ToDoAPI.Services
{
    public class ToDoService : IToDoService
    {
        private readonly IToDoRepo _toDoRepo;
        APIResponse res = new APIResponse();

        public ToDoService(IToDoRepo toDoRepo)
        {
            this._toDoRepo = toDoRepo;
        }

        public APIResponse CreateToDo(ToDoDTO toDoDTO)
        {
            try
            {
                toDoDTO.Id = Guid.NewGuid();

                var isCreated = _toDoRepo.CreateToDo(toDoDTO);
                if (isCreated)
                {
                    res.Code = "00";
                    res.Description = "success";
                    res.Data = null;

                    return res;
                }

                res.Code = "01";
                res.Description = "failed";
                res.Data = null;

                return res;

            }
            catch (System.Exception ex)
            {
                res.Code = "02";
                res.Description = ex.Message;

                return res;
            }
            
        }

        public APIResponse GetAllToDos()
        {
            try
            {
                var todos = _toDoRepo.getAllToDos();
                if (todos.Count > 0)
                {
                    res.Code = "00";
                    res.Description = "success";
                    res.Data = todos;

                    return res;
                }
                return res;
            }
            catch (System.Exception)
            {

                throw;
            }
            
        }

        public APIResponse DeleteToDo(string Id)
        {
            try
            {
                var isDeleted = _toDoRepo.DeleteToDoItem(Id);
                if (isDeleted)
                {
                    res.Code = "00";
                    res.Description = "success";
                    res.Data = null;

                    return res;
                }

                res.Code = "01";
                res.Description = "failed";
                return res;
            }
            catch (System.Exception)
            {

                throw;
            }

        }

        public APIResponse GetToDoById(string Id)
        {
            try
            {
                var todo = _toDoRepo.GetToDoItemById(Id);
                if (todo != null)
                {
                    res.Code = "00";
                    res.Description = "success";
                    res.Data = todo;

                    return res;
                }

                res.Code = "01";
                res.Description = "failed";
                return res;
            }
            catch (System.Exception)
            {

                throw;
            }

        }

        public APIResponse UpdateToDo(ToDoDTO todoDTO)
        {
            try
            {
                var isUpdated = _toDoRepo.UpdateToDoItemById(todoDTO);
                if (isUpdated)
                {
                    res.Code = "00";
                    res.Description = "success";
                    res.Data = null;

                    return res;
                }
                res.Code = "01";
                res.Description = "failed";
                return res;
            }
            catch (System.Exception)
            {

                throw;
            }

        }

    }
}
