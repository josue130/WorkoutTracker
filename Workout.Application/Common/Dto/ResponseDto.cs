using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workout.Application.Common.Dto
{
    public class ResponseDto
    {
        public int code { get; set; } = 200;
        public object? result { get; set; }
        public bool isSuccess { get; set; } = true;
        public string message { get; set; } = "";
    }

}
