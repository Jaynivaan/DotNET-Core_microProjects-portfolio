//gs
using Day08.CardCli.Renderers;
using Day08.CardCli.Config;

//program.cs  should only orchestrate.
//
//D - dependency Injection principle
//Highlevel flow should depend on abstractions and orchestractions

//Avoid
//-Rendering logic here
//-Data creation Logic
//-Large business logic.

Console.OutputEncoding = System.Text.Encoding.UTF8;

//step1:
//Get developer card data
var developerCard = CardConfig.GetDeveloperCard();

//Step2:
//Render Development card
CardRenderer.Render(developerCard);

Console.WriteLine();

Console.ForegroundColor = ConsoleColor.DarkGreen;

Console.WriteLine("Press any key to exit..");

Console.ResetColor();

Console.ReadLine();
