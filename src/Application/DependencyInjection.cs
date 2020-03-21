using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application {
    public static class DependencyInjection {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IWeatherForecastService, WeatherForecastService>();

            return services;
        }
    }
}