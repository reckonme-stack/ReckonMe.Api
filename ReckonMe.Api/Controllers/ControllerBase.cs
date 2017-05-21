using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace ReckonMe.Api.Controllers
{
    public class ControllerBase : Controller
    {
        public string Username => User.Claims.FirstOrDefault(x => x.Type.EndsWith("nameidentifier")).Value;
        public string Email => User.Claims.FirstOrDefault(x => x.Type.EndsWith("emailaddress")).Value;
    }
}