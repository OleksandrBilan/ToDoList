using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Models
{
    public class TodoDBContext : DbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }

        public TodoDBContext(DbContextOptions<TodoDBContext> options) 
            : base(options)
        { }
    }
}
