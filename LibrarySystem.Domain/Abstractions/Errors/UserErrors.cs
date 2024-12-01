using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LibrarySystem.Domain.Abstractions.Errors
{
    public static class UserErrors
    {
        public static readonly Error IsFound = new("User.IsFound", "This user already has an email", StatusCodes.Status409Conflict);
        public static readonly Error NotFound = new("User.NotFound", "This user has not an email", StatusCodes.Status401Unauthorized);
        public static readonly Error InValid = new("User.InValidCreditiona", "email or password is not correct", StatusCodes.Status401Unauthorized);
        public static readonly Error IsLocked = new("User.IsLocked", "This user was locked", StatusCodes.Status401Unauthorized);
        public static readonly Error InValidToken = new("User.InValidToken", "This token is inValid", StatusCodes.Status400BadRequest);
        public static readonly Error InValidRefreshToken = new("User.InValidRefreshToken", "This refreshToken is inValid", StatusCodes.Status400BadRequest);
        public static readonly Error EmailConfirmed = new("User.EmailConfirmed", "This email confirmed", StatusCodes.Status409Conflict);
        public static readonly Error EmailNotConfirmed = new("User.EmailNotConfirmed", "This email not confirmed", StatusCodes.Status400BadRequest);
        public static readonly Error InValidEmailype = new("User.InValidEmailype", "This email not gmail", StatusCodes.Status400BadRequest);







    }
}
