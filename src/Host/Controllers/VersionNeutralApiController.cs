using Microsoft.AspNetCore.Mvc;

namespace SeaPizza.Host.Controllers;

[Route("api/[controller]")]
[ApiVersionNeutral]
public class VersionNeutralApiController : BaseApiController
{
}
