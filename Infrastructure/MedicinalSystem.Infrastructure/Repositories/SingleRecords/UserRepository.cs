﻿using Bogus.DataSets;
using MedicinalSystem.Domain.Abstractions;
using MedicinalSystem.Domain.Entities;
using MedicinalSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicinalSystem.Infrastructure.Repositories.SingleRecords
{
    public class UserRepository(AppDbContext dbContext) : IUserRepository
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task Create(User entity) => await _dbContext.Users.AddAsync(entity);

        public async Task<IEnumerable<User>> Get(bool trackChanges, string? name)
        {
            var users = await (!trackChanges
                ? _dbContext.Users.OrderBy(d => d.Id).AsNoTracking()
                : _dbContext.Users.OrderBy(d => d.Id)).ToListAsync();
            if (!string.IsNullOrWhiteSpace(name))
            {
                users = users.Where(s => s.UserName.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return users;
        }

        public async Task<User?> GetById(Guid id, bool trackChanges) =>
            await (!trackChanges ?
                _dbContext.Users.AsNoTracking() :
                _dbContext.Users).SingleOrDefaultAsync(e => e.Id == id);

        public void Delete(User entity) => _dbContext.Users.Remove(entity);

        public void Update(User entity) => _dbContext.Users.Update(entity);

        public async Task SaveChanges() => await _dbContext.SaveChangesAsync();

        public async Task<int> CountAsync(string? name)
        {
            var users = await _dbContext.Users.ToListAsync();
            if (!string.IsNullOrWhiteSpace(name))
            {
                users = users.Where(s => s.UserName.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            return users.Count();
        }

        public async Task<IEnumerable<User>> GetPageAsync(int page, int pageSize, string? name)
        {
            var users = await _dbContext.Users.OrderBy(d => d.Id).ToListAsync();
            if (!string.IsNullOrWhiteSpace(name))
            {
                users = users.Where(s => s.UserName.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return users.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}
