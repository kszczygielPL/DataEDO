using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataEDO.DataSave;
using DataEDO.Model.Todo;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataEDO_TESTS
{
    [TestClass]
    public class FileLink_TEST
    {
        [TestMethod]
        [DataRow("1", "Title 1 ", "Long descrition")]
        public void FileLink_Save_Open_TEST(string id, string title, string description)
        {
            List<ToDo> _todos = new List<ToDo> {
                new ToDo
                {
                    Id = int.Parse(id),
                    Title = title,
                    Description = description,
                    IsNew = false,
                } 
            };

            FileLink _fileOpertions = new FileLink();
            _fileOpertions.SaveToDoList(_todos, "C:\\temp\\test_file.dat");

            List<ToDo> _todosLoaded = _fileOpertions.LoadToDoList("C:\\temp\\test_file.dat");

            CollectionAssert.AreEquivalent(_todos, _todosLoaded);
            Assert.AreEqual(_todos.Count, _todosLoaded.Count);

        }
    }
}
