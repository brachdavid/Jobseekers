# Jobseekers

Tato aplikace slou�� k evidenci uchaze�� o zam�stn�n�, 
konkr�tn� juniorn�ch program�tor�, kte�� jsou spole�n� 
se sv�mi osobn�mi �daji a dovednostmi ukl�d�ni do datab�ze. 
Aplikace umo��uje p�id�v�n�, vyhled�v�n�
a maz�n� kandid�t�.

## Funkce aplikace

- **P�id�n� nov�ho kandid�ta**: Ulo�� do datab�ze ve�ker� d�le�it� �daje o kandid�tovi, tedy jeho k�estn� jm�no, p��jmen�, datum narozen�, bydli�t� (m�sto), telefonn� ��slo, e-mail a seznam programovac�ch jazyk�, kter� ovl�d�.
- **V�pis v�ech kandid�t�**: Zobraz� seznam v�ech kandid�t� z datab�ze spole�n� s jejich atributy.
- **Vyhled�v�n� kandid�t�**: Vyhled� kandid�ty ulo�en� v datab�zi na z�klad� znalosti konkr�tn�ho programovac�ho jazyka.
- **Maz�n� kandid�t�**: Mo�nost smazat kandid�ta na z�klad� jeho ID, nebo smazat v�echny kandid�ty z datab�ze.

## Po�adavky na syst�m

- .NET SDK 6.0 nebo nov�j��
- SQL Server Express (LocalDB) pro spr�vu datab�ze
- Entity Framework Core 6.0 nebo nov�j��

## Instalace a spu�t�n�
1. Naklonujte repozit�� na sv�j lok�ln� stroj
    ```bash
    git clone <https://github.com/brachdavid/Jobseekers>
2. Otev�ete projekt v prost�ed� Visual Studio nebo jin�m C# editoru.
3. V konzoli Spr�vce bal��k� NuGet spus�te p��kaz:
    ```bash
    Add-Migration InitialCreate
4. Po vytvo�en� migrace aplikujte zm�ny do datab�ze pomoc� p��kazu:
    ```bash
    Update-Database
5. Sestavte projekt a spus�te konzolovou aplikaci.

## Struktura projektu
- **Program.cs**: Hlavn� vstupn� bod aplikace.
- **CommunicationService.cs**: T��da zaji��uj�c� komunikaci s u�ivatelem.
- **CandidateService.cs**: T��da obsahuj�c� logiku pro spr�vu kandid�t� a interakci s datab�z�.
- **InputValidation.cs**: T��da pro validaci vstupn�ch dat od u�ivatele.
- **ApplicationDbContext.cs**: Kontext datab�ze pro spr�vu entit pomoc� Entity Framework.
- **Candidate.cs**: T��da reprezentuj�c� uchaze�e o zam�stn�n�.
- **ProgrammingLanguage**: T��da reprezentuj�c� programovac� jazyk.

## Technologie
- **C#**
- **OOP**
- **Entity Framework Core**
- **SQL Server (LocalDB)**
- **Validace vstup�**

## Autor

David B�ach - brasik20@seznam.cz