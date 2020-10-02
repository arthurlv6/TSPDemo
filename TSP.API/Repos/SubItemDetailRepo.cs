using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSP.Shared;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TSP.API.Repos
{
    public class SubItemDetailRepo : BaseRepo<SubItemDetailRepo>
    {
        public SubItemDetailRepo(AppDbContext _dBContext, IMapper _mapper, ILogger<SubItemDetailRepo> logger) : base(_dBContext, _mapper,logger)
        {
        }
        public async Task<Tuple<IEnumerable<M>, double>> GetPageData<M>(int SubMenuItemId = 0,int page = 1, int size = 20, string keyword = "") where M : BaseModel
        {
            var queryable = dBContext.Set<SubItemDetail>().Where(d=>!d.Disabled).OrderBy(d=>d.Order).AsQueryable();
            Expression<Func<SubItemDetail, bool>> nameExpected = d => true;
            if (!string.IsNullOrEmpty(keyword))
            {
                nameExpected = d => d.Title.Contains(keyword);
            }

            Expression<Func<SubItemDetail, bool>> categoryExpected = d => true;
            if (SubMenuItemId > 0)
                categoryExpected = d => d.SubMenuItemId == SubMenuItemId;

            double count = await queryable.Where(nameExpected).Where(categoryExpected).CountAsync();
            double pagesQuantity = Math.Ceiling(count / size);
            var pagination = new PaginationModel() { Page = page, QuantityPerPage = size };
            return new Tuple<IEnumerable<M>, double>(await queryable.Where(nameExpected).Where(categoryExpected).Paginate(pagination).Select(d => d.ToModel<M>(mapper)).ToListAsync(), pagesQuantity);
        }
        public async Task<Maybe<bool>> UpdateImageAsync(int id, string imagePath)
        {
            try
            {
                var entity = dBContext.Set<SubItemDetail>().First(d => d.Id == id);
                entity.Image = imagePath;
                await dBContext.SaveChangesAsync();
                return Maybe.Ok(true);
            }
            catch (Exception ex)
            {
                return Maybe.Fail<bool>(ex.Message);
            }
        }
        public Maybe<SubItemDetailModel> Add(SubItemDetailModel model)
        {
            try
            {
                return model.Map(d => mapper.Map<SubItemDetail>(d))
                    .Action(d => dBContext.SubItemDetails.Add(d))
                    .Action(async d => await dBContext.SaveChangesAsync())
                    .Map(d => Maybe.Ok(d.ToModel<SubItemDetailModel>(mapper)));
            }
            catch (Exception ex)
            {
                return Maybe.Fail<SubItemDetailModel>(ex.Message);
            }
        }
    }
}
