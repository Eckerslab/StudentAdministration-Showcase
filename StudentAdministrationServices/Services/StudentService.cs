using System.Collections.ObjectModel;
using StudentAdministrationDatabase.Repositories.Interfaces;
using StudentAdministrationServices.Mapping;
using StudentAdministrationServices.Models;
using StudentAdministrationServices.Services.Interfaces;

namespace StudentAdministrationServices.Services;

/// <summary>
///     Student service implementation.
/// </summary>
public class StudentService : IStudentService
{
    private readonly IStudentRepository studentRepository;

    /// <summary>
    ///     StudentService constructor.
    /// </summary>
    /// <param name="studentRepository">The student repository used for all persistence operations.</param>
    public StudentService(IStudentRepository studentRepository)
    {
        this.studentRepository = studentRepository;
    }

    /// <summary>
    ///     Adds a new student.
    /// </summary>
    /// <param name="value">The student binding model to add.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task AddAsync(StudentBindingModel value)
    {
        await studentRepository.AddAsync(value.ToEntity());
    }

    /// <summary>
    ///     Deletes a student record asynchronously based on the provided unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the student to be deleted.</param>
    /// <returns>A task that represents the asynchronous delete operation.</returns>
    public async Task DeleteAsync(Guid id)
    {
        await studentRepository.DeleteAsync(id).ConfigureAwait(false);
    }

    /// <summary>
    ///     Retrieves all students along with their associated degree programs and universities.
    /// </summary>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains a list of
    ///     <see cref="StudentBindingModel" /> objects representing the students.
    /// </returns>
    public async Task<List<StudentBindingModel>> GetAllAsync()
    {
        var studentEntities = await studentRepository.GetAllAsync();
        return studentEntities.Select(EntityMappings.ToBindingModel).ToList();
    }

    /// <summary>
    ///     Retrieves a student by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the student to retrieve.</param>
    /// <returns>A task whose result is the matching <see cref="StudentBindingModel" />.</returns>
    public async Task<StudentBindingModel> GetByIdAsync(Guid id)
    {
        var student = await studentRepository.GetByIdAsync(id);
        return student.ToBindingModel();
    }

    /// <summary>
    ///     Retrieves a list of student list models, which contain summarized information about students.
    /// </summary>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains a list of
    ///     <see cref="StudentListModel" />.
    /// </returns>
    public async Task<ObservableCollection<StudentListModel>> GetAllStudentListModels()
    {
        // Uses the lightweight list query (degree program only) instead of the full graph with grades.
        var students = await studentRepository.GetAllForListAsync();
        return new ObservableCollection<StudentListModel>(students.Select(EntityMappings.ToListModel));
    }

    /// <summary>
    ///     Updates the student record with the values provided in the specified binding model.
    /// </summary>
    /// <param name="student">
    ///     A binding model containing the updated student information. The student must have a valid identifier.
    /// </param>
    /// <returns>A task that represents the asynchronous edit operation.</returns>
    public async Task UpdateAsync(StudentBindingModel student)
    {
        await studentRepository.UpdateAsync(student.ToEntity());
    }

    /// <summary>
    ///     Returns the next unique student number to assign to a newly created student.
    /// </summary>
    /// <returns>A task whose result is the next available, collision-free student number.</returns>
    public async Task<int> GetNextStudentNumberAsync()
    {
        return await studentRepository.GetNextStudentNumberAsync();
    }
}