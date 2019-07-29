using AnimalCentre.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalCentre.Models.Pricedures
{
    public abstract class Procedure : IProcedure
    {
        protected ICollection<IAnimal> procedureHistory;
        protected Procedure()
        {
            this.procedureHistory = new List<IAnimal>();
        }
        
        public virtual void DoService(IAnimal animal, int procedureTime)
        {
            if (animal.ProcedureTime < procedureTime)
            {
                throw new ArgumentException("Animal doesn't have enough procedure time");
            }
            animal.ProcedureTime -= procedureTime;
            procedureHistory.Add(animal);
        }

        public string History()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(this.GetType().Name);
            foreach (var animal in procedureHistory)
            {
                sb.AppendLine(animal.ToString());
            }
            return sb.ToString().TrimEnd();
        }
    }
}
