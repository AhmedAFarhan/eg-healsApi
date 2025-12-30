using EGHeals.Application.Dtos.RadiologyCenter.Examinations.Responses;
using EGHeals.Domain.Models.RadiologyCenter.Examinations;

namespace EGHeals.Application.Features.RadiologyCenter.Examinations.Queries.GetExaminations
{
    public record GetExaminationsQuery(QueryOptions<RadiologyCenter_ExaminationCost> QueryOptions) : IQuery<GetExaminationsResult>;
    public record GetExaminationsResult(EGResponse<PaginatedResult<ExaminationResponseDto>> Response);

}
