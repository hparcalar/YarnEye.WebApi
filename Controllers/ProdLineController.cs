using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YarnEye.WebApi.Context;
using YarnEye.WebApi.Models;

namespace YarnEye.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors()]
    public class ProdLineController : ControllerBase
    {
        YarnEyeContext _context;

        public ProdLineController(YarnEyeContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<ProdLineModel> Get()
        {
            ProdLineModel[] data = new ProdLineModel[0];
            try
            {
                data = _context.ProdLine.Select(d => new ProdLineModel
                {
                    ProdLineId = d.ProdLineId,
                    AssignmentId = d.AssignmentId,
                    OrderNo = d.OrderNo,
                    ProdLineCode = d.ProdLineCode,
                    ProdLineName = d.ProdLineName,
                }).OrderBy(d => d.ProdLineId).ToArray();
            }
            catch
            {

            }

            return data;
        }

        [HttpGet]
        [Route("{id}")]
        public ProdLineModel Get(int id)
        {
            ProdLineModel data = new ProdLineModel();
            try
            {
                data = _context.ProdLine.Where(d => d.ProdLineId == id).Select(d => new ProdLineModel
                {
                    ProdLineId = d.ProdLineId,
                    AssignmentId = d.AssignmentId,
                    OrderNo = d.OrderNo,
                    ProdLineCode = d.ProdLineCode,
                    ProdLineName = d.ProdLineName,
                }).FirstOrDefault();
            }
            catch
            {

            }

            return data;
        }

        [HttpPost]
        public BusinessResult Post(ProdLineModel model)
        {
            BusinessResult result = new BusinessResult();

            try
            {
                var dbObj = _context.ProdLine.FirstOrDefault(d => d.ProdLineId == model.ProdLineId);
                if (dbObj == null)
                {
                    dbObj = new ProdLine();
                    _context.ProdLine.Add(dbObj);
                }

                dbObj.AssignmentId = model.AssignmentId;
                dbObj.OrderNo = model.OrderNo;

                if (!string.IsNullOrEmpty(model.ProdLineCode))
                    dbObj.ProdLineCode = model.ProdLineCode;

                if (!string.IsNullOrEmpty(model.ProdLineName))
                    dbObj.ProdLineName = model.ProdLineName;

                _context.SaveChanges();
                result.Result = true;
                result.RecordId = dbObj.ProdLineId;
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
                var dbObj = _context.ProdLine.FirstOrDefault(d => d.ProdLineId == id);
                if (dbObj == null)
                    throw new Exception("Silinmesi istenen kayıt bulunamadı.");

                if (_context.YarnCheckResult.Any(d => d.ProdLineId == dbObj.ProdLineId))
                    throw new Exception("Bu hat test sonuçları tarihçesi içerdiği için silinemez.");

                _context.ProdLine.Remove(dbObj);

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
