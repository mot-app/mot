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
        public async Task GetingSomeUsersFromDatabaseTest()
        {
            var ovm = new OverViewModel();
            await ovm.GetAvailableUsers("123");
            Assert.IsTrue(ovm.Users.Count != 0);
        }

        [Test]
        public async Task GetingSomeMeetupsFromDatabaseTest()
        {
            var ovm = new OverViewModel();
            await ovm.GetMeetups("123");
            Assert.IsTrue(ovm.Meetups.Count == 0);
        }
    }
    
}