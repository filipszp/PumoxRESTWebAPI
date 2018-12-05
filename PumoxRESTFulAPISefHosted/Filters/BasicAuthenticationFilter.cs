﻿using System;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace PumoxRESTFulAPI.Filters.Filters
{
    /// <summary>Security REST WebAPI</summary>
    ///<example>Adnotacja na metodzie kontrolera<code title="nieaktywny">[BasicAuthenticationFilter(false)]</code></example>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class BasicAuthenticationFilter : AuthorizationFilterAttribute
    {
        private readonly bool _isActive = true;

        public BasicAuthenticationFilter()
        {
        }
        public BasicAuthenticationFilter(bool isActive)
        {
            _isActive = isActive;
        }
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //
            if (!_isActive) return;

            var authHeader = actionContext.Request.Headers.Authorization;
            if (authHeader != null)
            {
                var authenticationToken = actionContext.Request.Headers.Authorization.Parameter;
                var decodedAuthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));
                var usernamePasswordArray = decodedAuthenticationToken.Split(':');
                var userName = usernamePasswordArray[0];
                var password = usernamePasswordArray[1];

                // Replace this with your own system of security / means of validating credentials
                var isValid = userName == "pumox" && password == "pumox";

                if (isValid)
                {
                    var principal = new GenericPrincipal(new GenericIdentity(userName), null);
                    Thread.CurrentPrincipal = principal;
                    return;
                }
            }

            HandleUnathorized(actionContext);
        }

        private static void HandleUnathorized(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            actionContext.Response.Headers.Add("WWW-Authenticate", "Basic Scheme='Data' location = 'http://localhost:8080");
        }
    }
}

