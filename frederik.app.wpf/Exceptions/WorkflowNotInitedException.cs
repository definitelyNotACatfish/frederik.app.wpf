using System;
using System.Collections.Generic;
using System.Text;

namespace frederik.app.wpf.Exceptions
{
    public class WorkflowNotInitedException: Exception
    {
        public WorkflowNotInitedException(string message, params object?[] args) : base(string.Format(message, args))
        {
        }
    }
}
