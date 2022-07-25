﻿using Microsoft.EntityFrameworkCore;
using ProjetoFinalApi.Context;
using ProjetoFinalApi.Repository.Interfaces;
using System.Linq.Expressions;

namespace ProjetoFinalApi.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApiDbContext _context;

        public Repository(ApiDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> Get()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public T GetById(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().SingleOrDefault(predicate);
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Set<T>().Update(entity);
        }
    }
}
