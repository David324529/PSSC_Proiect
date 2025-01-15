using Data.Exceptions;
using Api.Models;
using Domain.Workflows;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ILogger<OrdersController> _logger;
    private readonly PlasareComandaWorkflow _plasareComandaWorkflow;

    public OrdersController(
        ILogger<OrdersController> logger,
        PlasareComandaWorkflow plasareComandaWorkflow)
    {
        _logger = logger;
        _plasareComandaWorkflow = plasareComandaWorkflow;
    }

    [HttpPost("place-order")]
    public IActionResult PlaceOrder([FromBody] ProcessOrderCommand command)
    {
        try
        {
            // Log pentru debugging
            _logger.LogInformation("Procesare comanda pentru clientul {CustomerId} catre adresa {DeliveryAddress}", 
                                    command.CustomerId, command.DeliveryAddress);

            //  procesarea comenzii
            var result = _plasareComandaWorkflow.Execute(command);

            //  veridficare erori in procesare
            if (result is OrderProcessFailedEvent failedEvent)
            {
                return BadRequest(failedEvent.Errors);
            }

            // Comanda plasata cu succes
            return Ok(result);
        }
        catch (OutOfStockException ex)
        {
            _logger.LogWarning("Stoc insuficient: {Message}", ex.Message);
            return BadRequest(new { error = ex.Message });  
        }
        catch (PaymentProcessingException ex)
        {
            _logger.LogError("Eroare la procesarea platii: {Message}", ex.Message);
            return BadRequest(new { error = "Eroare la procesarea platii: " + ex.Message });  
        }
        catch (Exception ex)
        {
            _logger.LogError("Eroare interna: {Message}", ex.Message);
            return StatusCode(500, new { error = "Eroare interna: " + ex.Message });  
        }
    }
}
