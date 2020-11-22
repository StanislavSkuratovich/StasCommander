using NUnit;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TreeSizeUnitTests
{
    [TestFixture]
    class FIlePlaceChangerUnitTests
    {
        FakeRepository repository = new FakeRepository();
        TreeSize.Persistence.IDiscReadable reader = new FakeRepository();
        TreeSize.Persistence.IDiscReWritable writer = new FakeRepository();
        TreeSize.Controllers.FIlePlaceChanger _changer = new TreeSize.Controllers.FIlePlaceChanger(new FakeRepository(), new FakeRepository());

        [Test]
        public void ChooseMethod_DeleteMethodName_AppropriateMethodReturned()
        {
            var name = "Delete";
            var actual = _changer.ChooseMethod(name).Method.Name;
            var excpected = "DeletePath";
            Assert.That(actual == excpected);
        }
        [Test]
         public void ChooseMethod_PasteMethodName_AppropriateMethodReturned()
        {
            var name = "Paste";
            var actual = _changer.ChooseMethod(name).Method.Name;
            var excpected = "PastePath";
            Assert.That(actual == excpected);
        }
        [Test]
        public void ChooseMethod_UnapproptiateMethodName_ExceptionCaught()
        {

            try
            {
                var name = "NotAddedMethodName";
                var actual = _changer.ChooseMethod(name).Method.Name;
                Assert.Fail(); // If it gets to this line, no exception was thrown
            }
            catch (Exception e) { }
           
        }

    }
}
