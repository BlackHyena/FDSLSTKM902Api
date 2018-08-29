
using FoodMaster.Models;
using FoodTransaction;
using Newtonsoft.Json;
using StdMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FDSLSTKM902Api.Controllers
{
    public class FDSLSTKM902Controller : ApiController
    {

        [HttpGet]
        [Route("GetSwineInfoTmps/{moduleCode}/{subModuleCode}/{orgCode}/")]
        public HttpResponseMessage GetSwineInfoTmps(string moduleCode, string subModuleCode, string orgCode)
        {
            try
            {
                var swineInfoColl = FDSLSTKM902Function.GetSwineInfoTmps(moduleCode, subModuleCode, orgCode);
                return Request.CreateResponse(HttpStatusCode.OK, swineInfoColl); ;
            }
            catch (Exception err)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, err);
            }
        }

        //[HttpGet]
        //[Route("GetLicensePlatesWithQueue/{orgCode}")]
        //public HttpResponseMessage GetLicensePlatesWithQueue(string orgCode)
        //{
        //    try
        //    {
        //        List<LicensePlate> licensePlates = FDSLSTKM902Function.GetLicensePlatesWithQueue(orgCode);
        //        return Request.CreateResponse(HttpStatusCode.OK, licensePlates);

        //    }
        //    catch (Exception err)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, err);
        //    }
        //}

        /// <summary>
        /// Get GetSwineInfo Data
        /// </summary>
        /// <param name="orgCode"></param>
        /// <param name="moduleCode"></param>
        /// <param name="subModuleCode"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSwineInfos/{moduleCode}/{subModuleCode}/{orgCode}")]
        public HttpResponseMessage GetSwineInfos(string moduleCode, string subModuleCode, string orgCode)
        {
            try
            {
                List<FdSwineInfo> fdSwineInfos = FDSLSTKM902Function.GetSwineInfo(orgCode, moduleCode, subModuleCode);
                return Request.CreateResponse(HttpStatusCode.OK, fdSwineInfos);
                //if (fdSwineInfos == null)

                //{
                //    throw new Exception($"Not found data with OrgCode:{orgCode}");
                //}
                //else
                //{
                //    return Request.CreateResponse(HttpStatusCode.OK, fdSwineInfos);
                //}
            }
            catch (Exception err)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, err);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moduleCode"></param>
        /// <param name="subModuleCode"></param>
        /// <param name="orgCode"></param>
        /// <param name="licensePlate"></param>
        /// <param name="farmCode"></param>
        /// <param name="refDocNo"></param>
        /// <returns>True=Existing, False=AddNew, Error=Complete</returns>
        [HttpGet]
        [Route("GetChkDataSwineTmp/{moduleCode}/{subModuleCode}/{orgCode}/{licensePlate}/{farmCode}/{refDocNo}")]
        public HttpResponseMessage GetChkDataSwineTmp(string moduleCode, string subModuleCode, string orgCode, string licensePlate, string farmCode, string refDocNo)
        {
            try
            {

                var swineInfoTmp = FDSLSTKM902Function.GetChkDataSwineTmp(moduleCode, subModuleCode, orgCode, licensePlate, farmCode, refDocNo);
                return Request.CreateResponse(HttpStatusCode.OK, swineInfoTmp);
            }
            catch (Exception err)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, err);
            }
        }

        //[HttpGet]
        //[Route("GetChkDataSwineTmp/{moduleCode}/{subModuleCode}/{orgCode}/{licensePlate}/{farmCode}/{refDocNo}")]
        //public HttpResponseMessage GetChkDataSwineTmp(string moduleCode, string subModuleCode,string orgCode, string licensePlate, string farmCode, string refDocNo)
        //{
        //    try
        //    {
        //        var serialFromLicense = FDSLSTKM902Function.GetSerialFromLicense(orgCode, licensePlate);
        //        var chkDataSwineTmp = FDSLSTKM902Function.GetChkDataSwineTmp(orgCode, serialFromLicense, farmCode, refDocNo, moduleCode, subModuleCode);
        //        return Request.CreateResponse(HttpStatusCode.OK, chkDataSwineTmp); ;
        //    }
        //    catch (Exception err)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest, err);
        //    }
        //}

        /// <summary>
        /// Get SwineInfoTemp
        /// </summary>
        /// <param name="moduleCode"></param>
        /// <param name="subModuleCode"></param>
        /// <param name="orgCode"></param>
        /// <param name="licensePlate"></param>
        /// <param name="farmCode"></param>
        /// <param name="refDocNo"></param>
        /// <returns>Error =Duplication RefDocNo,Null=New,Not Null=Existing</returns>
        [HttpGet]
        [Route("GetSwineInfoTmp/{moduleCode}/{subModuleCode}/{orgCode}/{licensePlate}/{farmCode}/{refDocNo}")]
        public HttpResponseMessage GetSwineInfoTmp(string moduleCode, string subModuleCode, string orgCode,
            string licensePlate, string farmCode, string refDocNo)
        {
            try
            {
                var serialFromLicense = FDSLSTKM902Function.GetSerialFromLicense(orgCode, licensePlate);
                var swineInfoTmp = FDSLSTKM902Function.GetSwineInfoTmp(orgCode, serialFromLicense, farmCode, refDocNo, moduleCode, subModuleCode);

                if (swineInfoTmp == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, swineInfoTmp);
                }
                else
                {
                    if (swineInfoTmp.CompleteStatus == "Y")
                    {
                        throw new Exception("M902MO_015");
                        //throw new Exception("ไม่สามารถรับหมูเป็นของเลขที่ใบส่งสินค้านี้ได้ เนื่องจากเอกสารใบส่งสินค้านี้จบคิวเรียบร้อยแล้ว");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, swineInfoTmp);
                    }
                }
            }
            catch (Exception err)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, err);
            }
        }
        //[HttpGet]
        //[Route("GetSwineInfoTmpEdit")]
        //public HttpResponseMessage GetSwineInfoTmpEdit([FromUri]string orgCode, string licensePlate, string farmCode, string refDocNo, string queueInNo, string swineNo, string moduleCode, string subModuleCode)
        //{
        //    try
        //    {
        //        var serialFromLicense = FDSLSTKM902Function.GetSerialFromLicense(orgCode, licensePlate);
        //        var swineInfoTmp = FDSLSTKM902Function.GetSwineInfoTmpEdit(orgCode, serialFromLicense, farmCode, refDocNo, queueInNo, swineNo, moduleCode, subModuleCode);
        //        return Request.CreateResponse(HttpStatusCode.OK, swineInfoTmp); ;
        //    }
        //    catch (Exception err)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest, err);
        //    }
        //}

        /// <summary>
        /// To Insert SwineInfoTmp
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SetSwineInfoTmp")]
        public HttpResponseMessage SetSwineInfoTmp(FdSwineInfoRequest request)
        {
            try
            {
                GlobalFunction.SetMobileRequest("SetSwineInfoTmp", JsonConvert.SerializeObject(request), request.UserId);
                var menuParam = StdHelper.GetMenuParam(request.ModuleCode, request.SubModuleCode, MenuType.M902MO);
                DateTime processDate = GlobalFunction.GetProcessDate(request.PlantCode, menuParam.pmLocation);

                FdSwineInfo fdSwineInfo = new FdSwineInfo();
                fdSwineInfo.PlantCode = request.PlantCode;
                fdSwineInfo.DocumentDate = processDate;
                fdSwineInfo.License = new LicensePlate();
                fdSwineInfo.License.License = request.License;
                fdSwineInfo.Farm = new Farm();
                fdSwineInfo.Farm.FarmCode = request.FarmCode;
                fdSwineInfo.RefDocNo = request.RefDocNo;
                fdSwineInfo.QueuePlanDate = processDate;
                fdSwineInfo.QueuePlanItem = request.QueuePlanItem;
                fdSwineInfo.QueuePlanNo = request.QueuePlanNo;
                fdSwineInfo.UserId = request.UserId;

                //Check Serial Queue 
                var queueSerial = FDSLSTKM902Function.GetQueueSerial(fdSwineInfo);
                if (queueSerial == null)
                {
                    queueSerial = new FmQueueSerial();
                    queueSerial.SerialNo = FDSLSTKM902Function.GetSerialNo(fdSwineInfo);
                    queueSerial.QueueNo = FDSLSTKM902Function.GetQueueNo(fdSwineInfo, menuParam);
                    //for fm queue 
                    FDSLSTKM902Function.SaveQueue(fdSwineInfo, queueSerial, menuParam);
                }


                fdSwineInfo.Items = new List<FdSwineInfoItem>();
                foreach (var item in request.Items)
                {
                    FdSwineInfoItem fdSwineInfoItem = new FdSwineInfoItem();
                    fdSwineInfoItem.Breeder = new Breeder();
                    fdSwineInfoItem.Breeder.BreederCode = item.BreederCode;
                    fdSwineInfoItem.SexFlag = item.SexFlag;
                    fdSwineInfoItem.SwineQty = item.SwineQty;
                    fdSwineInfoItem.SwineWgh = item.SwineWgh;
                    fdSwineInfoItem.FarmQty = item.FarmQty;
                    fdSwineInfoItem.FarmWgh = item.FarmWgh;
                    fdSwineInfoItem.RemarkCode = new SwineRemark();
                    fdSwineInfoItem.RemarkCode.RemarkCode = item.RemarkCode;

                    if (item.SwineQty > item.FarmQty)
                    {
                        throw new Exception("M902MO_020");
                        //throw new Exception("ไม่สามารถบันทึกข้อมูลได้ เนื่องจากปริมาณหน้าโรงมากกว่าปริมาณหน้าฟาร์ม "); 
                    }

                    if (menuParam.pmContainerType != "")
                    {
                        var minmaxWeight = FDSLSTKM902Function.GetMinMaxWeight(fdSwineInfo.PlantCode, item.BreederCode, menuParam);
                        if (minmaxWeight == null)
                        {
                            throw new Exception("M902MO_017");
                            //throw new Exception("ไม่พบข้อมูล Breeder เพื่อหา Min Max Weight ที่ GD3_SWINE_BREEDER"); 
                        }
                        var swineMinWeight = (minmaxWeight.SwineMinWgh * item.SwineQty);
                        var swineMaxWeight = (minmaxWeight.SwineMaxWgh * item.SwineQty);

                        if (item.SwineWgh < swineMinWeight)
                        {
                            throw new Exception("M902MO_018");
                            //throw new Exception("น้ำหนักรับหมูเป็น < มาตราฐานที่กำหนดไว้ (MinWeight) กรุณาทำการชั่งใหม่"); 
                        }
                        if (item.SwineWgh > swineMaxWeight)
                        {
                            throw new Exception("M902MO_019");
                            //throw new Exception("น้ำหนักรับหมูเป็น > มาตราฐานที่กำหนดไว้ (MaxWeight) กรุณาทำการชั่งใหม่"); 
                        }
                    }

                    var productCode = FDSLSTKM902Function.GetProductInStock(fdSwineInfo.PlantCode, item.BreederCode, menuParam);
                    if (productCode == "")
                    {
                        throw new Exception("M902MO_001");
                        //throw new Exception("ไม่พบรหัสสินค้าสำหรับบันทึก Stock รบกวนตรวจสอบการ Map สินค้า กับ ประเภทสุกรที่ Table : GD2_SWINE_BREEDER_CONFIG");
                    }
                    fdSwineInfoItem.ProductSwine = new ProductInfo();
                    fdSwineInfoItem.ProductSwine.ProductCode = productCode;
                    var brandCode = FDSLSTKM902Function.GetBrandInfo(fdSwineInfo.PlantCode, productCode);

                    if (brandCode == "")
                    {
                        throw new Exception("M902MO_002");
                        //throw new Exception("ไม่พบ Spec Code ของสินค้านี้ กรุณาตรวจสอบการ Set Up สินค้า (FM_BRAND_INFO)");
                    }
                    fdSwineInfoItem.ProductSwine.BrandCode = brandCode;

                    //for tmp  table
                    var queueInNo = FDSLSTKM902Function.GetQueueInNo(fdSwineInfo.PlantCode,
                        fdSwineInfo.DocumentDate,
                        fdSwineInfo.Farm.FarmCode,
                        fdSwineInfo.RefDocNo,
                        item.BreederCode,
                        queueSerial,
                        menuParam);
                    if (queueInNo == "")
                    {
                        queueInNo = FDSLSTKM902Function.GetFindQueue(fdSwineInfo, menuParam);
                    }
                    fdSwineInfoItem.QueueInNo = queueInNo;

                    int swineSeq = FDSLSTKM902Function.GetSwineSeqTmp(fdSwineInfo.PlantCode,
                        fdSwineInfo.DocumentDate,
                        fdSwineInfo.Farm.FarmCode,
                        fdSwineInfo.RefDocNo,
                        fdSwineInfoItem.QueueInNo,
                        queueSerial.SerialNo.ToString());
                    var swineNo = swineSeq.ToString().PadLeft(3, '0');
                    fdSwineInfoItem.SwineSeq = swineSeq;
                    fdSwineInfoItem.SwineNo = swineNo;



                    var ret = FDSLSTKM902Function.GetSumSwineTmp(fdSwineInfo.PlantCode, fdSwineInfo.DocumentDate, queueSerial.SerialNo.ToString(),  fdSwineInfoItem.QueueInNo);
                    if (ret != null)
                    {
                        var sumSwineQty = ret.SwineQty;
                        var sumFarmQty = ret.FarmQty;
                        if (item.SwineQty + sumSwineQty > sumFarmQty)
                        {
                            throw new Exception("M902MO_021");
                            //throw new Exception("ปริมาณรับหมูเป็นทั้งหมด มากกว่า ปริมาณหมูจากฟาร์ม กรุณาระบุปริมาณอีกครั้ง");
                        }
                    }
                    fdSwineInfo.Items.Add(fdSwineInfoItem);
                }
                FDSLSTKM902Function.SaveTmp(fdSwineInfo, queueSerial, menuParam);
                FDSLSTKM902Function.UpdateQueueControl(fdSwineInfo);

                //ต้องไปอยู่ใน setswineinfo เพราะต้องกดจบคิวก่อน 
                //var sumDataSwines = FDSLSTKM902Function.GetSumDataSwine(fdSwineInfo, queueSerial, menuParam);
                //var compSumDataSwines = FDSLSTKM902Function.PrepareSwineInfo(sumDataSwines);
                //FDSLSTKM902Function.SaveFinishQueue(compSumDataSwines, menuParam);

                //FDSLSTKM902Function.UpdateStatusTmp(fdSwineInfo, queueSerial);
                //

                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception err)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, err);
            }
        }

        /// <summary>
        /// To Update of SwineInfoTmp
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SetUpdateSwineInfoTmp")]
        public HttpResponseMessage SetUpdateSwineInfoTmp(FdSwineInfoRequest request)
        {
            try
            {
                GlobalFunction.SetMobileRequest("SetUpdateSwineInfoTmp", JsonConvert.SerializeObject(request), request.UserId);
                var menuParam = StdHelper.GetMenuParam(request.ModuleCode, request.SubModuleCode, MenuType.M902MO);
                DateTime processDate = GlobalFunction.GetProcessDate(request.PlantCode, menuParam.pmLocation);

                FdSwineInfo fdSwineInfo = new FdSwineInfo();
                fdSwineInfo.PlantCode = request.PlantCode;
                fdSwineInfo.DocumentDate = processDate;
                fdSwineInfo.License = new LicensePlate();
                fdSwineInfo.License.License = request.License;
                fdSwineInfo.Farm = new Farm();
                fdSwineInfo.Farm.FarmCode = request.FarmCode;
                fdSwineInfo.RefDocNo = request.RefDocNo;
                fdSwineInfo.QueuePlanDate = processDate;
                fdSwineInfo.QueuePlanItem = request.QueuePlanItem;
                fdSwineInfo.QueuePlanNo = request.QueuePlanNo;
                fdSwineInfo.UserId = request.UserId;
                fdSwineInfo.Serial = new FmQueueSerial();
                fdSwineInfo.Serial.SerialNo = long.Parse(request.Serial);
                fdSwineInfo.Items = new List<FdSwineInfoItem>();
                foreach (var item in request.Items)
                {

                    if (item.SwineQty > item.FarmQty)
                    {
                        throw new Exception("M902MO_020");
                        //throw new Exception("ไม่สามารถบันทึกข้อมูลได้ เนื่องจากปริมาณหน้าโรงมากกว่าปริมาณหน้าฟาร์ม "); 
                    }

                    if (menuParam.pmContainerType != "")
                    {
                        var minmaxWeight = FDSLSTKM902Function.GetMinMaxWeight(fdSwineInfo.PlantCode, item.BreederCode, menuParam);
                        if (minmaxWeight == null)
                        {
                            throw new Exception("M902MO_017");
                            //throw new Exception("ไม่พบข้อมูล Breeder เพื่อหา Min Max Weight ที่ GD3_SWINE_BREEDER"); 
                        }
                        var swineMinWeight = (minmaxWeight.SwineMinWgh * item.SwineQty);
                        var swineMaxWeight = (minmaxWeight.SwineMaxWgh * item.SwineQty);

                        if (item.SwineWgh < swineMinWeight)
                        {
                            throw new Exception("M902MO_018");
                            //throw new Exception("น้ำหนักรับหมูเป็น < มาตราฐานที่กำหนดไว้ (MinWeight) กรุณาทำการชั่งใหม่"); 
                        }
                        if (item.SwineWgh > swineMaxWeight)
                        {
                            throw new Exception("M902MO_019");
                            //throw new Exception("น้ำหนักรับหมูเป็น > มาตราฐานที่กำหนดไว้ (MaxWeight) กรุณาทำการชั่งใหม่"); 
                        }
                    }

                    FdSwineInfoItem fdSwineInfoItem = new FdSwineInfoItem();
                    fdSwineInfoItem.Breeder = new Breeder();
                    fdSwineInfoItem.Breeder.BreederCode = item.BreederCode;
                    fdSwineInfoItem.SexFlag = item.SexFlag;
                    fdSwineInfoItem.SwineQty = item.SwineQty;
                    fdSwineInfoItem.SwineWgh = item.SwineWgh;
                    fdSwineInfoItem.FarmQty = item.FarmQty;
                    fdSwineInfoItem.FarmWgh = item.FarmWgh;
                    fdSwineInfoItem.RemarkCode = new SwineRemark();
                    fdSwineInfoItem.RemarkCode.RemarkCode = item.RemarkCode;
                    fdSwineInfoItem.QueueInNo = item.QueueInNo;
                    fdSwineInfoItem.SwineNo = item.SwineNo;
                    fdSwineInfo.Items.Add(fdSwineInfoItem);
                }

                var ret = FDSLSTKM902Function.UpdateSwineInfoTmp(fdSwineInfo);
                return Request.CreateResponse(HttpStatusCode.OK, ret);
            }
            catch (Exception err)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, err);
            }
        }

        /// <summary>
        /// To Delete of SwineInfoTmp
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SetDeleteSwineInfoTmp")]
        public HttpResponseMessage SetDeleteSwineInfoTmp(FdSwineInfoRequest request)
        {
            try
            {
                GlobalFunction.SetMobileRequest("SetDeleteSwineInfoTmp", JsonConvert.SerializeObject(request), request.UserId);
                var menuParam = StdHelper.GetMenuParam(request.ModuleCode, request.SubModuleCode, MenuType.M902MO);
                DateTime processDate = GlobalFunction.GetProcessDate(request.PlantCode, menuParam.pmLocation);

                FdSwineInfo fdSwineInfo = new FdSwineInfo();
                fdSwineInfo.PlantCode = request.PlantCode;
                fdSwineInfo.DocumentDate = processDate;
                fdSwineInfo.Serial = new FmQueueSerial();
                fdSwineInfo.Serial.SerialNo = long.Parse(request.Serial);
                fdSwineInfo.Items = new List<FdSwineInfoItem>();
                foreach (var item in request.Items)
                {
                    FdSwineInfoItem fdSwineInfoItem = new FdSwineInfoItem();
                    fdSwineInfoItem.QueueInNo = item.QueueInNo;
                    fdSwineInfoItem.SwineNo = item.SwineNo;
                    fdSwineInfo.Items.Add(fdSwineInfoItem);
                }

                var ret = FDSLSTKM902Function.DeleteSwineInfoTmp(fdSwineInfo);
                return Request.CreateResponse(HttpStatusCode.OK, ret);
            }
            catch (Exception err)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, err);
            }
        }


        [HttpPost]
        [Route("SetSwineInfo")]
        public HttpResponseMessage SetSwineInfo(SwineInfoEndQueueRequest request)
        {
            try
            {
                GlobalFunction.SetMobileRequest("SetSwineInfo", JsonConvert.SerializeObject(request), request.UserId);
                var menuParam = StdHelper.GetMenuParam(request.ModuleCode, request.SubModuleCode, MenuType.M902MO);
                DateTime processDate = GlobalFunction.GetProcessDate(request.PlantCode, menuParam.pmLocation);

                FdSwineInfo fdSwineInfo = new FdSwineInfo();
                fdSwineInfo.PlantCode = request.PlantCode;
                fdSwineInfo.DocumentDate = processDate;
                fdSwineInfo.License = new LicensePlate();
                fdSwineInfo.License.License = request.License;
                fdSwineInfo.Farm = new Farm();
                fdSwineInfo.Farm.FarmCode = request.FarmCode;
                fdSwineInfo.RefDocNo = request.RefDocNo;
                fdSwineInfo.QueuePlanDate = processDate;
                fdSwineInfo.QueuePlanItem = request.QueuePlanItem;
                fdSwineInfo.QueuePlanNo = request.QueuePlanNo;
                fdSwineInfo.UserId = request.UserId;

                //Check Serial Queue 
                var queueSerial = FDSLSTKM902Function.GetQueueSerial(fdSwineInfo);

                var fdSwineInfos = FDSLSTKM902Function.GetSwineInfoSumForEndQueue(fdSwineInfo, queueSerial, menuParam);
                var fmSwineInfos = FDSLSTKM902Function.GetSwineInfoNotSumForEndQueue(fdSwineInfo, queueSerial, menuParam);
                var compSumDataSwines = FDSLSTKM902Function.PrepareSwineInfo(fdSwineInfos);
                var compDataSwines = FDSLSTKM902Function.PrepareSwineInfo(fmSwineInfos);

                FDSLSTKM902Function.SaveFinishQueue(compSumDataSwines, compDataSwines, menuParam);
                FDSLSTKM902Function.UpdateStatusTmp(fdSwineInfo, queueSerial);
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception err)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, err);
            }
        }

    }
}
