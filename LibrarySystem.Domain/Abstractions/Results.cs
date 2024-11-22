using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace LibrarySystem.Domain.Abstractions;
public static class ResultExtensions
{
    public static ObjectResult ToProblem(this Error errorResult)
    {
        var problem = Results.Problem(statusCode: errorResult.StatusCode);
        var problemDetails = problem.GetType().GetProperty(nameof(ProblemDetails))!.GetValue(problem) as ProblemDetails;
        problemDetails!.Extensions = new Dictionary<string, object?>
            {
                {"errors",new[]{errorResult} }
            };
        return new ObjectResult(problemDetails);
    }
}
