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
    public class EnvironmentController : ControllerBase
    {
        private readonly IGenericRepository<EnvironmentImage> _repository;
        private readonly ImagesUrlUploadResolver _urlResolver;

        public EnvironmentController(IGenericRepository<EnvironmentImage> repository, ImagesUrlUploadResolver urlResolver)
        {
            _repository = repository;
            _urlResolver = urlResolver;
        }

        /// <summary>
        /// استرجاع جميع صور البيئة.
        /// </summary>
        /// <returns>قائمة بجميع صور البيئة.</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "الحصول على جميع صور البيئة", Description = "استرجاع قائمة بجميع صور البيئة.")]
        public async Task<ActionResult<IEnumerable<EnvironmentImage>>> GetAll()
        {
            var environmentImages = await _repository.GetAllAsync();
            return Ok(environmentImages);
        }

        /// <summary>
        /// استرجاع صورة البيئة حسب المعرف.
        /// </summary>
        /// <param name="id">معرف صورة البيئة.</param>
        /// <returns>صورة البيئة المطلوبة.</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "الحصول على صورة البيئة حسب المعرف", Description = "استرجاع صورة البيئة بناءً على المعرف.")]
        public async Task<ActionResult<EnvironmentImage>> GetById(int id)
        {
            var environmentImage = await _repository.GetByIdAsync(id);
            if (environmentImage == null) return NotFound();
            return Ok(environmentImage);
        }

        /// <summary>
        /// إضافة صورة بيئة جديدة.
        /// </summary>
        /// <param name="imageDto">بيانات صورة البيئة.</param>
        /// <returns>رسالة تأكيد.</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "إضافة صورة بيئة جديدة", Description = "إضافة صورة بيئة جديدة مع تحميل الصورة والحصول على عنوان URL.")]
        public async Task<ActionResult> Create([FromForm] UploadImageDto imageDto)
        {
            if (imageDto == null) return BadRequest("بيانات الصورة غير صالحة.");

            // رفع الصورة والحصول على عنوان URL
            var imageUrl = await _urlResolver.UploadImage(imageDto.ImageUrl);

            // إنشاء كائن صورة بيئة جديدة
            var newEnvironmentImage = new EnvironmentImage
            {
                Url = imageUrl
            };

            await _repository.AddAsync(newEnvironmentImage);
            return Ok("تمت إضافة الصورة");
        }

        /// <summary>
        /// تحديث صورة البيئة حسب المعرف.
        /// </summary>
        /// <param name="id">معرف صورة البيئة.</param>
        /// <param name="imageDto">بيانات صورة البيئة الجديدة.</param>
        /// <returns>الصورة المحدثة.</returns>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "تحديث صورة البيئة حسب المعرف", Description = "تحديث صورة البيئة بناءً على المعرف مع تحميل الصورة الجديدة.")]
        public async Task<IActionResult> Update(int id, [FromForm] UploadImageDto imageDto)
        {
            if (imageDto == null) return BadRequest("بيانات الصورة غير صالحة.");

            var existingEnvironmentImage = await _repository.GetByIdAsync(id);
            if (existingEnvironmentImage == null) return NotFound();
            
            var updatedImageUrl = await _urlResolver.UploadImage(imageDto.ImageUrl);
            existingEnvironmentImage.Url = updatedImageUrl;  // تحديث المسار.

            await _repository.UpdateAsync(existingEnvironmentImage);
            return Ok(existingEnvironmentImage);
        }

        /// <summary>
        /// حذف صورة البيئة حسب المعرف.
        /// </summary>
        /// <param name="id">معرف صورة البيئة.</param>
        /// <returns>حالة العملية.</returns>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "حذف صورة البيئة حسب المعرف", Description = "حذف صورة البيئة بناءً على المعرف.")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingEnvironmentImage = await _repository.GetByIdAsync(id);
            if (existingEnvironmentImage == null) return NotFound();

            await _repository.DeleteAsync(id);

            return NoContent();
        }
    }
}
