using Domain.Interfaces;
using Domain.Interfaces.InterfaceServices;


namespace Domain.Services
{
    public class ServiceMessage : IServiceMessage
    {
        private readonly IMessage _Message;

        public ServiceMessage(IMessage message)
        {
            _Message = message;
        }





    }
}
