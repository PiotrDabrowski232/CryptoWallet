using CryptoWallet.Logic.Functions.Command;
using CryptoWallet.Logic.Functions.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CryptoWallet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        [Route("/CreateWallet")]
        public async Task<ActionResult> NewWallet([FromBody] string walletName)
        {
            try
            {
                var result = await _mediator.Send(new CreateWalletCommand(walletName));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("/Wallets")]
        public async Task<ActionResult> Wallets()
        {
            try
            {
                var result = await _mediator.Send(new GetAllWalletsQuery());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("/Wallet")]
        public async Task<ActionResult> Wallet([FromQuery] Guid id)
        {
            try
            {
                var result = await _mediator.Send(new GetSpecificWalletQuery(id));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("/Remove")]
        public async Task<ActionResult> RemoveWallet([FromQuery] Guid id)
        {
            var result = await _mediator.Send(new DeleteWalletCommand(id));

            if (!result)
                return NotFound();

            return Ok(result);
        }

        [HttpPut]
        [Route("/Wallet")]
        public async Task<ActionResult> Wallet([FromQuery] Guid id, [FromBody] string newWalletName)
        {
            try
            {
                var result = await _mediator.Send(new ChangeWalletNameCommand(id, newWalletName));

                if(!result)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
