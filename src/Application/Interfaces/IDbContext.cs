using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IDbContext
    {
        DbSet<WeatherForecast> WeatherForecast { get; set; }
        Task<int> SaveChangesAsync();
    }
}