using Ardalis.Specification;

namespace ApplicationCore.Interfaces;
/// <summary>
/// Интерфейс репозитория с базовыми методами для получения данных.
/// </summary>
public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class
{
}