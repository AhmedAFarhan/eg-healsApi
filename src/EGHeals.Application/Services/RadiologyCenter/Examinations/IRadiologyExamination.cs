using EGHeals.Application.Dtos.RadiologyCenter.Examinations.Responses;

namespace EGHeals.Application.Services.RadiologyCenter.Examinations
{
    public interface IRadiologyExaminationCostQueryService
    {
        Task<IEnumerable<RadiologyExaminationResponseDto>> GetExaminationsCostsByOwnershipAsync(QueryOptions<RadiologyExaminationResponseDto> options,
                                                                                           CancellationToken cancellationToken = default);
        Task<long> GetExaminationsCostsCountByOwnershipAsync(QueryFilters<RadiologyExaminationResponseDto> filters,
                                                             CancellationToken cancellationToken = default);
    }
}
