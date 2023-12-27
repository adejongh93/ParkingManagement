using AutoMapper;
using ParkingManagement.Database.DataModels;
using ParkingManagement.Services.DataModels;
using System.Linq.Expressions;

namespace ParkingManagement.Services.Mappers
{
    internal class VehicleMapper : IMapper, IVehicleMapper
    {
        private readonly IMapper mapper;

        public VehicleMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Vehicle, VehicleDto>();
                cfg.CreateMap<VehicleDto, Vehicle>();
            });

            mapper = config.CreateMapper();
        }

        public VehicleDto Map(Vehicle vehicle)
        {
            return Map<VehicleDto>(vehicle);
        }

        public TDestination Map<TDestination>(object source)
        {
            return mapper.Map<TDestination>(source);
        }

        public IConfigurationProvider ConfigurationProvider => throw new NotImplementedException();

        public TDestination Map<TDestination>(object source, Action<IMappingOperationOptions<object, TDestination>> opts)
        {
            throw new NotImplementedException();
        }

        public TDestination Map<TSource, TDestination>(TSource source, Action<IMappingOperationOptions<TSource, TDestination>> opts)
        {
            throw new NotImplementedException();
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination, Action<IMappingOperationOptions<TSource, TDestination>> opts)
        {
            throw new NotImplementedException();
        }

        public object Map(object source, Type sourceType, Type destinationType, Action<IMappingOperationOptions<object, object>> opts)
        {
            throw new NotImplementedException();
        }

        public object Map(object source, object destination, Type sourceType, Type destinationType, Action<IMappingOperationOptions<object, object>> opts)
        {
            throw new NotImplementedException();
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            throw new NotImplementedException();
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            throw new NotImplementedException();
        }

        public object Map(object source, Type sourceType, Type destinationType)
        {
            throw new NotImplementedException();
        }

        public object Map(object source, object destination, Type sourceType, Type destinationType)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source, object parameters = null, params Expression<Func<TDestination, object>>[] membersToExpand)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source, IDictionary<string, object> parameters, params string[] membersToExpand)
        {
            throw new NotImplementedException();
        }

        public IQueryable ProjectTo(IQueryable source, Type destinationType, IDictionary<string, object> parameters = null, params string[] membersToExpand)
        {
            throw new NotImplementedException();
        }
    }
}
