using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeladacWeb.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HeladacWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeladacBaseController : ControllerBase
    {
        protected HeladacDbContext context = new HeladacDBContextFactory().CreateDbContext(new string[0]);
    }
}
