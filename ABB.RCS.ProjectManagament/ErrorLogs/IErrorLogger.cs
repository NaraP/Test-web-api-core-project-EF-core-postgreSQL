using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABB.RCS.ProjectManagament.ErrorLogs
{
    public interface IErrorLogger
    {
        void ExceptionHandler(Exception ex, string MethodName, string ModuleName);
        void ExceptionWriteIntoTextFile(Exception ex, string MethodName, string StrLayer, string ModuleName);
    }
}
