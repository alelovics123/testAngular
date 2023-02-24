using MediatR;
using repos.Models;

namespace repos.Mediator
{
    public class DeleteUserCommand:IRequest
    {
        public int Id { get; set; }
        public bool? IsConfirmed { get; set; }
    }
}
