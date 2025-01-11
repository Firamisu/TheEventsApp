# TheEventsApp

## Opis

### Rola Administratora i Zarządzanie
- **Konto Administratora**:
  - Tylko konta z rolą **Admin** mogą usuwać wszystkie wydarzenia.
  - **Administrator nie może edytować wydarzeń utworzonych przez innych użytkowników.**
  - Domyślne dane logowania:
    - **Email**: `root@example.com`
    - **Hasło**: `Root123$` *(hasło można zmienić po zalogowaniu).*

### Uprawnienia Użytkowników
- **Użytkownicy**:
  - Mogą tworzyć, edytować i usuwać **swoje własne wydarzenia**.
  - Mogą zarządzać uczestnikami w **swoich wydarzeniach**.
- **Administrator (Root)**:
  - Może usuwać wszystkie wydarzenia.

### Zasady Dotyczące Wydarzeń
- Każde wydarzenie ma ustalony **maksymalny limit uczestników**, którego nie można przekroczyć.
- Użytkownik ma pełną kontrolę nad wydarzeniami, które sam utworzył.

---

## Instrukcje

1. Sklonuj repozytorium.
2. Otwórz projekt w Visual Studio.
3. Skonfiguruj bazę danych.
4. Uruchom aplikację.

### Domyślne Dane Logowania
- Email: `root@example.com`
- Hasło: `Root123$`

### Konfiguracja Bazy Danych
1. Utwórz bazę danych w lokalnym serwerze SQL o nazwie **EventsDB**.
2. Zaktualizuj `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "EventsDb": "Server=TwojaNazwaSerwera;Database=EventsDB;Trusted_Connection=True;MultipleActiveResultSets=true"
   }
   ```


### Migracja Bazy Danych

Otwórz Konsolę Menedżera Pakietów i uruchom polecenie:
```sh
Update-Database
```