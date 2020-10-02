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
    public class ContactUsRepo: BaseRepo<ContactUsRepo>
    {
        public ContactUsRepo(AppDbContext _dBContext, IMapper _mapper,ILogger<ContactUsRepo> logger) : base(_dBContext, _mapper,logger)
        {
        }
        public async Task<Maybe<ContactUsModel>> AddAsync(ContactUsModel model)
        {
            try
            {
                ContactUs detail = mapper.Map<ContactUs>(model);
                var addedEntity = dBContext.ContactUs.Add(detail);
                await dBContext.SaveChangesAsync();
                return Maybe.Ok(addedEntity.Entity.ToModel<ContactUsModel>(mapper));
            }
            catch (Exception ex)
            {
                return Maybe.Fail<ContactUsModel>(ex.Message);
            }
        }
    }
}
