using Talabeyah.TicketManagement.Domain.Enums;

namespace Talabeyah.TicketManagement.Application.Common.Interfaces;

public interface IChangeTicketColor
{
      Task ChangeTicketColourAsync(int ticketId, Color color);
}