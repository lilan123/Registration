using Registration.Domain.AggregatesModel.StudentAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Registration.Domain.AggregatesModel.TeacherAggregate
{
    public class TeacherEntity
    {

        public int RegId { get; set; }

        public string TeacherEmailAddress { get; set; }

        public string StudentEmailAddress { get; set; }

        public bool IsSuspend { get; set; }

        public string Notification { get; set; }
    }
}
