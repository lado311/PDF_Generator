using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PdfGenerator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        private readonly PdfGeneratorService _pdfGeneratorService;

        public PdfController(PdfGeneratorService pdfGeneratorService)
        {
            _pdfGeneratorService = pdfGeneratorService;
        }

        [HttpPost("generate")]
        public IActionResult GenerateEstimate([FromBody] PdfRequest request)
        {
            var pdf = _pdfGeneratorService.GeneratePdf(request.Title, request.Amount, request.IncludeVat,request.PdfDescription);
            return File(pdf, "application/pdf", "Estimate.pdf");
        }
    }
}
