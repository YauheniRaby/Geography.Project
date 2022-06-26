using Geography.BL.ModelsDTO;
using Geography.BL.Services.Abstract;
using Geography.DAL.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Geography.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DemoSnapshotController : ControllerBase
    {
        private readonly IDemoSnapshotService _demoSnapshotService;
        
        public DemoSnapshotController(IDemoSnapshotService demoSnapshotService)
        {
           _demoSnapshotService = demoSnapshotService;
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<DemoSnapshotDTO>>> GetAllAsync()
        {
            return Ok(await _demoSnapshotService.GetAllAsync());            
        }

        [HttpGet("getByFilter")]
        public async Task<ActionResult<IEnumerable<DemoSnapshotDTO>>> GetByFilterAsync([FromQuery] string filter)
        {
            if (_demoSnapshotService.Validation(filter))
                return Ok(await _demoSnapshotService.GetIntersectionsAsync(filter));
            return BadRequest();
        }

        [HttpGet("getSputnikArr")]
        public ActionResult<string[]> GetSputnicArr()
        {
            return Ok(_demoSnapshotService.GetSputnicArr());
        }

        [HttpDelete("{id:int}")]
        public async Task<StatusCodeResult> RemoveAsync(int id)
        {
            if(!await _demoSnapshotService.ExistsAsync(id))
                return NotFound();

            await _demoSnapshotService.RemoveAsync(id);
            return NoContent();
        }        

        [HttpPut]
        public async Task<StatusCodeResult> UpdateAsync(DemoSnapshotDTO demoSnapshotDTO)
        {
            if (!await _demoSnapshotService.ExistsAsync(demoSnapshotDTO.Id))
                return NotFound();

            await _demoSnapshotService.UpdateAsync(demoSnapshotDTO);
            return NoContent();
        }
    }
}