using MediatR;
using repos.Models;

namespace repos.Mediator
{
    public class CreateUserCommand:IRequest
    {
        public UserViewModel User { get; set; }
    }
}
