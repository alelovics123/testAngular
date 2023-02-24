using MediatR;
using repos.EF;
using repos.Models;

namespace repos.Mediator
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _userRepository.Add(request.User);
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                return Task.FromException(e);
            }
        }

        
    }
}
