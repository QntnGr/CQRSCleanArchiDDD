Implementations:
 .Repositories
 .services externes
 .EF Core code first
 .

 TO DO:
 .do not call repositories from controllers
 .add dto 
 .add aggregate and handler

 Migrations et configurations avec EF core 6:
	-dotnet ef --project NomDuProjet migrations add InitialCreate				(pour l'initialisation)
	-dotnet ef --project NomDuProjet migrations add NomMigration				(pour ajouter une migration)
	-dotnet ef --project NomDuProjet database update							(pour update la DB)