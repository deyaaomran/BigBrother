using BigBrother.Core.Entities;
using BigBrother.Core.Services.Contract;
using BigBrother.Repository.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBrother.Services.Services
{
    public class AssistantService : IAssistantService
    {
        private readonly AppDbContext _context;

        public AssistantService(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAssistantAsync(Asisstant assistant)
        {
            assistant.Status = "Pending";
            _context.asisstants.Add(assistant);
            await _context.SaveChangesAsync();
        }

        public async Task ApproveAssistantAsync(int assistantId)
        {
            var assistant = await _context.asisstants.FindAsync(assistantId);
            if (assistant == null) throw new Exception("Assistant not found.");

            assistant.Status = "Approved";
            await _context.SaveChangesAsync();
        }
    }
}
