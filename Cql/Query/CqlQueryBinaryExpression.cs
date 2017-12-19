﻿using Cql.Query.Execution;

namespace Cql.Query
{
    public class CqlQueryBinaryExpression : ICqlQueryExpression
    {
        public ConditionalOperator Operator { get; set; }

        public ICqlQueryExpression LeftExpression { get; set; }

        public ICqlQueryExpression RightExpression { get; set; }

        public void Accept(ICqlQueryExpressionVisitor expressionVisitor)
        {
            expressionVisitor.VisitQueryExpression(this);
        }
    }
}