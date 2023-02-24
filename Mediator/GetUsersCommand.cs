using MediatR;
using repos.Models;

namespace repos.Mediator
{
    public class GetUsersCommand:IRequest<IEnumerable<UserViewModel>>
    {
        public UserFilter Filter { get; set; }
    }
}
