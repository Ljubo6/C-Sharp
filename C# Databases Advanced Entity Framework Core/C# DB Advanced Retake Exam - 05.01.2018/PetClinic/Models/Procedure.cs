using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace PetClinic.Models
{
    public class Procedure
    {
        //        -	Id – integer, Primary Key
        //-	AnimalId ¬– integer, foreign key
        //-	Animal – the animal on which the procedure is performed(required)
        //-	VetId ¬– integer, foreign key
        //-	Vet – the clinic’s employed doctor servicing the patient(required)
        //-	ProcedureAnimalAids – collection of type ProcedureAnimalAid
        //-	Cost – the cost of the procedure, calculated by summing the price of the different services performed; does not need to be inserted in the database
        //-	DateTime – the date and time on which the given procedure is performed(required)

        public int Id { get; set; }

        public int AnimalId { get; set; }

        [Required]
        public Animal Animal { get; set; }

        public int VetId { get; set; }

        [Required]
        public Vet Vet { get; set; }

        public ICollection<ProcedureAnimalAid> ProcedureAnimalAids { get; set; } = new HashSet<ProcedureAnimalAid>();
        public decimal Cost => this.ProcedureAnimalAids.Sum(p => p.AnimalAid.Price);

        [Required]
        public DateTime DateTime { get; set; }

    }
}
