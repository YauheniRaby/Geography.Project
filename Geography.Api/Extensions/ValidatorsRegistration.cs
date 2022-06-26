using FluentValidation;
using Geography.BL.ModelsDTO;
using Geography.BL.Validators;

namespace Geography.Api.Extensions
{
    public static class ValidatorsRegistration
    {
        public static void AddValidators(this IServiceCollection services)
        {
            services.AddSingleton<IValidator<DemoSnapshotDTO>, DemoSnapshotDtoValidator>();
        }
    }
}
