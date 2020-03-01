using Registration.Domain.AggregatesModel.StudentAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.Application.Models
{
    public class RegisterModel
    {
        public string Teacher { get; set; }

        public string[] Students { get; set; }
    }
}
