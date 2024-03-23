using System;
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
using saafcity_fyp.Models;

namespace saafcity_fyp.Controllers
{
    public class ComplainnatsController : ApiController
    {
        private SaafCity_Database_2Entities db = new SaafCity_Database_2Entities();

        // GET: api/Complainnats
        public IQueryable<Complainnat> GetComplainnats()
        {
            return db.Complainnats;
        }

        // GET: api/Complainnats/5
        [ResponseType(typeof(Complainnat))]
        public async Task<IHttpActionResult> GetComplainnat(int id)
        {
            Complainnat complainnat = await db.Complainnats.FindAsync(id);
            if (complainnat == null)
            {
                return NotFound();
            }

            return Ok(complainnat);
        }

        // PUT: api/Complainnats/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutComplainnat(int id, Complainnat complainnat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != complainnat.Complainant_ID)
            {
                return BadRequest();
            }

            db.Entry(complainnat).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComplainnatExists(id))
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

        // POST: api/Complainnats
        [ResponseType(typeof(Complainnat))]
        public async Task<IHttpActionResult> PostComplainnat(Complainnat complainnat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Complainnats.Add(complainnat);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = complainnat.Complainant_ID }, complainnat);
        }

        // DELETE: api/Complainnats/5
        [ResponseType(typeof(Complainnat))]
        public async Task<IHttpActionResult> DeleteComplainnat(int id)
        {
            Complainnat complainnat = await db.Complainnats.FindAsync(id);
            if (complainnat == null)
            {
                return NotFound();
            }

            db.Complainnats.Remove(complainnat);
            await db.SaveChangesAsync();

            return Ok(complainnat);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ComplainnatExists(int id)
        {
            return db.Complainnats.Count(e => e.Complainant_ID == id) > 0;
        }
    }
}