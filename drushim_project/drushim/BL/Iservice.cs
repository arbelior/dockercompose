using drushim.Models.Request;
using drushim.Models.Response;

namespace drushim.BL
{
    public interface Iservice
    {
        Task<BaseResponse<List<ResOrderModel>>> GetOrderList(BaseRequest req);

        Task<BaseResponse<List<ResAdvertisingDestinationsModel>>> GetAdvertisingDestinations(BaseRequest req);

        Task<BaseResponse<List<ResCitiesListModel>>>GetCitiesList(BaseRequest req);

        Task<BaseResponse<List<ResJobScopeModel>>> GetJobScopes(BaseRequest req);

        Task<BaseResponse<List<ResBranchModel>>> GetBranches(BaseRequest req);

        Task<BaseResponse<List<ResCategoreyMoedl>>> GetCategories(BaseRequest req);

        Task<BaseResponse<List<ResAreaModel>>> GetArea(BaseRequest req);

        Task<BaseResponse<List<ResProfessionModel>>> GetProfession(BaseRequest req);

        Task<BaseResponse<List<ResProfessionModel>>> GetActiveProffesion(BaseRequest req);
        Task<BaseResponse<List<ResSubProfessionModel>>> GetSubProfession(ReqSubProfessionModel req);

        Task<BaseResponse<List<ResOrdersDetailsModel>>> GetOrdersDetails(ReqOrderModel req);

        Task<BaseResponse<List<ResOrdersDetailsModel>>> GetActiveJobs(ReqOrderModel req);

        Task<BaseResponse<List<ResAreaModel>>> GetActiveaAreas(BaseRequest req);

        Task<BaseResponse<List<JobModel>>> ActiveJobs(ReqOrderModel req);

        Task<BaseResponse<HomeResponse>> GetHomePageData(string Token);
    }

}
