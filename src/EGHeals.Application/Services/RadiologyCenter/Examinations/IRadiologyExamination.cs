using EGHeals.Application.Dtos.RadiologyCenter.Examinations.Responses;

namespace EGHeals.Application.Services.RadiologyCenter.Examinations
{
    public interface IRadiologyExaminationCostQueryService
    {
        Task<IEnumerable<ExaminationResponseDto>> GetExaminationsCostsByOwnershipAsync(QueryOptions<ExaminationResponseDto> options,
                                                                                           CancellationToken cancellationToken = default);
        Task<long> GetExaminationsCostsCountByOwnershipAsync(QueryFilters<ExaminationResponseDto> filters,
                                                             CancellationToken cancellationToken = default);
    }
}
