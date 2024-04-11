using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Pdf;
using System.Text;

public class PdfGenerationController : ControllerBase
{
    private readonly PdfService _pdfService;

    public PdfGenerationController(PdfService pdfService)
    {
        _pdfService = pdfService;
    }

    [HttpPost]
    [Route("api/pdfgeneration/post")]
    public IActionResult GeneratePdfWithAttachments(string htmlPDF, string[] othersPDFs, string callBackUrl)
    {
        try
        {
            // Gere um ID único para esta solicitação
            string requestId = Guid.NewGuid().ToString();

            // Inicie a geração do PDF em uma nova tarefa assíncrona
            _ = Task.Run(async () =>
            {
                // Simule um processo de geração de PDF
                await Task.Delay(5000); // Simula uma operação demorada

                // Gere o PDF a partir de um HTML
                var pdfFile = _pdfService.GeneratePdfFromHtml(htmlPDF); // ("<h1>Exemplo de PDF gerado a partir de HTML</h1>");

                // Adicione outros PDFs
                if (othersPDFs.Length > 0)
                {
                    pdfFile = _pdfService.AddPdfsToPdf(pdfFile, othersPDFs);
                }
                // Após a conclusão, notifique o cliente sobre o status atual
                NotifyClient(requestId, "completed", callBackUrl, pdfFile);

            });

            // Retorna um objeto com o ID de execução para acompanhar o status
            return Ok(new { RequestId = requestId });
        }
        catch (Exception ex)
        {
            // Em caso de erro, retorne um erro interno do servidor
            return Problem(ex.Message);
        }
    }

    [HttpGet]
    [Route("api/pdfgeneration/status/{requestId}")]
    public IActionResult GetStatus(string requestId)
    {
        // Implemente a lógica para recuperar o status da geração do PDF com base no ID da solicitação
        // Por momento estamos retornando um status fixo
        return Ok(new { RequestId = requestId, Status = "completed" });
    }    

    private void NotifyClient(string requestId, string status, string callbackUrl, byte[] payload)
    {
        //implementar a lógica para notificar o cliente sobre o status atual com base no callbackUrl
        //var client = new HttpClient();
        //var content = new StringContent(JsonConvert.SerializeObject(new { RequestId = requestId, Status = status, Payload = payload }), Encoding.UTF8, "application/json");
        //var response = client.PostAsync(callbackUrl, content).Result;

        ///Apenas logamos as informações e salvamos o arquivo localmente 
        Console.WriteLine($"Status update for request ID '{requestId}': {status}. Callback URL: {callbackUrl}");
        Console.WriteLine($"Payload: {Encoding.UTF8.GetString(payload)}");
        System.IO.File.WriteAllBytes($"c:\\temp\\{requestId}.pdf", payload);
    }
}
