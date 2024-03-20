using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Security.Extensions;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Constants;
using Microsoft.IdentityModel.Tokens;

namespace Core.Application.Pipelines.Authorization;

public class AuthorizationBehavior<TRequest,TResponse>:IPipelineBehavior<TRequest, TResponse>
    where TRequest:IRequest<TResponse>,ISecuredRequest
{
    private readonly IHttpContextAccessor _contextAccessor;

    public AuthorizationBehavior(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        List<string>? userRoleClaims = _contextAccessor.HttpContext.User.ClaimRoles();

        if(userRoleClaims== null)
        {
            throw new AuthorizationException("You are not authenticated");
        }

        bool isnotMatchedAUserRoleClaimWithRequestRoles = userRoleClaims
            .FirstOrDefault(userRoleClaim => userRoleClaim == GeneralOperationClaims.Admin
            || request.Roles.Any(role => role == userRoleClaim)).IsNullOrEmpty();

        if (isnotMatchedAUserRoleClaimWithRequestRoles)
            throw new AuthorizationException("You are not authenticated");

        TResponse response = await next();

        return response;
    }

}
