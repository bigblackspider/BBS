using System.Linq;
using BBS.Core.Models;

namespace BBS.Core.Tests.TestData
{
    internal class WebsiteDbSetMock : TestDbSet<Website>
    {
        public override Website Find(params object[] keyValues)
        {
            return this.SingleOrDefault(o => o.WebsiteId == (int) keyValues.Single());
        }
    }
}