using System;
using System.Text.Json.Serialization;

namespace drushim.Models.Response
{
    public class ResOrdersDetailsModel
    {
        //[JsonPropertyName("close_date")]
        //public DateTime CloseDate { get; set; }

        //[JsonPropertyName("closeDate_ddmmyyy")]
        //public string CloseDateFormatted { get; set; }

        //[JsonPropertyName("update_date")]
        //public DateTime UpdateDate { get; set; }

        //[JsonPropertyName("updateDate_ddmmyyyy")]
        //public string UpdateDateFormatted { get; set; }

        //[JsonPropertyName("orderDate")]
        //public DateTime OrderDate { get; set; }

        //[JsonPropertyName("orderDate_ddmmyyyy")]
        //public string OrderDateFormatted { get; set; }

        //[JsonPropertyName("start_advertising_date")]
        //public DateTime StartAdvertisingDate { get; set; }

        //[JsonPropertyName("end_advertising_date")]
        //public DateTime EndAdvertisingDate { get; set; }

        //[JsonPropertyName("internal_job_deadline")]
        //public DateTime InternalJobDeadline { get; set; }

        //[JsonPropertyName("deadline_date")]
        //public DateTime DeadlineDate { get; set; }

        //// פרטי הזמנה
        //[JsonPropertyName("order_id")]
        //public int OrderId { get; set; }

        //[JsonPropertyName("order_email")]
        //public string OrderEmail { get; set; }

        //[JsonPropertyName("orderno_external")]
        //public string OrderNoExternal { get; set; }

        //[JsonPropertyName("order_snif")]
        //public int OrderSnif { get; set; }

        //[JsonPropertyName("client_name")]
        //public string ClientName { get; set; }

        //[JsonPropertyName("clientno")]
        //public int ClientNo { get; set; }

        //[JsonPropertyName("client_parent_id")]
        //public int ClientParentId { get; set; }

        //[JsonPropertyName("client_parent_name")]
        //public string ClientParentName { get; set; }

        //[JsonPropertyName("rakaz")]
        //public string Rakaz { get; set; }

        //[JsonPropertyName("rakaz_handle_id")]
        //public int RakazHandleId { get; set; }

        //[JsonPropertyName("rakaz_handle_name")]
        //public string RakazHandleName { get; set; }

        //[JsonPropertyName("email_rakaz")]
        //public string EmailRakaz { get; set; }

        //[JsonPropertyName("email_snif")]
        //public string EmailSnif { get; set; }

        //[JsonPropertyName("category_id")]
        //public int CategoryId { get; set; }

        //[JsonPropertyName("category_name")]
        //public string CategoryName { get; set; }

        //[JsonPropertyName("profession_name")]
        //public string ProfessionName { get; set; }

        //[JsonPropertyName("tat_profession_name")]
        //public string TatProfessionName { get; set; }

        //[JsonPropertyName("ProffesionID")]
        //public int ProfessionId { get; set; }

        //[JsonPropertyName("SubProffesionID")]
        //public int SubProfessionId { get; set; }

        //[JsonPropertyName("work_area")]
        //public string WorkArea { get; set; }

        //[JsonPropertyName("tkofat_avoda_moza")]
        //public string WorkPeriod { get; set; }

        //[JsonPropertyName("car_owner")]
        //public string CarOwner { get; set; }

        //[JsonPropertyName("notes")]
        //public string Notes { get; set; }

        //[JsonPropertyName("notes_text")]
        //public string NotesText { get; set; }

        //[JsonPropertyName("is_html_notes")]
        //public int IsHtmlNotes { get; set; }

        //[JsonPropertyName("friend_reward")]
        //public int FriendReward { get; set; }

        //[JsonPropertyName("advertising_destination")]
        //public int AdvertisingDestination { get; set; }

        //[JsonPropertyName("eshkol")]
        //public string Eshkol { get; set; }

        //[JsonPropertyName("eshkol_name")]
        //public string EshkolName { get; set; }



        [JsonPropertyName("close_date")]
        public DateTime CloseDate { get; set; }

        [JsonPropertyName("closeDate_ddmmyyy")]
        public string CloseDateFormatted { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("email_rakaz")]
        public string EmailRakaz { get; set; }

        [JsonPropertyName("email_snif")]
        public string EmailSnif { get; set; }

        [JsonPropertyName("living_area1")]
        public string LivingArea1 { get; set; }

        [JsonPropertyName("living_area2")]
        public string LivingArea2 { get; set; }

        [JsonPropertyName("living_area3")]
        public string LivingArea3 { get; set; }

        [JsonPropertyName("living_area4")]
        public string LivingArea4 { get; set; }

        [JsonPropertyName("living_area5")]
        public string LivingArea5 { get; set; }

        [JsonPropertyName("living_area6")]
        public string LivingArea6 { get; set; }

        [JsonPropertyName("name_snif")]
        public string NameSnif { get; set; }

        [JsonPropertyName("order_snif")]
        public int OrderSnif { get; set; }

        [JsonPropertyName("notes")]
        public string Notes { get; set; }

        [JsonPropertyName("notes_text")]
        public string NotesText { get; set; }

        [JsonPropertyName("order_email")]
        public string OrderEmail { get; set; }

        [JsonPropertyName("order_id")]
        public int OrderId { get; set; }

        [JsonPropertyName("perot_tafked")]
        public string PerotTafked { get; set; }

        [JsonPropertyName("profession_name")]
        public string ProfessionName { get; set; }

        [JsonPropertyName("rakaz")]
        public string Rakaz { get; set; }

        [JsonPropertyName("tat_profession_name")]
        public string TatProfessionName { get; set; }

        [JsonPropertyName("telefon")]
        public string Telefon { get; set; }

        [JsonPropertyName("tkofat_avoda_moza")]
        public string TkofatAvodaMoza { get; set; }

        [JsonPropertyName("toar")]
        public int Toar { get; set; }

        [JsonPropertyName("update_date")]
        public DateTime UpdateDate { get; set; }

        [JsonPropertyName("updateDate_ddmmyyyy")]
        public string UpdateDateFormatted { get; set; }

        [JsonPropertyName("work_area")]
        public string WorkArea { get; set; }

        [JsonPropertyName("order_def_prof1")]
        public int OrderDefProf1 { get; set; }

        [JsonPropertyName("order_def_prof_name1")]
        public string OrderDefProfName1 { get; set; }

        [JsonPropertyName("order_def_subprof1")]
        public int OrderDefSubProf1 { get; set; }

        [JsonPropertyName("order_def_sub_prof_name1")]
        public string OrderDefSubProfName1 { get; set; }

        [JsonPropertyName("order_def_prof2")]
        public int OrderDefProf2 { get; set; }

        [JsonPropertyName("order_def_prof_name2")]
        public string OrderDefProfName2 { get; set; }

        [JsonPropertyName("order_def_subprof2")]
        public int OrderDefSubProf2 { get; set; }

        [JsonPropertyName("order_def_sub_prof_name2")]
        public string OrderDefSubProfName2 { get; set; }

        [JsonPropertyName("order_def_prof3")]
        public int OrderDefProf3 { get; set; }

        [JsonPropertyName("order_def_prof_name3")]
        public string OrderDefProfName3 { get; set; }

        [JsonPropertyName("order_def_subprof3")]
        public int OrderDefSubProf3 { get; set; }

        [JsonPropertyName("order_def_sub_prof_name3")]
        public string OrderDefSubProfName3 { get; set; }

        [JsonPropertyName("order_def_prof4")]
        public int OrderDefProf4 { get; set; }

        [JsonPropertyName("order_def_prof_name4")]
        public string OrderDefProfName4 { get; set; }

        [JsonPropertyName("order_def_subprof4")]
        public int OrderDefSubProf4 { get; set; }

        [JsonPropertyName("order_def_sub_prof_name4")]
        public string OrderDefSubProfName4 { get; set; }

        [JsonPropertyName("order_def_prof5")]
        public int OrderDefProf5 { get; set; }

        [JsonPropertyName("order_def_prof_name5")]
        public string OrderDefProfName5 { get; set; }

        [JsonPropertyName("order_def_subprof5")]
        public int OrderDefSubProf5 { get; set; }

        [JsonPropertyName("order_def_sub_prof_name5")]
        public string OrderDefSubProfName5 { get; set; }

        [JsonPropertyName("order_def_area1")]
        public int OrderDefArea1 { get; set; }

        [JsonPropertyName("order_def_area_name1")]
        public string OrderDefAreaName1 { get; set; }

        [JsonPropertyName("order_def_area2")]
        public int OrderDefArea2 { get; set; }

        [JsonPropertyName("order_def_area_name2")]
        public string OrderDefAreaName2 { get; set; }

        [JsonPropertyName("order_def_area3")]
        public int OrderDefArea3 { get; set; }

        [JsonPropertyName("order_def_area_name3")]
        public string OrderDefAreaName3 { get; set; }

        [JsonPropertyName("order_def_area4")]
        public int OrderDefArea4 { get; set; }

        [JsonPropertyName("order_def_area_name4")]
        public string OrderDefAreaName4 { get; set; }

        [JsonPropertyName("order_def_area5")]
        public int OrderDefArea5 { get; set; }

        [JsonPropertyName("order_def_area_name5")]
        public string OrderDefAreaName5 { get; set; }

        [JsonPropertyName("order_def_area6")]
        public int OrderDefArea6 { get; set; }

        [JsonPropertyName("order_def_area_name6")]
        public string OrderDefAreaName6 { get; set; }

        [JsonPropertyName("order_def_job_scope1")]
        public int OrderDefJobScope1 { get; set; }

        [JsonPropertyName("order_def_job_scope2")]
        public int OrderDefJobScope2 { get; set; }

        [JsonPropertyName("order_def_job_scope3")]
        public int OrderDefJobScope3 { get; set; }

        [JsonPropertyName("order_def_job_scope1_desc")]
        public string OrderDefJobScope1Desc { get; set; }

        [JsonPropertyName("order_def_job_scope2_desc")]
        public string OrderDefJobScope2Desc { get; set; }

        [JsonPropertyName("order_def_job_scope3_desc")]
        public string OrderDefJobScope3Desc { get; set; }

        [JsonPropertyName("orderno_external")]
        public string OrderNoExternal { get; set; }

        [JsonPropertyName("Order_place")]
        public string OrderPlace { get; set; }

        [JsonPropertyName("ProffesionID")]
        public int ProfessionId { get; set; }

        [JsonPropertyName("SubProffesionID")]
        public int SubProfessionId { get; set; }

        [JsonPropertyName("SubProffesionIDUntil")]
        public int SubProfessionIdUntil { get; set; }

        [JsonPropertyName("Branch")]
        public int Branch { get; set; }

        [JsonPropertyName("IaHot")]
        public int IaHot { get; set; }

        [JsonPropertyName("IsHot")]
        public int IsHot { get; set; }

        [JsonPropertyName("RakazID")]
        public int RakazId { get; set; }

        [JsonPropertyName("snifCode")]
        public int SnifCode { get; set; }

        [JsonPropertyName("orderDate")]
        public DateTime OrderDate { get; set; }

        [JsonPropertyName("orderDate_ddmmyyyy")]
        public string OrderDateFormatted { get; set; }

        [JsonPropertyName("advertising_destination")]
        public int AdvertisingDestination { get; set; }

        [JsonPropertyName("car_owner")]
        public string CarOwner { get; set; }

        [JsonPropertyName("client_name")]
        public string ClientName { get; set; }

        [JsonPropertyName("clientno")]
        public int ClientNo { get; set; }

        [JsonPropertyName("requirement1")]
        public int Requirement1 { get; set; }

        [JsonPropertyName("requirement2")]
        public int Requirement2 { get; set; }

        [JsonPropertyName("category_id")]
        public int CategoryId { get; set; }

        [JsonPropertyName("category_name")]
        public string CategoryName { get; set; }

        [JsonPropertyName("client_parent_id")]
        public int ClientParentId { get; set; }

        [JsonPropertyName("client_parent_name")]
        public string ClientParentName { get; set; }

        [JsonPropertyName("internal_job_deadline")]
        public string InternalJobDeadline { get; set; }

        [JsonPropertyName("rakaz_handle_id")]
        public int RakazHandleId { get; set; }

        [JsonPropertyName("rakaz_handle_name")]
        public string RakazHandleName { get; set; }

        [JsonPropertyName("start_advertising_date")]
        public string StartAdvertisingDate { get; set; }

        [JsonPropertyName("end_advertising_date")]
        public string EndAdvertisingDate { get; set; }

        [JsonPropertyName("friend_reward")]
        public int FriendReward { get; set; }

        [JsonPropertyName("is_html_notes")]
        public int IsHtmlNotes { get; set; }

        [JsonPropertyName("deadline_date")]
        public string DeadlineDate { get; set; }

        [JsonPropertyName("manning_type_code")]
        public string ManningTypeCode { get; set; }

        [JsonPropertyName("manning_type_text")]
        public string ManningTypeText { get; set; }

        [JsonPropertyName("role_level")]
        public string RoleLevel { get; set; }

        [JsonPropertyName("role_level_desc")]
        public string RoleLevelDesc { get; set; }

        [JsonPropertyName("supervisor_code")]
        public int SupervisorCode { get; set; }

        [JsonPropertyName("supervisor")]
        public string Supervisor { get; set; }

        [JsonPropertyName("recruiter_code")]
        public int RecruiterCode { get; set; }

        [JsonPropertyName("recruiter")]
        public string Recruiter { get; set; }

        [JsonPropertyName("eshkol")]
        public string Eshkol { get; set; }

        [JsonPropertyName("eshkol_name")]
        public string EshkolName { get; set; }
        public ResOrdersDetailsModel() { }
    }
}
