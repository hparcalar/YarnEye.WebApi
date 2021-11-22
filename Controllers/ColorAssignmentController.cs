using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YarnEye.WebApi.Context;
using YarnEye.WebApi.Models;

namespace YarnEye.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [EnableCors()]
    public class ColorAssignmentController : ControllerBase
    {
        YarnEyeContext _context;

        public ColorAssignmentController(YarnEyeContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<ColorAssignmentModel> Get()
        {
            ColorAssignmentModel[] data = new ColorAssignmentModel[0];
            try
            {
                data = _context.ColorAssignment.Select(d => new ColorAssignmentModel
                {
                    AssignmentId = d.AssignmentId,
                    AssignmentCode = d.AssignmentCode,
                    CreatedDate = d.CreatedDate,
                    IsActive = d.IsActive,
                    //SampleImage = d.SampleImage,
                    SetHue = d.SetHue,
                    SetSaturation = d.SetSaturation,
                    SetValue = d.SetValue,
                }).OrderBy(d => d.AssignmentId).ToArray();
            }
            catch
            {

            }

            return data;
        }

        [HttpGet]
        [Route("{id}")]
        public ColorAssignmentModel Get(int id)
        {
            ColorAssignmentModel data = new ColorAssignmentModel();
            try
            {
                data = _context.ColorAssignment
                    .Where(d => d.AssignmentId == id).Select(d => new ColorAssignmentModel
                    {
                        AssignmentId = d.AssignmentId,
                        AssignmentCode = d.AssignmentCode,
                        CreatedDate = d.CreatedDate,
                        IsActive = d.IsActive,
                        SampleImage = d.SampleImage,
                        SetHue = d.SetHue,
                        SetSaturation = d.SetSaturation,
                        SetValue = d.SetValue,
                    }).FirstOrDefault();
            }
            catch
            {

            }

            return data;
        }

        [HttpGet]
        [Route("ProdLine/{id}")]
        public ColorAssignmentModel ProdLine(int id)
        {
            ColorAssignmentModel data = new ColorAssignmentModel();
            try
            {
                var dbProdLine = _context.ProdLine.FirstOrDefault(d => d.ProdLineId == id);
                if (dbProdLine == null)
                    throw new Exception("Üretim hattı tanımına ulaşılamadı.");

                data = _context.ColorAssignment
                    .Where(d => d.AssignmentId == dbProdLine.AssignmentId).Select(d => new ColorAssignmentModel
                    {
                        AssignmentId = d.AssignmentId,
                        AssignmentCode = d.AssignmentCode,
                        CreatedDate = d.CreatedDate,
                        IsActive = d.IsActive,
                        //SampleImage = d.SampleImage,
                        SetHue = d.SetHue,
                        SetSaturation = d.SetSaturation,
                        SetValue = d.SetValue,
                    }).FirstOrDefault();
            }
            catch
            {

            }

            return data;
        }


        [HttpPost]
        public BusinessResult Post(ColorAssignmentModel model)
        {
            BusinessResult result = new BusinessResult();

            try
            {
                var dbObj = _context.ColorAssignment
                    .FirstOrDefault(d => d.AssignmentId == model.AssignmentId);
                if (dbObj == null)
                {
                    dbObj = new ColorAssignment();
                    _context.ColorAssignment.Add(dbObj);

                    model.CreatedDate = new DateTime();
                }

                dbObj.AssignmentId = model.AssignmentId;
                dbObj.AssignmentCode = model.AssignmentCode;
                dbObj.CreatedDate = model.CreatedDate;
                dbObj.IsActive = model.IsActive;
                dbObj.SampleImage = model.SampleImage;
                dbObj.SetHue = model.SetHue;
                dbObj.SetSaturation = model.SetSaturation;
                dbObj.SetValue = model.SetValue;

                _context.SaveChanges();
                result.Result = true;
                result.RecordId = dbObj.AssignmentId;
            }
            catch (System.Exception ex)
            {
                result.Result = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        [HttpDelete]
        public BusinessResult Delete(int id)
        {
            BusinessResult result = new BusinessResult();

            try
            {
                var dbObj = _context.ColorAssignment
                    .FirstOrDefault(d => d.AssignmentId == id);
                if (dbObj == null)
                    throw new Exception("Silinmesi istenen kayıt bulunamadı.");

                if (_context.ProdLine.Any(d => d.AssignmentId == dbObj.AssignmentId))
                    throw new Exception("Bu örneklem, şuan hatlarda kullanıldığı için silinemez.");

                _context.ColorAssignment.Remove(dbObj);

                _context.SaveChanges();
                result.Result = true;
            }
            catch (System.Exception ex)
            {
                result.Result = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
    }
}
