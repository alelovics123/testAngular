using MediatR;
using repos.EF;
using repos.Models;

namespace repos.Mediator
{
    public class ModifyUserCommandHandler : IRequestHandler<ModifyUserCommand>
    {
        private readonly IUserRepository _userRepository;
        public ModifyUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public  Task Handle(ModifyUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _userRepository.Modify(request.User);
                return Task.CompletedTask;
            }
            catch (Exception e) { return Task.FromException(e); }
        }

       
    }
}
