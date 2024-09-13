namespace Talabeyah.TicketManagement.Application.Tickets.Commands.ChangeTicketColour;

public class ChangeTicketColourValidator : AbstractValidator<ChangeTicketColour>
{
    public ChangeTicketColourValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
        
        RuleFor(v => v.Colour)
            .NotEmpty().WithMessage("Invalid colour.");

    }
}