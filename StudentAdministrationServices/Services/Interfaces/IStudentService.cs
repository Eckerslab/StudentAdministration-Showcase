using System.Collections.ObjectModel;
using StudentAdministrationServices.Models;

namespace StudentAdministrationServices.Services.Interfaces;

/// <summary>
///     Interface for student service operations.
/// </summary>
public interface IStudentService : IUpdateService<StudentBindingModel>, IReceiveService<StudentBindingModel>,
    IDeleteService, IAddService<StudentBindingModel>
{
    /// <summary>
    ///     Retrieves a list of student models representing all students.
    /// </summary>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains a list of student models for all
    ///     students. The list will be empty if no students are found.
    /// </returns>
    Task<ObservableCollection<StudentListModel>> GetAllStudentListModels();

    /// <summary>
    ///     Returns the next unique student number to assign to a newly created student.
    /// </summary>
    /// <returns>A task whose result is the next available, collision-free student number.</returns>
    Task<int> GetNextStudentNumberAsync();
}