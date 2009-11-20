using System;

namespace InRetail.Reporting
{
    public interface ISqlCreateBuilder
    {
        string CreateSqlCreateStatementFromDto(Type dtoType);
    }
}