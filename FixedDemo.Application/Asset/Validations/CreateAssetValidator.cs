using FixedDemo.Application.Asset.Commands;
using FluentValidation;

namespace FixedDemo.Application.Asset.Validations
{
    internal class CreateAssetValidator : AbstractValidator<CreateAssetCommand>
    {
        public CreateAssetValidator()
        {

        }
    }
}
