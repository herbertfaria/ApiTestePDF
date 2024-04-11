using System.Text;
using BenchmarkDotNet.Running;
using Pdf;

internal class Program
{
    public static void Main(string[] args) => BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
    // private static void Main(string[] args)
    // {
    //     string html = File.ReadAllText(@"D:\Download\ApiTestePDF\Performance\Tests\exemplo.html");
    //     Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    //     PdfService _pdfService = new PdfService();
    //     using (var fs = new FileStream(@"D:\Download\ApiTestePDF\Performance\Tests\GeneratePdfFromHtml.pdf", FileMode.Create, FileAccess.Write))
    //     {
    //         var byteArray = _pdfService.GeneratePdfFromHtml(html);
    //         fs.Write(byteArray, 0, byteArray.Length);
    //     }
    //     using (var fs = new FileStream(@"D:\Download\ApiTestePDF\Performance\Tests\GeneratePdfFromHtmliText.pdf", FileMode.Create, FileAccess.Write))
    //     {
    //         var byteArray = _pdfService.GeneratePdfFromHtmliText(html);
    //         fs.Write(byteArray, 0, byteArray.Length);
    //     };

    //     using (var fs = new FileStream(@"D:\Download\ApiTestePDF\Performance\Tests\GeneratePdfFromHtmlAbcpdf.pdf", FileMode.Create, FileAccess.Write))
    //     {
    //         var byteArray = _pdfService.GeneratePdfFromHtmlAbcpdf(html);
    //         fs.Write(byteArray, 0, byteArray.Length);
    //     };

    //     using (var fs = new FileStream(@"D:\Download\ApiTestePDF\Performance\Tests\GeneratePdfFromHtmlWkHtmlToPdf.pdf", FileMode.Create, FileAccess.Write))
    //     {
    //         var byteArray = _pdfService.GeneratePdfFromHtmlWkHtmlToPdf(html);
    //         fs.Write(byteArray, 0, byteArray.Length);
    //     };
    // }
}