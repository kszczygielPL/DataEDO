using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataEDO.DataSave;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataEDO_TESTS
{
    [TestClass]
    public class DatabaseLink_TEST
    {
        [TestMethod]
        [DataRow("Server= localhost; Database= master; Integrated Security=True;", "true")]
        [DataRow("bad result", "false")]

        public void DatabaseLink_CheckConnectionLink_TEST(string _connectionString, string expectedResult)
        {
            var databaseLink = new DatabaseLink();
            bool expectedResultBool = Boolean.Parse(expectedResult);

            Assert.AreEqual(expectedResultBool, databaseLink.CheckConnectionLink(_connectionString));
        }
    }
}
