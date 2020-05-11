using Autofac;
using System;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IComponentContext _context;

        public CommandDispatcher(IComponentContext contex)
        {
            _context = contex;
        }

        public async Task DispatchAsync<T>(T command) where T : ICommand
        {
            if(command == null)
            {
                throw new ArgumentNullException(nameof(command),
                    $"Command: '{typeof(T).Name}' can not be null.");
            }
            var handler = _context.Resolve<ICommandHandler<T>>(); // znajdz instancje ktora dziedziczy po IComandHandler<T> gdzie T jest np CreateUser
            await handler.HandleAsync(command); // wywołaj metode hadnelAsync znalezionej instancji. ICommandHandler posiada metode HandleAsync
        }
    }
}
