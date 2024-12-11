using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;
using NodeToStringBuilderBracketsBug;

// Define the EDM model
var model = EdmModelBuilder.TestModel;

// Define the OData query string
string text = "a sub (b sub c) eq 1";

// Parse and translate the OData query string
var parser = new ODataQueryOptionParser(
    model,
    EdmModelBuilder.GetEntityType(),
    EdmModelBuilder.GetEntitySet(),
    new Dictionary<string, string>() { { "$filter", text } });

var filterClause = parser.ParseFilter();

if (filterClause.Expression is BinaryOperatorNode binaryOperatorNode)
{
    NodeToStringBuilder visitor = new NodeToStringBuilder();
    var result = visitor.Visit(binaryOperatorNode);

    Console.WriteLine(result);
}

public class EdmModelBuilder
{
    public static IEdmModel TestModel = GetEdmModel();

    private static IEdmModel GetEdmModel()
    {
        var model = new EdmModel();

        var entityType = new EdmEntityType("NS", "Entity");
        entityType.AddKeys(entityType.AddStructuralProperty("ID", EdmPrimitiveTypeKind.Int32));
        entityType.AddStructuralProperty("a", EdmPrimitiveTypeKind.Int32);
        entityType.AddStructuralProperty("b", EdmPrimitiveTypeKind.Int32);
        entityType.AddStructuralProperty("c", EdmPrimitiveTypeKind.Int32);

        var entityContainer = new EdmEntityContainer("NS", "Container");
        var entitySet = entityContainer.AddEntitySet("Entities", entityType);
        model.AddElement(entityType);
        model.AddElement(entityContainer);

        return model;
    }

    public static IEdmEntityType? GetEntityType()
    {
        return TestModel.FindType("NS.Entity") as IEdmEntityType;
    }

    public static IEdmEntitySet GetEntitySet()
    {
        return TestModel.FindEntityContainer("Container").FindEntitySet("Entities");
    }
}