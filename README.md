# Library API

Proste REST API stworzone w ASP.NET Core z wykorzystaniem Entity Framework Core.  
Projekt umożliwia zarządzanie biblioteką: autorami, książkami oraz egzemplarzami książek.

---

## Technologie

- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- Swagger / OpenAPI

---

## Model danych

### Author
- Id
- FirstName
- LastName  
Relacja: **1 autor → wiele książek**

### Book
- Id
- Title
- Year
- AuthorId  
Relacja: **1 książka → wiele egzemplarzy**

### Copy
- Id
- Available
- BookId

---

## Relacje
Author 1 ────> * Book 1 ────> * Copy


---

## Endpointy API

### Authors

- GET `/authors` – lista autorów
- GET `/authors/{id}` – szczegóły autora
- POST `/authors` – dodanie autora
- PUT `/authors/{id}` – aktualizacja autora
- DELETE `/authors/{id}` – usunięcie autora

---

### Books

- GET `/books` – lista książek
- GET `/books/{id}` – szczegóły książki
- GET `/books?authorId={id}` – filtrowanie po autorze
- POST `/books` – dodanie książki
- PUT `/books/{id}` – aktualizacja książki
- DELETE `/books/{id}` – usunięcie książki

---

### Copies

- GET `/copies` – lista egzemplarzy
- GET `/copies/{id}` – szczegóły egzemplarza
- POST `/copies` – dodanie egzemplarza
- PUT `/copies/{id}` – aktualizacja egzemplarza
- DELETE `/copies/{id}` – usunięcie egzemplarza

