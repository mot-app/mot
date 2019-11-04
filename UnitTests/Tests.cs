using mot.ViewModels;
using NUnit.Framework;
using System.Threading.Tasks;

namespace UnitTests
{
    public class Tests
    {
        [Test]
        public async Task GettingUserFromDatabaseTest()
        {
            string id = "116466181289590143131";
            var pvm = new ProfileViewModel();
            await pvm.GetUser(id);
            Assert.AreEqual(id, pvm.Id);
        }

        [Test]
        public async Task UpdatingAvailabilityTest()
        {
            string id = "116466181289590143131";
            var pvm = new ProfileViewModel();
            await pvm.GetUser(id);
            var beforeAvailable = pvm.Available;
            var beforeText = pvm.DisplayAvailable;
            var beforeButton = pvm.DisplayAvailableButton;
            pvm.ChangeAvailable.Execute(id);
            Assert.AreNotEqual(beforeText, pvm.DisplayAvailable);
            Assert.AreNotEqual(beforeButton, pvm.DisplayAvailableButton);
            Assert.AreNotEqual(beforeAvailable, pvm.Available);
        }

        [Test]
        public async Task GetingSomeUsersFromDatabaseTest()
        {
            var ovm = new OverViewModel();
            await ovm.GetAvailableUsers("123");
            Assert.IsTrue(ovm.Users.Count != 0);
        }
    }
    
}