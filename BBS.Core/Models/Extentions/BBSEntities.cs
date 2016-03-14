using System.Data.Entity;
using BBS.Core.Models.Extentions;

// ReSharper disable once CheckNamespace
namespace BBS.Core.Models
{
    public partial class BBSEntities : IBBSEntities
    {
        public void MarkAsModified(Website item)
        {
            Entry(item).State = EntityState.Modified;
        }
    }
}