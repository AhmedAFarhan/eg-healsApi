namespace EGHeals.Application.Dtos.RadiologyCenter.Examinations.Responses
{
    public record ExaminationResponseDto(Guid Id, string Name, string Device, decimal Cost);
}
