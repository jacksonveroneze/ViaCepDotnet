﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace JacksonVeroneze.ViaCep.BuildingBlocks
{

    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly DbContext _context;

        protected BaseRepository(DbContext context)
            => _context = context;

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);

            await _context.SaveChangesAsync();
        }

        public Task<List<T>> FindAllAsync()
            => _context.Set<T>().ToListAsync();

        public Task<T> FindAsync(int id)
            => _context.Set<T>().FindAsync(id);
    }
}