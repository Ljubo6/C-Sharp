using System;
using System.Collections.Generic;
using System.Text;
using ViceCity.Models.Guns.Contracts;

namespace ViceCity.Models.Guns
{
    public class Pistol : Gun, IGun
    {
        private const int InitialBulletsPerBarel = 10;
        private const int InitialTotalBullets = 100;
        public Pistol(string name) 
            : base(name, InitialBulletsPerBarel, InitialTotalBullets)
        {
        }

        public override int Fire()
        {
            if (this.BulletsPerBarrel - 1 <= 0 && this.TotalBullets > 0)
            {
                this.BulletsPerBarrel = InitialBulletsPerBarel;
                this.TotalBullets -= InitialBulletsPerBarel;
                return 0;
            }
            else
            {
                this.BulletsPerBarrel--;
                return 1;
            }
        }
    }
}
