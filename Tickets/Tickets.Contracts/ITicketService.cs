using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Tickets.DataContract;

namespace Tickets.Contracts
{
    [ServiceContract(Namespace = "http://ASPPatterns.Chap6.EventTickets/")]
    public interface ITicketService
    {
        [OperationContract()]
        ReserveTicketResponse ReserveTicket(ReserveTicketRequest reserveTicketRequest);

        [OperationContract()]
        PurchaseTicketResponse PurchaseTicket(PurchaseTicketRequest purchaseTicketRequest);
    }
}
