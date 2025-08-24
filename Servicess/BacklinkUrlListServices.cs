using AutoMapper;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeProject.Data;
using OfficeProject.Models.DTO;
using OfficeProject.Models.Entities;
using OfficeProject.Models.Enums;
using System.Security.Claims;

namespace OfficeProject.Servicess
{
    public class BacklinkUrlListServices : IBacklinkUrlListServices
    {
        private readonly IDataSerializer? DataSerializer;
        private readonly IDbContextFactory<ApplicationDbContext> dbContextFactory;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper Mapper;
        public BacklinkUrlListServices(
            IDbContextFactory<ApplicationDbContext> dbContextFactory,
            IHttpContextAccessor httpContextAccessor, IMapper mapper, IDataSerializer? dataSerializer)
        {
            this.dbContextFactory = dbContextFactory;
            this.httpContextAccessor = httpContextAccessor;
            this.Mapper = mapper;
            DataSerializer = dataSerializer;
        }

        public async Task AddBacklinkUrlListAsync(List<BacklinkUrlListDTO> dtoList)
        {
            if (dtoList == null || !dtoList.Any())
                return;

            using (var context = dbContextFactory.CreateDbContext())
            {
                //var userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                //if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                //{
                //    throw new UnauthorizedAccessException("User is not authenticated.");
                //}

                var entities = dtoList.Select(dto => Mapper.Map<BacklinkUrlList>(dto)).ToList();
                await context.BacklinkUrlList.AddRangeAsync(entities);
                await context.SaveChangesAsync();
            }
        }

        public async Task<PagedResult<BacklinkUrlListDTO>> GetBacklinks(int pageNumber = 1, int pageSize = 50, ProjectCategory? category = null)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 50;

            using var context = dbContextFactory.CreateDbContext();

            var skip = (pageNumber - 1) * pageSize;

            var query = context.BacklinkUrlList
                       .Where(b => !b.IsSuspend);

            // 👇 Apply category filter if provided
            if (category.HasValue)
            {
                query = query.Where(b => b.ProjectCategory == category.Value);
            }

            //var query = context.BacklinkUrlList
            //                   .Where(b => !b.IsSuspend);

            var total = await query.CountAsync();
            var data = await query
                                     .OrderBy(b => b.BacklinkUrlId)
                                     .Skip(skip)
                                     .Take(pageSize)
                                     .ToListAsync();

            return new PagedResult<BacklinkUrlListDTO>
            {
                Data = Mapper.Map<List<BacklinkUrlListDTO>>(data),
                Total = total,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
        public async Task<bool> SuspendBacklinkAsync(int backlinkUrlId)
        {
            using var context = dbContextFactory.CreateDbContext();

            var backlink = await context.BacklinkUrlList
                                        .FirstOrDefaultAsync(b => b.BacklinkUrlId == backlinkUrlId);

            if (backlink == null)
                return false;

            backlink.IsSuspend = true;
            await context.SaveChangesAsync();

            return true;
        }



        public async Task SaveOrUpdateBacklinkUrlListAsync(BacklinkUrlListDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            using var context = dbContextFactory.CreateDbContext();

            //var userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            //    throw new UnauthorizedAccessException("User is not authenticated.");

            var entity = await context.BacklinkUrlList
                                      .FirstOrDefaultAsync(b => b.BacklinkUrlId == dto.BacklinkUrlId);

            if (entity != null)
            {
                entity.SiteUrl = dto.SiteUrl;
                context.BacklinkUrlList.Update(entity);
            }
            else
            {
                entity = Mapper.Map<BacklinkUrlList>(dto);
                await context.BacklinkUrlList.AddAsync(entity);
            }

            await context.SaveChangesAsync();
        }

    }
}
