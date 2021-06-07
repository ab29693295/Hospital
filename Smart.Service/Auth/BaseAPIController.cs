using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Edu.Tools;
using System.Web.Http;
using Smart.Service;

namespace Smart.Service
{
    public class BaseAPIController : ApiController
    {
        protected UnitOfWork unitOfWork = new UnitOfWork();
        protected log4net.ILog logger = LogHelper.GetInstance().Log;

    }
}
