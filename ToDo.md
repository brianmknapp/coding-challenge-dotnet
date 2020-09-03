# ToDo
AKA what I would have liked to do given more time

## Architecture
I'm a fan of Clean Architecture patters, especially as it pertains to testability.
Right now everything is at the WebApi layer, which can get large quickly. 
Typically this looks like:
* Core
* Infrastructure
* SharedKernel
* User Interfaces (WebApi, CLI, Web Page, etc)

Given the time to do these things, refactoring would take the explicit implementations out of the WebApi,
break the service layers into the Core, which contains domain stuff like the entity models, etc, and interfaces
to external dependencies (other apis, persistence layer, etc). 

The Infrastructure section would contain explicit implementations for the DbContexts and Repository details (EF, etc).

The SharedKernel would be a utility project that houses shared code (extensions, etc). If the project were to get large enough, this should become its own solution with a NuGet package (internal NuGet).

The UI layer then is totally abstracted from the specific implementations, leaving just the controllers, ApiModels (DTO's), and dependency injection tools.

## Code

Most of what I wrote was clean and followed SOLID design principles. I would like to handle more edge cases, add validation, probably use the Mediator pattern, and generally make it more user friendly. The errors as is don't give the user much to go on.

I'd also probably create a generic repository pattern, as if we're using EF, then it's useful to abstract the context even more.

I'd probably also switch the database to use Sqlite in memory at least, so it's relational, and allow it to be 

## Tests

Tests is where I'd spend a bit more time. The integration tests we have are okay, but are really full component tests.

The compensation controller uses both the compensation service and the employee service. Interactions with these services even with stubbed versions instead of mocked, would provide value in edge cases based on the external resources responses/state without needing to trigger it in live code.

We wouldn't see a ton of value in many repository layer tests at the unit or integration layer as we use EF to handle the interactions and our repository layer doesn't do much right now (assigns a GUID basically).

Our models should get tested in a sociable fashion, and there isn't much value in testing them directly as they are mainly POCOs.

## Business Logic

I'd probably also clarify some details on the two features, what should happen in event x or event y. This would be a great place to collaborate with the BA/Testers.

## Overall

Lot's of fun, really enjoyed it, and looking forward to any feedback you might have. Like I said in the interview, I thrive on feedback and learning, and I appreciate anything you're willing to transfer my way.