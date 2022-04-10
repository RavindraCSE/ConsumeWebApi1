using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi_Consume.Models;

namespace WebApi_Consume.Controllers
{
    public class DepartmentController : ApiController
    {
        private CompanyEntities db = new CompanyEntities();

        // GET: api/Department
        public IQueryable<tblDepartmentMaster> GettblDepartmentMasters()
        {
            return db.tblDepartmentMasters;
        }

        // GET: api/Department/5
        [ResponseType(typeof(tblDepartmentMaster))]
        public IHttpActionResult GettblDepartmentMaster(int id)
        {
            tblDepartmentMaster tblDepartmentMaster = db.tblDepartmentMasters.Find(id);
            if (tblDepartmentMaster == null)
            {
                return NotFound();
            }

            return Ok(tblDepartmentMaster);
        }

        // PUT: api/Department/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblDepartmentMaster(int id, tblDepartmentMaster tblDepartmentMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblDepartmentMaster.DeptId)
            {
                return BadRequest();
            }

            db.Entry(tblDepartmentMaster).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblDepartmentMasterExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Department
        [ResponseType(typeof(tblDepartmentMaster))]
        public IHttpActionResult PosttblDepartmentMaster(tblDepartmentMaster tblDepartmentMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblDepartmentMasters.Add(tblDepartmentMaster);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tblDepartmentMaster.DeptId }, tblDepartmentMaster);
        }

        // DELETE: api/Department/5
        [ResponseType(typeof(tblDepartmentMaster))]
        public IHttpActionResult DeletetblDepartmentMaster(int id)
        {
            tblDepartmentMaster tblDepartmentMaster = db.tblDepartmentMasters.Find(id);
            if (tblDepartmentMaster == null)
            {
                return NotFound();
            }

            db.tblDepartmentMasters.Remove(tblDepartmentMaster);
            db.SaveChanges();

            return Ok(tblDepartmentMaster);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblDepartmentMasterExists(int id)
        {
            return db.tblDepartmentMasters.Count(e => e.DeptId == id) > 0;
        }
    }
}