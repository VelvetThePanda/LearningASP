﻿using Microsoft.EntityFrameworkCore;

namespace Learning_ASP_Web.Models
{
    public class TodoContext : DbContext
    {
        public DbSet<ToDoItem> TodoItems { get; set; }
        public TodoContext(DbContextOptions options) : base(options) { }
    }
}