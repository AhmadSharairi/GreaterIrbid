using Api.Helper;
using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

[Route("api/[controller]")]
[ApiController]
public class ComplaintsController : ControllerBase
{
    private readonly IGenericRepository<Complaint> _repositoryComplaint;
    private readonly IGenericRepository<ComplaintType> _repositorycomplaintType;
    private readonly IGenericRepository<Category> _repositoryComplaintCategory;
    private readonly ImagesUrlComplaintResolver _urlResolver;
    private readonly IMapper _mapper;

    public ComplaintsController(IGenericRepository<Complaint> repositoryComplaint, 
    IGenericRepository<ComplaintType> repositorycomplaintType, 
    IGenericRepository<Category> repositoryComplaintCategory,  
    ImagesUrlComplaintResolver urlResolver,
    IMapper mapper )
    {
        _repositoryComplaint = repositoryComplaint;
        _repositoryComplaintCategory = repositoryComplaintCategory;
        _repositorycomplaintType = repositorycomplaintType;
        _urlResolver = urlResolver;
        _mapper = mapper;
    }

    /// <summary>
    /// استرجاع جميع الشكاوى.
    /// </summary>
    /// <returns>قائمة بجميع الشكاوى.</returns>
    [HttpGet]
    [SwaggerOperation(Summary = "الحصول على جميع الشكاوى", Description = "استرجاع قائمة بجميع الشكاوى.")]
    public async Task<ActionResult<IEnumerable<Complaint>>> GetComplaints()
    {
        var complaints = await _repositoryComplaint.GetAllAsync();
        return Ok(complaints);
    }

    /// <summary>
    /// استرجاع جميع أنواع الشكاوى.
    /// </summary>
    /// <returns>قائمة بأنواع الشكاوى مع الفئات المرتبطة بها.</returns>
    [HttpGet("complaintsName")]
    [SwaggerOperation(Summary = "الحصول على جميع أنواع الشكاوى", Description = "استرجاع قائمة بجميع أنواع الشكاوى مع الفئات المرتبطة بها.")]
    public async Task<ActionResult<IEnumerable<ComplaintType>>> GetComplaintsName()
    {
        var complaintsNames = await _repositorycomplaintType.GetAllWithIncludesAsync(cn => cn.Category);
        return Ok(complaintsNames);
    }

    /// <summary>
    /// استرجاع جميع فئات الشكاوى.
    /// </summary>
    /// <returns>قائمة بفئات الشكاوى.</returns>
    [HttpGet("complaintsCategory")]
    [SwaggerOperation(Summary = "الحصول على جميع فئات الشكاوى", Description = "استرجاع قائمة بجميع فئات الشكاوى.")]
    public async Task<ActionResult<IEnumerable<Category>>> GetComplaintsCategory()
    {
        var complaintsCategory = await _repositoryComplaintCategory.GetAllAsync();
        return Ok(complaintsCategory);
    }

    /// <summary>
    /// استرجاع نوع الشكوى حسب المعرف.
    /// </summary>
    /// <param name="id">معرف نوع الشكوى.</param>
    /// <returns>نوع الشكوى مع الفئة المرتبطة به.</returns>
    [HttpGet("complaintsName/{id}")]
    [SwaggerOperation(Summary = "الحصول على نوع الشكوى حسب المعرف", Description = "استرجاع نوع الشكوى مع الفئة المرتبطة به حسب المعرف.")]
    public async Task<ActionResult<ComplaintType>> GetcomplaintTypeById(int id)
    {
        var complaintType = await _repositorycomplaintType.GetByIdWithIncludesAsync(id, cn => cn.Category);

        if (complaintType == null)
        {
            return NotFound();
        }

        return Ok(complaintType);
    }

    /// <summary>
    /// إضافة شكوى جديدة.
    /// </summary>
    /// <param name="complaintDto">بيانات الشكوى.</param>
    /// <returns>رسالة تأكيد.</returns>
    [HttpPost]
    [SwaggerOperation(Summary = "إضافة شكوى جديدة", Description = "إضافة شكوى جديدة مع تحميل الصورة والحصول على عنوان URL.")]
    public async Task<ActionResult<Complaint>> PostComplaint([FromForm] ComplaintDto complaintDto)
    {
        if (complaintDto == null) return BadRequest("بيانات الشكوى غير صالحة.");

        // رفع الصورة والحصول على عنوان URL
        var newimageUrl = await _urlResolver.UploadImage(complaintDto.ImageUrl);

        // إنشاء كائن شكوى جديد
        var newComplaint = _mapper.Map<Complaint>(complaintDto);
        newComplaint.ImageUrl = newimageUrl;
        //newComplaint.Date =  DateTime.UtcNow;
        
        await _repositoryComplaint.AddAsync(newComplaint);
        return Ok("تمت إضافة الشكوى");
    }

    /// <summary>
    /// استرجاع الشكوى حسب المعرف.
    /// </summary>
    /// <param name="id">معرف الشكوى.</param>
    /// <returns>الشكوى.</returns>
    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "الحصول على الشكوى حسب المعرف", Description = "استرجاع الشكوى بناءً على المعرف.")]
    public async Task<ActionResult<Complaint>> GetComplaint(int id)
    {
        var complaint = await _repositoryComplaint.GetByIdAsync(id);
        if (complaint == null)
        {
            return NotFound();
        }
        return complaint;
    }

    /// <summary>
    /// حذف الشكوى حسب المعرف.
    /// </summary>
    /// <param name="id">معرف الشكوى.</param>
    /// <returns>حالة العملية.</returns>
    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "حذف الشكوى حسب المعرف", Description = "حذف الشكوى بناءً على المعرف.")]
    public async Task<IActionResult> Delete(int id)
    {
        var complaint = await _repositoryComplaint.GetByIdAsync(id);
        if (complaint == null)
        {
            return NotFound("الشكوى غير موجودة");
        }

        // حذف الشكوى
        await _repositoryComplaint.DeleteAsync(id);
        return NoContent();
    }
}
