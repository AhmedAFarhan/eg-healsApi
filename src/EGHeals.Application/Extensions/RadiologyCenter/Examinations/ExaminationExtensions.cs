using EGHeals.Application.Dtos.RadiologyCenter.Examinations.Responses;
using EGHeals.Domain.Models.RadiologyCenter.Examinations;

namespace EGHeals.Application.Extensions.RadiologyCenter.Examinations
{
    public static class ExaminationExtensions
    {
        public static IEnumerable<RadiologyExaminationResponseDto> ToExaminationsDtos(this IEnumerable<RadiologyCenter_ExaminationCost> examinations)
        {
            return examinations.Select(examination => new RadiologyExaminationResponseDto
            (
                Id: examination.Id.Value,
                Name: examination.RadiologyExamination.Name,
                Cost: examination.Cost
            ));
        }
        public static RadiologyExaminationResponseDto ToExaminationDto(this RadiologyCenter_ExaminationCost examination)
        {
            return new RadiologyExaminationResponseDto
            (
                Id: examination.Id.Value,
                Name: examination.RadiologyExamination.Name,
                Cost: examination.Cost
            );
        }
    }
}
