﻿namespace Application.Tickets.Commands.HandleTicket;

public class HandleTicketValidator : AbstractValidator<HandleTicket>
{
    public HandleTicketValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}