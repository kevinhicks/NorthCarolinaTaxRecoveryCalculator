using AttributeRouting;
using AttributeRouting.Web.Http;
using NorthCarolinaTaxRecoveryCalculator.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NorthCarolinaTaxRecoveryCalculator.Api
{
    [RoutePrefix("api")]
    public class PaymentVoucherController : ApiController
    {
        [POST("saveVoucher")]
        public string Post([FromBody]PaymentVoucher voucher)
        {
            return voucher.ID.ToString();
        }/*

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }*/
    }
}