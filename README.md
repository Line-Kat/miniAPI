# miniAPI - et miniprosjekt i .NET

Dette er et enkelt Web API bygget med ASP.NET Core. Prosjektet demonstrerer hvordan man kan strukturere en løsning med kontroller, modeller og endepunkter, og hvordan man kan bruke innebygd validering for å sikre datakvalitet.

## Versjonskontroll

Prosjektet bruker Git for versjonskontroll. Siden jeg har jobbet alene med utviklingen, har jeg valgt å jobbe direkte på main-branchen. I et team-prosjekt ville jeg brukt en branch-struktur (f. eks. feature-branches og bugfix-branches for å sikre oversikt og redusere risiko for merge-konflikter. )	

## Prosjektstruktur og namespace

Prosjektet er organisert etter ansvar og funksjon, med egne mapper og namespaces for modeller, tjenester, kontroller og datatilgang. Dette forbedrer prosjektets struktur, gjør koden lettere å navigere, og følger beste praksis i .NET. Det gir også mer presise `using`-direktiver og forhindrer navnekonflikter.

## Arkitektur

Prosjektet er bygget etter Model-View-Controller (MVC)-prinsippet:

- **Models** – domeneobjekter (Oppgave)

- **Repositories** – ansvar for datatilgang og kommunikasjon med databasen

- **Services** – inneholder forretningslogikk

- **Controllers** – håndterer HTTP-forespørsler og API-endepunkter

Dette gir et godt grunnlag for en skalerbar løsning med databaseintegrasjon, testbarhet og modulær arkitektur.

## Funksjonalitet

- Hente alle oppgaver (`GET /api/oppgaver`)
  -> Returnerer en liste over alle oppgavene fra databasen. Bruker async/await for effektiv ressursbruk.

- Hente oppgave etter ID (`GET /api/oppgaver/{id}`)
  -> Returnerer en bestemt oppgave basert på ID, eller 404 Not Found hvis den ikke finnes.

- Legge til ny oppgave (`POST /api/oppgaver`)
  -> Oppretter en ny oppgave i databasen og returnerer 201 Created.

- Slette oppgaver etter ID (`DELETE /api/oppgaver/{id}`)
  -> Fjerner oppgaven fra databasen hvis den finnes. Returnerer 200 OK eller 404 Not found.

- Alle metoder er implementert med `async/await` i repository, service og controller.

- API-et er koblet til en database via Entity Framework Core med migrasjoner.

## Teknologier

- **ASP.NET Core** – brukes til å bygge og strukturere API-et, håndtere HTTP-forespørsler og validere input via modellklasser. 

- **Swagger** – gir et interaktivt grensesnitt for testing og dokumentasjon av API-endepunkter.

- **Entity Framework Core** – brukes til å koble API-et til en database (SQLite), håndtere datamodellering og lagring via `DbContext` og migrasjoner.

- **In-memory data** – ble brukt i tidligere versjon for å simulere databehandling før databaseintegrasjon ble implementert.

## Formål

Prosjektet er laget som en del av min læring i .NET og C#. Det viser hvordan man kan bygge en enkel, men strukturert løsning med fokus på god kodepraksis, typografi og dokumentasjon.

## Videre arbeid

- [ ] Legge til metode for oppdatering av oppgaver (PUT).
- [ ] Implementere DTO-er for bedre API-struktur og datavalidering.
- [ ] Lage en enkel frontend i React for å visualisere og teste API-et.
