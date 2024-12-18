

using MediatR;
using Microsoft.Extensions.Localization;
using ScanDetector.Core.Bases;
using ScanDetector.Core.Features.Report.Commands.Models;
using ScanDetector.Core.Resources;
using ScanDetector.Service.Implementations;

namespace ScanDetector.Core.Features.Report.Commands.Handlers
{
    public class ReportCommandHandler : ResponseHandler,
         IRequestHandler<ReportRequest, BaseResponse<byte[]>>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly PdfReportByQuestPDF _pdfService;
        private readonly ExcelReportByClosedXML _excelService;

        public ReportCommandHandler(ExcelReportByClosedXML excelService, IStringLocalizer<SharedResources> stringLocalizer, PdfReportByQuestPDF pdfService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _pdfService = pdfService;
            _excelService = excelService;
        }

        public async Task<BaseResponse<byte[]>> Handle(ReportRequest request, CancellationToken cancellationToken)
        {


            try
            {
                byte[] res;
                switch (request.ReportType)
                {
                    case Data.Enums.ReportType.Pdf:
                        res = _pdfService.Generate(request.Items, request.Headers, request.Title, request.CultureCode);
                        break;
                    case Data.Enums.ReportType.Excel:
                        res = _excelService.Generate(request.Items, request.Headers, request.Title, request.CultureCode);
                        break;
                    default:
                        return BadRequest<byte[]>("Report type is null");
                }
                return Success(res);
            }
            catch (Exception ex)
            {
                // Handle errors and return appropriate response
                return BadRequest<byte[]>($"{ex.Message}");
            }
        }
    }
}
