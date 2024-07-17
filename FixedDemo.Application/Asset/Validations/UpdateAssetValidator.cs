using FixedDemo.Application.Asset.Commands;
using FluentValidation;

namespace FixedDemo.Application.Asset.Validations
{
    internal class UpdateAssetValidator : AbstractValidator<UpdateAssetCommand>
    {
        public UpdateAssetValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty.");
        }
    }
}
