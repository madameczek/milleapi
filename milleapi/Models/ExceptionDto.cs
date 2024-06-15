using System.Net;

namespace milleapi.Models;

public record ExceptionDto(HttpStatusCode StatusCode, string Description);