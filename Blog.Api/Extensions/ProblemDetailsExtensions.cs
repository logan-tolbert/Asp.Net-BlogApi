using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Extensions;


public static class ProblemDetailsExtensions
{
    public static IActionResult ArticleNotFound(this ControllerBase controller)
    {
        return controller.Problem(
            statusCode: StatusCodes.Status404NotFound,
            title: "404 Not Found",
            detail: "Article not found.");
    }

    public static IActionResult ArticleCreationFailed(this ControllerBase controller)
    {
        return controller.Problem(
            statusCode: StatusCodes.Status500InternalServerError,
            title: "500 Internal Server Error",
            detail: "Article creation failed.");
    }


}


