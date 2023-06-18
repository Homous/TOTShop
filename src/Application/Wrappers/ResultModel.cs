using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrappers;

public class ResultModel
{
    public string Message { get; set; }
    public bool Status { get; set; }
    public Object Data { get; set; }

    public ResultModel()
    {

    }
    public ResultModel(string message, bool status, object data)
    {
        this.Message = message;
        this.Status = status;
        this.Data = data;
    }
}
