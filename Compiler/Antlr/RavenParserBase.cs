using Antlr4.Runtime;

namespace Vixen.Raven.Antlr;

public abstract class RavenParserBase(ITokenStream input) : Parser(input);
