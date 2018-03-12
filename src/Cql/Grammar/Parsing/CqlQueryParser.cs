﻿using System;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Tree;
using Cmsql.Grammar.Parsing.Internal;

namespace Cmsql.Grammar.Parsing
{
    public class CqlQueryParser
    {
        public CqlQueryParseResult Parse(string cqlQuery)
        {
            if (string.IsNullOrWhiteSpace(cqlQuery))
            {
                throw new ArgumentException($"Parameter '{nameof(cqlQuery)}' is null, empty or whitespace.");
            }
            
            CmsqlParser parser = CreateParser(cqlQuery);
            parser.RemoveErrorListeners();

            CqlParserErrorListener errorListener = new CqlParserErrorListener();
            parser.AddErrorListener(errorListener);

            IParseTree parseTree = parser.queries();
            QueriesVisitor queriesVisitor = new QueriesVisitor();
            return new CqlQueryParseResult
            {
                Errors = errorListener.ParseErrors,
                Queries = queriesVisitor.Visit(parseTree)
            };
        }

        private CmsqlParser CreateParser(string cqlQuery)
        {
            return new CmsqlParser(
                new CommonTokenStream(
                    new CmsqlLexer(
                        new AntlrInputStream(cqlQuery))))
            {
                Interpreter = {PredictionMode = PredictionMode.Sll}
            };
        }
    }
}
