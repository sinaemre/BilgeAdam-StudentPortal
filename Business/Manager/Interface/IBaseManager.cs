using ApplicationCore.Entities.Abstract;
using DataAccess.Services.Interface;
using DTO.Abstract;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Manager.Interface
{
    public interface IBaseManager<A, C>
        where A : IBaseRepository<C>
        where C : BaseEntity
        //Örnek Kullanım => IBaseManager<ITeacherService, Teacher, >
    {
        Task<bool> AddAsync(BaseDTO dto);
        Task<bool> UpdateAsync(BaseDTO dto);
        Task<bool> DeleteAsync(BaseDTO dto);

        Task<T> GetByIdAsync<T>(Guid id);
        Task<T> GetByDefaultAsync<T>(Expression<Func<C, bool>> expression);
        Task<List<T>> GetByDefaultsAsync<T>(Expression<Func<C, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<C, bool>> expression);
        Task<List<TResult>> GetFilteredListAsync<TResult>
            (
                Expression<Func<C, TResult>> select,
                Expression<Func<C, bool>> where = null,
                Func<IQueryable<C>, IOrderedQueryable<C>> orderBy = null,
                Func<IQueryable<C>, IIncludableQueryable<C, object>> join = null
            );
    }
}
