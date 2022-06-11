using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Базовая сущность
/// </summary>
public class BaseEntity
{
    /// <summary>
    /// Самогенерирующийся идентификатор
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; protected set; }
}