﻿using DatabaseCaseExtractorDemo.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseCaseExtractorDemo.Base
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }

        #region InitData
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            Table1 record1 = new Table1()
            {
                Id = Guid.NewGuid(),
                IntOne = 1,
                NameOne = "A",
                DateOne = DateTime.Now
            };
            modelBuilder.Entity<Table1>().HasData(record1);
            Table2 record2 = new Table2()
            {
                Id = 1,
                IntSecond = 1,
                NameSecond = "A",
                DateSecond = DateTime.Now,
                TableOneId = record1.Id
            };
            modelBuilder.Entity<Table2>().HasData(record2);
            Table2 record3 = new Table2()
            {
                Id = 2,
                IntSecond = 1,
                NameSecond = "A",
                DateSecond = DateTime.Now,
                TableOneId = record1.Id
            };
            modelBuilder.Entity<Table2>().HasData(record3);
            Table3 record4 = new Table3()
            {
                Id = "A",
                IntThird = 1,
                NameThird = "A",
                DateThird = DateTime.Now,
                TableSecondId = 1
            };
            modelBuilder.Entity<Table3>().HasData(record4);
            Table3 record5 = new Table3()
            {
                Id = "B",
                IntThird = 1,
                NameThird = "A",
                DateThird = DateTime.Now,
                TableSecondId = 1
            };
            modelBuilder.Entity<Table3>().HasData(record5);
            Table3 record6 = new Table3()
            {
                Id = "C",
                IntThird = 1,
                NameThird = "A",
                DateThird = DateTime.Now,
                TableSecondId = 2
            };
            modelBuilder.Entity<Table3>().HasData(record6);
            Table3 record7 = new Table3()
            {
                Id = "D",
                IntThird = 1,
                NameThird = "A",
                DateThird = DateTime.Now,
                TableSecondId = 2
            };
            modelBuilder.Entity<Table3>().HasData(record7);
        }
        #endregion

        #region Models
        public DbSet<Table1> TableOnes { get; set; }
        public DbSet<Table2> TableSeconds { get; set; }
        public DbSet<Table3> TableThirds { get; set; }
        #endregion

    }
}
