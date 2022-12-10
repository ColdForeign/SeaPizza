using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SeaPizza.Infrastructure.Persistence;

public class DatabaseSettings : IValidatableObject
{
    public string ConnectionString { get; set; } = string.Empty;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(ConnectionString))
        {
            yield return new ValidationResult(
                $"{nameof(DatabaseSettings)}.{nameof(ConnectionString)} is not configured",
                new[] { nameof(ConnectionString) });
        }
    }
}
