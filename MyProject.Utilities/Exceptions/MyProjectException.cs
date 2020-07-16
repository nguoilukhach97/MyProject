using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject.Utilities.Exceptions
{
    public class MyProjectException: Exception
    {
        public MyProjectException()
        {

        }

        public MyProjectException(string message) : base(message)
        {

        }
        public MyProjectException(string message, Exception exception) : base(message, exception)
        {

        }
    }
}
