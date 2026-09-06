namespace StudentAdministrationServices.Services.Interfaces;

public interface IUpdateService<T> where T : class
{
    /// <summary>
    ///     Updates an existing value of type <typeparamref name="T" />.
    /// </summary>
    /// <param name="value">The entity containing the updated data. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous update operation.</returns>
    Task UpdateAsync(T value);
}