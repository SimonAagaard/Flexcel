using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;
using Domain;

namespace UnitTest
{
    [TestClass]
    public class SelctionTest
    {
        TestDataContainer testData = new TestDataContainer();
        SelectionController selectionController = new SelectionController();
        DataAccess.CSVImport import;

        [TestMethod]
        public void TestMethod_NoInputData()
        {
            selectionController.SelectWinners();
        }

        [TestMethod]
        public void TestMethod_HappyPath()
        {
            testData.FillListContainer_HappyPath();
            selectionController.SelectWinners();
        }
        [TestMethod]
        public void TestHours()
        {
            import = new DataAccess.CSVImport();

            import.ImportOffers(@"C:\Users\Moyum\Desktop\Skabelon til test\RouteNumbers.csv");

            
        }
    }
}
