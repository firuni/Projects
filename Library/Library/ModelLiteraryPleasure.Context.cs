﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Library
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class LiteraryPleasureEntities : DbContext
    {
        private static LiteraryPleasureEntities _context;

        public LiteraryPleasureEntities()
            : base("name=LiteraryPleasureEntities")
        {
        }

        public static LiteraryPleasureEntities GetContext()
        {
            if (_context == null)
                _context = new LiteraryPleasureEntities();
            return _context;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Reader> Readers { get; set; }
        public virtual DbSet<Realization> Realizations { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
    }
}