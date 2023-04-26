using SeaPizza.Application.Common.Interfaces;

namespace SeaPizza.Application.Common.Mailing;

public interface IEmailTemplateService : ITransientService
{
    string GenerateEmailTemplate<T>(string templateName, T mailTemplateModel);
}
