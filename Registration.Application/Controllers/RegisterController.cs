using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Registration.Application.Models;
using Registration.Domain.AggregatesModel.TeacherAggregate;

namespace Registration.Application.Controllers
{
    [Route("api")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private ITeacherRepository _teacherRepository;
        private readonly ILogger _logger;
        public RegisterController(
            ITeacherRepository teacherRepository,
            ILogger<RegisterController> logger)
        {
            _teacherRepository = teacherRepository;
            _logger = logger;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RegisterModel>> RegisterAsync(RegisterModel registerModel)
        {

            try
            {
                foreach (var student in registerModel.Students)
                {
                    TeacherEntity teacherEntity = new TeacherEntity();
                    teacherEntity.TeacherEmailAddress = registerModel.Teacher;
                    teacherEntity.StudentEmailAddress = student;

                    await _teacherRepository.AddTeacher(teacherEntity);

                }
                return Ok("Teacher " + registerModel.Teacher + "  successfully registered");
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            
        }

        [HttpGet("commonstudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetCommonstudents(string teacher)
        {

            try
            {
                var students =  _teacherRepository.FindStudentByTeacherEmail(teacher).Select(o => o.StudentEmailAddress);
                return Ok(students);
                 
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPost("suspend")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PostSuspendAsync([FromBody]StudentModel student)
        {

            try
            {
                await _teacherRepository.UpdateStudentSuspend(student.Student);
                return Ok();

            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPost("retrievefornotifications")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult PostRetrieveForNotificationsAsync([FromBody]NotificationModel notificationModel)
        {

            try
            {

                if (notificationModel.Notification.IndexOf("@")> 0)
                {
                    string str = new string(' ', 1) + notificationModel.Notification.Substring(notificationModel.Notification.IndexOf("@"), notificationModel.Notification.Length - notificationModel.Notification.IndexOf("@")).Insert(0, string.Empty);
                    string message = notificationModel.Notification.Substring(0, notificationModel.Notification.IndexOf("@"));
                    string[] Emails = str.Split(new string[] { " @" }, StringSplitOptions.None);
                    Emails = Emails.Skip(1).ToArray();

                    var students = _teacherRepository.FindStudentByNotification(notificationModel.Teacher, Emails, message).Select(a => a.StudentEmailAddress).ToList();
                    Recipients recipients = new Recipients();

                    recipients.recipients = students;
                    return Ok(recipients);
                }
                else
                {
                    var students = _teacherRepository.FindStudentByNotification(notificationModel.Teacher, notificationModel.Notification).Select(a => a.StudentEmailAddress).ToList();
                    Recipients recipients = new Recipients();

                    recipients.recipients = students.Distinct().ToList();

                    return Ok(recipients);
                }


            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

        }
    }
}