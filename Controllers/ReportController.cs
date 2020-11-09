using Microsoft.AspNetCore.Mvc;
using BookingServices.Models;
using BookingServices.Dao;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using HooksLearning.Common;
using System;
using System.Collections.Generic;
using Aspose.Cells;

namespace BookingServices.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ReportController : BaseAccountController
    {
        public ReportController(BookingServicesContext bookingServicesContext) : base(bookingServicesContext) { }

        [Authorize]
        [HttpGet]
        public async Task<ReturnObject> CommentList()
        {
            ReturnObject obj = new ReturnObject();
            DaoComment daoComment = new DaoComment(bookingServicesContext);
            try
            {
                var data = daoComment.GetAll();
                ReportManager rptManager = new ReportManager();
                Workbook wb = rptManager.GetReportExcel("CommentListTemplate.xlsx");
                Worksheet ws = wb.Worksheets[0];
                if (data.Count > 0)
                {
                    for (int i = 0; i < data.Count; i++)
                    {
                        int j = 3 + i;
                        ws.Cells["A" + j].PutValue(i + 1);
                        ws.Cells["B" + j].PutValue(data[i].Id);
                        ws.Cells["C" + j].PutValue(data[i].ServiceType);
                        ws.Cells["D" + j].PutValue(data[i].Rate);
                        ws.Cells["E" + j].PutValue(data[i].Content);
                        ws.Cells["F" + j].PutValue(data[i].CreatedAt);
                    }
                }
                ws.AutoFitColumns();
                obj.data = rptManager.SaveTmpExcel(wb, HttpContext, "CommentList");
            }
            catch (Exception ex)
            {
                obj = Util.returnError(ex);
            }
            return obj;
        }
    }
}
