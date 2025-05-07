using drushim.Appsettings;
using drushim.BHelpers;
using drushim.BL;
using drushim.Logger;
using drushim.Models.AiModels.Response;
using drushim.Models.AiModels.Reuqest;
using drushim.Models.Request;
using drushim.Models.Response;
using drushim.Services;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;

namespace drushim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrerController : ControllerBase
    {

        private readonly ServiceFacade _logic;
        private readonly ISearchCityServer _searchCityServer;
        private readonly Ilogger logger;
        public IConfiguration iconfig;
        public AppSettingsConfig appSettingsConfig;
        string facade = "carrerController";
        private string logFilePath = BLHelper.Createfiledirectorey();


        public CarrerController(ServiceFacade logic,Ilogger _logger, IConfiguration _iconfig , ISearchCityServer searchcityserver)
        {
            _logic = logic ?? throw new ArgumentNullException(nameof(logic), "ServiceFacade is null.");
            logger = _logger ?? throw new ArgumentNullException(nameof(logger), "logger is null.");
            iconfig = _iconfig ?? throw new ArgumentNullException(nameof(iconfig), "iconfig is null.");
            appSettingsConfig = new AppSettingsConfig(iconfig);
            _searchCityServer = searchcityserver ?? throw new ArgumentNullException(nameof(searchcityserver), "searchcityserver is null.");
        }


        [HttpPost]
        [Route("/Orderlist")]
        [ResponseCache(CacheProfileName = "Cachedefault")] 
        public async Task<ActionResult<BaseResponse<List<ResOrderModel>>>> GetOrderList()
        {
            try
            {
                BaseRequest req = new BaseRequest();
                var jwtSettings = iconfig.GetSection("token").Value.ToString();

                if (string.IsNullOrEmpty(jwtSettings))
                    throw new ArgumentNullException("token", "token is null.");

                req.token = jwtSettings;

                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP);
                
                logger.LogInfo("Start Controller Method : httpGet / Orderlist ", facade, BLHelper.GetCallingFunctionName(), appSettingsConfig.GetAppId());
                var response = await _logic.GetOrderList(req);

                if (appSettingsConfig.IsLoggingToFileEnabled())
                   BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRIT_LOG_FILE_PROCESS_STATUS, BLHelper.GetStatusModeMessege(response.IsSuccessfull));

                    return response.IsSuccessfull
                    ? Ok(response)
                    : BadRequest(response);

            }
            catch (Exception ex)
            {
                if (appSettingsConfig.IsLoggingToFileEnabled())
                     BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP,ex.Message);
                
                logger.LogException(ex.Message,facade, BLHelper.GetCallingFunctionName(), ex, appSettingsConfig.GetAppId());
                return StatusCode(StatusCodes.Status500InternalServerError, ErrorHelpers.GetExecption(ex));
            }
        }


        
        [HttpPost]
        [Route("/AdvertisingDestenation")]
        [ResponseCache(CacheProfileName = "Cachedefault")]
        public async Task<ActionResult<BaseResponse<List<ResAdvertisingDestinationsModel>>>>GetAdvertisingDestenation()
        {
            try
            {
                BaseRequest req = new BaseRequest();
                var jwtSettings = iconfig.GetSection("token").Value.ToString();

                if (string.IsNullOrEmpty(jwtSettings))
                    throw new ArgumentNullException("token", "token is null.");

                req.token = jwtSettings;

                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP);

                logger.LogInfo("Start Controller Method : httpGet / AdvertisingDestenation ", facade, BLHelper.GetCallingFunctionName(), appSettingsConfig.GetAppId());
                var response = await _logic.GetAdvertisingDestinations(req);

                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRIT_LOG_FILE_PROCESS_STATUS, BLHelper.GetStatusModeMessege(response.IsSuccessfull));

                return response.IsSuccessfull
                ? Ok(response)
                : BadRequest(response);

            }
            catch (Exception ex)
            {
                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP, ex.Message);

                logger.LogException(ex.Message, facade, BLHelper.GetCallingFunctionName(), ex, appSettingsConfig.GetAppId());
                return StatusCode(StatusCodes.Status500InternalServerError, ErrorHelpers.GetExecption(ex));
            }
        }

        [HttpPost]
        [Route("/GetCitiesList")]
        [ResponseCache(CacheProfileName = "Cachedefault")]
        public async Task<ActionResult<BaseResponse<List<ResCitiesListModel>>>> GetCitiesList()
        {
            try
            {
                BaseRequest req = new BaseRequest();
                var jwtSettings = iconfig.GetSection("token").Value.ToString();

                if (string.IsNullOrEmpty(jwtSettings))
                    throw new ArgumentNullException("token", "token is null.");

                req.token = jwtSettings;

                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP);

                logger.LogInfo("Start Controller Method : httpGet / GetCitiesList ", facade, BLHelper.GetCallingFunctionName(), appSettingsConfig.GetAppId());
                var response = await _logic.GetCitiesList(req);

                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRIT_LOG_FILE_PROCESS_STATUS, BLHelper.GetStatusModeMessege(response.IsSuccessfull));

                return response.IsSuccessfull
                ? Ok(response)
                : BadRequest(response);

            }
            catch (Exception ex)
            {
                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP, ex.Message);

                logger.LogException(ex.Message, facade, BLHelper.GetCallingFunctionName(), ex, appSettingsConfig.GetAppId());
                return StatusCode(StatusCodes.Status500InternalServerError, ErrorHelpers.GetExecption(ex));
            }
        }


        [HttpPost]
        [Route("/GetJobScopes")]
        [ResponseCache(CacheProfileName = "Cachedefault")]
        public async Task<ActionResult<BaseResponse<List<ResJobScopeModel>>>> GetJobScopes()
        {
            try
            {
                BaseRequest req = new BaseRequest();
                var jwtSettings = iconfig.GetSection("token").Value.ToString();

                if (string.IsNullOrEmpty(jwtSettings))
                    throw new ArgumentNullException("token", "token is null.");

                req.token = jwtSettings;

                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP);

                logger.LogInfo("Start Controller Method : httpGet / GetJobScopes ", facade, BLHelper.GetCallingFunctionName(), appSettingsConfig.GetAppId());
                var response = await _logic.GetJobScopes(req);

                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRIT_LOG_FILE_PROCESS_STATUS, BLHelper.GetStatusModeMessege(response.IsSuccessfull));

                return response.IsSuccessfull
                ? Ok(response)
                : BadRequest(response);

            }
            catch (Exception ex)
            {
                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP, ex.Message);

                logger.LogException(ex.Message, facade, BLHelper.GetCallingFunctionName(), ex, appSettingsConfig.GetAppId());
                return StatusCode(StatusCodes.Status500InternalServerError, ErrorHelpers.GetExecption(ex));
            }
        }


        [HttpPost]
        [Route("/GetBranches")]
        [ResponseCache(CacheProfileName = "Cachedefault")]
        public async Task<ActionResult<BaseResponse<List<ResBranchModel>>>> GetBranches()
        {
            try
            {
                BaseRequest req = new BaseRequest();
                var jwtSettings = iconfig.GetSection("token").Value.ToString();

                if (string.IsNullOrEmpty(jwtSettings))
                    throw new ArgumentNullException("token", "token is null.");

                req.token = jwtSettings;

                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP);

                logger.LogInfo("Start Controller Method : httpGet / GetBranches ", facade, BLHelper.GetCallingFunctionName(), appSettingsConfig.GetAppId());
                var response = await _logic.GetBranches(req);

                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRIT_LOG_FILE_PROCESS_STATUS, BLHelper.GetStatusModeMessege(response.IsSuccessfull));

                return response.IsSuccessfull
                ? Ok(response)
                : BadRequest(response);

            }
            catch (Exception ex)
            {
                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP, ex.Message);

                logger.LogException(ex.Message, facade, BLHelper.GetCallingFunctionName(), ex, appSettingsConfig.GetAppId());
                return StatusCode(StatusCodes.Status500InternalServerError, ErrorHelpers.GetExecption(ex));
            }
        }

        [HttpPost]
        [Route("/GetCategories")]
        [ResponseCache(CacheProfileName = "Cachedefault")]
        public async Task<ActionResult<BaseResponse<List<ResCategoreyMoedl>>>> GetCategories()
        {
            try
            {
                BaseRequest req = new BaseRequest();
                var jwtSettings = iconfig.GetSection("token").Value.ToString();

                if (string.IsNullOrEmpty(jwtSettings))
                    throw new ArgumentNullException("token", "token is null.");

                req.token = jwtSettings;

                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP);

                logger.LogInfo("Start Controller Method : httpGet / GetCategories ", facade, BLHelper.GetCallingFunctionName(), appSettingsConfig.GetAppId());
                var response = await _logic.GetCategories(req);

                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRIT_LOG_FILE_PROCESS_STATUS, BLHelper.GetStatusModeMessege(response.IsSuccessfull));

                return response.IsSuccessfull
                ? Ok(response)
                : BadRequest(response);

            }
            catch (Exception ex)
            {
                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP, ex.Message);

                logger.LogException(ex.Message, facade, BLHelper.GetCallingFunctionName(), ex, appSettingsConfig.GetAppId());
                return StatusCode(StatusCodes.Status500InternalServerError, ErrorHelpers.GetExecption(ex));
            }
        }


        [HttpPost]
        [Route("/GetArea")]
        [ResponseCache(CacheProfileName = "Cachedefault")]
        public async Task<ActionResult<BaseResponse<List<ResAreaModel>>>> GetArea()
        {
            try
            {
                BaseRequest req = new BaseRequest();
                var jwtSettings = iconfig.GetSection("token").Value.ToString();

                if (string.IsNullOrEmpty(jwtSettings))
                    throw new ArgumentNullException("token", "token is null.");

                req.token = jwtSettings;

                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP);

                logger.LogInfo("Start Controller Method : httpGet / GetArea ", facade, BLHelper.GetCallingFunctionName(), appSettingsConfig.GetAppId());
                var response = await _logic.GetArea(req);

                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRIT_LOG_FILE_PROCESS_STATUS, BLHelper.GetStatusModeMessege(response.IsSuccessfull));

                return response.IsSuccessfull
                ? Ok(response)
                : BadRequest(response);

            }
            catch (Exception ex)
            {
                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP, ex.Message);

                logger.LogException(ex.Message, facade, BLHelper.GetCallingFunctionName(), ex, appSettingsConfig.GetAppId());
                return StatusCode(StatusCodes.Status500InternalServerError, ErrorHelpers.GetExecption(ex));
            }
        }



        [HttpPost]
        [Route("/ActiveAreas")]
        [ResponseCache(CacheProfileName = "Cachedefault")]
        public async Task<ActionResult<BaseResponse<List<ResAreaModel>>>> GetActiveAreas()
        {
            List<ResAreaModel> all_list = new List<ResAreaModel>();

            try
            {
                BaseRequest req = new BaseRequest();
                var jwtSettings = iconfig.GetSection("token").Value.ToString();

                if (string.IsNullOrEmpty(jwtSettings))
                    throw new ArgumentNullException("token", "token is null.");

                req.token = jwtSettings;

                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP);

                logger.LogInfo("Start Controller Method : httpGet / GetActiveAreas ", facade, BLHelper.GetCallingFunctionName(), appSettingsConfig.GetAppId());
                var response = await _logic.GetActiveaAreas(req);

                var GetActiveJobsPerProfession = await _logic.GetActiveProffesion(req);

                if (response.Data != null)
                {

                    foreach (var area in response.Data)
                    {
                        if (area != null && area.AreaName != null && area.AreaId > 0)
                        {
                            if (GetActiveJobsPerProfession.Data.Any(x => x.areaid.Equals(area.AreaId)))
                            {
                                ResAreaModel obj = new ResAreaModel()
                                {
                                    AreaId = area.AreaId,
                                    AreaName = area.AreaName
                                };

                                if (!all_list.Contains(obj))
                                {
                                    all_list.Add(obj);
                                }
                            }
                        }
                    }
                }

                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRIT_LOG_FILE_PROCESS_STATUS, BLHelper.GetStatusModeMessege(response.IsSuccessfull));

                return response.IsSuccessfull
                ? Ok(all_list.Count() > 0 ? all_list : response)
                : BadRequest(response);

            }
            catch (Exception ex)
            {
                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP, ex.Message);

                logger.LogException(ex.Message, facade, BLHelper.GetCallingFunctionName(), ex, appSettingsConfig.GetAppId());
                return StatusCode(StatusCodes.Status500InternalServerError, ErrorHelpers.GetExecption(ex));
            }
        }


        [HttpPost]
        [Route("/GetProfession")]
        [ResponseCache(CacheProfileName = "Cachedefault")]
        public async Task<ActionResult<BaseResponse<List<ResProfessionModel>>>> GetProfession()
        {
            try
            {

                BaseRequest req = new BaseRequest();
                var jwtSettings = iconfig.GetSection("token").Value.ToString();

                if (string.IsNullOrEmpty(jwtSettings))
                    throw new ArgumentNullException("token", "token is null.");

                req.token = jwtSettings;

                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP);

                logger.LogInfo("Start Controller Method : httpGet / GetProfession ", facade, BLHelper.GetCallingFunctionName(), appSettingsConfig.GetAppId());
                var response = await _logic.GetProfession(req);

                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRIT_LOG_FILE_PROCESS_STATUS, BLHelper.GetStatusModeMessege(response.IsSuccessfull));

                return response.IsSuccessfull
                ? Ok(response)
                : BadRequest(response);

            }
            catch (Exception ex)
            {
                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP, ex.Message);

                logger.LogException(ex.Message, facade, BLHelper.GetCallingFunctionName(), ex, appSettingsConfig.GetAppId());
                return StatusCode(StatusCodes.Status500InternalServerError, ErrorHelpers.GetExecption(ex));
            }
        }


        [HttpGet]
        [Route("/GetActiveProfession")]
        [ResponseCache(CacheProfileName = "Cachedefault")]
        public async Task<ActionResult<BaseResponse<List<ResProfessionModel>>>> GetActiveProfession()
        {
            try
            {
                List<ResProfessionModel> all_list = new List<ResProfessionModel>();

                BaseRequest req = new BaseRequest();
                var jwtSettings = iconfig.GetSection("token").Value.ToString();

                if (string.IsNullOrEmpty(jwtSettings))
                    throw new ArgumentNullException("token", "token is null.");

                req.token = jwtSettings;

                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP);

                logger.LogInfo("Start Controller Method : httpGet / GetProfession ", facade, BLHelper.GetCallingFunctionName(), appSettingsConfig.GetAppId());
                var response = await _logic.GetActiveProffesion(req);

                ReqOrderModel request = new ReqOrderModel();
                request.token = jwtSettings;
                request.OrderNo = string.Empty;

                var responseactivejobs = await _logic.GetActiveJobs(request);

                foreach (var job in responseactivejobs.Data)
                {
                    if(job != null && job.OrderDefArea1 != null && job.OrderDefArea1  > 0)
                    {
                        if(response.Data.Any(x => x.ProfessionID.Equals(job.OrderDefProf1)))
                        {
                            ResProfessionModel obj = new ResProfessionModel()
                            {
                                ProfessionID = job.OrderDefProf1,
                                ProfessionName = job.ProfessionName,
                                areaid = job.OrderDefArea1
                            };

                            if(!all_list.Any(x => x.ProfessionID.Equals(obj.ProfessionID)))
                            {
                                all_list.Add(obj);
                            }
                        }
                    }
                }


                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRIT_LOG_FILE_PROCESS_STATUS, BLHelper.GetStatusModeMessege(response.IsSuccessfull));

                return response.IsSuccessfull
                ? Ok(all_list)
                : BadRequest(response);

            }
            catch (Exception ex)
            {
                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP, ex.Message);

                logger.LogException(ex.Message, facade, BLHelper.GetCallingFunctionName(), ex, appSettingsConfig.GetAppId());
                return StatusCode(StatusCodes.Status500InternalServerError, ErrorHelpers.GetExecption(ex));
            }
        }



        [HttpPost]
        [Route("/GetSubProfession")]
        [ResponseCache(CacheProfileName = "Cachedefault")]
        public async Task<ActionResult<BaseResponse<List<ResSubProfessionModel>>>> GetSubProfession([FromBody] ReqSubProfession request)
        {
            try
            {
                if (request.ProfessionID == null)
                    throw new ArgumentNullException("ProfessionID", "ProfessionID is null.");

                ReqSubProfessionModel req = new ReqSubProfessionModel();

                var jwtSettings = iconfig.GetSection("token").Value.ToString();

                if (string.IsNullOrEmpty(jwtSettings))
                    throw new ArgumentNullException("token", "token is null.");

                req.token = jwtSettings;
                req.ProfessionID = request.ProfessionID;

                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP);

                logger.LogInfo("Start Controller Method : httpGet / GetSubProfession ", facade, BLHelper.GetCallingFunctionName(), appSettingsConfig.GetAppId());
                var response = await _logic.GetSubProfession(req);

                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRIT_LOG_FILE_PROCESS_STATUS, BLHelper.GetStatusModeMessege(response.IsSuccessfull));

                return response.IsSuccessfull
                ? Ok(response)
                : BadRequest(response);

            }
            catch (Exception ex)
            {
                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP, ex.Message);

                logger.LogException(ex.Message, facade, BLHelper.GetCallingFunctionName(), ex, appSettingsConfig.GetAppId());
                return StatusCode(StatusCodes.Status500InternalServerError, ErrorHelpers.GetExecption(ex));
            }
        }

        [HttpPost]
        [Route("/GetOrdersDetails")]
        [ResponseCache(CacheProfileName = "Cachedefault")]
        public async Task<ActionResult<BaseResponse<List<ResOrdersDetailsModel>>>> GetOrdersDetails([FromBody] ReqOrder request)
        {
            try
            {

                ReqOrderModel req = new ReqOrderModel();

                var jwtSettings = iconfig.GetSection("token").Value.ToString();

                if (string.IsNullOrEmpty(jwtSettings))
                    throw new ArgumentNullException("token", "token is null.");

                req.token = jwtSettings;

                req.OrderNo = request.OrderNo ?? "";


                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP);

                logger.LogInfo("Start Controller Method : httpGet / GetOrdersDetails ", facade, BLHelper.GetCallingFunctionName(), appSettingsConfig.GetAppId());
                var response = await _logic.GetOrdersDetails(req);

                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRIT_LOG_FILE_PROCESS_STATUS, BLHelper.GetStatusModeMessege(response.IsSuccessfull));

                return response.IsSuccessfull
                ? Ok(response)
                : BadRequest(response);

            }
            catch (Exception ex)
            {
                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP, ex.Message);

                logger.LogException(ex.Message, facade, BLHelper.GetCallingFunctionName(), ex, appSettingsConfig.GetAppId());
                return StatusCode(StatusCodes.Status500InternalServerError, ErrorHelpers.GetExecption(ex));
            }
        }


        [HttpGet]
        [Route("GetActiveJobs")]
        [ResponseCache(CacheProfileName = "Cachedefault")]
        public async Task<ActionResult<BaseResponse<List<ResOrdersDetailsModel>>>> GetActiveJobs()
        {
            try
            {

                ReqOrder request;

                ReqOrderModel req = new ReqOrderModel();

                var jwtSettings = iconfig.GetSection("token").Value.ToString();

                if (string.IsNullOrEmpty(jwtSettings))
                    throw new ArgumentNullException("token", "token is null.");

                req.token = jwtSettings;

                req.OrderNo = string.Empty;


                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP);

                logger.LogInfo("Start Controller Method : httpGet / GetActiveJobs ", facade, BLHelper.GetCallingFunctionName(), appSettingsConfig.GetAppId());
                var response = await _logic.GetActiveJobs(req);

                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRIT_LOG_FILE_PROCESS_STATUS, BLHelper.GetStatusModeMessege(response.IsSuccessfull));

                return response.IsSuccessfull
                ? Ok(response)
                : BadRequest(response);

            }
            catch (Exception ex)
            {
                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP, ex.Message);

                logger.LogException(ex.Message, facade, BLHelper.GetCallingFunctionName(), ex, appSettingsConfig.GetAppId());
                return StatusCode(StatusCodes.Status500InternalServerError, ErrorHelpers.GetExecption(ex));
            }
        }


        [HttpGet]
        [Route("/ActiveJobs")]
        [ResponseCache(CacheProfileName = "Cachedefault")]
        public async Task<ActionResult<BaseResponse<List<ResOrdersDetailsModel>>>> ActiveJobs()
        {
            try
            {

                ReqOrder request;

                ReqOrderModel req = new ReqOrderModel();

                var jwtSettings = iconfig.GetSection("token").Value.ToString();

                if (string.IsNullOrEmpty(jwtSettings))
                    throw new ArgumentNullException("token", "token is null.");

                req.token = jwtSettings;

                req.OrderNo =  string.Empty;


                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP);

                logger.LogInfo("Start Controller Method : httpGet / GetActiveJobs ", facade, BLHelper.GetCallingFunctionName(), appSettingsConfig.GetAppId());
                //var response = await _logic.ActiveJobs(req);
                var response = await _logic.GetActiveJobs(req);
                
                var allorderids = new List<int>();

                //allorderids = response.Data.Where(x => x.OrderId != null && x.OrderId > 0 && !string.IsNullOrEmpty(x.Description)).Select(x => x.OrderId).ToList();


                List<JobModel> JobNames = response.Data
                  .Where(x => !string.IsNullOrEmpty(x.ProfessionName) && x.ProfessionId != null)
                  .Select(x => new JobModel
                  {
                      JobId = x.ProfessionId,
                      JobName = x.Description,
                      OrderId = x.OrderId,
                  })
                  .ToList();

                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRIT_LOG_FILE_PROCESS_STATUS, BLHelper.GetStatusModeMessege(response.IsSuccessfull));

                return response.IsSuccessfull
                ? Ok(JobNames.Count() > 0 ? JobNames : response.Data)
                : BadRequest(response);

            }
            catch (Exception ex)
            {
                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP, ex.Message);

                logger.LogException(ex.Message, facade, BLHelper.GetCallingFunctionName(), ex, appSettingsConfig.GetAppId());
                return StatusCode(StatusCodes.Status500InternalServerError, ErrorHelpers.GetExecption(ex));
            }
        }

     


        //[HttpPost]
        //[Route("/GetJobsPerAreas")]
        //public async Task<ActionResult<BaseResponse<List<ResOrdersDetailsModel>>>> GetJobsPerAreas(List<int> areasid)
        //{

        //    List<ResOrdersDetailsModel> JobNames = new List<ResOrdersDetailsModel>();

        //    try
        //    {
        //        //if (areas == null || areas.Count == 0)
        //        //    throw new ArgumentNullException("areas", "areas is null.");

        //        var jobs = await GetActiveJobs(); // ✅ קריאה פנימית

        //        if (jobs.Result is OkObjectResult okResult)
        //        {
        //            var jobList = okResult.Value as IEnumerable<dynamic>; // ← מניח שזה dynamic
        //            if (jobList != null)
        //            {
        //                foreach (var job in jobList) 
        //                { 
        //                    if(!JobNames.Contains(job))
        //                    JobNames.Add(job);
        //                }
      

        //            }


        //        }


                





        //    }
        //    catch (Exception ex)
        //    {
        //        if (appSettingsConfig.IsLoggingToFileEnabled())
        //            BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP, ex.Message);

        //        logger.LogException(ex.Message, facade, BLHelper.GetCallingFunctionName(), ex, appSettingsConfig.GetAppId());
        //        return StatusCode(StatusCodes.Status500InternalServerError, ErrorHelpers.GetExecption(ex));
        //    }

        //    return Ok(JobNames);
        //}



        [HttpPost]
        [Route("/ActiveJobDescription")]
        [ResponseCache(CacheProfileName = "Cachedefault")]
        public async Task<ActionResult<BaseResponse<List<ResOrdersDetailsModel>>>> ActiveJobDescription([FromBody] ReqOrder request)
        {
            try
            {

                ReqOrderModel req = new ReqOrderModel();

                var jwtSettings = iconfig.GetSection("token").Value.ToString();

                if (string.IsNullOrEmpty(jwtSettings))
                    throw new ArgumentNullException("token", "token is null.");

                req.token = jwtSettings;

                req.OrderNo = request.OrderNo ?? "";


                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP);

                logger.LogInfo("Start Controller Method : httpGet / GetActiveJobs ", facade, BLHelper.GetCallingFunctionName(), appSettingsConfig.GetAppId());
                //var response = await _logic.ActiveJobs(req);
                var response = await _logic.GetActiveJobs(req);


                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRIT_LOG_FILE_PROCESS_STATUS, BLHelper.GetStatusModeMessege(response.IsSuccessfull));

                return response.IsSuccessfull
                ? Ok(response)
                : BadRequest(response);

            }
            catch (Exception ex)
            {
                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP, ex.Message);

                logger.LogException(ex.Message, facade, BLHelper.GetCallingFunctionName(), ex, appSettingsConfig.GetAppId());
                return StatusCode(StatusCodes.Status500InternalServerError, ErrorHelpers.GetExecption(ex));
            }
        }





        [HttpGet]
        [Route("/HomePage")]
        [ResponseCache(CacheProfileName = "Cachedefault")]
        public async Task<ActionResult<BaseResponse<HomeResponse>>> HomePageData()
        {
            try
            {

                string req = string.Empty;

                var jwtSettings = iconfig.GetSection("token").Value.ToString();

                if (string.IsNullOrEmpty(jwtSettings))
                    throw new ArgumentNullException("token", "token is null.");

                req = jwtSettings;
    
                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP);

                var response = await _logic.GetHomePageData(req);

                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRIT_LOG_FILE_PROCESS_STATUS, BLHelper.GetStatusModeMessege(response.IsSuccessfull));

                return response.IsSuccessfull
                ? Ok(response)
                : BadRequest(response);

            }
            catch (Exception ex)
            {
                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP, ex.Message);

                logger.LogException(ex.Message, facade, BLHelper.GetCallingFunctionName(), ex, appSettingsConfig.GetAppId());
                return StatusCode(StatusCodes.Status500InternalServerError, ErrorHelpers.GetExecption(ex));
            }
        }

  


    }
}
