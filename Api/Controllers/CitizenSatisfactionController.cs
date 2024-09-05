using Api.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitizenSatisfactionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<CitizenSatisfaction> _repositorySatisfaction;

        public CitizenSatisfactionController(IMapper mapper, IGenericRepository<CitizenSatisfaction> repositorySatisfaction)
        {
            _mapper = mapper;
            _repositorySatisfaction = repositorySatisfaction;
        }

        /// <summary>
        /// إرسال نموذج تقييم المواطن
        /// </summary>
        /// <param name="citizenSatisfactionDto">بيانات تقييم المواطن</param>
        /// <returns>تقييم المواطن الجديد</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "إرسال نموذج تقييم المواطن", Description = "إرسال نموذج تقييم المواطن إلى النظام.")]
        public async Task<IActionResult> SubmitForm([FromBody] CitizenSatisfactionDto citizenSatisfactionDto)
        {
            if (citizenSatisfactionDto == null)
            {
                return BadRequest("Invalid Citizen Satisfaction data.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newSatisfaction = _mapper.Map<CitizenSatisfaction>(citizenSatisfactionDto);
            await _repositorySatisfaction.AddAsync(newSatisfaction);

            return CreatedAtAction(nameof(GetSatisfactionById), new { id = newSatisfaction.Id }, newSatisfaction);
        }

        /// <summary>
        /// الحصول على سجل تقييم المواطن بواسطة المعرف
        /// </summary>
        /// <param name="id">معرف تقييم المواطن</param>
        /// <returns>سجل تقييم المواطن</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "الحصول على سجل تقييم المواطن بواسطة المعرف", Description = "الحصول على سجل تقييم المواطن باستخدام المعرف المحدد.")]
        public async Task<IActionResult> GetSatisfactionById(int id)
        {
            var satisfaction = await _repositorySatisfaction.GetByIdAsync(id);
            if (satisfaction == null)
            {
                return NotFound();
            }

            var satisfactionDto = _mapper.Map<CitizenSatisfactionDto>(satisfaction);
            return Ok(satisfactionDto);
        }

        /// <summary>
        /// حذف سجل تقييم المواطن
        /// </summary>
        /// <param name="id">معرف تقييم المواطن</param>
        /// <returns>لا يوجد محتوى</returns>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "حذف سجل تقييم المواطن", Description = "حذف سجل تقييم المواطن باستخدام المعرف المحدد.")]
        public async Task<IActionResult> DeleteSatisfaction(int id)
        {
            var existingSatisfaction = await _repositorySatisfaction.GetByIdAsync(id);
            if (existingSatisfaction == null)
            {
                return NotFound();
            }

            await _repositorySatisfaction.DeleteAsync(id);
            return NoContent(); // Return 204 No Content for successful deletion
        }

        /// <summary>
        /// الحصول على جميع سجلات تقييم المواطن
        /// </summary>
        /// <returns>قائمة سجلات تقييم المواطن</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "الحصول على جميع سجلات تقييم المواطن", Description = "الحصول على قائمة بجميع سجلات تقييم المواطن.")]
        public async Task<IActionResult> GetAllSatisfactions()
        {
            var satisfactions = await _repositorySatisfaction.GetAllAsync();
            var satisfactionDtos = _mapper.Map<IEnumerable<CitizenSatisfactionDto>>(satisfactions);
            return Ok(satisfactionDtos);
        }
    }
}
