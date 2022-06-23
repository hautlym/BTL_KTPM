using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_KTPM.Application.Catalog.System.Dtos
{
    public class ApiSuccessResult<T>:ApiResult<T>
    {
        public string[] ValidationErrors { get; set; }

        public ApiSuccessResult(T resultObj)
        {
            IsSuccessed = true;
            ResultObj = resultObj;
        }

        public ApiSuccessResult()
        {
            IsSuccessed = true;
        }
    }
}
