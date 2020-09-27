using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSP.Shared;
using Microsoft.EntityFrameworkCore;

namespace TSP.EF.Repos
{
    public class SubSystemRepo:BaseRepo
    {
        public SubSystemRepo(AppDbContext _dBContext, IMapper _mapper) : base(_dBContext, _mapper)
        {
        }
        public virtual async Task<IEnumerable<M>> GetAll<M>(int subSystemId) where M : BaseModel
        {
            var temp= await dBContext.Set<SubSystem>()
                .Include(d => d.SubMenuItems)
                .ThenInclude(d => d.SubItemDetails)
                .Select(d => d.ToModel<M>(mapper))
                .ToListAsync();
            return temp;
        }
    }
}
