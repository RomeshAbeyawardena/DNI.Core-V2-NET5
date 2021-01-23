﻿using Humanizer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Abstractions
{
    public abstract class DbContextBase : DbContext
    {
        public DbContextBase(DbContextOptions dbContextOptions, bool singularizeTableNames = false)
            : base(dbContextOptions)
        {
            SingularizeTableNames = singularizeTableNames;
        }

        public bool SingularizeTableNames { get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if(SingularizeTableNames)
            {
                foreach(var entity in modelBuilder.Model.GetEntityTypes())
                {
                    entity.SetTableName(entity.Name.Singularize());
                }
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
