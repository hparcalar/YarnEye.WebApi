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
    public class ActiveAssignerController : ControllerBase
    {
        YarnEyeContext _context;

        public ActiveAssignerController(YarnEyeContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActiveAssignerModel Get(string hostname)
        {
            ActiveAssignerModel data = new ActiveAssignerModel();
            try
            {
                var dataModel = _context.ActiveAssigner.Where(d => d.IpAddr == hostname)
                    .Select(d => new ActiveAssignerModel{
                        ActiveAssignerId = d.ActiveAssignerId,
                        IpAddr = d.IpAddr,
                        SelectedLines = d.SelectedLines,
                        AssignerStatus = d.AssignerStatus,
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
        public BusinessResult Post(ActiveAssignerModel model)
        {
            BusinessResult result = new BusinessResult();

            try
            {
                var dbObj = _context.ActiveAssigner
                    .FirstOrDefault(d => d.IpAddr == model.IpAddr);
                if (dbObj == null)
                {
                    dbObj = new ActiveAssigner();
                    _context.ActiveAssigner.Add(dbObj);
                }

                dbObj.IpAddr = model.IpAddr;
                dbObj.SelectedLines = model.SelectedLines;
                dbObj.AssignerStatus = model.AssignerStatus;

                _context.SaveChanges();
                result.Result = true;
                result.RecordId = dbObj.ActiveAssignerId;
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
