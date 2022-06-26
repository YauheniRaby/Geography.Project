using FluentValidation;
using Geography.BL.ModelsDTO;
using Geography.DAL.Enums;

namespace Geography.BL.Validators
{
    public class DemoSnapshotDtoValidator : AbstractValidator<DemoSnapshotDTO>
    {
        public DemoSnapshotDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
            RuleFor(x => x.Sputnik)
                .Must(x => Enum.IsDefined(typeof(Sputnic), x));
            RuleFor(x => x.DateSnapshot)
                .NotEmpty();
            RuleFor(x => x.Cloudiness)
                .LessThanOrEqualTo(100)
                .GreaterThanOrEqualTo(0);
            RuleFor(x => x.Coil)
                .GreaterThanOrEqualTo(0);
            RuleFor(x => x.Geography)
                .Must(x =>
                    x.Type.ToLower() == "polygon"
                    && x.Points.Count() >= 4
                    && x.Points.All(p =>
                        p.Length == 2
                        && p[0] >= -180 && p[0] <= 180
                        && p[1] >= -90 && p[1] <= 90));
        }
    }
}
