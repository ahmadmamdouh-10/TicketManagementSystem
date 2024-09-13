namespace Talabeyah.TicketManagement.Application.Tickets.Commands.CreateTicket;

public class CreateTicketCommandValidator : AbstractValidator<CreateTicketCommand>
{
    public CreateTicketCommandValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Phone number is not valid.");

        RuleFor(x => x.Location)
            .NotNull().WithMessage("Location is required.");

        RuleFor(x => x.Location.Governorate)
            .NotEmpty().WithMessage("Governorate is required.");

        RuleFor(x => x.Location.City)
            .NotEmpty().WithMessage("City is required.");

        RuleFor(x => x.Location.District)
            .NotEmpty().WithMessage("District is required.");
        
    }
}