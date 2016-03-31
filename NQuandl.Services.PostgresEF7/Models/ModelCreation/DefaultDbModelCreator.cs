using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using NQuandl.Domain.Persistence.Entities;

namespace NQuandl.Services.PostgresEF7.Models.ModelCreation
{
    public class DefaultDbModelCreator : ICreateDbModel
    {
        public void Create(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Database>().HasKey(x => x.Id);
            modelBuilder.Entity<Dataset>().HasKey(x => x.Id);
            modelBuilder.Entity<RawResponse>().HasKey(x => x.Id);
        }
    }
}
