using Api.Dtos;
using Api.Helper;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations; 

namespace NewsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly IGenericRepository<NewsArticle> _newsRepository;
        private readonly IMapper _mapper;
        private readonly ImagesUrlUploadResolver _UrlResolver;

        public NewsController(IGenericRepository<NewsArticle> newsRepository, IMapper mapper, ImagesUrlUploadResolver urlResolver)
        {
            _newsRepository = newsRepository;
            _mapper = mapper;
            _UrlResolver = urlResolver;
        }

        /// <summary>
        /// الحصول على جميع مقالات الأخبار.
        /// </summary>
        /// <returns>قائمة بجميع مقالات الأخبار.</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "الحصول على جميع مقالات الأخبار", Description = "استرجاع قائمة بجميع مقالات الأخبار الموجودة.")]
        public async Task<ActionResult<List<NewsArticle>>> GetNews()
        {
            var newsArticles = await _newsRepository.GetAllAsync();
            return Ok(newsArticles);
        }

        /// <summary>
        /// الحصول على مقالة الأخبار حسب المعرف.
        /// </summary>
        /// <param name="id">معرف مقالة الأخبار.</param>
        /// <returns>مقالة الأخبار المطلوبة.</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "الحصول على مقالة الأخبار حسب المعرف", Description = "استرجاع مقالة الأخبار بناءً على المعرف، بما في ذلك الصور المرتبطة بها.")]
        public async Task<ActionResult<NewsArticle>> GetNewsById(int id)
        {
            var newsArticle = await _newsRepository.GetByIdWithIncludesAsync(id, na => na.Images);

            if (newsArticle == null)
            {
                return NotFound();
            }

            return Ok(newsArticle);
        }

        /// <summary>
        /// إضافة مقالة أخبار جديدة.
        /// </summary>
        /// <param name="newsArticleDto">بيانات مقالة الأخبار الجديدة.</param>
        /// <returns>رسالة تأكيد.</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "إضافة مقالة أخبار جديدة", Description = "إضافة مقالة أخبار جديدة مع تحميل الصورة الرئيسية وصور إضافية.")]
        public async Task<ActionResult> CreateNews([FromForm] NewsArticleDto newsArticleDto)
        {
            if (newsArticleDto == null)
            {
                return BadRequest("بيانات الأخبار غير صالحة.");
            }

            if (newsArticleDto.ImageUrl == null)
            {
                return BadRequest("بيانات الصورة الرئيسية غير صالحة.");
            }

            // رفع الصورة الرئيسية
            var imageUrl = await _UrlResolver.UploadImage(newsArticleDto.ImageUrl);

            // رفع الصور الإضافية
            var imageUrls = new List<string>();
            if (newsArticleDto.Images != null && newsArticleDto.Images.Any())
            {
                foreach (var imageFile in newsArticleDto.Images)
                {
                    var uploadedImageUrl = await _UrlResolver.UploadImage(imageFile);
                    imageUrls.Add(uploadedImageUrl);
                }
            }

            var news = _mapper.Map<NewsArticle>(newsArticleDto);
            news.ImageUrl = imageUrl;
            news.Date = DateTime.UtcNow;

            // إضافة الصور الإضافية إلى كائن الأخبار
            news.Images = imageUrls.Select(url => new NewsImage { Url = url }).ToList();

            try
            {
                await _newsRepository.AddAsync(news);
                return Ok("تم تحميل الأخبار بنجاح");
            }
            catch (Exception ex)
            {
                // تسجيل الاستثناء أو معالجة الأخطاء المحددة
                return StatusCode(StatusCodes.Status500InternalServerError, $"خطأ في تحميل الأخبار: {ex.Message}");
            }
        }

        /// <summary>
        /// تحديث مقالة الأخبار حسب المعرف.
        /// </summary>
        /// <param name="id">معرف مقالة الأخبار.</param>
        /// <param name="updateNewsArticleDto">بيانات مقالة الأخبار المحدثة.</param>
        /// <returns>المقالة المحدثة.</returns>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "تحديث مقالة الأخبار حسب المعرف", Description = "تحديث مقالة الأخبار بناءً على المعرف مع تحميل الصورة الرئيسية وصور إضافية جديدة إذا تم توفيرها.")]
        public async Task<IActionResult> UpdateNews(int id, [FromForm] UpdateNewsArticleDto updateNewsArticleDto)
        {
            var existingNewsArticle = await _newsRepository.GetByIdAsync(id);

            if (existingNewsArticle == null)
            {
                return NotFound();
            }

            // تحديث الحقول الأساسية فقط إذا تم توفيرها في DTO
            if (!string.IsNullOrEmpty(updateNewsArticleDto.Title))
            {
                existingNewsArticle.Title = updateNewsArticleDto.Title;
            }

            if (!string.IsNullOrEmpty(updateNewsArticleDto.Description))
            {
                existingNewsArticle.Description = updateNewsArticleDto.Description;
            }

            // تحديث الصورة الرئيسية إذا تم توفيرها
            if (updateNewsArticleDto.ImageUrl != null)
            {
                var newMainImageUrl = await _UrlResolver.UploadImage(updateNewsArticleDto.ImageUrl);
                existingNewsArticle.ImageUrl = newMainImageUrl;
            }

            // تحديث الصور الإضافية إذا تم توفيرها
            if (updateNewsArticleDto.Images != null && updateNewsArticleDto.Images.Any())
            {
                var newImageUrls = new List<string>();
                foreach (var imageFile in updateNewsArticleDto.Images)
                {
                    var uploadedImageUrl = await _UrlResolver.UploadImage(imageFile);
                    newImageUrls.Add(uploadedImageUrl);
                }

                // مسح الصور الحالية
                existingNewsArticle.Images.Clear();

                // إضافة الصور الجديدة
                foreach (var imageUrl in newImageUrls)
                {
                    existingNewsArticle.Images.Add(new NewsImage { Url = imageUrl });
                }
            }

            try
            {
                await _newsRepository.UpdateAsync(existingNewsArticle);
                return Ok("تم تحديث الأخبار بنجاح");
            }
            catch (Exception ex)
            {
                // تسجيل الاستثناء أو معالجة الأخطاء المحددة
                return StatusCode(StatusCodes.Status500InternalServerError, $"خطأ في تحديث الأخبار: {ex.Message}");
            }
        }

        /// <summary>
        /// حذف مقالة الأخبار حسب المعرف.
        /// </summary>
        /// <param name="id">معرف مقالة الأخبار.</param>
        /// <returns>حالة العملية.</returns>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "حذف مقالة الأخبار حسب المعرف", Description = "حذف مقالة الأخبار بناءً على المعرف.")]
        public async Task<IActionResult> DeleteNews(int id)
        {
            await _newsRepository.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>
        /// الحصول على مقالات الأخبار مع التصفح.
        /// </summary>
        /// <param name="pageNumber">رقم الصفحة.</param>
        /// <param name="pageSize">حجم الصفحة.</param>
        /// <returns>قائمة بمقالات الأخبار للتصفح.</returns>
        [HttpGet("pagination")]
        [SwaggerOperation(Summary = "الحصول على مقالات الأخبار مع التصفح", Description = "استرجاع مقالات الأخبار مع التصفح بناءً على رقم الصفحة وحجم الصفحة.")]
        public async Task<ActionResult<List<NewsArticle>>> GetNewsPagination([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var newsArticles = await _newsRepository.GetPagedAsync(pageNumber, pageSize);
            return Ok(newsArticles);
        }

        /// <summary>
        /// الحصول على معرّفات المقالات المجاورة لمقالة محددة.
        /// </summary>
        /// <param name="id">معرف مقالة الأخبار.</param>
        /// <returns>معرف المقالة السابقة واللاحقة.</returns>
        [HttpGet("{id}/adjacent")]
        [SwaggerOperation(Summary = "الحصول على معرّفات المقالات المجاورة", Description = "استرجاع معرف المقالة السابقة واللاحقة بناءً على معرف مقالة الأخبار الحالية.")]
        public async Task<ActionResult> GetAdjacentArticleIds(int id)
        {
            // استرجاع جميع المقالات، انتظر المهمة أولاً
            var allArticles = await _newsRepository.GetAllAsync();

            // ترتيب المقالات حسب تاريخ إنشائها واختيار معرّفاتها
            var allArticleIds = allArticles
                .OrderBy(na => na.Date)
                .Select(na => na.Id)
                .ToList();

            // العثور على فهرس المقالة الحالية
            var currentIndex = allArticleIds.IndexOf(id);

            if (currentIndex == -1)
            {
                return NotFound("المقالة غير موجودة");
            }

            int? previousId = null;
            int? nextId = null;

            // الحصول على معرف المقالة السابقة إذا كان موجودًا
            if (currentIndex > 0)
            {
                previousId = allArticleIds[currentIndex - 1];
            }

            // الحصول على معرف المقالة التالية إذا كان موجودًا
            if (currentIndex < allArticleIds.Count - 1)
            {
                nextId = allArticleIds[currentIndex + 1];
            }

            return Ok(new { previousId, nextId });
        }
    }
}
