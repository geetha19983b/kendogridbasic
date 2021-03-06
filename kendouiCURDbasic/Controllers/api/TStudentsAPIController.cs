﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using kendouiCURDbasic.Models;
using System.Linq.Dynamic;

namespace kendouiCURDbasic.Controllers
{
    [RoutePrefix("api/TStudentsAPI")]
    public class TStudentsAPIController : ApiController
    {
        private SampleDatabaseEntities db = new SampleDatabaseEntities();

        // GET: api/TStudents
        public IQueryable<TStudent> GetTStudents()
        {
            return db.TStudents;
        }

        // GET: api/TStudents/5
        [ResponseType(typeof(TStudent))]
        public async Task<IHttpActionResult> GetTStudent(int id)
        {
            TStudent tStudent = await db.TStudents.FindAsync(id);
            if (tStudent == null)
            {
                return NotFound();
            }

            return Ok(tStudent);
        }
        [HttpGet]
        [Route("StudentDetails/{id}")]
        public HttpResponseMessage GetStudentDetails(int id)
        //public async Task<IHttpActionResult> GetStudentDetails(int id)
        {
            try
            {
                var studesription = db.TDetails.Where(a => a.StudentID.Value.Equals(id)).ToList();
                return Request.CreateResponse<List<TDetail>>(HttpStatusCode.OK, studesription, Configuration.Formatters.JsonFormatter);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ex.Message, Configuration.Formatters.JsonFormatter);
            }
        }
        [HttpPost]
        [Route("Studs")]
        public HttpResponseMessage ServerRequest(Request request)
        {
            // compose the order by for sorting
            string order = "StudentID";

            // order the results
            if (request.sort != null && request.sort.Count > 0)
            {
                List<string> sorts = new List<string>();
                request.sort.ForEach(x => {
                    sorts.Add(string.Format("{0} {1}", x.field, x.dir));
                });

                order = string.Join(",", sorts.ToArray());
            }
            // get all of the records from the employees table in the
            // northwind database.  return them in a collection of user
            // defined model objects for easy serialization. skip and then
            // take the appropriate number of records for paging.
            var studs = (from e in db.TStudents
                        
                         .OrderBy(order.ToString())
                            .Skip(request.skip)
                            .Take(request.take)
                            
                         select e).ToArray();

            // returns the generic response object which will contain the 
            // employees array and the total count
           var resp =  new Models.Response(studs, db.TStudents.Count());

            return Request.CreateResponse(HttpStatusCode.OK, resp);
        }
        // PUT: api/TStudents/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutTStudent(int id, TStudent tStudent)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != tStudent.StudentID)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(tStudent).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TStudentExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}


        [ResponseType(typeof(void))]
        [HttpPut]
        public async Task<IHttpActionResult> PutTStudent(TStudent tStudent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

          
            db.Entry(tStudent).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TStudentExists(tStudent.StudentID))
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

        // POST: api/TStudents
        [ResponseType(typeof(TStudent))]
        public async Task<IHttpActionResult> PostTStudent(TStudent tStudent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TStudents.Add(tStudent);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tStudent.StudentID }, tStudent);
        }

        // DELETE: api/TStudents/5
        //[ResponseType(typeof(TStudent))]
        //public async Task<IHttpActionResult> DeleteTStudent(int id)
        //{
        //    TStudent tStudent = await db.TStudents.FindAsync(id);
        //    if (tStudent == null)
        //    {
        //        return NotFound();
        //    }

        //    db.TStudents.Remove(tStudent);
        //    await db.SaveChangesAsync();

        //    return Ok(tStudent);
        //}

        // DELETE: api/TStudentsAPI/5
        [ResponseType(typeof(TStudent))]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteTStudent(TStudent student)
        {
            TStudent tStudent = await db.TStudents.FindAsync(student.StudentID);
            if (tStudent == null)
            {
                return NotFound();
            }

            db.TStudents.Remove(tStudent);
            await db.SaveChangesAsync();

            return Ok(tStudent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TStudentExists(int id)
        {
            return db.TStudents.Count(e => e.StudentID == id) > 0;
        }
    }
}