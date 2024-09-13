using Domain.TicketAggregate.Entities;
using Domain.TicketAggregate.Enums;
using Domain.TicketAggregate.ValueObjects;

namespace Application.Tickets.Queries;

public class TicketDto
{
    public int Id { get; set; }
    public string PhoneNumber { get; set; }
    public Location Location { get; set; }
    public bool IsHandled { get; set; }
    public Colour Colour { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Ticket, TicketDto>();
        }
    }
}