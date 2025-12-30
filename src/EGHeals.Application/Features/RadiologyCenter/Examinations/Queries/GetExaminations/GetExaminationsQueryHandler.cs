using EGHeals.Application.Dtos.RadiologyCenter.Examinations.Responses;
using EGHeals.Application.Features.RadiologyCenter.Examinations.Queries.GetExaminations;
using EGHeals.Application.Services.RadiologyCenter.Examinations;

namespace EGHeals.Application.Features.RadiologyCenter.Examinations.Queries.GetExaminationsByOwnership
{
    public class GetExaminationsQueryHandler(/*IRadiologyExaminationCostQueryService examinationService*/) : IQueryHandler<GetExaminationsQuery, GetExaminationsResult>
    {
        public async Task<GetExaminationsResult> Handle(GetExaminationsQuery query, CancellationToken cancellationToken)
        {
            //var examinations = await examinationService.GetExaminationsCostsByOwnershipAsync(options: query.QueryOptions,
            //                                                                                 cancellationToken: cancellationToken);

            //var totalCount = await examinationService.GetExaminationsCostsCountByOwnershipAsync(filters: query.QueryOptions.QueryFilters,
            //                                                                                    cancellationToken: cancellationToken);

            //var pagination = new PaginatedResult<RadiologyExaminationResponseDto>(query.QueryOptions.PageIndex, query.QueryOptions.PageSize, totalCount, examinations);

            //var response = EGResponseFactory.Success(pagination, "Success operation.");

            //return new GetExaminationsResult(response);

            throw new NotImplementedException();
        }
    }
}
