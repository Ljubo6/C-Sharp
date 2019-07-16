using System;
using System.Collections.Generic;
using System.Text;

namespace VehiclesExtension.Vehicles.Contracts
{
    public interface IVehicle
    {
        double FuelQuantity { get; }
        double FuelConsumption { get; }
        double TankCapacity { get;}
        bool IsVehicleEmpty { get; set; }
        void Drive(double distance);
        void Refuel(double fuel);


    }
}
