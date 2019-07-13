using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MilitaryElite.Contracts
{
    public interface ILieutenantGeneral : IPrivate
    {
        ICollection<IPrivate> Privates { get; set; }
    }
}
