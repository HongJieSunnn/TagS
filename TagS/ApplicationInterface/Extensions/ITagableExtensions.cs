using HongJieSun.TagS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagS.Models.Referrers;

namespace TagS.ApplicationInterface.Extensions
{
    public static class ITagableExtensions
    {
        public static async Task AddTagAsync(this IReferrer referrer,Tag tag)
        {
            throw new NotImplementedException();
        }

        public static async Task RemoveTagAsync(this IReferrer referrer,Tag tag)
        {
            throw new NotImplementedException();   
        }
    }
}
