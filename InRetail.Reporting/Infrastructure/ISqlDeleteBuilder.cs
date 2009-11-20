using System.Collections.Generic;

namespace InRetail.Reporting
{
    public interface ISqlDeleteBuilder
    {
        string CreateSqlDeleteStatementFromDto<TDto>();
        string CreateSqlDeleteStatementFromDto<TDto>(IEnumerable<KeyValuePair<string, object>> example) where TDto : class;
    }
}