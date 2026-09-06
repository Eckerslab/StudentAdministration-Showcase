using StudentAdministrationDatabase.Models;
using StudentAdministrationServices.Models;

namespace StudentAdministrationServices.Mapping;

/// <summary>
///     Central mapping between database entities and the service-layer binding models.
/// </summary>
/// <remarks>
///     Keeping every entity ↔ binding-model conversion in one place removes the previously duplicated
///     (and slowly diverging) <c>ConvertTo</c> helpers that lived across the individual services.
/// </remarks>
public static class EntityMappings
{
    /// <summary>Maps a <see cref="Course" /> to a <see cref="CourseBindingModel" />.</summary>
    public static CourseBindingModel ToBindingModel(this Course course)
    {
        return new CourseBindingModel
        {
            Id = course.Id,
            Name = course.Name,
            Credit = course.Credit
        };
    }

    /// <summary>
    ///     Maps a <see cref="Grade" /> (with its <see cref="Grade.Course" /> loaded) to a
    ///     <see cref="GradeBindingModel" />.
    /// </summary>
    public static GradeBindingModel ToBindingModel(this Grade grade)
    {
        return new GradeBindingModel
        {
            Id = grade.Id,
            Note = grade.Value,
            Course = grade.Course.ToBindingModel()
        };
    }

    /// <summary>Maps a <see cref="Student" /> entity (with related data loaded) to a <see cref="StudentBindingModel" />.</summary>
    public static StudentBindingModel ToBindingModel(this Student student)
    {
        return new StudentBindingModel
        {
            Id = student.Id,
            FirstName = student.FirstName,
            LastName = student.LastName,
            Email = student.Email,
            Credits = student.Credits,
            StudentNumber = student.StudentNumber,
            DegreeProgram = new DegreeProgramBindingModel
            {
                Id = student.DegreeProgram.Id,
                Name = student.DegreeProgram.Name ?? string.Empty
            },
            University = new UniversityBindingModel
            {
                Id = student.University.Id,
                Name = student.University.Name!
            },
            Grades = student.Grades?.Select(ToBindingModel).ToList() ?? []
        };
    }

    /// <summary>Maps a <see cref="Student" /> entity to a lightweight <see cref="StudentListModel" />.</summary>
    public static StudentListModel ToListModel(this Student student)
    {
        return new StudentListModel
        {
            Id = student.Id,
            FirstName = student.FirstName,
            LastName = student.LastName,
            Email = student.Email,
            Credits = student.Credits,
            StudentNumber = student.StudentNumber,
            DegreeProgramListModel = new DegreeProgramListModel
            {
                Id = student.DegreeProgram!.Id,
                Name = student.DegreeProgram.Name!
            }
        };
    }

    /// <summary>Maps a <see cref="DegreeProgram" /> to a <see cref="DegreeProgramBindingModel" />.</summary>
    public static DegreeProgramBindingModel ToBindingModel(this DegreeProgram degreeProgram)
    {
        return new DegreeProgramBindingModel
        {
            Id = degreeProgram.Id,
            Name = degreeProgram.Name!
        };
    }

    /// <summary>Maps a <see cref="DegreeProgram" /> to a <see cref="DegreeProgramListModel" />.</summary>
    public static DegreeProgramListModel ToListModel(this DegreeProgram degreeProgram)
    {
        return new DegreeProgramListModel
        {
            Id = degreeProgram.Id,
            Name = degreeProgram.Name!
        };
    }

    /// <summary>Maps a <see cref="University" /> to a <see cref="UniversityBindingModel" />.</summary>
    public static UniversityBindingModel ToBindingModel(this University university)
    {
        return new UniversityBindingModel
        {
            Id = university.Id,
            Name = university.Name!
        };
    }

    /// <summary>Maps a <see cref="StudentBindingModel" /> back to a <see cref="Student" /> entity.</summary>
    public static Student ToEntity(this StudentBindingModel bindingModel)
    {
        return new Student
        {
            Id = bindingModel.Id,
            FirstName = bindingModel.FirstName,
            LastName = bindingModel.LastName,
            Email = bindingModel.Email,
            Credits = bindingModel.Credits,
            StudentNumber = bindingModel.StudentNumber,
            UniversityId = bindingModel.University!.Id,
            DegreeProgramId = bindingModel.DegreeProgram!.Id
        };
    }
}