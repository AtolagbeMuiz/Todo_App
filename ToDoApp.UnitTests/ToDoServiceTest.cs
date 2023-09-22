using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoAPI.Controllers;
using ToDoAPI.DTOs;
using ToDoAPI.Interfaces;
using ToDoAPI.Repository;
using ToDoAPI.Services;

namespace ToDoApp.UnitTests
{
    [TestClass]
    public class ToDoServiceTest
    {
        [TestMethod]
        public void CreateToDo_NewToDoItem_ReturnsTrue()
        {
            //Arange
            var todoMock = new Mock<IToDoRepo>();
            var todoRepo = new ToDoRepo();

            var todo = new ToDoDTO
            {
                Name = "Test Task",
                Description = "Test Description",
                Status = ToDoAPI.Enums.Status.NotStarted,
                Priority = ToDoAPI.Enums.Priority.High
            };

            //Act
            var res = todoMock.Setup(x => x.CreateToDo(todo)).Returns(true);

            var result = todoRepo.CreateToDo(todo);

            //Assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void CreateToDo_Successful_ValidateSuccessfulResponse()
        {
            // Arrange
            var toDoRepoMock = new Mock<IToDoRepo>();
            toDoRepoMock.Setup(repo => repo.CreateToDo(It.IsAny<ToDoDTO>())).Returns(true);

            var toDoService = new ToDoService(toDoRepoMock.Object);
            var todo = new ToDoDTO
            {
                Name = "Test Task",
                Description = "Test Description",
                Status = ToDoAPI.Enums.Status.NotStarted,
                Priority = ToDoAPI.Enums.Priority.High
            };

            // Act
            var result = toDoService.CreateToDo(todo);

            // Assert
            Assert.AreEqual("00", result.Code);
            Assert.AreEqual("success", result.Description);
            Assert.IsNull(result.Data);
        }

        [TestMethod]
        public void CreateToDo_Failed_ValidateFailedResponse()
        {
            // Arrange
            var toDoRepoMock = new Mock<IToDoRepo>();
            toDoRepoMock.Setup(repo => repo.CreateToDo(It.IsAny<ToDoDTO>())).Returns(false);

            var toDoService = new ToDoService(toDoRepoMock.Object);
            var todo = new ToDoDTO
            {
                Name = "Test Task",
                Description = "Test Description",
                Status = ToDoAPI.Enums.Status.NotStarted,
                Priority = ToDoAPI.Enums.Priority.High
            };

            // Act
            var result = toDoService.CreateToDo(todo);

            // Assert
            Assert.AreEqual("01", result.Code);
            Assert.AreEqual("failed", result.Description);
            Assert.IsNull(result.Data);
        }

        [TestMethod]
        public void GetAllToDos_Successful_ValidateSuccessfulResponse()
        {
            // Arrange
            var todos = new List<ToDoDTO> {
                new ToDoDTO
                {
                    Id = Guid.NewGuid(),
                    Name = "Task 1",
                    Description = "Description for Task 1",
                    Status = ToDoAPI.Enums.Status.NotStarted,
                    Priority = ToDoAPI.Enums.Priority.High
                },
                new ToDoDTO
                {
                    Id = Guid.NewGuid(),
                    Name = "Task 2",
                    Description = "Description for Task 2",
                    Status = ToDoAPI.Enums.Status.NotStarted,
                    Priority = ToDoAPI.Enums.Priority.High
                }
            };

            var toDoRepoMock = new Mock<IToDoRepo>();
            toDoRepoMock.Setup(repo => repo.getAllToDos()).Returns(todos);

            var toDoService = new ToDoService(toDoRepoMock.Object);

            // Act
            var result = toDoService.GetAllToDos();

            // Assert
            Assert.AreEqual("00", result.Code);
            Assert.AreEqual("success", result.Description);
            Assert.AreSame(todos, result.Data);
        }

        [TestMethod]
        public void DeleteToDo_Successful_ValidateSuccessfulResponse()
        {
            // Arrange
            var toDoRepoMock = new Mock<IToDoRepo>();
            toDoRepoMock.Setup(repo => repo.DeleteToDoItem(It.IsAny<string>())).Returns(true);

            var toDoService = new ToDoService(toDoRepoMock.Object);
            var todoId = Guid.NewGuid().ToString();

            // Act
            var result = toDoService.DeleteToDo(todoId);

            // Assert
            Assert.AreEqual("00", result.Code);
            Assert.AreEqual("success", result.Description);
            Assert.IsNull(result.Data);
        }

        [TestMethod]
        public void DeleteToDo_Failed_ValidateFailedResponse()
        {
            // Arrange
            var toDoRepoMock = new Mock<IToDoRepo>();
            toDoRepoMock.Setup(repo => repo.DeleteToDoItem(It.IsAny<string>())).Returns(false);

            var toDoService = new ToDoService(toDoRepoMock.Object);
            var todoId = Guid.NewGuid().ToString();

            // Act
            var result = toDoService.DeleteToDo(todoId);

            // Assert
            Assert.AreEqual("01", result.Code);
            Assert.AreEqual("failed", result.Description);
            Assert.IsNull(result.Data);
        }

        [TestMethod]
        public void GetToDoById_ExistingTodo_ValidatesSuccessfulResponse()
        {
            // Arrange
            var expectedTodo = new ToDoDTO { Id = Guid.NewGuid(), Name = "Todo Test" };
            var toDoRepoMock = new Mock<IToDoRepo>();
            toDoRepoMock.Setup(repo => repo.GetToDoItemById(It.IsAny<string>())).Returns(expectedTodo);

            var toDoService = new ToDoService(toDoRepoMock.Object);
            var todoId = expectedTodo.Id.ToString();

            // Act
            var result = toDoService.GetToDoById(todoId);

            // Assert
            Assert.AreEqual("00", result.Code);
            Assert.AreEqual("success", result.Description);
            Assert.AreSame(expectedTodo, result.Data);
        }

        [TestMethod]
        public void GetToDoById_NonExistingTodo_ValidatesFailedResponse()
        {
            // Arrange
            var toDoRepoMock = new Mock<IToDoRepo>();
            toDoRepoMock.Setup(repo => repo.GetToDoItemById(It.IsAny<string>())).Returns((ToDoDTO)null);

            var toDoService = new ToDoService(toDoRepoMock.Object);
            var todoId = Guid.NewGuid().ToString();

            // Act
            var result = toDoService.GetToDoById(todoId);

            // Assert
            Assert.AreEqual("01", result.Code);
            Assert.AreEqual("failed", result.Description);
            Assert.IsNull(result.Data);
        }

        [TestMethod]
        public void UpdateToDo_Successful_ValidatesSuccessfulResponse()
        {
            // Arrange
            var toDoRepoMock = new Mock<IToDoRepo>();
            toDoRepoMock.Setup(repo => repo.UpdateToDoItemById(It.IsAny<ToDoDTO>())).Returns(true);

            var toDoService = new ToDoService(toDoRepoMock.Object);
            var todo = new ToDoDTO
            {
                Id = Guid.NewGuid(),
                Name = "Test Task",
                Description = "Test Description",
                Status = ToDoAPI.Enums.Status.NotStarted,
                Priority = ToDoAPI.Enums.Priority.High
            };

            // Act
            var result = toDoService.UpdateToDo(todo);

            // Assert
            Assert.AreEqual("00", result.Code);
            Assert.AreEqual("success", result.Description);
            Assert.IsNull(result.Data);
        }

        [TestMethod]
        public void UpdateToDo_Failed_ValidatesFailedResponse()
        {
            // Arrange
            var toDoRepoMock = new Mock<IToDoRepo>();
            toDoRepoMock.Setup(repo => repo.UpdateToDoItemById(It.IsAny<ToDoDTO>())).Returns(false);

            var toDoService = new ToDoService(toDoRepoMock.Object);
            var todoDTO = new ToDoDTO();

            // Act
            var result = toDoService.UpdateToDo(todoDTO);

            // Assert
            Assert.AreEqual("01", result.Code);
            Assert.AreEqual("failed", result.Description);
            Assert.IsNull(result.Data);
        }


    }
}
