using MediatR;
using repos.Models;

namespace repos.Mediator
{
    public class ModifyUserCommand:IRequest
    {
        public UserViewModel User { get; set; }
    }
}
