using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursePol.Models
{
    interface IFolower
    {
        IEnumerable<Folower> Folowers { get; }
        void SaveFolower(Folower folower);
        Folower DeletFolower(int folowerID);
    }
}
