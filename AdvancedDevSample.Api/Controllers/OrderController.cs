using AdvancedDevSample.Application.DTOs;
using AdvancedDevSample.Application.Services;
using AdvancedDevSample.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AdvancedDevSample.Api.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrdersController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] CreateOrderRequest request)
        {
            try
            {
                var orderId = await _orderService.PlaceOrderAsync(request);
                return Ok(new { OrderId = orderId, Message = "Commande validée !" });
            }
            catch (DomaineException ex)
            {
                return BadRequest(ex.Message); // Erreur métier (Stock, etc.)
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erreur interne.");
            }
        }
    }
}