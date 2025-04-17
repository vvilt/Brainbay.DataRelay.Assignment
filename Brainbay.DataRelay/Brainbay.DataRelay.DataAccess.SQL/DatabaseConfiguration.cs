using System.ComponentModel.DataAnnotations;

namespace Brainbay.DataRelay.DataAccess.SQL;

public class DatabaseConfiguration
{
    [Required(ErrorMessage = $"{nameof(ConnectionString)} is required")]
    public string ConnectionString { get; set; }
}