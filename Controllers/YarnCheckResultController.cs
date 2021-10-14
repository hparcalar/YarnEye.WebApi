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
    public class YarnCheckResultController : ControllerBase
    {
        YarnEyeContext _context;

        public YarnCheckResultController(YarnEyeContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<YarnCheckResultModel> Get()
        {
            YarnCheckResultModel[] data = new YarnCheckResultModel[0];
            try
            {
                data = _context.YarnCheckResult.Select(d => new YarnCheckResultModel
                {
                    ResultId = d.ResultId,
                    AssignmentId = d.AssignmentId,
                    ColorHue = d.ColorHue,
                    ColorSaturation = d.ColorSaturation,
                    ColorValue = d.ColorValue,
                    CreatedDate = d.CreatedDate,
                    DiffHue = d.DiffHue,
                    DiffSaturation = d.DiffSaturation,
                    DiffValue = d.DiffValue,
                    ProdLineId = d.ProdLineId,
                    SerialNo = d.SerialNo,
                    TestResult = d.TestResult,
                }).OrderBy(d => d.ProdLineId).ToArray();
            }
            catch
            {

            }

            return data;
        }

        [HttpPost]
        public BusinessResult Post(YarnCheckResultModel model)
        {
            BusinessResult result = new BusinessResult();

            try
            {
                var dbObj = _context.YarnCheckResult
                    .FirstOrDefault(d => d.ResultId == model.ResultId);
                if (dbObj == null)
                {
                    dbObj = new YarnCheckResult();
                    _context.YarnCheckResult.Add(dbObj);

                    model.CreatedDate = DateTime.Now;
                }

                dbObj.AssignmentId = model.AssignmentId;
                dbObj.ColorHue = model.ColorHue;
                dbObj.ColorSaturation = model.ColorSaturation;
                dbObj.ColorValue = model.ColorValue;
                dbObj.CreatedDate = model.CreatedDate;
                dbObj.DiffHue = model.DiffHue;
                dbObj.DiffSaturation = model.DiffSaturation;
                dbObj.DiffValue = model.DiffValue;
                dbObj.ProdLineId = model.ProdLineId;
                dbObj.SerialNo = model.SerialNo;
                dbObj.TestResult = model.TestResult;

                _context.SaveChanges();
                result.Result = true;
                result.RecordId = dbObj.ResultId;
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
                var dbObj = _context.YarnCheckResult
                    .FirstOrDefault(d => d.ResultId == id);
                if (dbObj == null)
                    throw new Exception("Silinmesi istenen kayıt bulunamadı.");

                _context.YarnCheckResult.Remove(dbObj);

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
