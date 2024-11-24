namespace DTO.Concrete.AccountDTO;

public class EditUserDTO
{
  public Guid Id { get; set; }
  public DateTime CreatedDate { get; set; }
  public DateTime? UpdatedDate { get; set; }
  public string FirstName { get; set; }
  public string LastName { get; set; }
  public string Email { get; set; }
  public string UserName { get; set; }
  public DateTime BirthDate { get; set; }
  public string? Password { get; set; }
}