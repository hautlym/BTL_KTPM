using BTL_KTPM.Application.Catalog.Common;
using BTL_KTPM.Application.Catalog.Products.Dtos;
using BTL_KTPM.Application.Catalog.System.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace BTL_KTPM.ApiIntegration.Service.ProductApiClient
{
    public class ProductApiClient : IProductApiClient
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProductApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> CreateProduct(ProductCreateRequest request)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClient.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var requestContent = new MultipartFormDataContent();

            if (request.ThumbnailImage != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.ThumbnailImage.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.ThumbnailImage.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "ThumbnailImage", request.ThumbnailImage.FileName);
            }

            requestContent.Add(new StringContent(request.ProductName.ToString()), "ProductName");
            requestContent.Add(new StringContent(request.ProductTitle.ToString()), "ProductTitle");
            requestContent.Add(new StringContent(request.ProductOriginalPrice.ToString()), "ProductOriginalPrice");
            requestContent.Add(new StringContent(request.ProductDescription.ToString()), "ProductDescription");
            requestContent.Add(new StringContent(request.IsNewProduct.ToString()), "IsNewProduct");
            requestContent.Add(new StringContent(request.Discount.ToString()), "Discount");
            requestContent.Add(new StringContent(request.ProducerId.ToString()), "ProducerId");
            requestContent.Add(new StringContent(request.CategoryId.ToString()), "CategoryId");

            var response = await client.PostAsync($"/api/Product", requestContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(int id)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClient.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.DeleteAsync($"/api/Product/{id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }

        public async Task<List<ProductViewModel>> GetAll()
        {
            var client = _httpClient.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.GetAsync($"api/Product");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var myDeserializedObjList = (List<ProductViewModel>)JsonConvert.DeserializeObject(body, typeof(List<ProductViewModel>));
                return (myDeserializedObjList);
            }
            throw new Exception(body);
        }

        public async Task<ProductViewModel> GetById(int id)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClient.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.GetAsync($"/api/Product/{id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ProductViewModel>(body);

            return JsonConvert.DeserializeObject<ProductViewModel>(body);
        }

        public async Task<ApiResult<PageResult<ProductViewModel>>> GetPaging(GetProductPagingRequest request)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClient.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.GetAsync($"/api/Product/paging?keyword={request.keyword}&PageIndex={request.PageIndex}&PageSize={request.PageSize}&CategoryId={request.CategoryId}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var product = JsonConvert.DeserializeObject<ApiSuccessResult<PageResult<ProductViewModel>>>(body);

                return product;
            }
            return null;
        }

        public async Task<bool> UpdateProduct(int id, ProductUpdateRequest request)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClient.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var requestContent = new MultipartFormDataContent();

            if (request.ThumbnailImage != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.ThumbnailImage.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.ThumbnailImage.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "ThumbnailImage", request.ThumbnailImage.FileName);
            }
            requestContent.Add(new StringContent(request.Id.ToString()), "Id");
            requestContent.Add(new StringContent(request.ProductName.ToString()), "ProductName");
            requestContent.Add(new StringContent(request.ProductTitle.ToString()), "ProductTitle");
            requestContent.Add(new StringContent(request.ProductOriginalPrice.ToString()), "ProductOriginalPrice");
            requestContent.Add(new StringContent(request.ProductDescription.ToString()), "ProductDescription");
            requestContent.Add(new StringContent(request.IsNewProduct.ToString()), "IsNewProduct");
            requestContent.Add(new StringContent(request.Discount.ToString()), "Discount");
            requestContent.Add(new StringContent(request.ProducerId.ToString()), "ProducerId");
            requestContent.Add(new StringContent(request.CategoryId.ToString()), "CategoryId");

            var response = await client.PutAsync($"/api/Product", requestContent);
            return response.IsSuccessStatusCode;
        }
    }
}
