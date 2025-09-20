namespace DataLayer.Application.Models.CommandsAndQueries.AppUser.Commands.Register;

public record RegisterAppUserDto(string Email, string Password, string FirstName, string LastName)
{
}