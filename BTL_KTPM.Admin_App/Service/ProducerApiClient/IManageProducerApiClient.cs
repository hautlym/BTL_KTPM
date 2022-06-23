using BTL_KTPM.Application.Catalog.Common;
using BTL_KTPM.Application.Catalog.Producers.Dtos;
using BTL_KTPM.Application.Catalog.System.Dtos;

namespace BTL_KTPM.Admin_App.Service.ProducerApiClient
{
    public interface IManageProducerApiClient
    {
        Task<ApiResult<List<ProducerViewModels>>> GetAll();
        Task<PageResult<ProducerViewModels>> GetAllPaging(GetProducerRequest request);
        Task<ApiResult<bool>> CreateProducer(CreateProducerRequest request);
        Task<bool> UpdateProducer(int id, UpdateProducerRequest request);
        Task<ProducerViewModels> GetById(int id);
        Task<bool> Delete(int id);
    }
}
