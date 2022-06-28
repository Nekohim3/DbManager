﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DBManager
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SOFTMARINE_COMPANYEntities : DbContext
    {
        public SOFTMARINE_COMPANYEntities()
            : base("name=SOFTMARINE_COMPANYEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C_ADESettings> C_ADESettings { get; set; }
        public virtual DbSet<C_AttachedFile> C_AttachedFile { get; set; }
        public virtual DbSet<C_AttachedFileInfo> C_AttachedFileInfo { get; set; }
        public virtual DbSet<C_BaseRule> C_BaseRule { get; set; }
        public virtual DbSet<C_ChangeInfo> C_ChangeInfo { get; set; }
        public virtual DbSet<C_Country> C_Country { get; set; }
        public virtual DbSet<C_ExportImportInfo> C_ExportImportInfo { get; set; }
        public virtual DbSet<C_ExportImportPackage> C_ExportImportPackage { get; set; }
        public virtual DbSet<C_ExportImportStepStatistic> C_ExportImportStepStatistic { get; set; }
        public virtual DbSet<C_FileData> C_FileData { get; set; }
        public virtual DbSet<C_FileInfo> C_FileInfo { get; set; }
        public virtual DbSet<C_FunctionRule> C_FunctionRule { get; set; }
        public virtual DbSet<C_Person> C_Person { get; set; }
        public virtual DbSet<C_PersonServiceTime> C_PersonServiceTime { get; set; }
        public virtual DbSet<C_Rank> C_Rank { get; set; }
        public virtual DbSet<C_RemoveCommand> C_RemoveCommand { get; set; }
        public virtual DbSet<C_Role> C_Role { get; set; }
        public virtual DbSet<C_Ship> C_Ship { get; set; }
        public virtual DbSet<C_Ship_File> C_Ship_File { get; set; }
        public virtual DbSet<C_ShipGroup> C_ShipGroup { get; set; }
        public virtual DbSet<C_ShipOwner> C_ShipOwner { get; set; }
        public virtual DbSet<C_ShipProject> C_ShipProject { get; set; }
        public virtual DbSet<C_ShipProperty> C_ShipProperty { get; set; }
        public virtual DbSet<C_ShipPropertyValue> C_ShipPropertyValue { get; set; }
        public virtual DbSet<C_ShipSetting> C_ShipSetting { get; set; }
        public virtual DbSet<C_ShipStatus> C_ShipStatus { get; set; }
        public virtual DbSet<C_ShipType> C_ShipType { get; set; }
        public virtual DbSet<C_User> C_User { get; set; }
        public virtual DbSet<C_User_RequisitionDepartment> C_User_RequisitionDepartment { get; set; }
        public virtual DbSet<C_UserAccessByUnit> C_UserAccessByUnit { get; set; }
        public virtual DbSet<C_UserAccount> C_UserAccount { get; set; }
        public virtual DbSet<bdg_BudgetItem> bdg_BudgetItem { get; set; }
        public virtual DbSet<bdg_BudgetPlan> bdg_BudgetPlan { get; set; }
        public virtual DbSet<bdg_BudgetPlanItem> bdg_BudgetPlanItem { get; set; }
        public virtual DbSet<bdg_Currency> bdg_Currency { get; set; }
        public virtual DbSet<bdg_PaidmentDocument> bdg_PaidmentDocument { get; set; }
        public virtual DbSet<bdg_PaidmentPosition> bdg_PaidmentPosition { get; set; }
        public virtual DbSet<bdg_Rate> bdg_Rate { get; set; }
        public virtual DbSet<bdg_ShipOwnerCurrencyBaseYear> bdg_ShipOwnerCurrencyBaseYear { get; set; }
        public virtual DbSet<bdg_Supplier> bdg_Supplier { get; set; }
        public virtual DbSet<BlockData> BlockDatas { get; set; }
        public virtual DbSet<CommonData> CommonDatas { get; set; }
        public virtual DbSet<dms_Document> dms_Document { get; set; }
        public virtual DbSet<dms_Document_File> dms_Document_File { get; set; }
        public virtual DbSet<dms_DocumentTitle> dms_DocumentTitle { get; set; }
        public virtual DbSet<dms_DocumentTitle_File> dms_DocumentTitle_File { get; set; }
        public virtual DbSet<dms_File> dms_File { get; set; }
        public virtual DbSet<dms_FileDataExportInfo> dms_FileDataExportInfo { get; set; }
        public virtual DbSet<dms_FileInfo> dms_FileInfo { get; set; }
        public virtual DbSet<dms_Partition> dms_Partition { get; set; }
        public virtual DbSet<dms_smsChangeDocument> dms_smsChangeDocument { get; set; }
        public virtual DbSet<dms_smsDocument> dms_smsDocument { get; set; }
        public virtual DbSet<dms_smsDocument_File> dms_smsDocument_File { get; set; }
        public virtual DbSet<dms_smsDocument_Ship_NotVisible> dms_smsDocument_Ship_NotVisible { get; set; }
        public virtual DbSet<dms_smsPartition> dms_smsPartition { get; set; }
        public virtual DbSet<hseq_Inspection> hseq_Inspection { get; set; }
        public virtual DbSet<hseq_Inspection_File> hseq_Inspection_File { get; set; }
        public virtual DbSet<hseq_InspectionName> hseq_InspectionName { get; set; }
        public virtual DbSet<hseq_InspectionPlan> hseq_InspectionPlan { get; set; }
        public virtual DbSet<hseq_Inspector> hseq_Inspector { get; set; }
        public virtual DbSet<hseq_Observation> hseq_Observation { get; set; }
        public virtual DbSet<hseq_Observation_File> hseq_Observation_File { get; set; }
        public virtual DbSet<mrv_FuelOperation> mrv_FuelOperation { get; set; }
        public virtual DbSet<mrv_FuelOperation_File> mrv_FuelOperation_File { get; set; }
        public virtual DbSet<mrv_FuelOperationQuantity> mrv_FuelOperationQuantity { get; set; }
        public virtual DbSet<mrv_FuelType> mrv_FuelType { get; set; }
        public virtual DbSet<mrv_FuelType_Ship> mrv_FuelType_Ship { get; set; }
        public virtual DbSet<mrv_FuelType_VoyagePeriod> mrv_FuelType_VoyagePeriod { get; set; }
        public virtual DbSet<mrv_Port> mrv_Port { get; set; }
        public virtual DbSet<mrv_Report_EmissionSource> mrv_Report_EmissionSource { get; set; }
        public virtual DbSet<mrv_Voyage> mrv_Voyage { get; set; }
        public virtual DbSet<mrv_VoyagePeriod> mrv_VoyagePeriod { get; set; }
        public virtual DbSet<pms_Access> pms_Access { get; set; }
        public virtual DbSet<pms_CodeTMK> pms_CodeTMK { get; set; }
        public virtual DbSet<pms_CompanySetting> pms_CompanySetting { get; set; }
        public virtual DbSet<pms_CompanySetting_LogoFile> pms_CompanySetting_LogoFile { get; set; }
        public virtual DbSet<pms_Counter> pms_Counter { get; set; }
        public virtual DbSet<pms_CounterLogRecord> pms_CounterLogRecord { get; set; }
        public virtual DbSet<pms_CounterType> pms_CounterType { get; set; }
        public virtual DbSet<pms_File> pms_File { get; set; }
        public virtual DbSet<pms_FileDataExportInfo> pms_FileDataExportInfo { get; set; }
        public virtual DbSet<pms_FileInfo> pms_FileInfo { get; set; }
        public virtual DbSet<pms_FileInfo_Ship> pms_FileInfo_Ship { get; set; }
        public virtual DbSet<pms_FileInfoManagedByShip> pms_FileInfoManagedByShip { get; set; }
        public virtual DbSet<pms_FileLib> pms_FileLib { get; set; }
        public virtual DbSet<pms_FileLibInfo> pms_FileLibInfo { get; set; }
        public virtual DbSet<pms_FileManagedByShip> pms_FileManagedByShip { get; set; }
        public virtual DbSet<pms_InnerCompanySetting> pms_InnerCompanySetting { get; set; }
        public virtual DbSet<pms_Job> pms_Job { get; set; }
        public virtual DbSet<pms_Job_File> pms_Job_File { get; set; }
        public virtual DbSet<pms_Job_OldCopy> pms_Job_OldCopy { get; set; }
        public virtual DbSet<pms_Job_Spare> pms_Job_Spare { get; set; }
        public virtual DbSet<pms_JobHistoryCounterRecord> pms_JobHistoryCounterRecord { get; set; }
        public virtual DbSet<pms_JobHistoryRecord> pms_JobHistoryRecord { get; set; }
        public virtual DbSet<pms_JobHistoryRecord_File> pms_JobHistoryRecord_File { get; set; }
        public virtual DbSet<pms_JobOrderItem> pms_JobOrderItem { get; set; }
        public virtual DbSet<pms_JobOrderItem_File> pms_JobOrderItem_File { get; set; }
        public virtual DbSet<pms_JobOrderItem_InnerInfo> pms_JobOrderItem_InnerInfo { get; set; }
        public virtual DbSet<pms_JobPeriod> pms_JobPeriod { get; set; }
        public virtual DbSet<pms_JobPeriod_OldCopy> pms_JobPeriod_OldCopy { get; set; }
        public virtual DbSet<pms_JobPlan> pms_JobPlan { get; set; }
        public virtual DbSet<pms_JobReqApprovalStage> pms_JobReqApprovalStage { get; set; }
        public virtual DbSet<pms_JobStatisticSetting> pms_JobStatisticSetting { get; set; }
        public virtual DbSet<pms_JobType> pms_JobType { get; set; }
        public virtual DbSet<pms_MaterialCatalogue> pms_MaterialCatalogue { get; set; }
        public virtual DbSet<pms_MaterialCatalogueSection> pms_MaterialCatalogueSection { get; set; }
        public virtual DbSet<pms_Object> pms_Object { get; set; }
        public virtual DbSet<pms_Object_Counter> pms_Object_Counter { get; set; }
        public virtual DbSet<pms_Object_File> pms_Object_File { get; set; }
        public virtual DbSet<pms_Object_OldCopy> pms_Object_OldCopy { get; set; }
        public virtual DbSet<pms_Object_Spare> pms_Object_Spare { get; set; }
        public virtual DbSet<pms_ObjectCodePart> pms_ObjectCodePart { get; set; }
        public virtual DbSet<pms_OperatingTime> pms_OperatingTime { get; set; }
        public virtual DbSet<pms_OrderItemHistory> pms_OrderItemHistory { get; set; }
        public virtual DbSet<pms_Partition> pms_Partition { get; set; }
        public virtual DbSet<pms_ProposeItem> pms_ProposeItem { get; set; }
        public virtual DbSet<pms_RepairList> pms_RepairList { get; set; }
        public virtual DbSet<pms_RepairList_File> pms_RepairList_File { get; set; }
        public virtual DbSet<pms_RepairListApprovalStage> pms_RepairListApprovalStage { get; set; }
        public virtual DbSet<pms_RepairListApproveTable> pms_RepairListApproveTable { get; set; }
        public virtual DbSet<pms_RepairListItem> pms_RepairListItem { get; set; }
        public virtual DbSet<pms_RepairListItem_File> pms_RepairListItem_File { get; set; }
        public virtual DbSet<pms_RepairListMeasureUnit> pms_RepairListMeasureUnit { get; set; }
        public virtual DbSet<pms_RepairListPartition> pms_RepairListPartition { get; set; }
        public virtual DbSet<pms_RepairListRank> pms_RepairListRank { get; set; }
        public virtual DbSet<pms_RepairListSpare> pms_RepairListSpare { get; set; }
        public virtual DbSet<pms_RepairListSpare_File> pms_RepairListSpare_File { get; set; }
        public virtual DbSet<pms_RepairPlan> pms_RepairPlan { get; set; }
        public virtual DbSet<pms_RepairPlanRepairType> pms_RepairPlanRepairType { get; set; }
        public virtual DbSet<pms_RepairPlanTableManage> pms_RepairPlanTableManage { get; set; }
        public virtual DbSet<pms_Requisition> pms_Requisition { get; set; }
        public virtual DbSet<pms_Requisition_File> pms_Requisition_File { get; set; }
        public virtual DbSet<pms_Requisition_InnerInfo> pms_Requisition_InnerInfo { get; set; }
        public virtual DbSet<pms_RequisitionDepartment> pms_RequisitionDepartment { get; set; }
        public virtual DbSet<pms_RequisitionDestination> pms_RequisitionDestination { get; set; }
        public virtual DbSet<pms_RequisitionRank> pms_RequisitionRank { get; set; }
        public virtual DbSet<pms_RequisitionType> pms_RequisitionType { get; set; }
        public virtual DbSet<pms_SFI> pms_SFI { get; set; }
        public virtual DbSet<pms_Spare_File> pms_Spare_File { get; set; }
        public virtual DbSet<pms_Spare_Ship> pms_Spare_Ship { get; set; }
        public virtual DbSet<pms_SpareLib> pms_SpareLib { get; set; }
        public virtual DbSet<pms_SpareLib_File> pms_SpareLib_File { get; set; }
        public virtual DbSet<pms_Sparelib_InnerInfo> pms_Sparelib_InnerInfo { get; set; }
        public virtual DbSet<pms_SpareLib_OldCopy> pms_SpareLib_OldCopy { get; set; }
        public virtual DbSet<pms_SpareManufacturerCatalog> pms_SpareManufacturerCatalog { get; set; }
        public virtual DbSet<pms_SpareOrderItem> pms_SpareOrderItem { get; set; }
        public virtual DbSet<pms_SpareOrderItem_File> pms_SpareOrderItem_File { get; set; }
        public virtual DbSet<pms_SpareOrderItem_InnerInfo> pms_SpareOrderItem_InnerInfo { get; set; }
        public virtual DbSet<pms_SpareReqApprovalStage> pms_SpareReqApprovalStage { get; set; }
        public virtual DbSet<pms_StoreInventoryRecord> pms_StoreInventoryRecord { get; set; }
        public virtual DbSet<pms_StoreItem> pms_StoreItem { get; set; }
        public virtual DbSet<pms_StoreLocation> pms_StoreLocation { get; set; }
        public virtual DbSet<pms_StoreReserve> pms_StoreReserve { get; set; }
        public virtual DbSet<pms_StoreReserveForJobPlan> pms_StoreReserveForJobPlan { get; set; }
        public virtual DbSet<pms_StoreSection> pms_StoreSection { get; set; }
        public virtual DbSet<pms_TechPlace> pms_TechPlace { get; set; }
        public virtual DbSet<pms_Template> pms_Template { get; set; }
        public virtual DbSet<pms_TGroup> pms_TGroup { get; set; }
        public virtual DbSet<pms_TJob> pms_TJob { get; set; }
        public virtual DbSet<pms_TJob_File> pms_TJob_File { get; set; }
        public virtual DbSet<pms_TJob_Spare> pms_TJob_Spare { get; set; }
        public virtual DbSet<pms_TJobPeriod> pms_TJobPeriod { get; set; }
        public virtual DbSet<pms_TObject> pms_TObject { get; set; }
        public virtual DbSet<pms_TObject_File> pms_TObject_File { get; set; }
        public virtual DbSet<pms_TObject_Spare> pms_TObject_Spare { get; set; }
        public virtual DbSet<pms_TRepairList> pms_TRepairList { get; set; }
        public virtual DbSet<pms_TRepairList_File> pms_TRepairList_File { get; set; }
        public virtual DbSet<pms_TRepairListItem> pms_TRepairListItem { get; set; }
        public virtual DbSet<pms_TRepairListItem_File> pms_TRepairListItem_File { get; set; }
        public virtual DbSet<pms_TRepairListSpare> pms_TRepairListSpare { get; set; }
        public virtual DbSet<pms_TRepairListSpare_File> pms_TRepairListSpare_File { get; set; }
        public virtual DbSet<pms_User_Partition> pms_User_Partition { get; set; }
        public virtual DbSet<prs_CheckList> prs_CheckList { get; set; }
        public virtual DbSet<prs_CheckListItem> prs_CheckListItem { get; set; }
        public virtual DbSet<prs_DocumentType> prs_DocumentType { get; set; }
        public virtual DbSet<prs_PersonCard> prs_PersonCard { get; set; }
        public virtual DbSet<prs_PersonCard_File> prs_PersonCard_File { get; set; }
        public virtual DbSet<prs_PersonCard_InnerInfo> prs_PersonCard_InnerInfo { get; set; }
        public virtual DbSet<prs_PersonCard_ShipType> prs_PersonCard_ShipType { get; set; }
        public virtual DbSet<prs_PersonCardPhoto> prs_PersonCardPhoto { get; set; }
        public virtual DbSet<prs_PersonContract> prs_PersonContract { get; set; }
        public virtual DbSet<prs_PersonContract_File> prs_PersonContract_File { get; set; }
        public virtual DbSet<prs_PersonDocument> prs_PersonDocument { get; set; }
        public virtual DbSet<prs_PersonDocument_File> prs_PersonDocument_File { get; set; }
        public virtual DbSet<prs_Qualification> prs_Qualification { get; set; }
        public virtual DbSet<prs_СrewСhangePlan> prs_СrewСhangePlan { get; set; }
    }
}
