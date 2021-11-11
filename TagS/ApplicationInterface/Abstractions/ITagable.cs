using HongJieSun.TagS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagS.Models.Referrers;

namespace TagS.ApplicationInterface.Abstractions
{
    public interface ITagable
    {
        IReferrer ToReferrer();
    }
}
