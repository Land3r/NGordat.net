namespace ngordat.net.backend.services
{
  using System.Collections.Generic;

  /// <summary>
  /// ICrud interface.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public interface ICrud<T>
  {
    /// <summary>
    /// Gets the list of <see cref="T"/>.
    /// </summary>
    /// <returns>The list of <see cref="T"/>.</returns>
    IEnumerable<T> Get();

    /// <summary>
    /// Gets a <see cref="T"/>; based on it's Id.
    /// </summary>
    /// <param name="id">The Id of the <see cref="T"/> to retrieve.</param>
    /// <returns>The <see cref="T"/> that matches the provided id.</returns>
    T Get(string id);

    /// <summary>
    /// Creates a new <see cref="T"/>.
    /// </summary>
    /// <param name="toCreate">The <see cref="T"/> to create.</param>
    /// <returns>The <see cref="T"/> created.</returns>
    T Create(T toCreate);

    /// <summary>
    /// Updates the <see cref="T"/> with the provided Id.
    /// </summary>
    /// <param name="id">The Id of the <see cref="T"/> to update.</param>
    /// <param name="toUpdate">The <see cref="T"/> to update.</param>
    /// <returns>The <see cref="T"/> updated.</returns>
    T Update(string id, T toUpdate);

    /// <summary>
    /// Updates a <see cref="T"/>.
    /// </summary>
    /// <param name="toUpdate">The <see cref="T"/> to update.</param>
    /// <returns>The <see cref="T"/> updated.</returns>
    T Update(T toUpdate);

    /// <summary>
    /// Deletes the <see cref="T"/> with the provided Id.
    /// </summary>
    /// <param name="id">The Id of the <see cref="T"/> to delete.</param>
    /// <returns>Whether or not the operation was successfull.</returns>
    bool Delete(string id);

    /// <summary>
    /// Deletes a <see cref="T"/>.
    /// </summary>
    /// <param name="toDelete">The <see cref="T"/> to delete.</param>
    /// <returns>Whether or not the operation was successfull.</returns>
    bool Delete(T toDelete);
  }
}
