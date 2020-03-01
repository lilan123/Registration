using Microsoft.EntityFrameworkCore;
using Registration.Domain.AggregatesModel.TeacherAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registration.Infrastructure.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private RegistrationDbContext _dbContext;

        public TeacherRepository(RegistrationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task AddTeacher(TeacherEntity entity)
        {
            await _dbContext.Teachers.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public IEnumerable<TeacherEntity> FindStudentByNotification(string TeacherEmail, string[] StudentEmails, string Message)
        {
            return _dbContext.Teachers.Include(o => o.StudentEmailAddress).Where(o => o.TeacherEmailAddress == TeacherEmail && StudentEmails.Contains(o.StudentEmailAddress) && o.Notification.Contains(Message));
        }

        public IEnumerable<TeacherEntity> FindStudentByTeacherEmail(string teacherEmail)
        {
            return _dbContext.Teachers.Include(o => o.TeacherEmailAddress).Where(o => o.TeacherEmailAddress == teacherEmail);
        }

        public async Task UpdateStudentSuspend(string student)
        {
            IEnumerable<TeacherEntity> teacherEntities= _dbContext.Teachers.Include(o => o.StudentEmailAddress).Where(o => o.StudentEmailAddress == student);
            foreach (var stu in teacherEntities)
            {
                stu.IsSuspend = true;

                _dbContext.Attach(stu);
            }

            _dbContext.Entry(teacherEntities).Property("IsSuspend").IsModified = true;
            await _dbContext.SaveChangesAsync();

        }
    }
}
