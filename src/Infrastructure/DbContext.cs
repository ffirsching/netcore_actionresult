using System;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Domain.Entities;
using System.Linq;

namespace Infrastructure
{
    public class InMemoryDbContext : DbContext, IDbContext
    {

  
        public InMemoryDbContext(DbContextOptions<InMemoryDbContext> options) : base(options)
        {
        }

        public DbSet<WeatherForecast> WeatherForecast { get; set; }

      
    }
}
