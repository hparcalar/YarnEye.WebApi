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
    public class ActiveTesterController : ControllerBase
    {
        YarnEyeContext _context;

        public ActiveTesterController(YarnEyeContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActiveTesterModel Get(string hostname)
        {
            ActiveTesterModel data = new ActiveTesterModel();
            try
            {
                var dataModel = _context.ActiveTester.Where(d => d.IpAddr == hostname)
                    .Select(d => new ActiveTesterModel{
                        ActiveTesterId = d.ActiveTesterId,
                        IpAddr = d.IpAddr,
                        ProdLineId = d.ProdLineId,
                        TesterStatus = d.TesterStatus,
                    }).FirstOrDefault();

                if (dataModel != null)
                    data = dataModel;
            }
            catch
            {

            }

            return data;
        }

        [HttpPost]
        public BusinessResult Post(ActiveTesterModel model)
        {
            BusinessResult result = new BusinessResult();

            try
            {
                var dbObj = _context.ActiveTester
                    .FirstOrDefault(d => d.IpAddr == model.IpAddr);
                if (dbObj == null)
                {
                    dbObj = new ActiveTester();
                    _context.ActiveTester.Add(dbObj);
                }

                dbObj.IpAddr = model.IpAddr;
                dbObj.ProdLineId = model.ProdLineId;
                dbObj.TesterStatus = model.TesterStatus;

                _context.SaveChanges();
                result.Result = true;
                result.RecordId = dbObj.ActiveTesterId;
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
