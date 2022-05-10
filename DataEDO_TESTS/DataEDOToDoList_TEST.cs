using DataEDO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataEDO_TESTS
{
    [TestClass]
    public class DataEDOToDoList_TEST
    {
        [TestMethod]
        public void DataEDOToDoList_ChangeStatus_DefaultNoItems_TEST()
        {
            DataEDOToDoList dataEDOForm = new DataEDOToDoList();
            dataEDOForm.SetActionsForFormStats();
            dataEDOForm.SetCurrentFormStatus(FormStatuses.DefaultNoItems);

            Assert.IsTrue(dataEDOForm.SBAdd.Enabled);
            Assert.IsFalse(dataEDOForm.SBEdit.Enabled);
            Assert.IsFalse(dataEDOForm.SBCancel.Enabled);
            Assert.IsFalse(dataEDOForm.SBSave.Enabled);
            Assert.IsFalse(dataEDOForm.TitleTextEdit.Enabled);
            Assert.IsFalse(dataEDOForm.DescriptionMemoEdit.Enabled);
            Assert.IsFalse(dataEDOForm.DateDateEdit.Enabled);

            Assert.IsTrue(dataEDOForm.gcDataSourceSelection.Enabled);

            Assert.IsTrue(dataEDOForm.GCToDoList.Enabled);

        }
        [TestMethod]
        public void DataEDOToDoList_ChangeStatus_DefaultPresentItems_TEST()
        {
            DataEDOToDoList dataEDOForm = new DataEDOToDoList();
            dataEDOForm.SetActionsForFormStats();
            dataEDOForm.SetCurrentFormStatus(FormStatuses.DefaultPresentItems);

            Assert.IsTrue(dataEDOForm.SBAdd.Enabled);
            Assert.IsTrue(dataEDOForm.SBEdit.Enabled);
            Assert.IsFalse(dataEDOForm.SBCancel.Enabled);
            Assert.IsFalse(dataEDOForm.SBSave.Enabled);
            Assert.IsFalse(dataEDOForm.TitleTextEdit.Enabled);
            Assert.IsFalse(dataEDOForm.DescriptionMemoEdit.Enabled);
            Assert.IsFalse(dataEDOForm.DateDateEdit.Enabled);

            Assert.IsTrue(dataEDOForm.gcDataSourceSelection.Enabled);

            Assert.IsTrue(dataEDOForm.GCToDoList.Enabled);

        }
        [TestMethod]
        public void DataEDOToDoList_ChangeStatus_Adding_TEST()
        {
            DataEDOToDoList dataEDOForm = new DataEDOToDoList();
            dataEDOForm.SetActionsForFormStats();
            dataEDOForm.SetCurrentFormStatus(FormStatuses.Adding);

            Assert.IsFalse(dataEDOForm.SBAdd.Enabled);
            Assert.IsFalse(dataEDOForm.SBEdit.Enabled);
            Assert.IsTrue(dataEDOForm.SBCancel.Enabled);
            Assert.IsFalse(dataEDOForm.SBSave.Enabled);
            Assert.IsTrue(dataEDOForm.TitleTextEdit.Enabled);
            Assert.IsTrue(dataEDOForm.DescriptionMemoEdit.Enabled);
            Assert.IsTrue(dataEDOForm.DateDateEdit.Enabled);

            Assert.IsFalse(dataEDOForm.gcDataSourceSelection.Enabled);

            Assert.IsFalse(dataEDOForm.GCToDoList.Enabled);

            
        }

        [TestMethod]
        public void DataEDOToDoList_ChangeStatus_Editing_TEST()
        {
            DataEDOToDoList dataEDOForm = new DataEDOToDoList();
            dataEDOForm.SetActionsForFormStats();
            dataEDOForm.SetCurrentFormStatus(FormStatuses.Editing);

            Assert.IsFalse(dataEDOForm.SBAdd.Enabled);
            Assert.IsFalse(dataEDOForm.SBEdit.Enabled);
            Assert.IsTrue(dataEDOForm.SBCancel.Enabled);
            Assert.IsFalse(dataEDOForm.SBSave.Enabled);
            Assert.IsTrue(dataEDOForm.TitleTextEdit.Enabled);
            Assert.IsTrue(dataEDOForm.DescriptionMemoEdit.Enabled);
            Assert.IsTrue(dataEDOForm.DateDateEdit.Enabled);

            Assert.IsFalse(dataEDOForm.gcDataSourceSelection.Enabled);

            Assert.IsFalse(dataEDOForm.GCToDoList.Enabled);

        }


    }
}
