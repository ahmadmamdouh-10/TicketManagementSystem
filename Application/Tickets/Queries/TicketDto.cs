using Talabeyah.TicketManagement.Domain.Entities;
using Talabeyah.TicketManagement.Domain.Enums;
using Talabeyah.TicketManagement.Domain.ValueObjects;

namespace Talabeyah.TicketManagement.Application.Tickets.Queries;

public class TicketDto
{
    public int Id { get; set; }
    public string PhoneNumber { get; set; }
    public LocationDto Location { get; set; }

    public DateTimeOffset Created { get; set; }
    public bool IsHandled { get; set; }
    public Color Colour { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Ticket, TicketDto>()
                .ForMember(t => t.PhoneNumber, opt => opt.MapFrom(t => t.PhoneNumber.Value));
        }
    }
}