using BTL_KTPM.Application.Catalog.Common;
using BTL_KTPM.Application.Catalog.Producers.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_KTPM.Application.Catalog.Producers
{
    public interface IManageProducer
    {
        Task<List<ProducerViewModels>> GetAllProducer();
        Task<PageResult<ProducerViewModels>> GetAlllPaging(GetProducerRequest request);
        Task<int> Create(CreateProducerRequest request);
        Task<int> Update(UpdateProducerRequest request);
        Task<int> Delete(int ProducerId);
        Task<ProducerViewModels> GetById(int ProducerId);
    }
}
