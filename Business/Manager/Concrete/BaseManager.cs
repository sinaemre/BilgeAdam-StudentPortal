using ApplicationCore.Entities.Abstract;
using AutoMapper;
using Business.Manager.Interface;
using DataAccess.Services.Interface;
using DTO.Abstract;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Manager.Concrete
{
    public class BaseManager<A, C> : IBaseManager<A, C>
        where A : IBaseRepository<C>
        where C : BaseEntity
    {
        private readonly A _service;
        private readonly IMapper _mapper;

        public BaseManager(A service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<bool> AddAsync(BaseDTO dto)
        {
            var entity = _mapper.Map<C>(dto);
            return await _service.AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(BaseDTO dto)
        {
            var entity = _mapper.Map<C>(dto);
            return await _service.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(BaseDTO dto)
        {
            var entity = _mapper.Map<C>(dto);
            return await _service.DeleteAsync(entity);
        }

        public async Task<bool> AnyAsync(Expression<Func<C, bool>> expression)
            => await _service.AnyAsync(expression);


        public async Task<T> GetByDefaultAsync<T>(Expression<Func<C, bool>> expression)
        {
            var entity = await _service.GetByDefaultAsync(expression);
            var dto = _mapper.Map<T>(entity);
            return dto;
        }

        public async Task<List<T>> GetByDefaultsAsync<T>(Expression<Func<C, bool>> expression)
        {
            var entityList = await _service.GetByDefaultsAsync(expression);
            var dtoList = _mapper.Map<List<T>>(entityList);
            return dtoList;
        }

        public async Task<T> GetByIdAsync<T>(Guid id)
        {
            var entity = await _service.GetByIdAsync(id);
            var dto = _mapper.Map<T>(entity);
            return dto;
        }

        public async Task<List<TResult>> GetFilteredListAsync<TResult>
            (
                Expression<Func<C, TResult>> select, 
                Expression<Func<C, bool>> where = null, 
                Func<IQueryable<C>, IOrderedQueryable<C>> orderBy = null,
                Func<IQueryable<C>, IIncludableQueryable<C, object>> join = null
            )

            => await _service.GetFilteredListAsync(select, where, orderBy, join);
    }
}
