using AutoMapper;
using CarDealer.Dtos.Export;
using CarDealer.Dtos.Import;
using CarDealer.Models;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            this.CreateMap<ImportSupplierDto, Supplier>();
            this.CreateMap<ImportCustomersDto, Customer>();
            this.CreateMap<ImportSaleDto, Sale>();

            this.CreateMap<Car, ExportCarsWithDistanceDto>();

            this.CreateMap<Supplier, ExportLocalSuppliersDto>();


        }
    }
}
