# WHAT'S INSIDE

+ In-Memory SQLLite for test, Postgres otherwise
+ Models and migrations (Stakeholder, Event, Comment)
  
+ Services (StakeholderService, EventService, CommentService) with CRUD ops
+ Services' implementation tests (IService<Stakeholder, int>, IService<Event, int>)

+ Controllers (StakeholderController, EventController, CommentController) with CRUD / HTTP ops + pagination
+ Partial controllers' implementation unit tests (Controller<Stakeholder, int>)

  
+ OpenAPI documentation at `/swagger`
+ Dockerfile with test running prior to build
+ Docker container waits 30s for database readiness
+ Docker compose files (production and development) with env vars & fallback values

# WHAT'S NOT INSIDE

+ API E2E tests
+ Entities paranoid deletion
+ Controller sorting NOR default sorting
+ Services' implementation tests for IService<Comment, int>
+ GraphQL, HAL, custom response wrapper, pagination metadata in API responses
+ Environment variables to configure the container aren't documented
+ Controllers' unit tests for Controller<Event, int> and Controller<Comment, int>

# GETTING STARTED

From within the project root...

Run tests?
```sh
$ dotnet test TestProgrammationConformitTest
```

Run application? `::5000`
```sh
$ ConnectionStrings__ConformitDb=<database-connection-string> \
      dotnet run --project TestProgrammationConformit
```

Build Docker image? It won't build if some tests are failing.
```sh
$ docker build --tag <custom-tag> .
```

Start with docker-compose? `::80`
```sh
$ docker-compose up --build
```

Start with docker-compose development environment settings? `::80`
```sh
$ docker-compose --file docker-compose.override.yml up --build
```

- - - - - - - - - - -

# GOALS
Le but de l'exercice est de faire une REST API pour de la gestion d'evenement.
Il doit etre possible d'ajouter, modifier, supprimer et visualiser des evenements (sous forme de liste et de manière individuel).
Je peux gérer des commentaires pour un evenements (création, modification, suppression, visualisation).
Pour le retour de la liste des evenements, chaque evenement doit contenir sa liste de commentaires.
La liste des événements doit permettre la pagination.
Les migrations de database doivent etre faite avec Entity Framework Core.
Les retours doivent etre de type JSON.

Un évènement est constitué d'un titre de 100 caracteres maximum, d'une description et d'une personne impliquée dans l'évènement.
Un commentaire est constitué d'une description et d'une date et est obligatoirement relié à un evenement.

# PROVISION
	Projet de base sans controlleur avec Context (complet + migration auto) et docker de setup avec un postgresql
