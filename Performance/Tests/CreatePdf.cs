using System.Text;
using BenchmarkDotNet.Attributes;
using Pdf;

namespace Performance;
[Config(typeof(AntiVirusConfig))]
[MemoryDiagnoser]
public class CreatePdf
{
    string html = "";
    PdfService _pdfService = new PdfService();


    [Params(1)]
    public int N;

    [GlobalSetup]
    public void Setup()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        html = File.ReadAllText(@"D:\Download\ApiTestePDF\Performance\Tests\exemplo.html");
    }

    [Benchmark]
    public void GeneratePdfFromHtml()
    {
        for (int i = 0; i < N; i++)
        {
            var t = _pdfService.GeneratePdfFromHtml(html);
        }
    }

    // [Benchmark]
    // public void GeneratePdfFromHtmlAbcpdf()
    // {
    //     for (int i = 0; i < N; i++)
    //     {
    //         var t = _pdfService.GeneratePdfFromHtmlAbcpdf(html);
    //     }
    // }
    [Benchmark]
    public void GeneratePdfFromHtmliText()
    {
        for (int i = 0; i < N; i++)
        {
            var t = _pdfService.GeneratePdfFromHtmliText(html);
        }
    }
    [Benchmark]
    public void GeneratePdfFromHtmlWkHtmlToPdf()
    {
        for (int i = 0; i < N; i++)
        {
            var t = _pdfService.GeneratePdfFromHtmlWkHtmlToPdf(html);
        }
    }
}
