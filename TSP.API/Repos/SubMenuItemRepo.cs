using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSP.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TSP.API.Repos
{
    public class SubMenuItemRepo : BaseRepo<SubMenuItemRepo>
    {
        public SubMenuItemRepo(AppDbContext _dBContext, IMapper _mapper, ILogger<SubMenuItemRepo> logger) : base(_dBContext, _mapper,logger)
        {
        }
        public virtual async Task<Maybe<List<M>>> GetAllAsync< M>(int subSystemId) where M : BaseModel
        {
            try
            {
                return Maybe.Ok(await dBContext.Set<SubMenuItem>()
                .Where(d => d.SubSystemId == subSystemId)
                .OrderBy(d => d.Order)
                .Select(d => d.ToModel<M>(mapper))
                .ToListAsync());
            }
            catch (Exception ex)
            {
                return Maybe.Fail<List<M>>(ex.Message, ex);
            }
        }
    }
}
