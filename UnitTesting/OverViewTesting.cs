using Microsoft.VisualStudio.TestTools.UnitTesting;
using mot.ViewModels;
using System.Threading.Tasks;

namespace UnitTesting
{
    [TestClass]
    public class OverViewTesting
    {
        [TestMethod]
        public async void GetingSomeUsersFromDatabaseTest()
        {
            var ovm = new OverViewModel();
            await ovm.GetAvailableUsers();
            Assert.IsTrue(ovm.Users.Count != 0);
        }
    }
}
