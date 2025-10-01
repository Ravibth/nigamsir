namespace Gateway.API.Middlewares.MiddlewareHelpers.RolesAndPermissionsMiddlewareHelper.DTOs
{
    public class UpdateUserDto
    {
        public string[]? roles { get; set; }
        public string? role_ids { get; set; }
        public string? emp_code { get; set; }
        public bool? status { get; set; }
    }
}
