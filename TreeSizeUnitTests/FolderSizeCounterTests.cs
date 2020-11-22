using System;
using System.Collections.Generic;
using System.Text;
using TreeSize;
using TreeSize.Classes;
using TreeSize.Controllers;
using TreeSize.Persistence;
using NUnit;
using NUnit.Framework;
//using Moq;
using System.Threading.Tasks;


namespace TreeSizeUnitTests
{
    [TestFixture]
    public class FolderSizeCounterTests
    {
        public static string ExternalFolderName = "ExternalFolder";
        public static string InnerFolderName = "InnerFolder";
        public IDiscReadable _reader = new FakeRepository();
        public FolderCounter counter = new FolderCounter(new FakeRepository());


        [Test]
        public async Task CountSizeFoldersOfFolderViewModelAsync_FolderAsListInnerFileObjects_SizeCounted()
        {
            var excpected = 2000;
            var pocessor = new FolderModel { FullPath = ExternalFolderName };
            var testFolder = new List<FIleObject>();
            testFolder.Add(pocessor);
            testFolder = await counter.CountSizeFoldersOfFolderViewModelAsync(testFolder);
            var res = testFolder[0].Size;
            Assert.That(res == excpected);
        } 
    }

}
