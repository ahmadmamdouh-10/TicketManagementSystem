namespace Talabeyah.TicketManagement.Application.Tickets.Commands.HandleTicket;

public class HandleTicketValidator : AbstractValidator<HandleTicketCommand>
{
    public HandleTicketValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}