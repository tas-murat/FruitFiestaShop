using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Common.BaseCore.Results.Abstract
{
    public interface IResult
    {
        [JsonIgnore(Condition =JsonIgnoreCondition.WhenWritingNull)]
        public bool Success { get;  }
        [JsonIgnore(Condition =JsonIgnoreCondition.WhenWritingNull)]
        public string Message { get;  }
        [JsonIgnore(Condition =JsonIgnoreCondition.WhenWritingNull)]
        public string ProjectName { get;  }
        [JsonIgnore(Condition =JsonIgnoreCondition.WhenWritingNull)]
        public string ErrorType { get;  }
    }
}
