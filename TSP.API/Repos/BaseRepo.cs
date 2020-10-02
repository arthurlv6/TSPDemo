using AutoMapper;
using TSP.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TSP.API.Repos
{
    public class BaseRepo<R> 
    {
        protected readonly AppDbContext dBContext;
        protected readonly IMapper mapper;
        protected readonly ILogger<R> logger;

        public BaseRepo(AppDbContext _dBContext, IMapper _mapper, ILogger<R> _logger)
        {
            dBContext = _dBContext;
            mapper = _mapper;
            logger = _logger;
        }
        public virtual async Task<Maybe<List<M>>> GetAllAsync<T, M>(int page=1,int size=20,string keyword = "") where T : BaseEntity where M : BaseModel
        {
            try
            {
                return (await dBContext.Set<T>()
               .Where(d => d.Name.Contains(keyword))
               .OrderBy(d => d.Id)
               .Skip((page - 1) * size)
               .Take(size)
               .Select(d => d.ToModel<M>(mapper))
               .ToListAsync())
               .Action(d => logger.LogInformation("Get all done."))
               .Map(d => Maybe.Ok(d));
            }
            catch (Exception ex)
            {
                return ("GetAll failed, detail:" + ex.Message)
                    .Action(d => logger.LogError(d))
                    .Map(d => Maybe.Fail<List<M>>(d));
            }
        }
        public async Task<Maybe<M>> GetOneAsync<T,M>(int id) where T : BaseEntity where M : BaseModel
        {
            try
            {
                return (await dBContext.Set<T>()
                        .AsNoTracking()
                        .FirstOrDefaultAsync(d => d.Id == id))
                        .Map(d =>
                        {
                            if (d != null)
                                return Maybe.Ok(d.ToModel<M>(mapper));
                            else
                                return Maybe.Ok(default(M));
                        });
            }
            catch (Exception ex)
            {
                return Maybe.Fail<M>("GetOne failed, detail:" + ex.Message);
            }
        }
        public async Task<Maybe<bool>> UpdateAsync<T, M>(M m) where T : BaseEntity where M : BaseModel
        {
            try
            {
                return (await dBContext.Set<T>().FirstAsync(d => d.Id == m.Id))
                .Map(d => mapper.Map(m, d))
                .Action(d=> dBContext.SaveChanges())
                .Map(d=> Maybe.Ok(true));
            }
            catch (Exception ex)
            {
                return Maybe.Fail<bool>("Update failed, detail:" + ex.Message);
            }
        }
        public virtual async Task<Maybe<Tuple<IEnumerable<M>, double>>> GetPageData<T, M>(int page = 1, int size = 20, string keyword = "") where M : BaseModel where T : BaseEntity
        {
            try
            {
                var queryable = dBContext.Set<T>().OrderBy(d => d.Id).AsQueryable();
                Expression<Func<T, bool>> nameExpected = d => true;
                if (!string.IsNullOrEmpty(keyword))
                {
                    nameExpected = d => d.Name.Contains(keyword);
                }

                double count = await queryable.Where(nameExpected).CountAsync();
                double pagesQuantity = Math.Ceiling(count / size);
                var pagination = new PaginationModel() { Page = page, QuantityPerPage = size };
                return Maybe.Ok(new Tuple<IEnumerable<M>, double>(await queryable.Where(nameExpected).Paginate(pagination).Select(d => d.ToModel<M>(mapper)).ToListAsync(), pagesQuantity));
            }
            catch (Exception ex)
            {
                return Maybe.Fail<Tuple<IEnumerable<M>, double>> ("Update failed, detail:" + ex.Message);
            }
        }
    }
}
