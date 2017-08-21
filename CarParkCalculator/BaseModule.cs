using Autofac;
using Autofac.Core;
using CarParkCalculator.Abstractions;
using CarParkCalculator.Data;
using CarParkCalculator.Models;
using CarParkCalculator.Services;
using CarParkCalculator.Validators;

namespace CarParkCalculator {
    public class BaseModule : Module {

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<StirngValidator>().As<IValidator<string>>().Keyed<IValidator<string>>(0);
            builder.RegisterType<DateTimeValidator>().As<IValidator<string>>().Keyed<IValidator<string>>(0);
            builder.RegisterType<DurationValidator>().As<IValidator<ParkingTimer>>();

            builder.RegisterType<SpecialJsonRepo>().As<IRepo<Special>>().As<ISpecialRepo>().SingleInstance();
            builder.RegisterType<GeneralJsonRepo>().As<IRepo<General>>().As<IGeneralRepo>().SingleInstance();

            builder.RegisterType<CalculateService>().As<ICalculateService>().SingleInstance();
            builder.RegisterType<SpecialService>().As<IService<Special>>().As<ISpecialService>().SingleInstance();
            builder.RegisterType<GeneralService>().As<IService<General>>().As<IGeneralService>().SingleInstance();

            builder.RegisterType<AppService>().As<IAppService>().
                WithParameter( new ResolvedParameter( (pi, ctx) => pi.ParameterType == typeof(IValidator<string>), (pi, ctx) => ctx.ResolveKeyed<IValidator<string>>(0))).SingleInstance();
        }

    }
}