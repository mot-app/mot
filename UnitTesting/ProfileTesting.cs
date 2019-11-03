using Microsoft.VisualStudio.TestTools.UnitTesting;
using mot.ViewModels;
using System.Threading.Tasks;

namespace UnitTesting
{
    [TestClass]
    public class ProfileTesting
    {
        [TestMethod]
        public async void GettingUserFromDatabaseTest()
        {
            string id = "116466181289590143131";
            var pvm = new ProfileViewModel();
            await pvm.GetUser(id);
            Assert.AreEqual(id, pvm.Id);
        }
    }
}
