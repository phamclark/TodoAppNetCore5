using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Stimulsoft.Report;
using Stimulsoft.Report.Angular;
using Stimulsoft.Report.Mvc;
using Stimulsoft.Report.Web;
using TodoApp.Models.DTOs;

namespace TodoApp.Controllers
{
    public class StimulsoftReportController : Controller
    {
        [HttpPost("InitStimulsoftReport")]
        public IActionResult InitStimulsoftReport()
        {
            var requestParams = StiNetCoreViewer.GetRequestParams(this);
            var options = new StiAngularViewerOptions
            {
                Actions = {ViewerEvent = "ViewerEvent"}, 
                Theme = StiViewerTheme.Office2010Silver
            };
            return StiAngularViewer.ViewerDataResult(requestParams, options);
        }
        
        //ViewerEvent() that will process viewer requests.
        [HttpPost("ViewerEvent")] 
        public IActionResult ViewerEvent([FromBody]List<CartItemDto> carts)
        {
            var requestParams = StiNetCoreViewer.GetRequestParams(this);
            if (requestParams.Action != StiAction.GetReport) return StiNetCoreViewer.ProcessRequestResult(this);
            var report = StiReport.CreateNewReport();
            var path = StiAngularHelper.MapPath(this, $"Reports/EmployeeReport.mrt");
            report.Load(path);
                
            //Data mapping
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(carts);
            var pDt = JsonConvert.DeserializeObject<DataTable>(json);
            report.RegData("OrderCarts", pDt);
            return StiNetCoreViewer.GetReportResult(this, report);
        }
    }
}