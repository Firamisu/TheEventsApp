# TheEventsApp

## Opis

### Rola Administratora i Zarządzanie
- **Konto Administratora**:
  - Tylko konta z rolą **Admin** mogą tworzyć, edytować lub usuwać wydarzenia.
  - Domyślnie skonfigurowane jest konto administratora z następującymi danymi logowania:
    - **Nazwa użytkownika**: `root@example.com`
    - **Hasło**: `Root123$` *(hasło można zmienić po zalogowaniu).*

### Uprawnienia Użytkowników
- **Użytkownicy**:
  - Mogą przeglądać i dołączać do dostępnych wydarzeń.
  - Liczba uczestników jest ograniczona maksymalnym limitem ustawionym dla każdego wydarzenia.

- **Administrator (Root)**:
  - Może **tworzyć**, **edytować** i **usuwać** wydarzenia.
  - Ma możliwość **usuwania uczestników** z wydarzeń.

### Zasady Dotyczące Wydarzeń
- Każde wydarzenie ma ustalony **maksymalny limit uczestników**, którego nie można przekroczyć.
- Administrator ma pełną kontrolę nad zarządzaniem uczestnikami i szczegółami wydarzeń, zapewniając sprawne działanie platformy.

### Dodatkowe Informacje
- Przy rejestrowaniu nowego konta, nie trzeba potwierdzać adresu e-mail.

# Instrukcje

1. Sklonuj repozytorium
2. Otwórz projekt w Visual Studio
3. Skonfiguruj bazę danych
4. Uruchom aplikację

### Domyślne Dane Logowania
- Email: `root@example.com`
- Hasło: `Root123$`

### Konfiguracja Bazy Danych

Utwórz bazę danych w lokalnym serwerze SQL o nazwie **EventsDB**.

Otwórz plik **appsettings.json** i zaktualizuj connection string, aby wskazywał na Twoją lokalną bazę danych SQL.

```json
"ConnectionStrings": {
	"EventsDb": "Server=TwojaNazwaSerwera;Database=EventsDB;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

### Migracja Bazy Danych

Otwórz **Konsolę Menedżera Pakietów** i uruchom poniższe polecenie:

```sh
Update-Database
```





