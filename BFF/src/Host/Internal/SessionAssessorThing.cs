using System;
using Microsoft.AspNetCore.Http;

namespace IdentityModel.AspNetCore
{
    public class SessionAssessorThing: ISessionAssessorFake
    {
        private IHttpContextAccessor _contextAccessor;
        
        public SessionAssessorThing(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string SomeData
        {
            get
            {
                return _contextAccessor.HttpContext.Request.Path + " " + DateTime.Now.ToString();
            }
        }
    }
}