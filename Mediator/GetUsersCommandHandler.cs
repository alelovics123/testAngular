using MediatR;
using repos.EF;
using repos.Models;

namespace repos.Mediator
{
    public class GetUsersCommandHandler : IRequestHandler<GetUsersCommand,IEnumerable<UserViewModel>>
    {
        private readonly IUserRepository _userRepository;
        public GetUsersCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        async Task<IEnumerable<UserViewModel>> IRequestHandler<GetUsersCommand, IEnumerable<UserViewModel>>.Handle(GetUsersCommand request, CancellationToken cancellationToken)
        {
            return _userRepository.GetAll((n) =>
            (request.Filter.Id==null|| request.Filter.Id==n.Id)&&
         (String.IsNullOrWhiteSpace(request.Filter.NameOrPlaceFilter) || n.PlaceOfBirth.Contains(request.Filter.NameOrPlaceFilter) || n.Name.Contains(request.Filter.NameOrPlaceFilter)) &&
         (request.Filter.ToDateOfBirth == null || request.Filter.ToDateOfBirth >= n.DateOfBirth) &&
         (request.Filter.FromDateOfBirth == null || request.Filter.FromDateOfBirth <= n.DateOfBirth) &&
         (request.Filter.RecommenderId == null || request.Filter.RecommenderId == n.RecommenderId)
         ).AsEnumerable();
        }
    }
}
