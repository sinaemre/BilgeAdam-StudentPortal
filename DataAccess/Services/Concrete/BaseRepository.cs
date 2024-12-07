using ApplicationCore.Consts;
using ApplicationCore.Entities.Abstract;
using ApplicationCore.Entities.Concrete;
using ApplicationCore.UserEntites.Concrete;
using DataAccess.Context.ApplicationContext;
using DataAccess.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services.Concrete
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        private readonly IUserService _userService;
        private readonly DbSet<T> _table;

        public BaseRepository(AppDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
            _table = context.Set<T>();
        }

        public async Task<bool> AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            return await SaveAsync();
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            entity.UpdatedDate = DateTime.Now;
            entity.Status = Status.Modified;
            var email = "";
            var entityType = entity.GetType();
            if (entityType == typeof(Teacher))
            {
                var teacher = await _context.Teachers.FirstOrDefaultAsync(x => x.Id == entity.Id);
                email = teacher != null ? teacher.Email : "";
               
            }
            else if (entityType == typeof(Student))
            {
                var student = await _context.Students.FirstOrDefaultAsync(x => x.Id == entity.Id);
                email = student != null ? student.Email : "";
            }
            else if (entityType == typeof(CustomerManager))
            {
                var customerManager = await _context.CustomerManagers.FirstOrDefaultAsync(x => x.Id == entity.Id);
                email = customerManager != null ? customerManager.Email : "";
            }
            if (!string.IsNullOrEmpty(email))
            {
                var appUser = await _userService.FindUserByEmailAsync(email);
                if (appUser != null)
                {
                    appUser.Email = entityType == typeof(Teacher) ? (entity as Teacher).Email :
                                    entityType == typeof(Student) ? (entity as Student).Email :
                                    entityType == typeof(CustomerManager) ? (entity as CustomerManager).Email : "";
                    appUser.NormalizedEmail = appUser.Email.Replace('i', 'ı').ToUpper();
                    if (!string.IsNullOrEmpty(appUser.Email))
                    {
                        var result = await _userService.UpdateUserAsync(appUser);
                        if (!result)
                            throw new Exception("Kullanıcı maili güncellenemedi!");
                    }
                }
            }
            _table.Update(entity);
            return await SaveAsync();
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            entity.DeletedDate = DateTime.Now;
            entity.Status = Status.Passive;
            _table.Update(entity);
            return await SaveAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
            => await _table.AnyAsync(expression);


        public async Task<T> GetByDefaultAsync(Expression<Func<T, bool>> expression)
            => await _table.FirstOrDefaultAsync(expression);

        public async Task<List<T>> GetByDefaultsAsync(Expression<Func<T, bool>> expression)
             => await _table.Where(expression).ToListAsync();

        public async Task<T> GetByIdAsync(Guid id)
            => await _table.FirstOrDefaultAsync(x => x.Status != Status.Passive && x.Id == id);

        public async Task<List<TResult>> GetFilteredListAsync<TResult>
            (
                Expression<Func<T, TResult>> select, 
                Expression<Func<T, bool>> where = null, 
                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                Func<IQueryable<T>, IIncludableQueryable<T, object>> join = null
            )
        {
            IQueryable<T> query = _table;

            if (join != null)
                query = join(query);
            if (where != null)
                query = query.Where(where);
            if (orderBy != null)
                return await orderBy(query).Select(select).ToListAsync();

            return await query.Select(select).ToListAsync();
        }

        private async Task<bool> SaveAsync()
            => await _context.SaveChangesAsync() > 0 ? true : false;
    }
}
