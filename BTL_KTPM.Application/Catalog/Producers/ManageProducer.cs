using BTL_KTPM.Application.Catalog.Common;
using BTL_KTPM.Application.Catalog.Producers.Dtos;
using BTL_KTPM.Data.EF;
using BTL_KTPM.Data.entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_KTPM.Application.Catalog.Producers
{
    public class ManageProducer : IManageProducer
    {
        private readonly BTL_KTPMDbContext _context;
        public ManageProducer(BTL_KTPMDbContext context)
        {
            _context = context;
        }
        public async Task<int> Create(CreateProducerRequest request)
        {
            var producer = new Producer()
            {
                ProducerName = request.ProducerName,
                Description = request.Description
            };
            _context.Producers.Add(producer);
            await _context.SaveChangesAsync();
            return producer.ProducerId;
        }

        public async Task<int> Delete(int ProducerId)
        {
            var producer = await _context.Producers.Where(x => x.ProducerId == ProducerId).FirstOrDefaultAsync();
            if (producer == null) throw new BTL_KTPMException("Can not find product");
            _context.Producers.Remove(producer);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<ProducerViewModels>> GetAllProducer()
        {
            var data = await _context.Producers.Select(x => new ProducerViewModels()
            {
                Id = x.ProducerId,
                ProducerName = x.ProducerName,
                Description = x.Description
            }).ToListAsync();
            return data;
        }

        public async Task<PageResult<ProducerViewModels>> GetAlllPaging(GetProducerRequest request)
        {
            var query = from p in _context.Producers
                        select p;
            if (!string.IsNullOrEmpty(request.keyword))
            {
                query = query.Where(x => x.ProducerName.Contains(request.keyword));
            }
            var totalRow = query.Count();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).Select(x => new ProducerViewModels()
            {
                Id = x.ProducerId,
                ProducerName = x.ProducerName,
                Description = x.Description
            }).ToListAsync();
            var pageResult = new PageResult<ProducerViewModels>
            {
                TotalRecords = totalRow,
                Items = data,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
            };
            return pageResult;
        }

        public async Task<ProducerViewModels> GetById(int ProducerId)
        {
            var producer = await _context.Producers.Where(x => x.ProducerId == ProducerId).FirstOrDefaultAsync();

            if (producer != null)
            {
                var categoryViewModels = new ProducerViewModels()
                {
                    Id = producer.ProducerId,
                    ProducerName = producer.ProducerName,
                    Description = producer.Description
                };
                return categoryViewModels;
            }
            else
            {
                return null;
            }
        }

        public async Task<int> Update(UpdateProducerRequest request)
        {
            var producer = await _context.Producers.Where(x => x.ProducerId == request.id).FirstOrDefaultAsync();
            if (producer == null) throw new BTL_KTPMException("Can not find producer");
            producer.ProducerName = request.ProducerName;
            producer.Description = request.Description;
            _context.Producers.Update(producer);
            return await _context.SaveChangesAsync();
        }
    }
}
