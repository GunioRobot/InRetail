using System.Collections.Generic;

namespace InRetail.Reporting
{
    public interface ISqlSelectBuilder 
    {
        string CreateSqlSelectStatementFromDto<TDto>() ;
        string CreateSqlSelectStatementFromDto<TDto>(IEnumerable<KeyValuePair<string, object>> example) where TDto : class;
    }
}