
//gs

using Day08.CardCli.Models;

namespace Day08.CardCli.Config
{
	//S-SRP
	//This class only prepares Configuration /data.
	//
	// In future: 
	// - could load from JSON
	// - could load from Database
	// - could load from Api
	//
	//Renderer should not care where the data flowing from..

	public static class CardConfig
	{
		public static DeveloperCard GetDeveloperCard()
		{
			return new DeveloperCard
			{
				Name = " Jayakrishnan ParameswaranKutty",
				Role = " DotNET Developer | DotNET Core Practitioner",

				Github = "github.com/Jaynivaan",

				Website = "https://www.hellonivaan.com",

				LearningDirection =
					"Clean Architecture | AI Systems | Distributed Thinking",

				ArchitecturePhilosophy =
					"Small Completed Systems build stronger Engineering intuition",

				CurrentQuest =
					"Mastering Clean Dependency flow and S.O.L.I.D Architecture.",

				FavouriteEnergy =
					"Curiosity + Discipline + Consisitency + my Heart",

				MyPrimePrinciple =
					"give without attachment to expectations or outcomes..Just give,,, give__give++++"
			};


		}


	}


}