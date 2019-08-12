using System;
using System.Collections.Generic;
using System.Text;
using ViceCity.Models.Guns.Contracts;

namespace ViceCity.Models.Guns
{
    public class Rifle : Gun, IGun
    {
        private const int InitialBulletsPerBarel = 50;
        private const int InitialTotalBullets = 500;
        public Rifle(string name) 
            : base(name, InitialBulletsPerBarel, InitialTotalBullets)
        {
        }

        public override int Fire()
        {
            if (this.BulletsPerBarrel - 5 == 0 && this.TotalBullets > 0)
            {
                this.BulletsPerBarrel -= 5;
                this.BulletsPerBarrel = InitialBulletsPerBarel;
                this.TotalBullets -= InitialBulletsPerBarel;
                return 5;
            }
            else
            {
                this.BulletsPerBarrel -= 5;
                return 5;
            }

        }
    }
}
