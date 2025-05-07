using drushim.BHelpers;
using drushim.Models.Request;
using drushim.Models.Response;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using static drushim.BHelpers.ConstantOperation;
using drushim.Logger;
using Microsoft.Extensions.Logging;
using drushim.Appsettings;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System.Collections;

namespace drushim.BL

{
    public class ServiceFacade : Iservice
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string facade;
        private create_error err;
        private readonly Ilogger logger;
        public IConfiguration iconfig;
        public AppSettingsConfig appSettingsConfig;
        private string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), ConstantOperation.logfile);
        private string reasonfailed = string.Empty;
        private readonly HttpClient httpClient;

        public ServiceFacade(IHttpClientFactory httpClientFactory, Ilogger _logger, IConfiguration _iconfig)
        {
            _httpClientFactory = httpClientFactory;
            facade = "ServiceFacade";
            logger = _logger;
            iconfig = _iconfig;
            appSettingsConfig = new AppSettingsConfig(iconfig);
            httpClient = _httpClientFactory.CreateClient("WithProxy");
        }

 
        private string GetHrefService(string href)
        {
            string reasonfailed = string.Empty;
            if(string.IsNullOrEmpty(href))
            {
                reasonfailed = GetErrorMessage(err = new create_error(facade, BLHelper.GetCallingFunctionName(), NOT_GET_HREF));

                logger.LogError(reasonfailed, facade, BLHelper.GetCallingFunctionName(), appSettingsConfig.GetAppId());
                BLHelper.GetLogError(facade, BLHelper.GetCallingFunctionName(), reasonfailed);
                throw new Exception(reasonfailed);
            }
            return appSettingsConfig.GetHref(href);
        }

        private static HttpContent CreateJsonContent<T>(T request)
        {
            string jsonRequest = JsonConvert.SerializeObject(request);
            return new StringContent(jsonRequest, Encoding.UTF8, "application/json");
        }

        #region api from Adam To drushim

        public async Task<BaseResponse<List<ResOrderModel>>> GetOrderList(BaseRequest req)
        {

            try
            {
                logger.LogInfo("Start: getorderlist", facade, BLHelper.GetCallingFunctionName(), appSettingsConfig.GetAppId());

                if (req == null || string.IsNullOrEmpty(req.token))
                {
                    reasonfailed = "Request or token is null/empty.";
                    if(appSettingsConfig.IsLoggingToFileEnabled())
                       BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_PROCESS_STEP, reasonfailed);
                    
                    logger.LogError(reasonfailed, facade, BLHelper.GetCallingFunctionName(), appSettingsConfig.GetAppId());
                    throw new ArgumentException(reasonfailed, nameof(req));
                }

                //HttpClient httpClient = _httpClientFactory.CreateClient();

                HttpContent content = CreateJsonContent(req);

                var response = await httpClient.PostAsync(GetHrefService(HrefHeaders.GetOrdersList), content);

                if (!response.IsSuccessStatusCode)
                {
                    reasonfailed = $"API call failed: {response.StatusCode}";
                    if (appSettingsConfig.IsLoggingToFileEnabled())
                         BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_PROCESS_STEP, reasonfailed);
                    
                    return BaseResponse<List<ResOrderModel>>.Fail(reasonfailed);
                }

                string responseData = await response.Content.ReadAsStringAsync();

                var orderData = System.Text.Json.JsonSerializer.Deserialize<List<ResOrderModel>>(responseData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                //var orderData = System.Text.Json.JsonSerializer.Deserialize<List<ResOrderModel>>(responseData);

                return orderData != null
                    ? BaseResponse<List<ResOrderModel>>.Success(orderData)
                    : BaseResponse<List<ResOrderModel>>.Fail("Failed to deserialize response data");
            }
            catch (Exception ex)
            {
                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP, ex.Message);
                
                logger.LogException(ex.Message, facade, BLHelper.GetCallingFunctionName(), ex, appSettingsConfig.GetAppId());
                return BaseResponse<List<ResOrderModel>>.Fail($"Exception: {ex.Message}");
            }
        }

        public async Task<BaseResponse<List<ResAdvertisingDestinationsModel>>> GetAdvertisingDestinations(BaseRequest req)
        {

            try
            {
                logger.LogInfo("Start: GetAdvertisingDestinations", facade, BLHelper.GetCallingFunctionName(), appSettingsConfig.GetAppId());

                if (req == null || string.IsNullOrEmpty(req.token))
                {
                    reasonfailed = "Request or token is null/empty.";
                    if (appSettingsConfig.IsLoggingToFileEnabled())
                        BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_PROCESS_STEP, reasonfailed);

                    logger.LogError(reasonfailed, facade, BLHelper.GetCallingFunctionName(), appSettingsConfig.GetAppId());
                    throw new ArgumentException(reasonfailed, nameof(req));
                }

                //HttpClient httpClient = _httpClientFactory.CreateClient();

                HttpContent content = CreateJsonContent(req);

                var response = await httpClient.PostAsync(GetHrefService(HrefHeaders.GetAdvertisingDestinations), content);

                if (!response.IsSuccessStatusCode)
                {
                    reasonfailed = $"API call failed: {response.StatusCode}";
                    if (appSettingsConfig.IsLoggingToFileEnabled())
                        BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_PROCESS_STEP, reasonfailed);

                    return BaseResponse<List<ResAdvertisingDestinationsModel>>.Fail(reasonfailed);
                }

                string responseData = await response.Content.ReadAsStringAsync();

                var GetAdvertiseData = System.Text.Json.JsonSerializer.Deserialize<List<ResAdvertisingDestinationsModel>>(responseData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                //var GetAdvertiseData = System.Text.Json.JsonSerializer.Deserialize<List<ResAdvertisingDestinationsModel>>(responseData);

                return GetAdvertiseData != null
                    ? BaseResponse<List<ResAdvertisingDestinationsModel>>.Success(GetAdvertiseData)
                    : BaseResponse<List<ResAdvertisingDestinationsModel>>.Fail("Failed to deserialize response data");
            }
            catch (Exception ex)
            {
                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP, ex.Message);

                logger.LogException(ex.Message, facade, BLHelper.GetCallingFunctionName(), ex, appSettingsConfig.GetAppId());
                return BaseResponse<List<ResAdvertisingDestinationsModel>>.Fail($"Exception: {ex.Message}");
            }
        }


        public async Task<BaseResponse<List<ResCitiesListModel>>>GetCitiesList(BaseRequest req)
        {

            try
            {
                logger.LogInfo("Start: GetCitiesList", facade, BLHelper.GetCallingFunctionName(), appSettingsConfig.GetAppId());

                if (req == null || string.IsNullOrEmpty(req.token))
                {
                    reasonfailed = "Request or token is null/empty.";
                    if (appSettingsConfig.IsLoggingToFileEnabled())
                        BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_PROCESS_STEP, reasonfailed);

                    logger.LogError(reasonfailed, facade, BLHelper.GetCallingFunctionName(), appSettingsConfig.GetAppId());
                    throw new ArgumentException(reasonfailed, nameof(req));
                }

                //HttpClient httpClient = _httpClientFactory.CreateClient();

                HttpContent content = CreateJsonContent(req);

                var response = await httpClient.PostAsync(GetHrefService(HrefHeaders.GetCitiesList), content);

                if (!response.IsSuccessStatusCode)
                {
                    reasonfailed = $"API call failed: {response.StatusCode}";
                    if (appSettingsConfig.IsLoggingToFileEnabled())
                        BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_PROCESS_STEP, reasonfailed);

                    return BaseResponse<List<ResCitiesListModel>>.Fail(reasonfailed);
                }

                string responseData = await response.Content.ReadAsStringAsync();

                var Getcities = System.Text.Json.JsonSerializer.Deserialize<List<ResCitiesListModel>>(responseData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });


                return Getcities != null
                    ? BaseResponse<List<ResCitiesListModel>>.Success(Getcities)
                    : BaseResponse<List<ResCitiesListModel>>.Fail("Failed to deserialize response data");
            }
            catch (Exception ex)
            {
                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP, ex.Message);

                logger.LogException(ex.Message, facade, BLHelper.GetCallingFunctionName(), ex, appSettingsConfig.GetAppId());
                return BaseResponse<List<ResCitiesListModel>>.Fail($"Exception: {ex.Message}");
            }
        }

        public async Task<BaseResponse<List<ResJobScopeModel>>> GetJobScopes(BaseRequest req)
        {

            try
            {
                logger.LogInfo("Start: GetJobScopes", facade, BLHelper.GetCallingFunctionName(), appSettingsConfig.GetAppId());

                if (req == null || string.IsNullOrEmpty(req.token))
                {
                    reasonfailed = "Request or token is null/empty.";
                    if (appSettingsConfig.IsLoggingToFileEnabled())
                        BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_PROCESS_STEP, reasonfailed);

                    logger.LogError(reasonfailed, facade, BLHelper.GetCallingFunctionName(), appSettingsConfig.GetAppId());
                    throw new ArgumentException(reasonfailed, nameof(req));
                }

                //HttpClient httpClient = _httpClientFactory.CreateClient();

                HttpContent content = CreateJsonContent(req);

                var response = await httpClient.PostAsync(GetHrefService(HrefHeaders.GetJobScopes), content);

                if (!response.IsSuccessStatusCode)
                {
                    reasonfailed = $"API call failed: {response.StatusCode}";
                    if (appSettingsConfig.IsLoggingToFileEnabled())
                        BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_PROCESS_STEP, reasonfailed);

                    return BaseResponse<List<ResJobScopeModel>>.Fail(reasonfailed);
                }

                string responseData = await response.Content.ReadAsStringAsync();

                var getJobScope = System.Text.Json.JsonSerializer.Deserialize<List<ResJobScopeModel>>(responseData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                //var getJobScope = System.Text.Json.JsonSerializer.Deserialize<List<ResJobScopeModel>>(responseData);

                return getJobScope != null
                    ? BaseResponse<List<ResJobScopeModel>>.Success(getJobScope)
                    : BaseResponse<List<ResJobScopeModel>>.Fail("Failed to deserialize response data");
            }
            catch (Exception ex)
            {
                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP, ex.Message);

                logger.LogException(ex.Message, facade, BLHelper.GetCallingFunctionName(), ex, appSettingsConfig.GetAppId());
                return BaseResponse<List<ResJobScopeModel>>.Fail($"Exception: {ex.Message}");
            }
        }

        public async Task<BaseResponse<List<ResBranchModel>>> GetBranches(BaseRequest req)
        {

            try
            {
                logger.LogInfo("Start: GetBranches", facade, BLHelper.GetCallingFunctionName(), appSettingsConfig.GetAppId());

                if (req == null || string.IsNullOrEmpty(req.token))
                {
                    reasonfailed = "Request or token is null/empty.";
                    if (appSettingsConfig.IsLoggingToFileEnabled())
                        BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_PROCESS_STEP, reasonfailed);

                    logger.LogError(reasonfailed, facade, BLHelper.GetCallingFunctionName(), appSettingsConfig.GetAppId());
                    throw new ArgumentException(reasonfailed, nameof(req));
                }

                //HttpClient httpClient = _httpClientFactory.CreateClient();

                HttpContent content = CreateJsonContent(req);

                var response = await httpClient.PostAsync(GetHrefService(HrefHeaders.GetBranches), content);

                if (!response.IsSuccessStatusCode)
                {
                    reasonfailed = $"API call failed: {response.StatusCode}";
                    if (appSettingsConfig.IsLoggingToFileEnabled())
                        BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_PROCESS_STEP, reasonfailed);

                    return BaseResponse<List<ResBranchModel>>.Fail(reasonfailed);
                }

                string responseData = await response.Content.ReadAsStringAsync();

                var getbranch = System.Text.Json.JsonSerializer.Deserialize<List<ResBranchModel>>(responseData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                //var getbranch = JsonConvert.DeserializeObject<List<ResBranchModel>>(responseData);

                return getbranch != null
                    ? BaseResponse<List<ResBranchModel>>.Success(getbranch)
                    : BaseResponse<List<ResBranchModel>>.Fail("Failed to deserialize response data");
            }
            catch (Exception ex)
            {
                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP, ex.Message);

                logger.LogException(ex.Message, facade, BLHelper.GetCallingFunctionName(), ex, appSettingsConfig.GetAppId());
                return BaseResponse<List<ResBranchModel>>.Fail($"Exception: {ex.Message}");
            }
        }

        public async Task<BaseResponse<List<ResCategoreyMoedl>>> GetCategories(BaseRequest req)
        {

            try
            {
                logger.LogInfo("Start: GetCategories", facade, BLHelper.GetCallingFunctionName(), appSettingsConfig.GetAppId());

                if (req == null || string.IsNullOrEmpty(req.token))
                {
                    reasonfailed = "Request or token is null/empty.";
                    if (appSettingsConfig.IsLoggingToFileEnabled())
                        BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_PROCESS_STEP, reasonfailed);

                    logger.LogError(reasonfailed, facade, BLHelper.GetCallingFunctionName(), appSettingsConfig.GetAppId());
                    throw new ArgumentException(reasonfailed, nameof(req));
                }

                //HttpClient httpClient = _httpClientFactory.CreateClient();

                HttpContent content = CreateJsonContent(req);

                var response = await httpClient.PostAsync(GetHrefService(HrefHeaders.GetCategories), content);

                if (!response.IsSuccessStatusCode)
                {
                    reasonfailed = $"API call failed: {response.StatusCode}";
                    if (appSettingsConfig.IsLoggingToFileEnabled())
                        BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_PROCESS_STEP, reasonfailed);

                    return BaseResponse<List<ResCategoreyMoedl>>.Fail(reasonfailed);
                }

                string responseData = await response.Content.ReadAsStringAsync();

                var gecategories = System.Text.Json.JsonSerializer.Deserialize<List<ResCategoreyMoedl>>(responseData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                //var gecategories = JsonConvert.DeserializeObject<List<ResCategoreyMoedl>>(responseData);

                return gecategories != null
                    ? BaseResponse<List<ResCategoreyMoedl>>.Success(gecategories)
                    : BaseResponse<List<ResCategoreyMoedl>>.Fail("Failed to deserialize response data");
            }
            catch (Exception ex)
            {
                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP, ex.Message);

                logger.LogException(ex.Message, facade, BLHelper.GetCallingFunctionName(), ex, appSettingsConfig.GetAppId());
                return BaseResponse<List<ResCategoreyMoedl>>.Fail($"Exception: {ex.Message}");
            }
        }

        public async Task<BaseResponse<List<ResAreaModel>>> GetArea(BaseRequest req)
        {

            try
            {
                logger.LogInfo("Start: GetArea", facade, BLHelper.GetCallingFunctionName(), appSettingsConfig.GetAppId());

                if (req == null || string.IsNullOrEmpty(req.token))
                {
                    reasonfailed = "Request or token is null/empty.";
                    if (appSettingsConfig.IsLoggingToFileEnabled())
                        BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_PROCESS_STEP, reasonfailed);

                    logger.LogError(reasonfailed, facade, BLHelper.GetCallingFunctionName(), appSettingsConfig.GetAppId());
                    throw new ArgumentException(reasonfailed, nameof(req));
                }

                //HttpClient httpClient = _httpClientFactory.CreateClient();

                HttpContent content = CreateJsonContent(req);

                var response = await httpClient.PostAsync(GetHrefService(HrefHeaders.GetArea), content);

                if (!response.IsSuccessStatusCode)
                {
                    reasonfailed = $"API call failed: {response.StatusCode}";
                    if (appSettingsConfig.IsLoggingToFileEnabled())
                        BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_PROCESS_STEP, reasonfailed);

                    return BaseResponse<List<ResAreaModel>>.Fail(reasonfailed);
                }

                string responseData = await response.Content.ReadAsStringAsync();

                var getarea = System.Text.Json.JsonSerializer.Deserialize<List<ResAreaModel>>(responseData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                //var getarea = JsonConvert.DeserializeObject<List<ResAreaModel>>(responseData);

                return getarea != null
                    ? BaseResponse<List<ResAreaModel>>.Success(getarea)
                    : BaseResponse<List<ResAreaModel>>.Fail("Failed to deserialize response data");
            }
            catch (Exception ex)
            {
                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP, ex.Message);

                logger.LogException(ex.Message, facade, BLHelper.GetCallingFunctionName(), ex, appSettingsConfig.GetAppId());
                return BaseResponse<List<ResAreaModel>>.Fail($"Exception: {ex.Message}");
            }
        }

        public async Task<BaseResponse<List<ResProfessionModel>>> GetProfession(BaseRequest req)
        {

            try
            {
                logger.LogInfo("Start: GetProfession", facade, BLHelper.GetCallingFunctionName(), appSettingsConfig.GetAppId());

                if (req == null || string.IsNullOrEmpty(req.token))
                {
                    reasonfailed = "Request or token is null/empty.";
                    if (appSettingsConfig.IsLoggingToFileEnabled())
                        BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_PROCESS_STEP, reasonfailed);

                    logger.LogError(reasonfailed, facade, BLHelper.GetCallingFunctionName(), appSettingsConfig.GetAppId());
                    throw new ArgumentException(reasonfailed, nameof(req));
                }

                //HttpClient httpClient = _httpClientFactory.CreateClient();

     
                HttpContent content = CreateJsonContent(req);

                var response = await httpClient.PostAsync(GetHrefService(HrefHeaders.GetProfession), content);

                if (!response.IsSuccessStatusCode)
                {
                    reasonfailed = $"API call failed: {response.StatusCode}";
                    if (appSettingsConfig.IsLoggingToFileEnabled())
                        BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_PROCESS_STEP, reasonfailed);

                    return BaseResponse<List<ResProfessionModel>>.Fail(reasonfailed);
                }

                string responseData = await response.Content.ReadAsStringAsync();

                var getprofession = System.Text.Json.JsonSerializer.Deserialize<List<ResProfessionModel>>(responseData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                //var getprofession = JsonConvert.DeserializeObject<List<ResProfessionModel>>(responseData);

                return getprofession != null
                    ? BaseResponse<List<ResProfessionModel>>.Success(getprofession)
                    : BaseResponse<List<ResProfessionModel>>.Fail("Failed to deserialize response data");
            }
            catch (Exception ex)
            {
                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP, ex.Message);

                logger.LogException(ex.Message, facade, BLHelper.GetCallingFunctionName(), ex, appSettingsConfig.GetAppId());
                return BaseResponse<List<ResProfessionModel>>.Fail($"Exception: {ex.Message}");
            }
        }


        public async Task<BaseResponse<List<ResSubProfessionModel>>> GetSubProfession(ReqSubProfessionModel req)
        {

            try
            {
                logger.LogInfo("Start: GetSubProfession", facade, BLHelper.GetCallingFunctionName(), appSettingsConfig.GetAppId());

                if (req == null || string.IsNullOrEmpty(req.token)  || req.ProfessionID == null || req.ProfessionID == 0)
                {
                    reasonfailed = "Request or token is null/empty or ProfessionID is null or empty.";
                    if (appSettingsConfig.IsLoggingToFileEnabled())
                        BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_PROCESS_STEP, reasonfailed);

                    logger.LogError(reasonfailed, facade, BLHelper.GetCallingFunctionName(), appSettingsConfig.GetAppId());
                    throw new ArgumentException(reasonfailed, nameof(req));
                }

                //HttpClient httpClient = _httpClientFactory.CreateClient();

                HttpContent content = CreateJsonContent(req);

                var response = await httpClient.PostAsync(GetHrefService(HrefHeaders.GetSubProfession), content);

                if (!response.IsSuccessStatusCode)
                {
                    reasonfailed = $"API call failed: {response.StatusCode}";
                    if (appSettingsConfig.IsLoggingToFileEnabled())
                        BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_PROCESS_STEP, reasonfailed);

                    return BaseResponse<List<ResSubProfessionModel>>.Fail(reasonfailed);
                }

                string responseData = await response.Content.ReadAsStringAsync();

                var getsubprofession = System.Text.Json.JsonSerializer.Deserialize<List<ResSubProfessionModel>>(responseData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                //var getsubprofession = JsonConvert.DeserializeObject<List<ResSubProfessionModel>>(responseData);

                return getsubprofession != null
                    ? BaseResponse<List<ResSubProfessionModel>>.Success(getsubprofession)
                    : BaseResponse<List<ResSubProfessionModel>>.Fail("Failed to deserialize response data");
            }
            catch (Exception ex)
            {
                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP, ex.Message);

                logger.LogException(ex.Message, facade, BLHelper.GetCallingFunctionName(), ex, appSettingsConfig.GetAppId());
                return BaseResponse<List<ResSubProfessionModel>>.Fail($"Exception: {ex.Message}");
            }
        }

        public async Task<BaseResponse<List<ResOrdersDetailsModel>>> GetOrdersDetails(ReqOrderModel req)
        {

            try
            {
                logger.LogInfo("Start: GetOrdersDetails", facade, BLHelper.GetCallingFunctionName(), appSettingsConfig.GetAppId());
                HttpContent content;
                var response = new HttpResponseMessage();
                if (req == null || string.IsNullOrEmpty(req.token))
                {
                    reasonfailed = "Request or token is null/empty or GetOrdersDetails is null or empty.";
                    if (appSettingsConfig.IsLoggingToFileEnabled())
                        BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_PROCESS_STEP, reasonfailed);

                    logger.LogError(reasonfailed, facade, BLHelper.GetCallingFunctionName(), appSettingsConfig.GetAppId());
                    throw new ArgumentException(reasonfailed, nameof(req));
                }

                //HttpClient httpClient = _httpClientFactory.CreateClient();

                if (string.IsNullOrEmpty(req.OrderNo))
                {
                    var obj = new
                    {
                        token = req.token
                    };
                    content = CreateJsonContent(obj);

                }
                else
                {
                    content = CreateJsonContent(req);
                }
                response = await httpClient.PostAsync(GetHrefService(HrefHeaders.GetOrdersDetails), content);
                if (!response.IsSuccessStatusCode)
                {
                    reasonfailed = $"API call failed: {response.StatusCode}";
                    if (appSettingsConfig.IsLoggingToFileEnabled())
                        BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_PROCESS_STEP, reasonfailed);

                    return BaseResponse<List<ResOrdersDetailsModel>>.Fail(reasonfailed);
                }

                string responseData = await response.Content.ReadAsStringAsync();


                //var getorder = JsonHelper.DeserializeWithConversions<List<ResOrdersDetailsModel>>(responseData);
                var getorder = System.Text.Json.JsonSerializer.Deserialize<List<ResOrdersDetailsModel>>(responseData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });


                //var getorder = JsonConvert.DeserializeObject<List<ResOrdersDetailsModel>>(responseData);


                return getorder != null
                    ? BaseResponse<List<ResOrdersDetailsModel>>.Success(getorder)
                    : BaseResponse<List<ResOrdersDetailsModel>>.Fail("Failed to deserialize response data");
            }
            catch (Exception ex)
            {
                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP, ex.Message);

                logger.LogException(ex.Message, facade, BLHelper.GetCallingFunctionName(), ex, appSettingsConfig.GetAppId());
                return BaseResponse<List<ResOrdersDetailsModel>>.Fail($"Exception: {ex.Message}");
            }
        }

        public async Task<BaseResponse<List<ResOrdersDetailsModel>>> GetActiveJobs(ReqOrderModel req)
        {
            try
            {
                List<ResOrdersDetailsModel> ActiveJobsList = new List<ResOrdersDetailsModel>();

                Task<BaseResponse<List<ResOrdersDetailsModel>>> alljobs = GetOrdersDetails(req);

                if (alljobs == null || alljobs.Result.Data.Count() < 1)
                    throw new Exception("Not found jobs");

                Task<BaseResponse<List<ResProfessionModel>>> activeproffessions = GetActiveProffesion(req);

                if (activeproffessions == null || activeproffessions.Result.Data.Count() < 1)
                    throw new Exception("Not found active proffessions");


                foreach (var job in alljobs.Result.Data)
                {
                    if (!string.IsNullOrEmpty(job.ProfessionName) && job.ProfessionId != null && job.ProfessionId > 0)
                    {
                        if (activeproffessions.Result.Data.Any(x => x.ProfessionID == job.ProfessionId))
                        {
                            if (!ActiveJobsList.Contains(job))
                            {
                                ActiveJobsList.Add(job);

                            }

                            else continue;
                        }
                    }
                }

                return ActiveJobsList != null
                ? BaseResponse<List<ResOrdersDetailsModel>>.Success(ActiveJobsList)
                : BaseResponse<List<ResOrdersDetailsModel>>.Fail("Failed to deserialize response data");

            }
            catch (Exception ex)
            {
                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP, ex.Message);

                logger.LogException(ex.Message, facade, BLHelper.GetCallingFunctionName(), ex, appSettingsConfig.GetAppId());
                return BaseResponse<List<ResOrdersDetailsModel>>.Fail($"Exception: {ex.Message}");
            }
        }
       
        #endregion


        #region api to get data from bezeq drushim

        public async Task<BaseResponse<List<ResAreaModel>>> GetActiveaAreas(BaseRequest req)
        {

            try
            {
                List<ResAreaModel> ListActiveAreas = new List<ResAreaModel>();

                List<string> areanames = new List<string>();

                logger.LogInfo("Start: GetActiveaAreas", facade, BLHelper.GetCallingFunctionName(), appSettingsConfig.GetAppId());

                if (req == null || string.IsNullOrEmpty(req.token))
                {
                    reasonfailed = "Request or token is null/empty.";
                    if (appSettingsConfig.IsLoggingToFileEnabled())
                        BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_PROCESS_STEP, reasonfailed);

                    logger.LogError(reasonfailed, facade, BLHelper.GetCallingFunctionName(), appSettingsConfig.GetAppId());
                    throw new ArgumentException(reasonfailed, nameof(req));
                }

                ReqOrderModel requestbody = new ReqOrderModel()
                {
                    token = req.token,
                    OrderNo = ""
                };

                var AllActiveJobs = GetActiveJobs(requestbody);

                if (AllActiveJobs == null || AllActiveJobs.Result.Data.Count() < 1)
                    throw new Exception("Not found active jobs");

                foreach (var job in AllActiveJobs.Result.Data)
                {
                    if (job != null)
                    {
                        if (!string.IsNullOrEmpty(job.LivingArea1))
                        {
                            if (!areanames.Contains(job.LivingArea1))
                            {
                                areanames.Add(job.LivingArea1);
                            }
                        }
                        if (!string.IsNullOrEmpty(job.LivingArea2))
                        {
                            if (!areanames.Contains(job.LivingArea2))
                            {
                                areanames.Add(job.LivingArea2);
                            }
                        }
                        if (!string.IsNullOrEmpty(job.LivingArea3))
                        {
                            if (!areanames.Contains(job.LivingArea3))
                            {
                                areanames.Add(job.LivingArea3);
                            }
                        }
                        if (!string.IsNullOrEmpty(job.LivingArea4))
                        {
                            if (!areanames.Contains(job.LivingArea4))
                            {
                                areanames.Add(job.LivingArea4);
                            }
                        }
                        if (!string.IsNullOrEmpty(job.LivingArea5))
                        {
                            if (!areanames.Contains(job.LivingArea5))
                            {
                                areanames.Add(job.LivingArea5);
                            }
                        }
                        if (!string.IsNullOrEmpty(job.LivingArea6))
                        {
                            if (!areanames.Contains(job.LivingArea6))
                            {
                                areanames.Add(job.LivingArea6);
                            }
                        }
                    }
                }

                var allareas = GetArea(req);

                if(allareas == null || allareas.Result.Data.Count() < 1)
                    throw new Exception("Not found areas");

                foreach (var area in allareas.Result.Data)
                {
                    if (area == null)
                        continue;

                    else
                    {
                        if (!string.IsNullOrEmpty(area.AreaName) && areanames.Contains(area.AreaName))
                        {
                            if (!ListActiveAreas.Contains(area))
                            {
                                ListActiveAreas.Add(area);
                            }

                        }
                    }
                }

                return ListActiveAreas != null && ListActiveAreas.Count() > 0
                       ? BaseResponse<List<ResAreaModel>>.Success(ListActiveAreas)
                       : BaseResponse<List<ResAreaModel>>.Fail("Failed to deserialize response data");

            }
            catch (Exception ex)
            {
                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP, ex.Message);

                logger.LogException(ex.Message, facade, BLHelper.GetCallingFunctionName(), ex, appSettingsConfig.GetAppId());
                return BaseResponse<List<ResAreaModel>>.Fail($"Exception: {ex.Message}");
            }
        }

        public async Task<BaseResponse<List<JobModel>>>ActiveJobs(ReqOrderModel req)
        {
            var response = await GetOrdersDetails(req);
            List<ResOrdersDetailsModel> ActiveJobsList = response.Data.ToList();

            if (ActiveJobsList == null || ActiveJobsList.Count < 1)
                throw new Exception("Not found active jobs");

            if(ActiveJobsList.Any(x => x.OrderId == 1005))
            {

            }
            
            List<JobModel> JobNames = ActiveJobsList
                .Where(x => !string.IsNullOrEmpty(x.ProfessionName) && x.ProfessionId != null)
                .Select(x => new JobModel
                {
                    JobId = x.ProfessionId,
                    JobName = x.Description,
                    OrderId = x.OrderId
                })
                .ToList();

            if(JobNames.Any(x => x.OrderId == 1005))
            {

            }

            if (JobNames == null || JobNames.Count < 1)
                throw new Exception("Not found active job names");

                        //JobNames = JobNames
                        //.GroupBy(x => x.JobId)
                        //.Select(g => g.First())
                        //.ToList();

            if (JobNames.Any(x => x.OrderId == 1005))
            {

            }

            return  JobNames != null && JobNames.Count() > 0 ? 
                BaseResponse<List<JobModel>>.Success(JobNames) : 
                BaseResponse<List<JobModel>>.Fail("Failed to deserialize response data");
        }

        public async Task<BaseResponse<List<ResProfessionModel>>> GetActiveProffesion(BaseRequest req)
        {

            try
            {
                logger.LogInfo("Start: GetProfession", facade, BLHelper.GetCallingFunctionName(), appSettingsConfig.GetAppId());

                if (req == null || string.IsNullOrEmpty(req.token))
                {
                    reasonfailed = "Request or token is null/empty.";
                    if (appSettingsConfig.IsLoggingToFileEnabled())
                        BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_PROCESS_STEP, reasonfailed);

                    logger.LogError(reasonfailed, facade, BLHelper.GetCallingFunctionName(), appSettingsConfig.GetAppId());
                    throw new ArgumentException(reasonfailed, nameof(req));
                }

                //HttpClient httpClient = _httpClientFactory.CreateClient();

                var bodyrequest = new
                {
                    token = req.token,
                    isAssignedToJob = "1"
                };

                HttpContent content = CreateJsonContent(bodyrequest);

                var response = await httpClient.PostAsync(GetHrefService(HrefHeaders.GetProfession), content);

                if (!response.IsSuccessStatusCode)
                {
                    reasonfailed = $"API call failed: {response.StatusCode}";
                    if (appSettingsConfig.IsLoggingToFileEnabled())
                        BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_PROCESS_STEP, reasonfailed);

                    return BaseResponse<List<ResProfessionModel>>.Fail(reasonfailed);
                }

                string responseData = await response.Content.ReadAsStringAsync();

                var getprofession = System.Text.Json.JsonSerializer.Deserialize<List<ResProfessionModel>>(responseData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                //var getprofession = JsonConvert.DeserializeObject<List<ResProfessionModel>>(responseData);

                return getprofession != null
                    ? BaseResponse<List<ResProfessionModel>>.Success(getprofession)
                    : BaseResponse<List<ResProfessionModel>>.Fail("Failed to deserialize response data");
            }
            //catch (Exception ex)
            //{
            //    if (appSettingsConfig.IsLoggingToFileEnabled())
            //        BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP, ex.Message);

            //    logger.LogException(ex.Message, facade, BLHelper.GetCallingFunctionName(), ex, appSettingsConfig.GetAppId());
            //    return BaseResponse<List<ResProfessionModel>>.Fail($"Exception: {ex.Message}");
            //}


            catch (TaskCanceledException ex)
            {
                // קרה ביטול קריאה - רפרש, סגירה וכו'
                reasonfailed = "The request was canceled (probably due to client refresh or close).";
                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP, reasonfailed);

                logger.LogError(reasonfailed, facade, BLHelper.GetCallingFunctionName(), appSettingsConfig.GetAppId());

                return BaseResponse<List<ResProfessionModel>>.Fail(reasonfailed);
            }
            catch (HttpRequestException ex)
            {
                // בעיית תקשורת מול שרת חיצוני
                reasonfailed = $"HttpRequestException: {ex.Message}";
                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP, reasonfailed);

                logger.LogException(ex.Message, facade, BLHelper.GetCallingFunctionName(), ex, appSettingsConfig.GetAppId());

                return BaseResponse<List<ResProfessionModel>>.Fail(reasonfailed);
            }
            catch (Exception ex)
            {
                // כל שגיאה אחרת
                reasonfailed = $"General Exception: {ex.Message}";
                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP, reasonfailed);

                logger.LogException(ex.Message, facade, BLHelper.GetCallingFunctionName(), ex, appSettingsConfig.GetAppId());
                return BaseResponse<List<ResProfessionModel>>.Fail(reasonfailed);
            }
        }

        #endregion

        #region home response send to www drushim page
        
        public async Task<BaseResponse<HomeResponse>> GetHomePageData(string Token)
        {
            HomeResponse res = new HomeResponse();

            try
            {

                BaseRequest basereq = new BaseRequest()
                {
                    token = Token
                };

                ReqOrderModel orders = new ReqOrderModel()
                {
                    token = Token,
                    OrderNo = ""
                };

                var areas = await GetActiveaAreas(basereq);             
                var jobs = await ActiveJobs(orders);
                var proffessions = await GetActiveProffesion(basereq);

                res.areas = areas.Data;
                res.jobs = jobs.Data;
                res.activeProfessions = proffessions.Data;

                return res != null && res.activeProfessions.Count() > 0 && res.areas.Count() > 0 && res.jobs.Count() > 0
                    ? BaseResponse<HomeResponse>.Success(res)
                    : BaseResponse< HomeResponse>.Fail("Failed to deserialize response data");


            }
            catch (Exception ex)
            {
                if (appSettingsConfig.IsLoggingToFileEnabled())
                    BLHelper.WriteFileLog(facade, BLHelper.GetCallingFunctionName(), TraceEventTypeToLogFile.WRITE_LOG_FILE_START_APP, ex.Message);

                logger.LogException(ex.Message, facade, BLHelper.GetCallingFunctionName(), ex, appSettingsConfig.GetAppId());
                return BaseResponse<HomeResponse>.Fail($"Exception: {ex.Message}");
            }


        }

        #endregion
    }
}

