
using iText.Html2pdf;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using WebSupergoo.ABCpdf12;
using Wkhtmltopdf.NetCore;
using WkHtmlToPdfDotNet;

namespace Pdf;

public class PdfService
{
    private static readonly ConverterProperties properties = new ConverterProperties();
    private static readonly SynchronizedConverter converter = new SynchronizedConverter(new PdfTools());
    public byte[] GeneratePdfFromHtml(string htmlContent)
    {
        //Magia para converter HTML em PDF
        using (MemoryStream ms = new MemoryStream())
        {
            using (Document document = new Document())
            {
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                document.Open();
                using (var sr = new StringReader(htmlContent))
                {
                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, sr);
                }
                document.Close();
            }
            // Retorna o conteúdo do MemoryStream como um array de bytes
            return ms.ToArray();
        }
    }
    public byte[] GeneratePdfFromHtmlAbcpdf(string htmlContent)
    {
        //Magia para converter HTML em PDF
        byte[] result;
        using (Doc doc = new Doc())
        {

            doc.AddImageHtml(htmlContent);
            result = doc.GetData();
        }
        return result;
    }

    public byte[] GeneratePdfFromHtmliText(string htmlContent)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            HtmlConverter.ConvertToPdf(htmlContent, ms);
            // Retorna o conteúdo do MemoryStream como um array de bytes
            return ms.ToArray();
        }
    }

    public byte[] GeneratePdfFromHtmlWkHtmlToPdf(string htmlContent)
    {
        var doc = new HtmlToPdfDocument()
        {
            GlobalSettings = {
        ColorMode = ColorMode.Color,
        Orientation = Orientation.Landscape,
        PaperSize = PaperKind.A4Plus,
    },
            Objects = {
        new ObjectSettings() {
            PagesCount = true,
            HtmlContent = htmlContent,
            WebSettings = { DefaultEncoding = "utf-8" },
            HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 }
        }
    }
        };
        return converter.Convert(doc);
    }

    public byte[] AddPdfsToPdf(byte[] basePdf, string[] pdfPathsToAdd)
    {
        var reader = new PdfReader(basePdf);
        using (var outputStream = new MemoryStream())
        {
            var stamper = new PdfStamper(reader, outputStream);
            int totalPages = reader.NumberOfPages;
            //Itera sobre os PDFs a serem adicionados
            foreach (var pdfPath in pdfPathsToAdd)
            {
                var pdfReader = new PdfReader(pdfPath);
                //para cada página do PDF a ser adicionado, insira uma nova página no PDF base a partir da ultima página (totalPages)
                for (var i = 1; i <= pdfReader.NumberOfPages; i++)
                {
                    totalPages++;
                    stamper.InsertPage(totalPages, pdfReader.GetPageSizeWithRotation(i));
                    var pdfContentByte = stamper.GetUnderContent(totalPages);
                    var importedPage = stamper.GetImportedPage(pdfReader, i);
                    pdfContentByte.AddTemplate(importedPage, 0, 0);
                }
            }
            stamper.Close();
            reader.Close();
            return outputStream.ToArray();
        }
    }
}
