### GitSearcher - Prosta aplikacja do wyszukiwania użytkowników GitHub
GitSearcher to prosta aplikacja desktopowa stworzona w technologii WPF (Windows Presentation Foundation) w języku C#. Aplikacja pozwala na wyszukiwanie użytkowników GitHub oraz pobieranie informacji o ich repozytoriach, korzystając z API GitHub. Zastosowano wzorzec projektowy MVVM (Model-View-ViewModel), co zapewnia przejrzysty podział między interfejsem użytkownika, logiką aplikacji oraz danymi.

### Spis treści
- Funkcje
- Instalacja
- Technologie

### Funkcje
- Wyszukiwanie użytkowników GitHub: Wprowadź nazwę konta GitHub, aby wyszukać informacje o jego repozytoriach.
- Lista repozytoriów: W aplikacji znajduje się lista repozytoriów użytkownika wraz z ich nazwami,  avatarami, opisami oraz linkami do repozytorium.

### Instalacja
 - Sklonuj repozytorium na swoje urządzenie:
  ```cs
git clone https://github.com/twoje/repozytorium.git
  ```
- Otwórz projekt w Visual Studio.
- Przygotuj środowisko, instalując wymagane pakiety NuGet (np. Newtonsoft.Json).
- Uruchom aplikację.

### Technologie
- Język programowania: C#
- Interfejs użytkownika: WPF (Windows Presentation Foundation)
- Wzorzec projektowy: MVVM (Model-View-ViewModel)
- API: GitHub REST API v3
