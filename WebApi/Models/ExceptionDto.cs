using System.Net;

namespace WebApi.Models;

public record ExceptionDto(HttpStatusCode StatusCode, string Description);