using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Core.Interfaces;
using Api.Helper;
using Api.Dtos;
using Swashbuckle.AspNetCore.Annotations; 

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdviceController : ControllerBase
    {
        private readonly IGenericRepository<AdviceImage> _repository;
        private readonly ImagesUrlUploadResolver _urlResolver;

        public AdviceController(IGenericRepository<AdviceImage> repository, ImagesUrlUploadResolver urlResolver)
        {
            _repository = repository;
            _urlResolver = urlResolver;
        }

        /// <summary>
        /// الحصول على جميع صور النصائح.
        /// </summary>
        /// <returns>قائمة بجميع صور النصائح.</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "الحصول على جميع صور النصائح", Description = "استرجاع قائمة بجميع صور النصائح الموجودة.")]
        public async Task<ActionResult<IEnumerable<AdviceImage>>> GetAll()
        {
            var adviceImages = await _repository.GetAllAsync();
            return Ok(adviceImages);
        }

        /// <summary>
        /// الحصول على صورة نصيحة حسب المعرف.
        /// </summary>
        /// <param name="id">معرف صورة النصيحة.</param>
        /// <returns>صورة النصيحة المطلوبة.</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "الحصول على صورة نصيحة حسب المعرف", Description = "استرجاع صورة النصيحة بناءً على المعرف.")]
        public async Task<ActionResult<AdviceImage>> GetById(int id)
        {
            var adviceImage = await _repository.GetByIdAsync(id);
            if (adviceImage == null) return NotFound();
            return Ok(adviceImage);
        }

        /// <summary>
        /// إضافة صورة نصيحة جديدة.
        /// </summary>
        /// <param name="imageDto">بيانات الصورة الجديدة.</param>
        /// <returns>رسالة تأكيد.</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "إضافة صورة نصيحة جديدة", Description = "إضافة صورة نصيحة جديدة مع تحميل الصورة إلى الخادم.")]
        public async Task<ActionResult> Create([FromForm] UploadImageDto imageDto)
        {
            if (imageDto == null) return BadRequest("بيانات الصورة غير صالحة.");

            // رفع الصورة والحصول على عنوان URL
            var imageUrl = await _urlResolver.UploadImage(imageDto.ImageUrl);

            // إنشاء كائن AdviceImage جديد
            var newAdviceImage = new AdviceImage
            {
                Url = imageUrl
            };

            await _repository.AddAsync(newAdviceImage);
            return Ok("تمت إضافة الصورة بنجاح");
        }

        /// <summary>
        /// تحديث صورة نصيحة حسب المعرف.
        /// </summary>
        /// <param name="id">معرف صورة النصيحة.</param>
        /// <param name="imageDto">بيانات الصورة المحدثة.</param>
        /// <returns>صورة النصيحة المحدثة.</returns>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "تحديث صورة نصيحة حسب المعرف", Description = "تحديث صورة نصيحة بناءً على المعرف مع تحميل الصورة الجديدة.")]
        public async Task<IActionResult> Update(int id, [FromForm] UploadImageDto imageDto)
        {
            if (imageDto == null) return BadRequest("بيانات الصورة غير صالحة.");

            var existingAdviceImage = await _repository.GetByIdAsync(id);
            if (existingAdviceImage == null) return NotFound();
            
            var updatedImageUrl = await _urlResolver.UploadImage(imageDto.ImageUrl);
            existingAdviceImage.Url = updatedImageUrl;  // تحديث المسار.

            await _repository.UpdateAsync(existingAdviceImage);
            return Ok(existingAdviceImage);
        }

        /// <summary>
        /// حذف صورة نصيحة حسب المعرف.
        /// </summary>
        /// <param name="id">معرف صورة النصيحة.</param>
        /// <returns>حالة العملية.</returns>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "حذف صورة نصيحة حسب المعرف", Description = "حذف صورة نصيحة بناءً على المعرف.")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingAdviceImage = await _repository.GetByIdAsync(id);
            if (existingAdviceImage == null) return NotFound();

            await _repository.DeleteAsync(id);

            return NoContent();
        }
    }
}
