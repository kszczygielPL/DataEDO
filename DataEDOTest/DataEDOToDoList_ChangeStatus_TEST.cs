using DataEDO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataEDOTest
{
    [TestClass]
    public class DataEDOToDoList_ChangeStatus_TEST
    {
        [TestMethod]
        public void DataEDOToDoList_ChangeStatusTest()
        {
            DataEDOToDoList toDoListForm = new DataEDOToDoList();

            toDoListForm.SetActionsForFormStats();
        }
    }
}
