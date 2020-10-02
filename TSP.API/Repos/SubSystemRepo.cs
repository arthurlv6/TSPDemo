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
    public class SubSystemRepo:BaseRepo<SubSystemRepo>
    {
        public SubSystemRepo(AppDbContext _dBContext, IMapper _mapper, ILogger<SubSystemRepo> logger) : base(_dBContext, _mapper,logger)
        {
        }
        public virtual async Task<Maybe<List<M>>> GetAll<M>(int subSystemId) where M : BaseModel
        {
            try
            {
                return (await dBContext.Set<SubSystem>()
                        .Include(d => d.SubMenuItems)
                        .ThenInclude(d => d.SubItemDetails)
                        .Select(d => d.ToModel<M>(mapper))
                        .ToListAsync())
                        .Action(d => logger.LogInformation("SubSystemRepo GetAll called"))
                        .Map(d => Maybe.Ok(d));
            }
            catch (Exception ex)
            {
                return ex.Action(d => logger.LogError("Search subsystem data failed, detail:" + d.Message))
                    .Map(d => Maybe.Fail<List<M>>(d.Message));
            }
        }
    }
}
