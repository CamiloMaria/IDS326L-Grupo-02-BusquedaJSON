// See https://aka.ms/new-console-template for more information
using Antlr4.Runtime;
using BusquedaJson;
using System;
using System.Text.Json;



const string PUNTO = ".";
const string DOBLEPUNTO = ":";
static JsonElement GetFirstElement(JsonElement element) => element[0];
static JsonElement GetLastElement(JsonElement element) => element[-1];
static JsonElement GetElementPosition(JsonElement element, int index) => element[index];


static JsonElement SearchElement(JsonElement inicialElement, string[] roadToElement)
{

    if (inicialElement.ValueKind == JsonValueKind.Object)
    {
        foreach (JsonProperty element in inicialElement.EnumerateObject())
        {

            if (element.Name == roadToElement[0])
            {
                if (roadToElement.Length > 1)
                    return SearchElement(element.Value, roadToElement[1..]);
                else
                    return element.Value;
            }
            
                element.Value.GetRawText();
            
        }
    }

   


    throw new Exception($"No se encontró el elemento {roadToElement[0]}");
}

static JsonElement SearchElementString(JsonElement elementoInicial, string caminoElemento)
{
    if (caminoElemento.Contains(PUNTO))
    {
        return SearchElement(elementoInicial, caminoElemento.Split(PUNTO, StringSplitOptions.TrimEntries));
    }
    else
    {
        return SearchElement(elementoInicial, caminoElemento.Split(DOBLEPUNTO, StringSplitOptions.TrimEntries));
    }

}



static void BucarArbol(string expresion)
{
            

    

    var inputStream = new AntlrInputStream(expresion);
    var busquedaJsonLexer = new busquedaJsonLexer(inputStream);
    var commonTokenStream = new CommonTokenStream(busquedaJsonLexer);
    var busquedaJsonParser = new busquedaJsonParser(commonTokenStream);
    var busquedaJsonContext = busquedaJsonParser.program();
    var visitor = new BusquedaJson.BusquedaJson();
    visitor.Visit(busquedaJsonContext);

}


static void menu(JsonElement root, string json)
{
    bool salir = false;
    JsonElement elementoActual = root;

    do
    {
        Console.WriteLine("\nSeleccione una opcion:\n" +
                  "1. Mostrar el Json completo\n" +
                  "2. Mostar posicion actual\n" +
                  "3. Regresar al root\n" +
                  "4. Buscar expresion\n" +
                  "5. Buscar por posición\n" +
                  "6. Buscar primer posición\n" +
                  "7. Buscar ultima posición\n" +
                  "Escape. Salir");

        ConsoleKeyInfo key = Console.ReadKey(true);

        Console.Clear();
        switch (key.Key)
        {
            case ConsoleKey.D1:
                Console.WriteLine(root);
                break;
            case ConsoleKey.D2:
                Console.WriteLine(elementoActual);
                break;
            case ConsoleKey.D3:
                elementoActual = root;
                break;
            case ConsoleKey.D4:
                string expresion = "";
                Console.WriteLine("Digite su expresion: ");
                do
                {
                    expresion = Console.ReadLine();
                    if (expresion == "")
                    {
                        Console.WriteLine("\nDigite una expresion valida: ");
                    }
                    else if (expresion == null)
                    {
                        Console.WriteLine("\nNo se permiten expresiones nulas");
                    }
                } while (String.IsNullOrEmpty(expresion));

                Console.Clear();

                BucarArbol(expresion);

                elementoActual = SearchElementString(elementoActual, expresion);
                Console.WriteLine(elementoActual);

                break;
            case ConsoleKey.D5:
                Console.Write("Digite la posicion:");               
                int index = Convert.ToInt16(Console.ReadLine());
                elementoActual = GetElementPosition(elementoActual, index);
                Console.WriteLine(elementoActual);
                break;
            case ConsoleKey.D6:
                elementoActual = GetFirstElement(elementoActual);
                Console.WriteLine(elementoActual);
                break;
            case ConsoleKey.D7:
                elementoActual = GetLastElement(elementoActual);
                Console.WriteLine(elementoActual);               
                break;
            case ConsoleKey.Escape:
                salir = true;
                break;
            default:
                Console.WriteLine("Opcion Invalida");
                break;
        }

    } while (salir != true) ;
    
}

string json = @"{
    ""firstName"": ""John"",
    ""lastName"": ""doe"",
    ""age"": 26,
    ""address"": {
        ""streetAddress"": ""naist street"",
        ""city"": ""Nara"",
        ""postalCode"": ""630-0192""
    },
    ""phoneNumbers"": [
        {
            ""type"": ""iPhone"",
            ""number"": ""0123-4567-8888""
        },
        {
            ""type"": ""home"",
            ""number"": ""0123-4567-8910""
        }
    ]
}";

using (JsonDocument doc = JsonDocument.Parse(json))
{
    JsonElement root = doc.RootElement;
    menu(root, json);
}


