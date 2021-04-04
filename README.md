# ZTPSBD
## Wstęp
Aplikacja bazodanowa będzie rozwiązywać problem zarządzania sklepem internetowym o tematyce wegańskiej żywności “Vege Shama”.
Rozwiązanie pozwala klientom na przeszukiwanie bazy produktów i składanie zamówień, umożliwia również po założeniu konta na sprawdzenie historii zamówień. Sprzedawcy będą również mogli zarządzać produktami w sklepie, a Administrator aplikacji będzie mógł zarządzać użytkownikami i produktami.
## Analiza wymagań systemu
### Wymagania funkcjonalne
 - Możliwość zakładania konta.
 - Możliwość składania zamówień przez klientów (zalogowanych i nie).
 - Możliwość zarządzania produktami i kategoriami do których należą przez sprzedawców / administratorów systemu.
 - Możliwość zarządzania użytkownikami przez administratorów.
 - Możliwość przeglądania swoich zamówień przez użytkownika.
 - Możliwość przechowywania adresów do dostawy przez użytkownika.
 - Możliwość edycji danych swojego konta przez użytkownika.

### Wymaganie niefunkcjonalne
 - Zabezpieczenie edycji chronionych danych przez nieuprawnionych użytkowników
 - Czytelny interfejs zależny od typu użytkownika
 
## Wykorzystane technologie
W projekcie wykorzystaliśmy następujące technologie:
 - Język programowania: C#
 - Frameworki: ASP.NET Core 3.1, Razor Pages
 - Framework ORM: Entity Framework Core
 - Relacyjna baza danych: SQL Server 2019 Express
