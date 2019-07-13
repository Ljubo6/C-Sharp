using MilitaryElite.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MilitaryElite.Contracts
{
    public interface ICommando : ISpecialisedSoldier
    {
        ICollection<IMission> Missions { get; set; }
    }
}
