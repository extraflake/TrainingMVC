using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TrainingMSHR.Models;

namespace TrainingMSHR.Context
{
    public class MyContext : DbContext
    {
        public MyContext() : base("MyConnection") { }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}