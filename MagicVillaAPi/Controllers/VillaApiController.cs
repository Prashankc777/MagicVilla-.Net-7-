using MagicVillaAPi.Data;
using MagicVillaAPi.Models;
using MagicVillaAPi.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MagicVillaAPi.Controllers
{
    [ApiController , Route("api/villaAPI")]
    public class VillaApiController : ControllerBase
    {
        [HttpGet]
        public ActionResult< IEnumerable<VillaDto>> GetVillas()
        {
            return Ok(VillaStore.villaList);
        }

        [HttpGet("id:int")]
        [ProducesResponseType(statusCode:StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDto> GetVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var villa =  VillaStore.villaList.FirstOrDefault(x => x.Id == id);
            if (villa == null)
            {
                return NotFound();
            }

            return Ok(villa);
        }



        [HttpPost]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDto> CreateVilla(VillaDto villaDto)
        {
            if (villaDto is null)
            {
                return BadRequest();
            }

            if (villaDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            villaDto.Id = VillaStore.villaList.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;
            VillaStore.villaList.Add(villaDto);
            return Ok(villaDto);

        }

    }
}
 