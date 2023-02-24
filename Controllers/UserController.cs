using MediatR;
using Microsoft.AspNetCore.Mvc;
using repos.EF;
using repos.Mediator;
using repos.Models;

namespace repos.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserController> _logger;
    private readonly IMediator _mediator;
    public UserController(ILogger<UserController> logger, IUserRepository userRepository,IMediator mediator)
    {
        _logger = logger;
        _userRepository = userRepository;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IEnumerable<UserViewModel>> GetAsync(UserFilter filter)
    {
       return  await _mediator.Send(new GetUsersCommand { Filter=filter});
        
    }
    [HttpPost]
    public StatusCodeResult Add([FromBody] UserViewModel model)
    {
        try
        {
            _userRepository.Add(model);
            return StatusCode(StatusCodes.Status200OK);
        }
        catch (UserAlreadyExistsExption)
        {
            return StatusCode(StatusCodes.Status409Conflict);
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    [HttpPost(Name ="AddDefaults")]
    public StatusCodeResult AddDefaults()
    {
        try
        {
            _userRepository.AddDefaults();
            return StatusCode(StatusCodes.Status200OK);
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    [HttpDelete]
    public StatusCodeResult Delete(int id,bool? hasConfirmed) 
    {
        try
        {
            _userRepository.Delete(id, hasConfirmed);
            return StatusCode(StatusCodes.Status200OK);
        }
        catch (HasReferenceForDeleteExption) 
        {
            return StatusCode(StatusCodes.Status304NotModified);
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
    [HttpPut]
    public StatusCodeResult Update([FromBody] UserViewModel model) 
    {
        try
        {
            _userRepository.Modify(model);
            return StatusCode(StatusCodes.Status200OK);
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
