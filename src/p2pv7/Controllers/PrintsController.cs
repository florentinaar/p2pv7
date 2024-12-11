using Grpc.Core;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using p2pv7.Services;
using p2pv7.Services.OrderService;

namespace p2pv7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrintsController : ControllerBase
    {
        private readonly IPrintService _printsService;
        private readonly IOrderService _orderService;
        public PrintsController(IPrintService printService, IOrderService orderService)
        {
            _printsService = printService;
            _orderService = orderService;
        }

        [HttpGet("PrintPDF")]
        public FileResult PrintPDF()
        {
            var orders = _orderService.GetAllOrdersToList();
            var constant = _printsService.GenerateDoc(orders);

            return File(constant, "application/vnd", "Test.pdf");
        }


        //public FileResult DownloadFile(string fileName)
        //{
        //    //Build the File Path.
        //    string path = Server.MapPath("~/Files/") + fileName;

        //    //Read the File data into Byte Array.
        //    byte[] bytes = System.IO.File.ReadAllBytes(path);

        //    //Send the File to Download.
        //    return File(bytes, "application/octet-stream", fileName);
        //}

    }
}

