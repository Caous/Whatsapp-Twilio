using DomainDrivenDesign.Application.Interfaces;
using DomainDrivenDesign.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DomainDrivenDesign.Presentation.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
[ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
[ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
public class WhatsappController : ControllerBase
{
    private readonly IWhatsappService _serviceWhatsapp;

    public WhatsappController(IWhatsappService serviceWhatsapp)
    {
        _serviceWhatsapp = serviceWhatsapp;
    }
    [HttpGet("GetAllMenssagens")]
    public async Task<IActionResult> GetAllMessage()
    {

        BrokerLastMessagesResult result = await _serviceWhatsapp.GetMessages(null);

        if (result == null)
            return NoContent();

        return Ok(result);

    }
}
