namespace CarRentalSystem.Web.Common
{
    using Application.Common;
    using Microsoft.AspNetCore.Mvc;

    public static class ResultExtensions
    {
        public static async Task<ActionResult<TData>> ToActionResult<TData>(this Task<TData> resultTask)
        {
            TData result = await resultTask;

            if (result == null)
            {
                return new NotFoundResult();
            }

            return result;
        }

        public static async Task<ActionResult> ToActionResult(this Task<Result> resultTask)
        {
            Result result = await resultTask;

            if (!result.Succeeded)
            {
                return new BadRequestObjectResult(result.Errors);
            }

            return new OkResult();
        }

        public static async Task<ActionResult<TData>> ToActionResult<TData>(this Task<Result<TData>> resultTask)
        {
            Result<TData> result = await resultTask;

            if (!result.Succeeded)
            {
                return new BadRequestObjectResult(result.Errors);
            }

            return result.Data;
        }
    }
}
