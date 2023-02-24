using MediatR;
using repos.EF;
using repos.Models;

namespace repos.Mediator
{
    public class CreateDefaultUsersCommandHandler : IRequestHandler<CreateDefaulUsersCommand>
    {
        private readonly IUserRepository _userRepository;
        public CreateDefaultUsersCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task Handle(CreateDefaulUsersCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _userRepository.AddDefaults();
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                return Task.FromException(e);
            }
        }
    }
}
