using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Net;
using ToDoAPI.Controllers;
using ToDoAPI.DTOs;
using ToDoAPI.Interfaces;
using ToDoAPI.Model;

namespace ToDoApp.UnitTests
{
    [TestClass]
    public class ToDoControllerTest
    {
        [TestMethod]
        public void CreateToDo_NewToDoItem_ReturnsHTTPStatusCode()
        {
            //Arange
            var todoMock = new Mock<IToDoService>();
            var todoController = new ToDoController(todoMock.Object);

            var response = new APIResponse();

            var todo = new ToDoDTO
            {
                Name = "Test Task",
                Description = "Test Description",
                Status = ToDoAPI.Enums.Status.NotStarted,
                Priority = ToDoAPI.Enums.Priority.High
            };

            //Act
            var res = todoMock.Setup(x => x.CreateToDo(todo)).Returns(response);

            var result = (ObjectResult) todoController.CreateToDo(todo).Result;

            //Assert
            Assert.IsNotNull(result);
            //Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public void GetToDos_FetchAllToDos_ValidateResult()
        {
            //Arrange
            var todoMock = new Mock<IToDoService>();
            var todoController = new ToDoController(todoMock.Object);

            var response = new APIResponse();

            //Act
            var res = todoMock.Setup(x => x.GetAllToDos()).Returns(response);

            var result = (ObjectResult)todoController.GetToDos().Result;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DeleteToDo_DeleteToDoById_ValidateResult()
        {
            //Arrange
            var todoMock = new Mock<IToDoService>();
            var todoController = new ToDoController(todoMock.Object);

            var response = new APIResponse();

            var Id = Convert.ToString(Guid.NewGuid());

            //Act
            var res = todoMock.Setup(x => x.DeleteToDo(Id)).Returns(response);

            var result = (ObjectResult)todoController.DeleteToDo(Id).Result;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetToDoById_GetToDoById_ValidateResult()
        {
            //Arrange
            var todoMock = new Mock<IToDoService>();
            var todoController = new ToDoController(todoMock.Object);

            var response = new APIResponse();

            var Id = Convert.ToString(Guid.NewGuid());

            //Act
            var res = todoMock.Setup(x => x.GetToDoById(Id)).Returns(response);

            var result = (ObjectResult)todoController.GetToDoById(Id).Result;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UpdateToDo_EditToDOItem_ValidatesResult()
        {
            //Arange
            var todoMock = new Mock<IToDoService>();
            var todoController = new ToDoController(todoMock.Object);

            var response = new APIResponse();

            var todo = new ToDoDTO
            {
                Id= Guid.NewGuid(),
                Name = "Test Task",
                Description = "Test Description",
                Status = ToDoAPI.Enums.Status.NotStarted,
                Priority = ToDoAPI.Enums.Priority.High
            };

            //Act
            var res = todoMock.Setup(x => x.UpdateToDo(todo)).Returns(response);

            var result = (ObjectResult)todoController.UpdateToDoItem(todo).Result;

            //Assert
            Assert.IsNotNull(result);
        }

    }
}
