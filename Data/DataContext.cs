using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base("name=DefaultConnection") { }

        //DB Sets
        public DbSet<TaskModel> Tasks { get; set; }
    }
}
