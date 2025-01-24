using BigBrother.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBrother.Core.Services.Contract
{
    public interface IAssistantService
    {
        Task AddAssistantAsync(Asisstant assistant);
        Task ApproveAssistantAsync(int assistantId);
    }
}
