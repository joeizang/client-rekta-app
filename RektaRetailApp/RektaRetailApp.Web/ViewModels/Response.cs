using System;
using System.Collections.Generic;

namespace RektaRetailApp.Web.ViewModels
{
    public class Response<T> where T : class
    {
        public T Data { get; } = null!;

        public List<object>? Errors { get; } = new List<object>();

        public string CurrentResponseStatus { get; }

        public Response(string currentResponseStatus, dynamic errors)
        {
            CurrentResponseStatus = currentResponseStatus;
            Errors?.Add(errors);
        }

        public Response(T data, string currentResponseStatus, dynamic errors = null!)
        {
            Data = data;
            CurrentResponseStatus = currentResponseStatus;
            if (errors == null || CurrentResponseStatus == ResponseStatus.Success) return;
            Errors?.Add(errors);
            CurrentResponseStatus = ResponseStatus.NonAction;
            Errors?.Add(new
            {
                ErrorMessage = "The response is assuming an invalid state and has been defaulted to a state of fault!"
            });
        }
    }

    public class ResponseStatus
    {
        public static string Success { get; } = nameof(Success);
        
        public static string Error { get; } = nameof(Error);

        public static string Failure { get; } = nameof(Failure);

        public static string NonAction { get; } = nameof(NonAction);
    }


    public class TaskPerformed
    {
        public static string Creation { get; } = nameof(Creation);

        public static string Modification { get; } = nameof(Modification);

        public static string Deletion { get; } = nameof(Deletion);
    }

}
