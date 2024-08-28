namespace PdfGenerator
{
    public class PdfRequest
    {
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public bool IncludeVat { get; set; }
        public string PdfDescription { get; set; }
    }
}
