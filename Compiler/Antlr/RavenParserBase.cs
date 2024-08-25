using Antlr4.Runtime;

namespace Vixen.Raven;

public abstract class RavenParserBase(ITokenStream input) : Parser(input);
