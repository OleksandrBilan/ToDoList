using NUnit.Framework;
using Moq;
using ToDoList.DB;
using ToDoList.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using ToDoList.Services;

namespace NUnitTodoListTests
{
    public class Tests
    {
        private Mock<ITodoRepository> _repositoryMock;
        private List<TodoItem> _testItems;
        private TodoListService _service;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<ITodoRepository>();
            _testItems = new List<TodoItem>() {
                new TodoItem(1, 2, "Sasha", "code unit tests", "05-06-2021"),
                new TodoItem(2, 0, "Sasha", "test unit tests", "05-06-2021")
            };
            _service = new TodoListService(_repositoryMock.Object);
        }

        [Test]
        public void GetAllItemsTest_ReturnsItemsIEnumerable()
        {
            //Arrange
            _repositoryMock
                .Setup(p => p.Get())
                .Returns(_testItems.AsEnumerable());
            
            //Act
            var resItems = _service.GetAllItems();

            //Assert
            Assert.AreEqual(_testItems, resItems);
        }

        [Test]
        public void GetItemByIdTest_ReturnsItem()
        {
            //Arrange
            _testItems = new List<TodoItem>() {
                new TodoItem(1, 2, "Sasha", "code unit tests", "05-06-2021"),
                new TodoItem(2, 0, "Sasha", "test unit tests", "05-06-2021")
            };
            _repositoryMock
                .Setup(p => p.Get(It.IsAny<int>()))
                .Returns(_testItems[1]);

            //Act
            TodoItem resItem = _service.GetItemById(2);

            //Assert
            Assert.AreEqual(_testItems[1], resItem);
        }

        [Test]
        public void GetItemByIdTest_ThrowsException()
        {
            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _service.GetItemById(0));
        }

        [Test]
        public void AddItemTest_AddsCorrect()
        {
            //Arrange
            TodoItem testItem = new TodoItem(3, 2, "assignee", "text", "10-06-2021");
            _repositoryMock
                .Setup(p => p.Create(It.IsAny<TodoItem>()))
                .Callback<TodoItem>(item => _testItems.Add(item));

            //Act
            _service.AddItem(testItem);

            //Assert
            Assert.AreEqual(_testItems[_testItems.Count - 1], testItem);
        }

        [Test]
        public void AddItemTest_ThrowsException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => _service.AddItem(null));
        }

        [Test]
        public void EditItemTest_ReturnsCorrect()
        {
            //Arrange
            TodoItem testItem = new TodoItem(3, 0, "oleksandr", "some text", "10-06-2021");
            _repositoryMock
                .Setup(p => p.Update(It.IsAny<TodoItem>()))
                .Callback<TodoItem>(item => _testItems[_testItems.Count - 1] = item);

            //Act
            _service.EditItem(testItem);

            //Assert
            Assert.AreEqual(testItem, _testItems[_testItems.Count - 1]);
        }

        [Test]
        public void EditItemTest_ThrowsException()
        {
            //Assert
            Assert.Throws<ArgumentNullException>(() => _service.EditItem(null));
        }

        [Test]
        public void DeleteItemTest_ReturnsDeletedItem()
        {
            //Arrange
            _testItems = new List<TodoItem>() {
                new TodoItem(1, 2, "Sasha", "code unit tests", "05-06-2021"),
                new TodoItem(2, 0, "Sasha", "test unit tests", "05-06-2021")
            };
            TodoItem testItem = new TodoItem(3, 0, "sasha", "some text", "10-06-2021");
            _testItems.Add(testItem);
            _repositoryMock
                .Setup(p => p.Delete(It.IsAny<int>()))
                .Callback<int>(id => _testItems.RemoveAt(id - 1))
                .Returns(testItem);

            //Act
            TodoItem resItem = _service.DeleteItem(3);

            //Assert
            Assert.IsTrue(_testItems.Count == 2);
            Assert.AreEqual(testItem, resItem);
        }

        [Test]
        public void DeleteItemTest_ThrowsException()
        {
            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _service.DeleteItem(0));
        }
    }
}