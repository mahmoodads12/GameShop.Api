# 🎮 GameStore API Backend

Eine moderne .NET Web API für die Verwaltung eines Game Stores. Diese API bietet vollständige CRUD-Operationen für Spiele und Genres mit Entity Framework Core und SQLite Datenbank.

## ✨ Features

- **🎯 Spielverwaltung**: Vollständige CRUD-Operationen für Spiele
- **🏷️ Genre-Management**: Verwaltung von Spielgenres
- **🗄️ Datenbank**: SQLite mit Entity Framework Core
- **🔒 Validierung**: Data Annotations für Eingabevalidierung
- **📊 DTOs**: Strukturierte Datenübertragung
- **🚀 Minimal API**: Moderne .NET 6+ Minimal API Implementierung

## 🛠️ Tech Stack

- **Framework**: .NET 10.0
- **Datenbank**: SQLite mit Entity Framework Core
- **Architektur**: Minimal API mit Repository Pattern
- **Validierung**: Data Annotations
- **Language**: C# mit Nullable Reference Types

## 🚀 Schnellstart

### Voraussetzungen

- .NET 10.0 SDK installiert
- Visual Studio 2022 oder VS Code

### Installation

1. **Repository klonen oder Projekt öffnen**

2. **Dependencies installieren**:
```bash
dotnet restore
```

3. **Datenbank-Migration ausführen** (automatisch beim Start):
```bash
dotnet run
```

4. **API starten**:
```bash
dotnet run --project GameStore.Api
```

5. **API testen**: [http://localhost:5075](http://localhost:5075)

## 📁 Projektstruktur

```
GameStore.Api/
├── Api/                    # API Endpunkte
│   ├── GameEndPoints.cs    # Spiel-Endpunkte
│   └── GenreEndPoints.cs   # Genre-Endpunkte
├── Data/                   # Datenbank-Kontext
├── Dtos/                   # Data Transfer Objects
├── Models/                 # Datenmodelle
├── Program.cs              # Anwendungseinstieg
├── GameStore.Api.csproj    # Projektdatei
└── game.http              # API Test-Dateien
```

## 🔌 API Endpunkte

### Spiele

| Methode | Endpunkt | Beschreibung |
|---------|----------|-------------|
| `GET` | `/games` | Alle Spiele abrufen |
| `GET` | `/games/{id}` | Spezifisches Spiel abrufen |
| `POST` | `/games` | Neues Spiel erstellen |
| `PUT` | `/games/{id}` | Spiel aktualisieren |
| `DELETE` | `/games/{id}` | Spiel löschen |

### Genres

| Methode | Endpunkt | Beschreibung |
|---------|----------|-------------|
| `GET` | `/genres` | Alle Genres abrufen |

## 📊 Datenmodelle

### GameModel
```csharp
public class GameModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public GenreModel? Genre { get; set; }
    public int GenreId { get; set; }
    public decimal Price { get; set; }
    public DateTime ReleaseDate { get; set; }
}
```

### GenreModel
```csharp
public class GenreModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
}
```

## 🔄 DTOs

### CreateGameDto
```csharp
public record CreateGameDto(
    [Required][StringLength(50)] string Name,
    [Range(1, 50)] int GenreId,
    [Range(1, 100)] decimal Price,
    DateTime ReleaseDate
);
```

### GameDetailsDto
```csharp
public record GameDetailsDto(
    int Id,
    string Name,
    int GenreId,
    decimal Price,
    DateTime ReleaseDate
);
```

### GameSummaryDto
```csharp
public record GameSummaryDto(
    int Id,
    string Name,
    string GenreName,
    decimal Price,
    DateTime ReleaseDate
);
```

## 🧪 API Beispiele

### Alle Spiele abrufen
```http
GET http://localhost:5075/games
```

### Neues Spiel erstellen
```http
POST http://localhost:5075/games
Content-Type: application/json

{
    "name": "The Legend of Zelda: Breath of the Wild",
    "genreId": 2,
    "price": 59.99,
    "releaseDate": "2017-03-03"
}
```

### Spiel aktualisieren
```http
PUT http://localhost:5075/games/1
Content-Type: application/json

{
    "name": "Updated Game Name",
    "genreId": 2,
    "price": 49.99,
    "releaseDate": "2017-03-03"
}
```

### Spiel löschen
```http
DELETE http://localhost:5075/games/1
```

### Alle Genres abrufen
```http
GET http://localhost:5075/genres
```

## 🗄️ Datenbank

Die API verwendet SQLite als Datenbank. Die Datenbankdatei `GameStore.db` wird automatisch im Projektverzeichnis erstellt.

### Migrationen

Die Datenbank wird automatisch beim Start migriert. Für manuelle Migrationen:

```bash
# Migration erstellen
dotnet ef migrations add InitialCreate

# Migration anwenden
dotnet ef database update
```

## 🛡️ Validierung

Die API verwendet Data Annotations für die Eingabevalidierung:

- **Name**: Erforderlich, max. 50 Zeichen
- **GenreId**: Erforderlich, Bereich 1-50
- **Price**: Erforderlich, Bereich 1-100
- **ReleaseDate**: Erforderlich

## 🐛 Fehlerbehebung

### Häufige Probleme

**1. Datenbankverbindungsfehler**
```bash
# Datenbank löschen und neu erstellen
rm GameStore.db
dotnet run
```

**2. Port bereits verwendet**
```bash
# Anderen Port verwenden
dotnet run --urls="http://localhost:5000"
```

**3. Entity Framework Fehler**
```bash
# Pakete neu installieren
dotnet restore
dotnet clean
dotnet build
```

## 🚀 Verfügbare Commands

```bash
# Projekt starten
dotnet run

# Projekt bauen
dotnet build

# Projekt testen
dotnet test

# Pakete wiederherstellen
dotnet restore

# Datenbank-Migration
dotnet ef database update
```

## 🔧 Konfiguration

### appsettings.json
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### appsettings.Development.json
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

## 📝 Entwicklungshinweise

- Die API verwendet Minimal API Pattern für sauberen und prägnanten Code
- Entity Framework Core mit SQLite für einfache Entwicklung
- DTOs für sichere Datenübertragung
- Automatische Datenbank-Migration beim Start
- Strukturierte Fehlerbehandlung

## 🤝 Mitwirken

1. Repository forken
2. Feature Branch erstellen
3. Änderungen durchführen
4. Tests hinzufügen
5. Pull Request einreichen

## 📄 Lizenz

Dieses Projekt ist unter der MIT Lizenz lizenziert.

---

**Hinweis**: Die API läuft standardmäßig auf Port 5075. Stellen Sie sicher, dass dieser Port frei ist oder passen Sie die Konfiguration entsprechend an.
