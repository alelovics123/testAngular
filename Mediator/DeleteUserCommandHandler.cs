using MediatR;
using repos.EF;
using repos.Models;

namespace repos.Mediator
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _userRepository;
        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        Task IRequestHandler<DeleteUserCommand>.Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _userRepository.Delete(request.Id,request.IsConfirmed);
                return Task.CompletedTask;
            }
            catch (Exception e) { return Task.FromException(e); }
        }
    }
}
