using CryptoWallet.Logic.Dto.Crypto;
using CryptoWallet.Logic.Functions.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CryptoWallet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptocurrencyController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        [Route("/NewCrypto")]
        public async Task<ActionResult> NewCrypto([FromBody] NewCryptoDto crypto)
        {
            try
            {
                var result = await _mediator.Send(new CreateCryptoCommand(crypto));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("/RemoveCrypto")]
        public async Task<ActionResult> RemoveCrypto([FromQuery] Guid id)
        {
            try
            {
                var result = await _mediator.Send(new DeleteCryptoCommand(id));

                if (!result)
                    return NotFound();
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("/UpdateCrypto")]
        public async Task<ActionResult> UpdateCrypto([FromBody] UpdateCryptoDto crypto)
        {
            try
            {
                var result = await _mediator.Send(new UpdateCryptoCommand(crypto));

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
