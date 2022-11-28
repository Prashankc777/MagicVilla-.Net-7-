using MagicVillaAPi.Data;
using MagicVillaAPi.Logging;
using MagicVillaAPi.Models;
using MagicVillaAPi.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MagicVillaAPi.Controllers
{
    [ApiController , Route("api/villaAPI")]
    public class VillaApiController : ControllerBase
    {
        private readonly Ilogging _logger;
        public VillaApiController(Ilogging logger)
        {
            _logger = logger;
        }
        
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
                _logger.Log("Get Villas" , "");
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (villaDto is null)
            {
                return BadRequest();
            }

            if (VillaStore.villaList.FirstOrDefault(u=>u.Name.ToLower() == villaDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError" , "Villa already exist");
                return BadRequest(modelState: ModelState);
            }

            if (villaDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            villaDto.Id = VillaStore.villaList.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;
            VillaStore.villaList.Add(villaDto);
            return Ok(villaDto);

        }


        [HttpDelete("{id}:int", Name = "DeleteVilla")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();

            }
            var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            VillaStore.villaList.Remove(villa);
            return NoContent();
        }

        //public IActionResult UpdateVilla(int id , [FromBody] VillaDto villaDto)
        //{
        //    if (villaDto is null || id != villaDto.Id)
        //    {
        //        return BadRequest();
        //    }

        //    var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);

        //    VillaStore.villaList.Add(villa);
        //    return NoContent();
        //}

        

    }
}
 