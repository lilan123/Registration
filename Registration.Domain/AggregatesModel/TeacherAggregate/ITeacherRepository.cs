using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Registration.Domain.AggregatesModel.TeacherAggregate
{
    public interface ITeacherRepository 
    {
        Task AddTeacher(TeacherEntity entity);

        Task UpdateStudentSuspend(string student);

        IEnumerable<TeacherEntity> FindStudentByNotification(string TeacherEmail, string StudentEmail, string Message);

        IEnumerable<TeacherEntity> FindStudentByTeacherEmail(string Email);
    }
}
