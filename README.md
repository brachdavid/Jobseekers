# Jobseekers

Tato aplikace slouí k evidenci uchazeèù o zamìstnání, 
konkrétnì juniorních programátorù, kteøí jsou spoleènì 
se svımi osobními údaji a dovednostmi ukládáni do databáze. 
Aplikace umoòuje pøidávání, vyhledávání
a mazání kandidátù.

## Funkce aplikace

- **Pøidání nového kandidáta**: Uloí do databáze veškeré dùleité údaje o kandidátovi, tedy jeho køestní jméno, pøíjmení, datum narození, bydlištì (mìsto), telefonní èíslo, e-mail a seznam programovacích jazykù, které ovládá.
- **Vıpis všech kandidátù**: Zobrazí seznam všech kandidátù z databáze spoleènì s jejich atributy.
- **Vyhledávání kandidátù**: Vyhledá kandidáty uloené v databázi na základì znalosti konkrétního programovacího jazyka.
- **Mazání kandidátù**: Monost smazat kandidáta na základì jeho ID, nebo smazat všechny kandidáty z databáze.

## Poadavky na systém

- .NET SDK 6.0 nebo novìjší
- SQL Server Express (LocalDB) pro správu databáze
- Entity Framework Core 6.0 nebo novìjší

## Instalace a spuštìní
1. Naklonujte repozitáø na svùj lokální stroj
    ```bash
    git clone <https://github.com/brachdavid/Jobseekers>
2. Otevøete projekt v prostøedí Visual Studio nebo jiném C# editoru.
3. V konzoli Správce balíèkù NuGet spuste pøíkaz:
    ```bash
    Add-Migration InitialCreate
4. Po vytvoøení migrace aplikujte zmìny do databáze pomocí pøíkazu:
    ```bash
    Update-Database
5. Sestavte projekt a spuste konzolovou aplikaci.

## Struktura projektu
- **Program.cs**: Hlavní vstupní bod aplikace.
- **CommunicationService.cs**: Tøída zajišující komunikaci s uivatelem.
- **CandidateService.cs**: Tøída obsahující logiku pro správu kandidátù a interakci s databází.
- **InputValidation.cs**: Tøída pro validaci vstupních dat od uivatele.
- **ApplicationDbContext.cs**: Kontext databáze pro správu entit pomocí Entity Framework.
- **Candidate.cs**: Tøída reprezentující uchazeèe o zamìstnání.
- **ProgrammingLanguage**: Tøída reprezentující programovací jazyk.

## Technologie
- **C#**
- **OOP**
- **Entity Framework Core**
- **SQL Server (LocalDB)**
- **Validace vstupù**

## Autor

David Bøach - brasik20@seznam.cz