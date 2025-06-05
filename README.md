# ⚽ FootballManager

FootballManager to rozbudowana aplikacja webowa stworzona w ASP.NET Core MVC, umożliwiająca zarządzanie piłkarzami, drużynami, trenerami i meczami. Użytkownicy mogą przeglądać dane, filtrować je i sortować, a administratorzy mają dostęp do panelu zarządzania oraz listy kont użytkowników.
________________________________________
### 🚀 Funkcjonalności
-	🔐 Rejestracja i logowanie użytkowników
-	🧑‍🏫 Przeglądanie listy trenerów
-	🧍‍♂️ Lista piłkarzy z możliwością sortowania i filtrowania po drużynie
-	🏆 Przeglądanie rozegranych meczów z wynikami i datami
-	🏟️ Lista drużyn z opisami, krajami, punktami i kapitanami
-	👤 Panel administratora z listą zarejestrowanych użytkowników
-	🌐 Widoki Razor (.cshtml), layout i stylowanie
-	🗂️ Seedy z piłkarzami, drużynami, meczami i trenerami
________________________________________
### 🧰 Technologie
-	ASP.NET Core MVC (.NET 6.0)
-	Entity Framework Core (SQLite)
-	Razor Views / Layout / Partial Views
-	Sesje ASP.NET Core
-	Własna autoryzacja i role
-	CSS (własny styl + Bootstrap/utility classes)
________________________________________



### 🗃️ Struktura katalogów
```text
FootballManager/
├── Controllers/         # HomeController, PlayerController, TeamController, MatchController, CoachController, AccountController
├── Models/              # Players, Teams, Matches, Coach, User
├── Views/               # Razor Views (.cshtml) + Layout, Partial Views
│   ├── Players/
│   ├── Teams/
│   ├── Matches/
│   ├── Coach/
│   ├── Logins/
│   └── Rejester/
├── wwwroot/             # CSS, JS, grafiki, favicony
├── Program.cs           # Entry point
├── Startup.cs           # Middleware, DI, konfiguracja baz danych
├── appsettings.json     # Konfiguracja połączenia z bazą SQLite
└── football.db          # SQLite DB z danymi piłkarskimi
```
________________________________________
### 👤 Role i dostęp
| Rola   | Uprawnienia                                    |
|--------|------------------------------------------------|
| Admin  | Tworzenie, edycja, usuwanie danych, zarządzanie użytkownikami |
| User   | Przeglądanie danych, logowanie, rejestracja    |

	
________________________________________



### 🔐 Bezpieczeństwo
Aplikacja wykorzystuje podstawowe mechanizmy ochrony danych i kontroli dostępu:
- Hasła użytkowników są haszowane (SHA256)
- Sesje ASP.NET do przechowywania zalogowanego użytkownika
- Dostęp do widoków ograniczony rolą (IsAdmin, IsLogged)
- Formularze walidowane po stronie serwera
- Widoki publiczne i prywatne wyraźnie oddzielone
________________________________________
### 🧪 Przykładowe widoki

| Widok                   | Ścieżka             |
|-------------------------|---------------------|
| Logowanie               | `/IO/Logins`         |
| Rejestracja             | `/IO/Register`      |
| Lista piłkarzy          | `/IO/Players`       |
| Lista drużyn            | `/IO/Teams`         |
| Mecze                   | `/IO/Match`         |
| Trenerzy                | `/IO/Coach`         |
| Użytkownicy (admin)     | `/IO/Users`         |


________________________________________
### 🧩 Dane przykładowe (seedy)
* Piłkarze: Lewandowski, Messi, Ronaldo, Neymar, Mbappé, Modrić itd.
* Drużyny: FC Barcelona, Real Madrid, PSG, Al Nassr, Al Hilal
* Trenerzy: Xavi, Ancelotti, Guardiola itd.
*  Mecze: FC Barcelona vs Real Madrid, PSG vs Man City...
________________________________________



### 📦 Uruchomienie lokalne
Wymagania
•	.NET SDK 6.0 lub nowszy
•	Visual Studio 2022 lub Visual Studio Code
Krok po kroku

```bash
git clone <repo-url>
cd FootballManager
dotnet run
