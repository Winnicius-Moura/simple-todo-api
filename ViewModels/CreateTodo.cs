using System.ComponentModel.DataAnnotations;

namespace crudStudiesNET.ViewModels
{
  public class CreateTodoViewModel
  {
    [Required(ErrorMessage = "Title is required")]
    public required string Title { get; set; }
  }
}